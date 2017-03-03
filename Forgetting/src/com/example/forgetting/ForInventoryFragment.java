package com.example.forgetting;

import android.app.Activity;
import android.content.ContentValues;
import android.content.CursorLoader;
import android.database.Cursor;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

public class ForInventoryFragment extends ForFragment {

	@Override
	public void add_item(String text) {
		ContentValues contentValues = new ContentValues();
		contentValues.put("name", text);
		cursorLoader.Insert(contentValues);
	}
	
	@Override
	public void add_item(String text, String path) {
		ContentValues contentValues = new ContentValues();
		contentValues.put("name", text);
		cursorLoader.Insert(contentValues);
	}

	@Override
	protected CursorLoader init() {
		cursorLoader = new InventoryCursorLoader(getActivity(), handler);
		return cursorLoader;
	}

	@Override
	protected void Update() {
		cursorAdapter = cursorLoader.UpdateCursorAdapter();
		setListAdapter(cursorAdapter);
	}
	
    @Override
    public void onListItemClick(ListView l, View v, int position, long id) {
    	Log.d("Wangd933", "onListItemClick start");
    	if( istouch ) { istouch = false; return; }
        // Notify the parent activity of selected item
  //  	int inventory_id = cursorLoader.getId();
    	Cursor cursor=(Cursor)getListView().getItemAtPosition(position);
    	int nameColumnIndex = cursor.getColumnIndex("_id");
		int inventory_id = cursor.getInt(nameColumnIndex);
        mCallback.onSelected(inventory_id);
        
        // Set the item as checked to be highlighted when in two-pane layout
        getListView().setItemChecked(position, true);
        Log.d("Wangd933", "onListItemClick end");
    }
    
    @Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState){
    	Log.d("Wangd933", "ForFragment onCreateView");
		return inflater.inflate(R.layout.listview_layout, container, false);
	}

	@Override
	public void add_item(String text, String path, byte[] thumbbail) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void updateItem(int _id, String content) {
		// TODO Auto-generated method stub
		ContentValues contentValues = new ContentValues();
		contentValues.put("name", content);
		Log.d("wangd933", ""+_id);
		cursorLoader.Update(_id, contentValues, null, null);
	}

}
