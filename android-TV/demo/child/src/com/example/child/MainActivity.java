package com.example.child;

import java.lang.reflect.Field;

import com.example.ch.R;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.SharedPreferences;
import android.content.DialogInterface.OnKeyListener;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

public class MainActivity extends Activity {
	//存储本地数据，儿童模式的相关数据，如时间，密码
	private static SharedPreferences.Editor editor ;
	
	private static SharedPreferences sharedPreferences;
	
	private Button startchild;
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
    	
        super.onCreate(savedInstanceState);
        
        setContentView(R.layout.activity_main);
        
        sharedPreferences = getSharedPreferences("setting", 0);
        
        editor = sharedPreferences.edit();
        
        String pw = sharedPreferences.getString("pw", "null");
        
        if( pw.equals("null") ){//初次进入用户必须先将密码设定好，才能进行后续操作
        	
        	pw("设置初始密码",pw,this,false,3);//false: 设定密码dialog只有设定好密码后才能关闭
        	
        }
        
        if( sharedPreferences.getLong("time", -1) == -1 ) {
        	
        	editor.putLong("time",20*60*1000);//默认设置锁屏时间间隔20分钟
        	
        	editor.commit();
        	
        }
        
        startchild = (Button) this.findViewById(R.id.startchild);
        String schild = sharedPreferences.getString("startchild","null");
        
        Button settime = (Button) this.findViewById(R.id.settime);
        Button resetpw = (Button) this.findViewById(R.id.resetpw);
        if( schild.equals("on") ) {
        	startchild.setText("关闭儿童模式");
        }
        
