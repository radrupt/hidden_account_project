#include <ImageProc.h>
#include <opencv2/core/core.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <string>
#include <vector>
#include <stdio.h>
#include <stdlib.h>

using namespace cv;
using namespace std;

int thresh = 40;
RNG rng(12345);
//函数声明
void clear_coincide_rec( vector<Rect> *boundRect );
int coincide_area(Point rect1_tl, Point rect1_br, Point rect2_tl, Point rect2_br);
char* jstringTostring(JNIEnv* env, jstring jstr);
jstring stoJstring(JNIEnv* env, const char* pat);
IplImage * change4channelTo3InIplImage(IplImage * src);

JNIEXPORT jstring JNICALL Java_com_example_forgetting_ImageProc_grayProc(
		JNIEnv* env, jclass obj, jstring srcimage,jstring filepath){

	/// 载入原图像, 返回3通道图像
	Mat src = imread( jstringTostring(env,srcimage), 1 );

    Mat src_gray;
    cvtColor(src, src_gray, CV_BGR2GRAY);
    blur( src_gray, src_gray, Size(3,3));

    //获得轮廓
    Mat canny_output;
	vector<vector<Point> > contours;
	vector<Vec4i> hierarchy;
    int min_rectangle_width_height = 20;  //5%: 最小的矩形宽余图片宽的比值

    /// 使用Threshold检测边缘
    Canny( src_gray, canny_output, thresh, thresh*3,3);
    //  threshold( src_gray, threshold_output, thresh, 255, THRESH_BINARY );
    /// 找到轮廓
    findContours( canny_output, contours, hierarchy, CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE, Point(0, 0) );

    /// 多边形逼近轮廓 + 获取矩形和圆形边界框
    vector<vector<Point> > contours_poly( contours.size() );
    vector<Rect> boundRect( contours.size() );

    for( int i = 0; i < contours.size(); i++ )
       { approxPolyDP( Mat(contours[i]), contours_poly[i], 3, true );
         boundRect[i] = boundingRect( Mat(contours_poly[i]) );
       }

    clear_coincide_rec(&boundRect);
    /// 画多边形轮廓 + 包围的矩形框 + 圆形框
    int image_index = 1;
    string filename;
    string allSplitedImage;
    for( int i = 0; i< contours.size(); i++ ){
         int width = boundRect[i].br().x - boundRect[i].tl().x;
         int height = boundRect[i].br().y - boundRect[i].tl().y;
         if( src.cols/(width+1) < min_rectangle_width_height &&
        		 src.rows/(height+1) < min_rectangle_width_height )  {
        	 Mat tt(src,Rect(boundRect[i]));
        	 stringstream ss;
        	 ss<<image_index;

        	 filename = jstringTostring(env,filepath);
        	 filename+= ss.str()+".jpg";
        	 imwrite(filename,tt);
        	 image_index++;
        	 allSplitedImage+=filename+"#";
         }

    }
    return stoJstring(env,(char*)allSplitedImage.c_str());
}
void clear_coincide_rec( vector<Rect> *boundRect )
{
  vector<Rect>::iterator iter1, iter2;
  int x1,x2,y1,y2;
  int coinarea,area1,area2;//相交的面积
  int coinarea_scale = 3; //相交的比例最大值(和小的矩形相比)
  for( iter1 = boundRect->begin(); (iter1+1) != boundRect->end(); ++iter1 )
  {
    for( iter2 = iter1 + 1; iter2 != boundRect->end(); ++iter2 )
    {
        coinarea = coincide_area( iter1->tl(), iter1->br(),
                                  iter2->tl(), iter2->br() );
        if( coinarea == 0 )
            continue;
        area1 = (iter1->br().x - iter1->tl().x)*(iter1->br().y - iter1->tl().y);
        area2 = (iter2->br().x - iter2->tl().x)*(iter2->br().y - iter2->tl().y);


        if( area1 < area2 ){
           if( area1/(coinarea+1) < coinarea_scale ) {
               iter1->width = 0;
               iter1->height = 0;
           }
        }
        else{
            if( area2/(coinarea+1) < coinarea_scale ) {
              iter2->width = 0;
               iter2->height = 0;
            }
        }
    }
  }
}

//return the area of coincide(0: is not coincide)
int coincide_area(Point rect1_tl, Point rect1_br, Point rect2_tl, Point rect2_br)
{
    int width1 = rect1_br.x - rect1_tl.x;
    int height1 = rect1_br.y - rect1_tl.y;
    int width2 = rect2_br.x - rect2_tl.x;
    int height2 = rect2_br.y - rect2_tl.y;

    //中心坐标
    int xc1 = (rect1_br.x + rect1_tl.x)/2;
    int yc1 = (rect1_br.y + rect1_tl.y)/2;
    int xc2 = (rect2_br.x + rect2_tl.x)/2;
    int yc2 = (rect2_br.y + rect2_tl.y)/2;

    Point coincide_tl;
    Point coincide_rb;
    if( abs(xc1-xc2) <= width1/2 + width2/2 &&
        abs(yc1-yc2) <= height1/2 + height2/2 ){
        coincide_tl.x = rect1_tl.x < rect2_tl.x ? rect2_tl.x : rect1_tl.x;
        coincide_tl.y = rect1_tl.y < rect2_tl.y ? rect2_tl.y : rect1_tl.y;
        coincide_rb.x = rect1_br.x < rect2_br.x ? rect1_br.x : rect2_br.x;
        coincide_rb.y = rect1_br.y < rect2_br.y ? rect1_br.y : rect2_br.y;

        return (coincide_rb.x - coincide_tl.x)*(coincide_rb.y - coincide_tl.y);
    }
    return 0;
}
char* jstringTostring(JNIEnv* env, jstring jstr)
{
	char* rtn = NULL;
	jclass clsstring = env->FindClass("java/lang/String");
	jstring strencode = env->NewStringUTF("utf-8");
	jmethodID mid = env->GetMethodID(clsstring, "getBytes", "(Ljava/lang/String;)[B");
	jbyteArray barr= (jbyteArray)env->CallObjectMethod(jstr, mid, strencode);
	jsize alen = env->GetArrayLength(barr);
	jbyte* ba = env->GetByteArrayElements(barr, JNI_FALSE);
	if (alen > 0)
	{
	rtn = (char*)malloc(alen + 1);

	memcpy(rtn, ba, alen);
	rtn[alen] = 0;
	}
	env->ReleaseByteArrayElements(barr, ba, 0);
	return rtn;
}

jstring stoJstring(JNIEnv* env, const char* pat)
{
	int        strLen    = strlen(pat);
	jclass     jstrObj   = (env)->FindClass("java/lang/String");
	jmethodID  methodId  = (env)->GetMethodID(jstrObj, "<init>", "([BLjava/lang/String;)V");
	jbyteArray byteArray = (env)->NewByteArray( strLen);
	jstring    encode    = (env)->NewStringUTF("gbk");
	(env)->SetByteArrayRegion(byteArray, 0, strLen, (jbyte*)pat);
	return (jstring)(env)->NewObject(jstrObj, methodId, byteArray, encode);
}
IplImage * change4channelTo3InIplImage(IplImage * src) {
  if (src->nChannels != 4) {
    return NULL;
  }

  IplImage * destImg = cvCreateImage(cvGetSize(src), IPL_DEPTH_8U, 3);
  for (int row = 0; row < src->height; row++) {
    for (int col = 0; col < src->width; col++) {
      CvScalar s = cvGet2D(src, row, col);
      cvSet2D(destImg, row, col, s);
    }
  }

  return destImg;
}
