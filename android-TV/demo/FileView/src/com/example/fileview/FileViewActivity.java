package com.example.fileview;

import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.example.fileview.R;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.hardware.usb.UsbManager;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;


public class FileViewActivity extends Activity implements UsbReceiver.Callback {

	String[] mFrom = new String[]{"img","text","lastModified","size","path","dir"};//ad
    int[] mTo = new int[]{R.id.img,R.id.text,R.id.lastmodified,R.id.file_size};
    ListView listView;
    TextView filePathView;				//��¼��ǰ��·��
    UsbReceiver usbReceiver;			
    List<Map<String,Object>> mList;		//listview�������
    MyFile myFile;						//��ǰ�ĸ�Ŀ¼�ļ�
    int prePlace;						//��¼��ǰĿ¼���ϼ�Ŀ¼��λ�ã��������б�
    int isout = -1;						//��¼�û���root_dir�°��·��ؼ��Ĵ���
    String usb_root_dir = "/mnt/usb";	
    String root_dir = "/mnt";			
    boolean pause = false; 				//��¼Ӧ���Ƿ�ֹͣ
    SimpleAdapter mAdapter;
    
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.folder_home);
        getApplicationContext();
        
        listView = (ListView) findViewById(R.id.filelist);
        filePathView = (TextView) findViewById(R.id.filepath);
        
        mList = new ArrayList<Map<String,Object>>();
        mAdapter = new SimpleAdapter(this,mList,R.layout.item,mFrom,mTo);  
        listView.setAdapter(mAdapter);
        
        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {  

			@Override
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				HashMap<String,Object> map = (HashMap<String, Object>) parent.getItemAtPosition(position);  
				prePlace = position;
				String path = (String) map.get("path");
				if( ((String)map.get("dir")).equals("true") )//�����ǰ�ļ����ļ���,�������һ��Ŀ¼
					view(listView,path);	
			}  
        });  
    }
    /*
     * ��ť���������ֱ����KEYCODE_BACK:���˼�
     * 				KEYCODE_3: ����Ŀ¼�˳�
     * 				KEYCODE_4������
     * */
    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if(keyCode == KeyEvent.KEYCODE_BACK) 	{//���˼�
        	if(!myFile.getFile().getPath().equals(root_dir))  {
        		isout = -1;
    			view(listView,myFile.getFile().getPath().replace("/"+myFile.getFile().getName(), ""));
    			return true;
    		}else{ 
    			isout++;
    			if( isout == 0 )  {
    				Toast.makeText(getApplicationContext(), "�ٰ�һ���˳�",
    				     Toast.LENGTH_SHORT).show();
    				return true;
    			}
    		}
        }else if(keyCode == KeyEvent.KEYCODE_3){//���ּ�3�˳�
        //	this.onStop();
        	this.finish();
        	return true;
        }
        else if(keyCode == KeyEvent.KEYCODE_4){//���ּ�4��ѡ������ʽ
        	new AlertDialog.Builder(this)
         	.setTitle("��ѡ��")
         	.setIcon(android.R.drawable.ic_dialog_info)                
         	.setSingleChoiceItems(new String[] {"Ĭ��","����","ʱ��"}, 0, 
         	  new DialogInterface.OnClickListener() {
         	                              
         	     public void onClick(DialogInterface dialog, int which) {
         	    	if( which == 0 ) {//Ĭ������
         	    		MyFile.setSortType(MyFile.sort_default);
         	        	view(listView,myFile.getFile().getPath());
         	    	}
         	        if( which == 1 ) {
         	        	MyFile.setSortType(MyFile.sort_file_type);
         	        	view(listView,myFile.getFile().getPath());
         	        }else if( which == 2 ) {
         	        	MyFile.setSortType(MyFile.sort_time);
         	        	view(listView,myFile.getFile().getPath());
         	        } 
         	        dialog.dismiss();
         	     }
         	  }
         	)
         	.setNegativeButton("ȡ��", null)
         	.show();
        	return true;
        }
        return super.onKeyDown(keyCode, event);
    }
        
    /*
     * ���ݵ�ǰpath����listview�������
     * */
    public void view(ListView listView,String path){
    	mList.clear();
    	myFile = null;//���listView������
        myFile = new MyFile(path);
        if( !(myFile == null) && myFile.getFile().isDirectory()  ){ 
	        Map<String,Object> mMap = null; 
	        File file = null;
	        MyFile[] myFiles = myFile.getChilds();
	        boolean has_file = false;
	        String current_path = path;
	        for(MyFile myFile : myFiles){ 
	        	file = myFile.getFile();
	        	//����U���ļ���ʹ�γ�Ҳ�л��棬����ϵͳ���Ỻ��U������ļ���
	        	//���Ե�U���ļ����ļ�����Ϊ0������ΪU�̲�����
	        	//��������������˳���ܸ���
        		if( file.canRead() == false 
        				|| ( file.getPath().equals(usb_root_dir+"/"+file.getName())
        					 && file.listFiles().length == 0) ) continue;
        		mMap = new HashMap<String,Object>();  
	            mMap.put("img", file.isDirectory() ? 
	            			R.drawable.folder : R.drawable.file);  
	            mMap.put("text", file.getName());  
	            mMap.put("lastModified", MyFile.getLastModified(file.lastModified())); 
	            mMap.put("size", file.isDirectory() ? "" : getFileSize(file.length()));
	            mMap.put("path", file.getPath());
	            mMap.put("dir",file.isDirectory() ? "true" : "false");
	            mList.add(mMap);  
	            has_file = true;
	        }       
	        if( myFile.getChilds().length == 0 || !has_file) {
	        	current_path += " (��ǰĿ¼��û���ļ�)";  
	        }
	        mAdapter.notifyDataSetChanged();
	        listView.setSelection( prePlace > myFiles.length -1 ? 0 : prePlace);
	        filePathView.setText(current_path);
	       
        }
    }
    
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
    /*
     * ����U�̲�ε�ʱ��,����usb·����,��ˢ��Ŀ¼
     */
	@Override
	public void fresh(Context context, Intent intent) {
        String action = intent.getAction();

        if( !pause && action.equals(Intent.ACTION_MEDIA_MOUNTED)){
        	Toast.makeText(context, "USB�Ѿ�����", Toast.LENGTH_SHORT).show();
            if( myFile.getFile().getPath().equals(usb_root_dir)){
    			view(listView,usb_root_dir);
    		}
        }
        else if( !pause && action.equals(Intent.ACTION_MEDIA_EJECT)){
        	Toast.makeText(context, "USB�Ѿ��γ�", Toast.LENGTH_SHORT).show();
        	File file = new File(usb_root_dir);
            if( file == null ) view(listView,root_dir);
            File[] files = file.listFiles();
            boolean has_usb = false;
            for(File _file: files){ 
            	if( _file.listFiles().length == 0 ){//ʵ�������usb�ǲ����ڣ�
            		if( myFile.getFile().getPath().contains(usb_root_dir+"/"+_file.getName())
            			|| myFile.getFile().getPath().equals(usb_root_dir)){
            			view(listView,usb_root_dir);
            		}
            	}else has_usb =true;
            }
        }
	}

	/*
     * ע��usb�㲥����
     * */
    private void registerReceiver() {
		usbReceiver = new UsbReceiver((UsbManager) getSystemService(Context.USB_SERVICE),this);
		IntentFilter filter = new IntentFilter();
		filter.addAction(Intent.ACTION_MEDIA_MOUNTED);
		filter.addAction(Intent.ACTION_MEDIA_EJECT);
		filter.addDataScheme("file");
		registerReceiver(usbReceiver, filter);
	}
	
    @Override
	protected void onPause() {
		super.onPause();
		pause = true;
	}

    @Override
	protected void onResume() {
		super.onResume();
		pause = false;
	}

    @Override
	protected void onStart() {
		super.onStart();
		registerReceiver();
        view(listView,root_dir);
	}

    @Override
	protected void onStop() {
		super.onStop();
		unregisterReceiver(usbReceiver);
	}
}
