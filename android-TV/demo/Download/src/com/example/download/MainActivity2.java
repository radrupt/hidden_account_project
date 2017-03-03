package com.example.download;

import java.util.ArrayList;  
import java.util.HashMap;  
import java.util.List;  
import java.util.Map;  

import com.example.entity.LoadInfo;
import com.example.service.Downloader;
  
import android.app.ListActivity;  
import android.os.AsyncTask;  
import android.os.Bundle;  
import android.os.Handler;  
import android.os.Message;  
import android.util.Log;
import android.view.View;  
import android.widget.Button;  
import android.widget.LinearLayout;  
import android.widget.LinearLayout.LayoutParams;  
import android.widget.ProgressBar;   
import android.widget.TextView;  
import android.widget.Toast;  
  
public class MainActivity2 extends ListActivity {   
     // �̶����ص���Դ·��������������������ϵĵ�ַ  
     private static final String URL = "http://download.haozip.com/";  
     // �̶�������ص����ֵ�·����SD��Ŀ¼��  
     private static final String SD_PATH = "/mnt/sdcard/";  
     // ��Ÿ���������  
     private Map<String, Downloader> downloaders = new HashMap<String, Downloader>();  
     // �������������Ӧ�Ľ�����  
     private Map<String, ProgressBar> ProgressBars = new HashMap<String, ProgressBar>();  
     /** 
      * ������Ϣ���������ʱ���½����� 
      */  
     private Handler mHandler = new Handler() {  
         public void handleMessage(Message msg) {  
             if (msg.what == 1) {  
                 String url = (String) msg.obj;  
                 int length = msg.arg1;  
                 ProgressBar bar = ProgressBars.get(url);  
                 if (bar != null) {  
                     // ���ý���������ȡ��length���ȸ���  
                     bar.incrementProgressBy(length);  
                     if (bar.getProgress() == bar.getMax()) {  
                         LinearLayout layout = (LinearLayout) bar.getParent();  
                         TextView resouceName=(TextView)layout.findViewById(R.id.tv_resouce_name);  
                         Toast.makeText(MainActivity2.this, "["+resouceName.getText()+"]������ɣ�", Toast.LENGTH_SHORT).show();  
                         // ������ɺ��������������map�е��������  
                         layout.removeView(bar);  
                         ProgressBars.remove(url);  
                         downloaders.get(url).delete(url);  
                         downloaders.get(url).reset();  
                         downloaders.remove(url);  
                           
                         Button btn_start=(Button)layout.findViewById(R.id.btn_start);  
                         Button btn_pause=(Button)layout.findViewById(R.id.btn_pause);  
                         btn_pause.setVisibility(View.GONE);  
                         btn_start.setVisibility(View.GONE);  
                     }  
                 }  
             }  
         }  
     };  
     @Override  
     public void onCreate(Bundle savedInstanceState) {  
         super.onCreate(savedInstanceState);  
         setContentView(R.layout.main);  
         Log.i("wd","dsf");
         showListView();  
     }  
     // ��ʾlistView���������������  
     private void showListView() {  
         List<Map<String, String>> data = new ArrayList<Map<String, String>>();  
         Map<String, String> map = new HashMap<String, String>();  
         map.put("name", "haozip_v3.1.exe");  
         data.add(map);  
         map = new HashMap<String, String>();  
         map.put("name", "haozip_v3.1_hj.exe");  
         data.add(map);  
         map = new HashMap<String, String>();  
         map.put("name", "haozip_v2.8_x64_tiny.exe");  
         data.add(map);  
         map = new HashMap<String, String>();  
         map.put("name", "haozip_v2.8_tiny.exe");  
         data.add(map);  
         DownLoadAdapter adapter=new DownLoadAdapter(this,data);    
         setListAdapter(adapter);  
           
     }  
     /** 
      * ��Ӧ��ʼ���ذ�ť�ĵ���¼� 
      */  
     public void startDownload(View v) {  
         // �õ�textView������   
         LinearLayout layout = (LinearLayout) v.getParent();  
         String resouceName = ((TextView) layout.findViewById(R.id.tv_resouce_name)).getText().toString();  
         String urlstr = URL + resouceName;  
         String localfile = SD_PATH + resouceName;  
         //���������߳���Ϊ4����������Ϊ�˷������̶���  
         String threadcount = "4";  
         DownloadTask downloadTask=new DownloadTask(v);  
         downloadTask.execute(urlstr,localfile,threadcount);  
         
     };  
    class DownloadTask extends AsyncTask<String, Integer, LoadInfo>{  
        Downloader downloader=null;   
        View v=null;  
        String urlstr=null;  
        public DownloadTask(final View v){  
            this.v=v;  
        }    
        @Override  
        protected void onPreExecute() {   
            Button btn_start=(Button)((View)v.getParent()).findViewById(R.id.btn_start);  
            Button btn_pause=(Button)((View)v.getParent()).findViewById(R.id.btn_pause);  
            btn_start.setVisibility(View.GONE);  
            btn_pause.setVisibility(View.VISIBLE);  
        }  
        @Override  
        protected LoadInfo doInBackground(String... params) {  
            urlstr=params[0];  
            String localfile=params[1];  
            int threadcount=Integer.parseInt(params[2]);  
             // ��ʼ��һ��downloader������  
             downloader = downloaders.get(urlstr);  
             if (downloader == null) {  
                 downloader = new Downloader(urlstr, localfile, threadcount, MainActivity2.this, mHandler);  
                 downloaders.put(urlstr, downloader);  
             }  
             if (downloader.isdownloading())  
                 return null;  
             // �õ�������Ϣ��ĸ�����ɼ���  
             return downloader.getDownloaderInfors();   
        }  
        @Override  
        protected void onPostExecute(LoadInfo loadInfo) {  
            if(loadInfo!=null){  
                 // ��ʾ������  
                 showProgress(loadInfo, urlstr, v);  
                 // ���÷�����ʼ����  
                 downloader.download();  
            }   
        }  
           
     };  
     /** 
      * ��ʾ������ 
      */  
     private void showProgress(LoadInfo loadInfo, String url, View v) {  
         ProgressBar bar = ProgressBars.get(url);  
         if (bar == null) {  
             bar = new ProgressBar(this, null, android.R.attr.progressBarStyleHorizontal);  
             bar.setMax(loadInfo.getFileSize());  
             bar.setProgress(loadInfo.getComplete());  
             ProgressBars.put(url, bar);  
             LinearLayout.LayoutParams params = new LayoutParams(LayoutParams.FILL_PARENT, 5);  
             ((LinearLayout) ((LinearLayout) v.getParent()).getParent()).addView(bar, params);  
         }  
     }  
     /** 
      * ��Ӧ��ͣ���ذ�ť�ĵ���¼� 
      */  
     public void pauseDownload(View v) {  
         LinearLayout layout = (LinearLayout) v.getParent();  
         String resouceName = ((TextView) layout.findViewById(R.id.tv_resouce_name)).getText().toString();  
         String urlstr = URL + resouceName;  
         downloaders.get(urlstr).pause();  
         Button btn_start=(Button)((View)v.getParent()).findViewById(R.id.btn_start);  
         Button btn_pause=(Button)((View)v.getParent()).findViewById(R.id.btn_pause);  
         btn_pause.setVisibility(View.GONE);  
         btn_start.setVisibility(View.VISIBLE);  
     }  
 }  