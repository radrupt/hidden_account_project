package com.example.child;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

public class BootCompletedReceiver extends BroadcastReceiver  
{  
  
    @Override  
    public void onReceive(Context context, Intent intent)  
    {  
    	if( context.getSharedPreferences("setting", 0).getString("startchild", "null").equals("on")){
    		PollingUtils.startPollingService(context, 
    				context.getSharedPreferences("setting", 0).getLong("time", -1)/60/10, MyService.class, MyService.ACTION);
    	}
    }  
}  
