package com.example.forgetting;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.PixelFormat;
import android.hardware.Camera;
import android.hardware.Camera.PictureCallback;
import android.util.Log;
import android.view.SurfaceHolder;
import android.view.SurfaceView;

public class CameraSurfacePreview extends SurfaceView implements SurfaceHolder.Callback{

	private String tag = "Wangd933";
	private SurfaceHolder myHolder = null;
	private Camera myCamera ;
	
	public Camera getCamera(){ 
		return myCamera; 
	}
	public void setCamera( Camera camera ){ 
		 myCamera = camera; 
	} 
	
	public CameraSurfacePreview(Context context){
		super(context);
		/*使用hardware相机API的基本流程是：
		 *
		 * */
		setZOrderOnTop(false);
		myHolder = getHolder();
		
		myHolder.setFormat(PixelFormat.TRANSPARENT);//translucent半透明 transparent透明
		myHolder.addCallback(this);
	}

	@Override
	public void surfaceChanged(SurfaceHolder holder, int format, int w, int h) {
		// TODO Auto-generated method stub
		Log.d(tag, "surfaceChanged() is called ");
		try{
			Log.d(tag, "startPreview() is called");
			myCamera.startPreview();
			Log.d(tag, "startPreview() is called");
		}catch(Exception e){
			Log.d(tag, "Error ");
		}
	}

	public void open() throws Exception{
		myCamera = Camera.open(0);
		myCamera.setPreviewDisplay(getHolder());
	}
	
	@Override
	public void surfaceCreated(SurfaceHolder holder) {
		// TODO Auto-generated method stub
		Log.d(tag, "surfaceCreated() is called");
		try{
			open();
		}catch(Exception e){
			Log.d(tag, "Error setting camera preview！"+ e.getMessage());
		}
	}

	@Override
	public void surfaceDestroyed(SurfaceHolder holder) {
		Log.d(tag, "SurfaceDestroyed() is called");
		if( myCamera != null ){
			Log.d(tag, "myCamera.stopPreview start");
			myCamera.stopPreview();
			Log.d(tag, "myCamera.stopPreview end");
			Log.d(tag, "myCamera.release start");
			myCamera.release();
			myCamera = null;
			Log.d(tag, "myCamera.release end");
		}
	}
	
	public void takePicture(PictureCallback imageCallback){
		myCamera.takePicture(null,null,imageCallback);
	}
	
	@Override
	public void draw(Canvas canvas){
		
	}
	
}
