package com.example.forgetting;

import android.R.integer;
import android.content.ContentValues;
import android.content.CursorLoader;
import android.database.Cursor;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.TextView;

public class ForMyObjectFragment extends ForFragment {
	@Override
	public void onActivityCreated(Bundle savedInstanceState){
		super.onActivityCreated(savedInstanceState);
	}
	@Override
	public void add_item(String text ) {
		ContentValues contentValues = new ContentValues();
		contentValues.put("inventory_id", inventory_id);
		contentValues.put("content", text);
		cursorLoader.Insert(contentValues);
	}
	
	public void add_item(String text, String path ) {
		ContentValues contentValues = new ContentValues();
		contentValues.put("inventory_id", inventory_id);
		contentValues.put("bigimage_path", path);
		contentValues.put("content", text);
		cursorLoader.Insert(contentValues);
	}

	public void add_item(String text, String path, byte[] thumbbail){
		ContentValues contentValues = new ContentValues();
		contentValues.put(DBHelper.MyObject.COLUMN_NAME_INVENTORY_ID, inventory_id);
		contentValues.put(DBHelper.MyObject.COLUMN_NAME_BIGIMAGEPATH, path);
		contentValues.put(DBHelper.MyObject.COLUMN_NAME_CONTENT, text);
		contentValues.put(DBHelper.MyObject.COLUMN_NAME_SMALLIMAGE, thumbbail);
		cursorLoader.Insert(contentValues);
	}
	
	@Override
    public void onListItemClick(ListView l, View v, int position, long id) {
        if(MainActivity.is_add) mCallback.onSelected(0);
    }
	
	@Override
	protected CursorLoader init() {
		Log.d("Wangd933", "ForMyObjectFragment init start");
		inventory_id = getArguments().getInt("inventory_id") ;
		cursorLoader = new MyObjectCursorLoader(getActivity(), handler,inventory_id);
		Log.d("Wangd933", "ForMyObjectFragment init end");
		return cursorLoader;
	}

	@Override
	protected void Update() {
		cursorAdapter = cursorLoader.UpdateCursorAdapter();
		setListAdapter(cursorAdapter);
	}
	
    @Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState){
    	Log.d("Wangd933", "ForFragment onCreateView");
		return inflater.inflate(R.layout.listview_layout, container, false);
	}
	@Override
	public void updateItem(int _id, String content) {
		// TODO Auto-generated method stub
		ContentValues contentValues = new ContentValues();
		contentValues.put("inventory_id", inventory_id);
		contentValues.put("content", content);
		Log.d("wangd933", ""+_id);
		cursorLoader.Update(_id, contentValues, null, null);
	}
	
}
