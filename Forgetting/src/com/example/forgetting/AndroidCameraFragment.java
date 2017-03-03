package com.example.forgetting;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;

import android.R.integer;
import android.app.Fragment;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.hardware.Camera;
import android.hardware.Camera.PictureCallback;
import android.os.Bundle;
import android.os.Environment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ImageView;

public class AndroidCameraFragment extends Fragment 
	implements OnClickListener ,PictureCallback{
	public boolean wholePicture = true;
	private String tag = "Wangd933";
	private Button take_whole_Picture_Button = null;
	private Button take_part_Picture_Button = null;
	private CameraSurfacePreview mySurfacePreview = null;
	private String takenPicturePlaceString = null;
	private DrawImageView drawImageView = null;
//	private ImageView imageView = null;
	private View view = null;
//	private Context parentContext = null;
	@Override
	public void onCreate(Bundle savedInstanceState) {
		Log.d(tag, "AndroidCameraActivity onCreate start");
		super.onCreate(savedInstanceState);
		Log.d(tag, "AndroidCameraActivity onCreate end");
		
	}
	public void onActivityCreated(Bundle savedInstanceState){
		super.onActivityCreated(savedInstanceState);
		/*关联拍照按钮，添加监听  button start */
		take_whole_Picture_Button = (Button)view.findViewById(R.id.picture_whole_take);
		take_whole_Picture_Button.setOnClickListener(this);
		take_part_Picture_Button = (Button)view.findViewById(R.id.picture_part_take);
		take_part_Picture_Button.setOnClickListener(this);
		/*Button end*/
		
		FrameLayout preview = (FrameLayout)view.findViewById(R.id.camera_preview);
		mySurfacePreview = new CameraSurfacePreview(getActivity());
		
		preview.addView(mySurfacePreview);
	    drawImageView = new DrawImageView(getActivity());
		preview.addView(drawImageView);
		
		drawImageView.onDraw(new Canvas());
	}
    @Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState){
    	Log.d("Wangd933", "AndroidCameraFragment onCreateView");
    	view = inflater.inflate(R.layout.activity_camera, container, false);
		return view;
	}
	//实现OnClickListener里的抽象方法
	@Override
	public void onClick(View v){
		String buttonText = ((Button)v).getText().toString();
		
		if( buttonText.equals("picture_part_take") ) 
			wholePicture = false;
		
		take_whole_Picture_Button.setEnabled(false);
		take_part_Picture_Button.setEnabled(false);
		//get an image from the camera
		mySurfacePreview.takePicture(this);
	}

	@Override
	public void onPictureTaken(byte[] data, Camera camera) {
		// save picture to SDscard
		File pictureFile = getOutMediaFile();
		if( null == pictureFile){
			Log.d("Wangd933","pictrue is no exist");
		}
		try{
			FileOutputStream fos = new FileOutputStream(pictureFile);
			fos.write(data);
			fos.close();
			Log.d(tag, "***************************************************************");
			
			Bitmap bitmap = BitmapFactory.decodeFile(takenPicturePlaceString);
			//拍的不是截图
			if( !wholePicture ) {
				float width = bitmap.getWidth();
				float height = bitmap.getHeight();
				
				float surfacewidth = mySurfacePreview.getWidth() ;
				float surfaceheight = mySurfacePreview.getHeight() ;
				
				float left = drawImageView.left < 0 ? 0 : drawImageView.left;
				float right = drawImageView.right > surfacewidth ? surfacewidth : drawImageView.right;
				float bottom = drawImageView.bottom > surfaceheight ? surfaceheight : drawImageView.bottom;
				float top = drawImageView.top < 0 ? 0 : drawImageView.top;
				
				Log.d(tag, takenPicturePlaceString+": size = " + data.length + " width =  " + width + " height =  " + height);
				Log.d(tag, " width =  " +surfacewidth + " height =  " + surfaceheight);
				Log.d(tag, "drawImageView.left : "+ drawImageView.left);
				Log.d(tag, "drawImageView.top : "+ drawImageView.top);
				Log.d(tag, "drawImageView.right : "+ drawImageView.right);
				Log.d(tag, "drawImageView.bottom : "+ drawImageView.bottom);
				float xscale = width/surfacewidth;
				float yscale = height/surfaceheight;
				Log.d(tag, "xscale: "+ String.valueOf(xscale));
				Log.d(tag, "yscale: "+ String.valueOf(yscale));
				bitmap = Bitmap.createBitmap(bitmap,(int)(left*xscale), 
					(int)(top*yscale),
					(int)((right - left)*xscale),(int)((bottom - top)*yscale));
			}
			saveBitmap(bitmap);
			ImageView imageView = (ImageView)getActivity().findViewById(R.id.imageview);
			imageView.setImageBitmap(bitmap);
			imageView.setTag(takenPicturePlaceString);
			
			//显示已经拍好的图片
		}catch(FileNotFoundException e){
			Log.d("Wangd933", "not found the picture of the place! "+e.getMessage());
		}catch(IOException e){
			Log.d("Wangd933","failed to save the picture!" + e.getMessage() );
		}
//		//Restart the preview and reenable the shutter button 
//		//so that we can take another picture
//		camera.startPreview();
//		
//		//See if need to enable or not 
//		take_Picture_Button.setEnabled(true);
		MainActivity.hastakephoto = false;
		getFragmentManager().popBackStack();
		
		
	}
	
	public void saveBitmap(Bitmap bitmap) throws IOException
    {
            File file = new File(takenPicturePlaceString);
            FileOutputStream out;
            try{
                    out = new FileOutputStream(file);
                    if(bitmap.compress(Bitmap.CompressFormat.PNG, 70, out)) 
                    {
                            out.flush();
                            out.close();
                    }
            } 
            catch (FileNotFoundException e) 
            {
                    e.printStackTrace();
            } 
            catch (IOException e) 
            {
                    e.printStackTrace(); 
            }
    }
	
	private File getOutMediaFile(){
		
		File picDir = Environment.getExternalStoragePublicDirectory( Environment.DIRECTORY_PICTURES);
		if( !picDir.exists() ){
			if (! picDir.mkdirs()){  
	            Log.d("Wand933", "failed to create directory");  
	            return null;  
	        }
		}
		//get the current time
		String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss").format(new Date());
		takenPicturePlaceString = picDir.getPath() + File.separator + "IMAGE_" + timeStamp + ".bmp";
		return new File(takenPicturePlaceString);
		
	}
	
	@Override
	public void onPause(){
		super.onPause();
		Camera camera = mySurfacePreview.getCamera();
		if( camera != null ){
			mySurfacePreview.surfaceDestroyed(mySurfacePreview.getHolder());
			
		}
	}
	@Override
	public void onResume(){
		super.onResume();
		Camera camera = mySurfacePreview.getCamera();
		try{
			if( camera != null ){
				mySurfacePreview.open();
			}
		}catch(Exception e){
			Log.d(tag, "Error setting camera preview！"+ e.getMessage());
		}
	}

}
