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
	//�洢�������ݣ���ͯģʽ��������ݣ���ʱ�䣬����
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
        
        if( pw.equals("null") ){//���ν����û������Ƚ������趨�ã����ܽ��к�������
        	
        	pw("���ó�ʼ����",pw,this,false,3);//false: �趨����dialogֻ���趨���������ܹر�
        	
        }
        
        if( sharedPreferences.getLong("time", -1) == -1 ) {
        	
        	editor.putLong("time",20*60*1000);//Ĭ����������ʱ����20����
        	
        	editor.commit();
        	
        }
        
        startchild = (Button) this.findViewById(R.id.startchild);
        String schild = sharedPreferences.getString("startchild","null");
        
        Button settime = (Button) this.findViewById(R.id.settime);
        Button resetpw = (Button) this.findViewById(R.id.resetpw);
        if( schild.equals("on") ) {
        	startchild.setText("�رն�ͯģʽ");
        }
        
        startchild.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View v) {
				String pw = sharedPreferences.getString("pw","null");
		        if( pw.equals("null") ) {
		        	pw("���ó�ʼ����",pw,MainActivity.this,false,3);
		        }else {
		        	
		        	String isstart = sharedPreferences.getString("startchild","null");
		        	if( isstart.equals("null") || isstart.equals("down") ){
		        		editor.putString("startchild","on");
		        		startchild.setText("�رն�ͯģʽ");
		        		editor.commit();
		        		PollingUtils.startPollingService(MainActivity.this, getTime(), MyService.class, MyService.ACTION);
		        	}else{
		        		pw("�����������",pw,MainActivity.this,true,1);
		        	}
		        }
			}
        });
        
        settime.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View v) {
				String pw = getPW();
		        if( pw.equals("null") ) {
		        	pw("���ó�ʼ����",pw,MainActivity.this,true,3);
		        }else {
					pw("�����������",pw,MainActivity.this,true,2);
		        }
			}
        	
        });
        resetpw.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View v) {
				String pw = getPW();
		        if( pw.equals("null") ) {
		        	pw("���ó�ʼ����", pw,MainActivity.this,true,3);
		        }else {
		        	pw("����������", pw,MainActivity.this,true,3);
		        }
			}
        	
        });
    }
    
    /*
     ** �����м����ط�Ҫ�õ����ƵĲ��֣�����ʹ��һ�����֣�����ͬ�Ĵ��������ó�ʼ���룬��ֻ��Ҫһ�������
     *  ���������µ����룬����Ҫ���������
     *  ������֤Ҳ����Ҫһ������
     *  model: 1�����رն�ͯģʽʱ���������֤
     *  	   2������������ʱ��ʱ���������֤
     *  	   3��������������֤
     */
    private void pw(final String title, final String originpw, final Context context, final boolean forbideenback,final int model){
    	
    	LayoutInflater factory = LayoutInflater.from(context);
    	View textEntryView = factory.inflate(R.layout.pw_layout, null);
    	
    	final EditText oripw = (EditText) textEntryView.findViewById(R.id.oripw);
    	final LinearLayout layout = (LinearLayout) textEntryView.findViewById(R.id.ori_layout);
    	if( originpw.equals("null") ||  model == 1 || model == 2 ) layout.setVisibility(View.INVISIBLE);
    	
        final EditText newpw = (EditText) textEntryView.findViewById(R.id.newpw);
        TextView new_text = (TextView) textEntryView.findViewById(R.id.new_text);
        if( title.equals("���ó�ʼ����") ) new_text.setText("��ʼ���룺");
        if( model == 1 || model == 2 ) new_text.setText("�������룺");
        
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
        .setPositiveButton("ȷ��",
                new DialogInterface.OnClickListener() {

                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                    	if( model == 1 || model == 2  ){
                    		if( newpw.getText().toString().equals(originpw) ){
                    			Toast.makeText(context, "������ȷ", Toast.LENGTH_SHORT).show();
                    			if( model == 1 ){
            		        		editor.putString("startchild","down");
            		        		startchild.setText("������ͯģʽ");
            		        		editor.commit();
            		        		PollingUtils.stopPollingService(MainActivity.this, MyService.class, MyService.ACTION);
                    			}else{
                    				new AlertDialog.Builder(MainActivity.this)
                		         	.setTitle("��ѡ��")
                		         	.setIcon(android.R.drawable.ic_dialog_info)  
                		         	.setSingleChoiceItems(new String[] {"20����","40����","1Сʱ"},(int) getTime()/2000-1, 
                		         	  new DialogInterface.OnClickListener() {
                		         	                              
                		         	     public void onClick(DialogInterface dialog, int which) {
                	                	
                		         	    	if( which == 0 ) {//Ĭ������
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
                		         	.setNegativeButton("ȡ��", null)
                		         	.show();
                    			}
    	                		canCloseDialog(dialog, true);
    	                		dialog.dismiss();
                    		}else{
                    			canCloseDialog(dialog, false);
                    			Toast.makeText(context, "�����������������", Toast.LENGTH_SHORT).show();
                    		}
                    	}
                    	else if( (!originpw.equals("null") && !oripw.getText().toString().equals(originpw)) )  {
                    		Toast.makeText(context, "ԭ�������", Toast.LENGTH_SHORT).show();
                    		canCloseDialog(dialog, false);
                    	}else if( newpw.getText().toString().length() < 1 ){
                    		Toast.makeText(context, "���볤��������λ", Toast.LENGTH_SHORT).show();
                    		canCloseDialog(dialog, false);
                    	}else {
	                		editor.putString("pw",newpw.getText().toString());
	                		editor.commit();
	                		Toast.makeText(context, "�������óɹ�", Toast.LENGTH_SHORT).show();
	                		canCloseDialog(dialog, true);
	                		dialog.dismiss();
                    	}
                    }

                }).show();
        
    }
    /*
     * ����dialog�Ƿ���Թر�
     * close: true�������Թر�
     * 		  false���������Թر�
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
    
    protected static String getStart(){//�ж��Ƿ�����ͯģʽ
    	return sharedPreferences.getString("startchild", "null");
    }
}
