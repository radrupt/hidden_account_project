package com.example.forgetting;

import android.content.Context;
import android.util.AttributeSet;
import android.util.Log;
import android.view.View;
import android.widget.TextView;

public class MyTextView extends TextView{

	public MyTextView(Context context) {
		super(context);
//		// TODO Auto-generated constructor stub
//		this.setOnLongClickListener(new OnLongClickListener() {
//			
//			@Override
//			public boolean onLongClick(View arg0) {
//				// TODO Auto-generated method stub
//				Tools.MessageShow(arg0.getContext(), "dsfds");
//				Log.d("wangddafdsf", "ccccccccccccc");
//				return false;
//			}
//		});
	}
	public MyTextView(Context context, AttributeSet attrs) {
		super(context,attrs);
//		this.setOnLongClickListener(new OnLongClickListener() {
//			
//			@Override
//			public boolean onLongClick(View arg0) {
//				// TODO Auto-generated method stub
//				Tools.MessageShow(arg0.getContext(), "dsfds");
//				Log.d("wangddafdsf", "ccccccccccccc");
//				return false;
//			}
//		});
	}
	
	public MyTextView(Context context, AttributeSet attrs,int defStyle) {
		super(context,attrs,defStyle);
		/*this.setOnLongClickListener(new OnLongClickListener() {
			
			@Override
			public boolean onLongClick(View arg0) {
				// TODO Auto-generated method stub
				Tools.MessageShow(arg0.getContext(), "dsfds");
				return true;
			}
		});*/
	}
}
