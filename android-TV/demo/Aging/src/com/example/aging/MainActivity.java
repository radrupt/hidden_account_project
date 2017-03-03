package com.example.aging;

import java.text.Format;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.TimeZone;
import java.util.Timer;
import java.util.TimerTask;

import Log.MyLog;
import android.R.color;
import android.app.Activity;
import android.graphics.Color;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnKeyListener;
import android.widget.RelativeLayout;
import android.widget.TextView;

public class MainActivity extends Activity{

	private TextView view;
	private RelativeLayout relativeLayout;
	private long starttime;
	private boolean first = true;
	private int background_color_int = 0;
	private boolean background_color_bool = false;
	private String[] backgroundcolor={"#000000","#FFFF00","#FF0000","#FFFFFF","#0000FF","#008000"};
	
	final Handler handler = new Handler(){    
	    public void handleMessage(Message msg) {    
	        switch (msg.what) {        
	           case 1:        
	        	   if( first ){ first = false; starttime = System.currentTimeMillis();}
	        	   view.setText(getUsedTime(System.currentTimeMillis(),starttime));
	        	   if( background_color_bool ) relativeLayout.setBackgroundColor(Color.parseColor(backgroundcolor[background_color_int++%6]));;
	        	   background_color_bool = !background_color_bool;
	               break;        
	            }        
	            super.handleMessage(msg);    
	        }      
	   };   
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {  
        super.onCreate(savedInstanceState);  
        setContentView(R.layout.activity_main);  
        
        view = (TextView) this.findViewById(R.id.use_time);
        relativeLayout = (RelativeLayout) this.findViewById(R.id.background_color);
        
		TimerTask task = new TimerTask(){  
		      public void run() {  
		    	  Message message = new Message();        
		          message.what = 1;        
		          handler.sendMessage(message); 
		   }  
		};
		Timer timer = new Timer(true);
		timer.schedule(task,1000, 1000);
		
    }  
    
    public static String getUsedTime(long currenttime,long starttime){ 
		Date d = new Date(currenttime - starttime);
		SimpleDateFormat simpleFormat = new SimpleDateFormat("HH:mm:ss");
		simpleFormat.setTimeZone(TimeZone.getTimeZone("GMT+0"));
		return simpleFormat.format(d);
	}
      
    @Override  
    protected void onDestroy() {  
        super.onDestroy();  
    }
    
    @Override 
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        return super.onKeyDown(keyCode, event);
    }
   
}
