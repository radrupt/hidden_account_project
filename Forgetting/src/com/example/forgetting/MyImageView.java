package com.example.forgetting;

import android.content.Context;
import android.content.Intent;
import android.util.AttributeSet;
import android.util.Log;
import android.view.View;
import android.widget.ImageView;
import android.widget.Toast;

public class MyImageView extends ImageView{
	
	public static String imagepath = "imagepath";
	
	private Context context;
	
	private void PreViewBigPicture(String path){
		Log.d("Wangd933", "------------------------------------start");
		Intent intent = new Intent(context,
				BigPictureDialogActivity.class); 
		Log.d("Wangd933", "------------------------------------end");
		intent.putExtra(imagepath, path);
		
		context.startActivity(intent); 
		
	}
	
	public MyImageView(Context context) {
		super(context);
		this.context = context;
		this.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				String path = ((ImageView)arg0).getTag().toString();
				PreViewBigPicture(path);
			}
			
		});
	}
	
	public MyImageView(Context context, AttributeSet attrs) {
		super(context,attrs);
		this.context = context;
		this.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				String path = ((ImageView)arg0).getTag().toString();
				PreViewBigPicture(path);
			}
		});
	}
	
	public MyImageView(Context context, AttributeSet attrs,int defStyle) {
		super(context,attrs,defStyle);
		this.context = context;
		this.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				String path = ((ImageView)arg0).getTag().toString();
				PreViewBigPicture(path);
			}
		});
	}

}
