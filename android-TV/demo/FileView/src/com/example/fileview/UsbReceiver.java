package com.example.fileview;

import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.hardware.usb.UsbManager;
import android.util.Log;
import android.widget.Toast;

public class UsbReceiver extends BroadcastReceiver  {
	
	UsbManager usb_Manager;
	
	protected Callback mCallback;
	
	public interface Callback {
        /** Called by ForListFragment when a list item is selected */
        public void fresh(Context context, Intent intent);
    }
	
	
	public UsbReceiver(UsbManager usb_Manager, Activity fileViewActivity) {
		this.usb_Manager = usb_Manager;
		mCallback = (Callback)fileViewActivity;
	}

	@Override
	public void onReceive(Context context, Intent intent) {
         mCallback.fresh( context,  intent);
	}


}
