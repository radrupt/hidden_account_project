package com.example.forgetting;

import android.content.Context;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.SimpleCursorAdapter;
import android.widget.TextView;

public class MySimpleCursorAdapter extends SimpleCursorAdapter {
	//private Cursor m_cursor;
	//private Context m_context;
	private byte[] tempsmallpicture;
	public MySimpleCursorAdapter(Context context, int layout, Cursor c,
			String[] from, int[] to, int flags) {
		super(context, layout, c, from, to,flags);
	//	this.m_cursor = c;
	//	this.m_context = context;
	}
	
	@Override
	public View newView(Context context, Cursor cursor, ViewGroup parent) {
		final View view = super.newView(context, cursor, parent);
		ViewHolder holder = new ViewHolder();
		holder.textView = (TextView) view.findViewById(R.id.text1);
		holder.smallimageView = (ImageView) view.findViewById(R.id.image1);
		view.setTag(holder);
		return view;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		
		return super.getView(position, convertView, parent);
	}


	@Override
	public void setViewImage(ImageView v, String value){
        Bitmap bitmap=null;
        try {
        	bitmap =BitmapFactory.decodeByteArray(tempsmallpicture, 0, tempsmallpicture.length);
        } catch (Exception e) {
                e.printStackTrace();
        }
        v.setImageBitmap(bitmap);
	}

	@Override
	public void bindView(View view, Context context, Cursor cursor) {
		ViewHolder holder = (ViewHolder) view.getTag();
		tempsmallpicture = cursor.getBlob(cursor.getColumnIndex(DBHelper.MyObject.COLUMN_NAME_SMALLIMAGE));
		setViewImage(holder.smallimageView,null);
		setViewText(holder.textView, cursor.getString(cursor
				.getColumnIndex(DBHelper.MyObject.COLUMN_NAME_CONTENT)));
		holder.smallimageView.setTag(cursor.getString(cursor
				.getColumnIndex(DBHelper.MyObject.COLUMN_NAME_BIGIMAGEPATH)));
		//super.bindView(view, context, cursor);
	}

	final static class ViewHolder {
		public ImageView smallimageView;
		public TextView textView;
	}
}

