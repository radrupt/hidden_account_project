package com.example.fileview;

import java.io.File;
import java.text.Format;
import java.text.SimpleDateFormat;
import java.util.Arrays;
import java.util.Comparator;
import java.util.Date;

import android.util.Log;

public class MyFile {
	
	public static final int sort_default = 0;//Ĭ��ϵͳ����
	
	public static final int sort_time  = 1;//�޸�ʱ��
	
	public static final int sort_file_type  = 2;//�ļ�����
	
	private MyFile[] childs = null;
	private File file;
	private static int sort_type = sort_default;//Ĭ������ʽ
	private long last_modified;
	private static boolean flag = false;

	public MyFile(String path){
		//�쳣����
		try{
			this.file = new File(path);
			last_modified = this.file.lastModified();
		}catch(Exception e){
			Log.w("wangd933","Exception: " + e);
		}
	}
	
	public MyFile[] getChilds(){
		//��֤childsֻ��ʼ��һ��,������û��ı�������ʽ������������childs
		if( file != null && this.file.isDirectory() && (childs ==null || flag == true) ) {
			childs = fileToMyFile(this.file.listFiles());
			flag = false;
		}
		if( sort_type == sort_time ) {
			orderByDate(this.childs);
		}else if( sort_type == sort_file_type ) {
			orderByFileType(this.childs);
		}
		return this.childs;
	}
	
	public File getFile(){
		return this.file;
	}	
	
	public long lastModified(){ 
		return this.last_modified; 
	}

	/*
	 * ��ϵͳ���ļ�����ת��Ϊ�Զ�����ļ�����
	 * �Ա�����ʱ����
	 * */
	private MyFile[] fileToMyFile(File[] files){
		MyFile[] childs = new MyFile[files.length];
		for(int i = 0; i < files.length; i++){
			childs[i] = new MyFile(files[i].getPath());
		}
		return childs;
	}
	
	public static String getLastModified(long last_modified){

		Date d = new Date(last_modified);
		Format simpleFormat = new SimpleDateFormat("yyyy/MM/dd hh:mm:ss");
		return "�޸�����: " + simpleFormat.format(d);
	}
	
	/*
	 * ��������ʽ
	 * */
	public static void setSortType(int sorttype){
		flag = true;
		sort_type = sorttype; 
	}
	
	/*
	 * �ļ��޸�ʱ���������
	 */
	public static void orderByDate(MyFile[] fs) {  
	    Arrays.sort(fs,new Comparator<MyFile>(){  
	        public int compare(MyFile f1, MyFile f2) {  
	        	long diff = f1.lastModified() - f2.lastModified();  
	            if (diff > 0)  
	                return 1;  
	            else if (diff == 0)  
	                return 0;  
	            else  
	                return -1;  
	        }  
	        public boolean equals(Object obj) {  
	            return true;  
	        }  
	          
	    });   
	}  
	/*
	 * �ļ����������ļ������ļ�֮ǰ
	 */
	public static void orderByFileType(MyFile[] fs) {  
	    Arrays.sort(fs,new Comparator<MyFile>(){  
	        public int compare(MyFile f1, MyFile f2) {  
	            int _f1 = f1.getFile().isDirectory() ? 1 : 0; 
	            int _f2 = f2.getFile().isDirectory() ? 1 : 0; 
	            int diff  = _f2 - _f1;
	            if (diff > 0)  
	                return 1;  
	            else if (diff == 0)  
	                return 0;  
	            else  
	                return -1;  
	        }  
	        public boolean equals(Object obj) {  
	            return true;  
	        }  
	          
	    });   
	}  
}
