package com.example.child;

import android.app.Service;
import android.content.Intent;
import android.os.IBinder;

public class MyService extends Service {  
  
	public static final String ACTION = "com.ryantang.service.PollingService";
	
    @Override  
    public void onCreate() {  
        super.onCreate();  
    }  
  
    @Override  
    public int onStartCommand(Intent intent, int flags, int startId) {  
        Intent i = new Intent();  
        i.setAction("mark.zhang"); 
        i.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        i.addCategory(Intent.CATEGORY_DEFAULT);  
        startActivity(i);  
        return super.onStartCommand(intent, flags, startId);  
    }  
      
    @Override  
    public void onDestroy() {  
        super.onDestroy();  
    }  
  
    @Override  
    public IBinder onBind(Intent intent) {  
        return null;  
    }  
  
}  