package com.example.forgetting;

import android.content.ContentValues;
import android.content.Context;
import android.content.CursorLoader;
import android.database.Cursor;
import android.widget.CursorAdapter;

public abstract class MyCursorLoader extends CursorLoader {

	public MyCursorLoader(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	abstract public void Insert(ContentValues contentValues);
	//_id 为0表示ALL模式
	abstract public void Delete(int _id, String selection, String[] selectionArgs);
	
	abstract public void Update(int _id, ContentValues values, String selection, String[] selectionArgs);
	
	abstract public Cursor Query();
	
	abstract public CursorAdapter UpdateCursorAdapter();
	
	abstract public CursorAdapter GetCursorAdapter(Cursor cursor);
}
