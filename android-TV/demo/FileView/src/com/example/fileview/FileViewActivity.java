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
    TextView filePathView;				//记录当前的路径
    UsbReceiver usbReceiver;			
    List<Map<String,Object>> mList;		//listview里的数据
    MyFile myFile;						//当前的根目录文件
    int prePlace;						//记录当前目录在上级目录的位置，适用于列表
    int isout = -1;						//记录用户在root_dir下按下返回键的次数
    String usb_root_dir = "/mnt/usb";	
    String root_dir = "/mnt";			
    boolean pause = false; 				//记录应用是否停止
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
				if( ((String)map.get("dir")).equals("true") )//如果当前文件是文件夹,则进入下一级目录
					view(listView,path);	
			}  
        });  
    }
    /*
     * 按钮键监听，分别监听KEYCODE_BACK:后退键
     * 				KEYCODE_3: 任意目录退出
     * 				KEYCODE_4：排序
     * */
    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if(keyCode == KeyEvent.KEYCODE_BACK) 	{//后退键
        	if(!myFile.getFile().getPath().equals(root_dir))  {
        		isout = -1;
    			view(listView,myFile.getFile().getPath().replace("/"+myFile.getFile().getName(), ""));
    			return true;
    		}else{ 
    			isout++;
    			if( isout == 0 )  {
    				Toast.makeText(getApplicationContext(), "再按一次退出",
    				     Toast.LENGTH_SHORT).show();
    				return true;
    			}
    		}
        }else if(keyCode == KeyEvent.KEYCODE_3){//数字键3退出
        //	this.onStop();
        	this.finish();
        	return true;
        }
        else if(keyCode == KeyEvent.KEYCODE_4){//数字键4，选择排序方式
        	new AlertDialog.Builder(this)
         	.setTitle("请选择")
         	.setIcon(android.R.drawable.ic_dialog_info)                
         	.setSingleChoiceItems(new String[] {"默认","类型","时间"}, 0, 
         	  new DialogInterface.OnClickListener() {
         	                              
         	     public void onClick(DialogInterface dialog, int which) {
         	    	if( which == 0 ) {//默认排序
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
         	.setNegativeButton("取消", null)
         	.show();
        	return true;
        }
        return super.onKeyDown(keyCode, event);
    }
        
    /*
     * 根据当前path生成listview里的数据
     * */
    public void view(ListView listView,String path){
    	mList.clear();
    	myFile = null;//清空listView的数据
        myFile = new MyFile(path);
        if( !(myFile == null) && myFile.getFile().isDirectory()  ){ 
	        Map<String,Object> mMap = null; 
	        File file = null;
	        MyFile[] myFiles = myFile.getChilds();
	        boolean has_file = false;
	        String current_path = path;
	        for(MyFile myFile : myFiles){ 
	        	file = myFile.getFile();
	        	//由于U盘文件即使拔出也有缓存，但是系统不会缓存U盘里的文件，
	        	//所以当U盘文件里文件数量为0，则认为U盘不存在
	        	//或运算后的与运算顺序不能更换
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
	        	current_path += " (当前目录下没有文件)";  
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
     * 当有U盘插拔的时候,且在usb路径下,则刷新目录
     */
	@Override
	public void fresh(Context context, Intent intent) {
        String action = intent.getAction();

        if( !pause && action.equals(Intent.ACTION_MEDIA_MOUNTED)){
        	Toast.makeText(context, "USB已经插入", Toast.LENGTH_SHORT).show();
            if( myFile.getFile().getPath().equals(usb_root_dir)){
    			view(listView,usb_root_dir);
    		}
        }
        else if( !pause && action.equals(Intent.ACTION_MEDIA_EJECT)){
        	Toast.makeText(context, "USB已经拔出", Toast.LENGTH_SHORT).show();
        	File file = new File(usb_root_dir);
            if( file == null ) view(listView,root_dir);
            File[] files = file.listFiles();
            boolean has_usb = false;
            for(File _file: files){ 
            	if( _file.listFiles().length == 0 ){//实际上这个usb是不存在，
            		if( myFile.getFile().getPath().contains(usb_root_dir+"/"+_file.getName())
            			|| myFile.getFile().getPath().equals(usb_root_dir)){
            			view(listView,usb_root_dir);
            		}
            	}else has_usb =true;
            }
        }
	}

	/*
     * 注册usb广播监听
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
