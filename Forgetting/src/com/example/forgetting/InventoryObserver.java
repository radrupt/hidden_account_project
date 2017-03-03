package com.example.forgetting;

import android.database.ContentObserver;
import android.os.Handler;
import android.os.Message;

public class InventoryObserver extends ContentObserver{
	public static final String TAG = "InventoryObserver";  
    private Handler handler;  
      
    public InventoryObserver(Handler handler) {  
        super(handler);  
        this.handler = handler;  
    }  
      
    @Override  
    public void onChange(boolean selfChange) {  
        super.onChange(selfChange);  
        //向handler发送消息,更新查询记录  
        Message msg = new Message();  
        handler.sendMessage(msg);  
    }  
}