        startchild.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View v) {
				String pw = sharedPreferences.getString("pw","null");
		        if( pw.equals("null") ) {
		        	pw("设置初始密码",pw,MainActivity.this,false,3);
		        }else {
		        	
		        	String isstart = sharedPreferences.getString("startchild","null");
		        	if( isstart.equals("null") || isstart.equals("down") ){
		        		editor.putString("startchild","on");
		        		startchild.setText("关闭儿童模式");
		        		editor.commit();
		        		PollingUtils.startPollingService(MainActivity.this, getTime(), MyService.class, MyService.ACTION);
		        	}else{
		        		pw("输入解锁密码",pw,MainActivity.this,true,1);
		        	}
		        }
			}
        });
        
        settime.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View v) {
				String pw = getPW();
		        if( pw.equals("null") ) {
		        	pw("设置初始密码",pw,MainActivity.this,true,3);
		        }else {
					pw("输入解锁密码",pw,MainActivity.this,true,2);
		        }
			}
        	
        });
        resetpw.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View v) {
				String pw = getPW();
		        if( pw.equals("null") ) {
		        	pw("设置初始密码", pw,MainActivity.this,true,3);
		        }else {
		        	pw("设置新密码", pw,MainActivity.this,true,3);
		        }
			}
        	
        });
    }
    
    /*
     ** 由于有几个地方要用的类似的布局，所以使用一个布局，做不同的处理，如设置初始密码，则只需要一个输入框
     *  但是设置新的密码，则需要两个输入框
     *  密码验证也是需要一个密码
     *  model: 1——关闭儿童模式时候的密码验证
     *  	   2——设置锁屏时间时候的密码验证
     *  	   3——不需密码验证
     */
    private void pw(final String title, final String originpw, final Context context, final boolean forbideenback,final int model){
    	
    	LayoutInflater factory = LayoutInflater.from(context);
    	View textEntryView = factory.inflate(R.layout.pw_layout, null);
    	
    	final EditText oripw = (EditText) textEntryView.findViewById(R.id.oripw);
    	final LinearLayout layout = (LinearLayout) textEntryView.findViewById(R.id.ori_layout);
    	if( originpw.equals("null") ||  model == 1 || model == 2 ) layout.setVisibility(View.INVISIBLE);
    	
        final EditText newpw = (EditText) textEntryView.findViewById(R.id.newpw);
        TextView new_text = (TextView) textEntryView.findViewById(R.id.new_text);
        if( title.equals("设置初始密码") ) new_text.setText("初始密码：");
        if( model == 1 || model == 2 ) new_text.setText("解锁密码：");
        
        new AlertDialog.Builder(context)
        .setTitle(title)
        .setView(textEntryView)
        .setOnKeyListener(new OnKeyListener(){

			@Override
			public boolean onKey(DialogInterface dialog, int keyCode,
					KeyEvent event) {
				if (keyCode == event.KEYCODE_BACK && !forbideenback) {
					return true;
		        }
				return false;
			}
        	
        })
        .setPositiveButton("确定",
                new DialogInterface.OnClickListener() {

                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                    	if( model == 1 || model == 2  ){
                    		if( newpw.getText().toString().equals(originpw) ){
                    			Toast.makeText(context, "密码正确", Toast.LENGTH_SHORT).show();
                    			if( model == 1 ){
            		        		editor.putString("startchild","down");
            		        		startchild.setText("开启儿童模式");
            		        		editor.commit();
            		        		PollingUtils.stopPollingService(MainActivity.this, MyService.class, MyService.ACTION);
                    			}else{
                    				new AlertDialog.Builder(MainActivity.this)
                		         	.setTitle("请选择")
                		         	.setIcon(android.R.drawable.ic_dialog_info)  
                		         	.setSingleChoiceItems(new String[] {"20分钟","40分钟","1小时"},(int) getTime()/2000-1, 
                		         	  new DialogInterface.OnClickListener() {
                		         	                              
                		         	     public void onClick(DialogInterface dialog, int which) {
                	                	
                		         	    	if( which == 0 ) {//默认排序
                		                		editor.putLong("time",20*60*1000);
                		         	    	}
                		         	        if( which == 1 ) {
                		         	        	editor.putLong("time",40*60*1000);
                		         	        }else if( which == 2 ) {
                		         	        	editor.putLong("time",60*60*1000);
                		         	        } 
                		         	        editor.commit();
                		         	        dialog.dismiss();
                		         	     }
                		         	  }
                		         	)
                		         	.setNegativeButton("取消", null)
                		         	.show();
                    			}
    	                		canCloseDialog(dialog, true);
    	                		dialog.dismiss();
                    		}else{
                    			canCloseDialog(dialog, false);
                    			Toast.makeText(context, "密码错误，请重新输入", Toast.LENGTH_SHORT).show();
                    		}
                    	}
                    	else if( (!originpw.equals("null") && !oripw.getText().toString().equals(originpw)) )  {
                    		Toast.makeText(context, "原密码错误", Toast.LENGTH_SHORT).show();
                    		canCloseDialog(dialog, false);
                    	}else if( newpw.getText().toString().length() < 1 ){
                    		Toast.makeText(context, "密码长度少于四位", Toast.LENGTH_SHORT).show();
                    		canCloseDialog(dialog, false);
                    	}else {
	                		editor.putString("pw",newpw.getText().toString());
	                		editor.commit();
	                		Toast.makeText(context, "密码设置成功", Toast.LENGTH_SHORT).show();
	                		canCloseDialog(dialog, true);
	                		dialog.dismiss();
                    	}
                    }

                }).show();
        
    }
    /*
     * 控制dialog是否可以关闭
     * close: true——可以关闭
     * 		  false——不可以关闭
     * */
    private void canCloseDialog(DialogInterface dialogInterface, boolean close) {  
        try {  
            Field field = dialogInterface.getClass().getSuperclass().getDeclaredField("mShowing");  
            field.setAccessible(true);  
            field.set(dialogInterface, close);  
        } catch (Exception e) {  
            e.printStackTrace();  
        }  
    }
    
    protected static long getTime(){
    	
    	return sharedPreferences.getLong("time", -1)/60/10;
    }
    
    protected static String getPW(){
    	return sharedPreferences.getString("pw", "null");
    }
    
    protected static String getStart(){//判断是否开启儿童模式
    	return sharedPreferences.getString("startchild", "null");
    }
}
