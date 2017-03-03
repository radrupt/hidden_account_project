package com.example.download;

import java.util.HashMap;
import java.util.Map;

import com.example.entity.DownloadUIInfo;
import com.example.entity.LoadInfo;
import com.example.service.Downloader;

import android.app.Activity;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RemoteViews;
import android.widget.Toast;

public class MainActivity extends Activity {

	public static final String PROCESS_PAUSE_ACTION = "process_pause";
	
	public static final String PROCESS_CANCEL_ACTION = "process_cancel";
	
	private static final String URL = "http://download.haozip.com/haozip_v3.1.exe"; 
	
	private static final String SD_PATH = "/mnt/sdcard/AA/";  
	// 存放各个下载uri下载相关信息  
    private Map<String, DownloadUIInfo> downloadUIInfos = new HashMap<String, DownloadUIInfo>();  
	
	private NotificationManager manager;
	
	private Receiver receiver;
	
	private EditText editText;
	private String newuri = "";
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        manager = (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);
        editText = (EditText)this.findViewById(R.id.url_string);
        editText.setText(URL);
        Button processButton = (Button)this.findViewById(R.id.download_button);
        processButton.setOnClickListener(new OnClickListener(){

        	@Override
            public void onClick(View v) {
        		newuri = editText.getText().toString();
               
                
                String threadcount = "4";  
                DownloadTask downloadTask=new DownloadTask(v);  
                downloadTask.execute(newuri,SD_PATH+"qqq.apk",threadcount);  
            }
        	
        });
        initButtonReceiver();
        
    }
    class DownloadTask extends AsyncTask<String, Integer, LoadInfo>{  
        DownloadUIInfo downloadUIInfo = null;
        View v=null;  
        String urlstr=null;  
        public DownloadTask(final View v){  
            this.v=v;  
        }    
        @Override  
        protected void onPreExecute() {   
        }  
        @Override  
        protected LoadInfo doInBackground(String... params) {  
            urlstr=params[0];  
            String localfile=params[1];  
            int threadcount=Integer.parseInt(params[2]);  
             // 初始化一个downloader下载器  
            downloadUIInfo = downloadUIInfos.get(urlstr);  
             if (downloadUIInfo == null) {
        		 Log.i("qwer","sads");
                 Downloader downloader = new Downloader(urlstr, localfile, threadcount, MainActivity.this, mHandler);
//                 if( downloader.isFirst(urlstr) ) return null; 
	             Notification noti = new Notification();
	             noti.icon = R.drawable.ic_launcher;
	             noti.flags |= Notification.FLAG_ONGOING_EVENT;
	             noti.contentIntent = PendingIntent.getActivity(MainActivity.this, 
	             			0, new Intent( MainActivity.this, MainActivity2.class), 0);
                 downloadUIInfo = new DownloadUIInfo(downloader, null,noti);
                 downloadUIInfos.put(urlstr, downloadUIInfo);
             }  
             if (downloadUIInfo.getDownloader().isdownloading() )  {
                 return null;  
             }
             // 得到下载信息类的个数组成集合  
             return downloadUIInfo.getDownloader().getDownloaderInfors();   
        }  
        @Override  
        protected void onPostExecute(LoadInfo loadInfo) {  
            if(loadInfo!=null){  
                 // 显示进度条  
                 showProgress(loadInfo, urlstr, v);  
                 // 调用方法开始下载 
                 downloadUIInfo.getDownloader().download();  
            }   
        }  
           
     }; 
     /** 
      * 执行结束后（完成，中断，）显示进度条 
      */  
     private void showProgress(LoadInfo loadInfo, String url, View v) { 
    	 
     }  
     /** 
      * 利用消息处理机制适时更新进度条 
      */  
     private Handler mHandler = new Handler() { 
    	 
         public void handleMessage(Message msg) { 
        	 
             if (msg.what == 1) { 
            	 
                 String url = (String) msg.obj;  
                 int completelength = msg.arg1;  
                 int fileSize = msg.arg2;
                 RemoteViews remoteViews = new RemoteViews(MainActivity.this.getPackageName(),R.layout.notifyview);
                 Intent btIntent = new Intent().setAction(PROCESS_PAUSE_ACTION);
        		 btIntent.putExtra("url", url);
        		 PendingIntent btPendingIntent = PendingIntent.getBroadcast(getBaseContext(), 0, btIntent, PendingIntent.FLAG_UPDATE_CURRENT);
        		 
        		 Intent btIntent1 = new Intent().setAction(PROCESS_CANCEL_ACTION);
        		 btIntent1.putExtra("url", url);
        		 PendingIntent btPendingIntent1 = PendingIntent.getBroadcast(getBaseContext(), 0, btIntent1, PendingIntent.FLAG_UPDATE_CURRENT);
        		 
        		 remoteViews.setOnClickPendingIntent(R.id.process_pause,btPendingIntent);
        		 remoteViews.setOnClickPendingIntent(R.id.process_cancel,btPendingIntent1); 
                 DownloadUIInfo downloadUIInfo = downloadUIInfos.get(url);
                 if (downloadUIInfo != null) {  
                	 Notification noti = downloadUIInfos.get(url).getNotification();
                     noti.contentView = remoteViews;
                     // 设置进度条按读取的length长度更新  
                	 remoteViews.setTextViewText(R.id.process_text, (int)((float)completelength/(float)fileSize*100)+"%   "+getFileSize(completelength)+"/"+getFileSize(fileSize));
                	 remoteViews.setProgressBar(R.id.process_bar, 100, (int)((float)completelength/(float)fileSize*100), false); 
                	 manager.notify(downloadUIInfos.get(url).getNotificationID(), noti);
                	 if (completelength ==  fileSize) {  
                    	 remoteViews.setTextViewText(R.id.process_text,"完成" + getFileSize(fileSize));
                    	 manager.notify(downloadUIInfos.get(url).getNotificationID(), noti);
                    	 //downloadUIInfos.get(url).getDownloader().pause();
         				downloadUIInfos.get(url).getDownloader().delete(url);
         				downloadUIInfos.get(url).getDownloader().reset();
         				downloadUIInfos.get(url).clear();
                         downloadUIInfos.remove(url);  
                     } 
                 }  
             }  
         }  
     }; 
     
     private String getFileSize(long size){
     	if( size < 1024 ) return size + "B";
     	if( size < 1048576 ) return (long)(size>>10)+ binaryToDecimal(size) + "K";
     	if( size < 1073741824 ) 
     		return (long)(size>>20)+ binaryToDecimal(size>>10&1023) + "M";
 		return (long)(size>>30)+ binaryToDecimal(size>>20&1023) + "G";
     }
     private String binaryToDecimal(long binary){
     	int decimal = (int)((float)((float)(binary&1023)/1024.0)*10);
     	return (decimal > 0) ? "."+decimal : "";
     }
 	public void initButtonReceiver(){
 		receiver = new Receiver();
 		IntentFilter intentFilter = new IntentFilter();
 		intentFilter.addAction(PROCESS_PAUSE_ACTION);
 		intentFilter.addAction(PROCESS_CANCEL_ACTION);
 		registerReceiver(receiver, intentFilter);
 	}
     public class Receiver extends BroadcastReceiver{
		@Override
		public void onReceive(Context context, Intent intent) {
			String url = intent.getStringExtra("url");
			Toast.makeText(getApplicationContext(), url,
				     Toast.LENGTH_SHORT).show();
			if( intent.getAction().equals(PROCESS_PAUSE_ACTION) ){
				if( downloadUIInfos.get(url).getDownloader().isdownloading() ){
					downloadUIInfos.get(url).getDownloader().pause();
				}else{
					downloadUIInfos.get(url).getDownloader().download();
				}
			}else if( intent.getAction().equals(PROCESS_CANCEL_ACTION) ){
				if(downloadUIInfos.get(url) ==null  )return;
				Toast.makeText(getApplicationContext(), downloadUIInfos.get(url).getNotificationID()+"",
					     Toast.LENGTH_SHORT).show();
				manager.cancel(downloadUIInfos.get(url).getNotificationID());
				downloadUIInfos.get(url).getDownloader().pause();
				downloadUIInfos.get(url).getDownloader().delete(url);
				downloadUIInfos.get(url).getDownloader().reset();
				downloadUIInfos.get(url).clear();
                downloadUIInfos.remove(url);
			}
		}
	 }
     @Override
     public void onDestroy(){
    	 super.onDestroy();
    	// this.unregisterReceiver(receiver);
     }
}
