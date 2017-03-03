package com.example.forgetting;

import android.content.ContentProvider;
import android.content.ContentUris;
import android.content.ContentValues;
import android.content.UriMatcher;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.net.Uri;
import android.util.Log;

public class MyContentProvider extends ContentProvider{

	private static final UriMatcher matcher ;
	private DBHelper helper = null;
	private SQLiteDatabase db = null;
	
	public static final String AUTHORITY = "com.example.forgetting.MyContentProvider";
	private static final int INVENTORY_ALL = 0;
	private static final int INVENTORY_ONE = 1;
	private static final int MYOBJECT_ALL = 2;
	private static final int MYOBJECT_ONE = 3;
	
	public static final String CONTENT_TYPE = "vnd.android.cursor.dir/vnd.example.";
	public static final String CONTENT_ITEM_TYPE = "vnd.android.cursor.item/vnd.example.";
	public static final String TABLE_NAME_INVENTORY = DBHelper.Inventory.TABLE_NAME;
	public static final String TABLE_NAME_MYOBJECT = DBHelper.MyObject.TABLE_NAME;
	
    static {  
        matcher = new UriMatcher(UriMatcher.NO_MATCH);  
          
        matcher.addURI(AUTHORITY, TABLE_NAME_INVENTORY, INVENTORY_ALL);   //匹配记录集合  
        matcher.addURI(AUTHORITY, TABLE_NAME_INVENTORY+"/#", INVENTORY_ONE); //匹配单条记录
        
        matcher.addURI(AUTHORITY, TABLE_NAME_MYOBJECT, MYOBJECT_ALL);   //匹配记录集合  
        matcher.addURI(AUTHORITY, TABLE_NAME_MYOBJECT+"/#", MYOBJECT_ONE); //匹配单条记录
    }
    
	@Override
	public boolean onCreate() {
		Log.d("Wangd933", "MyContentProvider onCreate() start: ");
		helper = new DBHelper(getContext());
		Log.d("Wangd933", "MyContentProvider onCreate() end: ");
		return true;
	}
    
	@Override  
    public String getType(Uri uri) {  
        int match = matcher.match(uri);  
        switch (match) {  
	        case INVENTORY_ALL:  
	            return CONTENT_TYPE + TABLE_NAME_INVENTORY;  
	        case INVENTORY_ONE:  
	            return CONTENT_ITEM_TYPE + TABLE_NAME_INVENTORY;  
	        case MYOBJECT_ALL:  
	            return CONTENT_TYPE + TABLE_NAME_MYOBJECT;  
	        case MYOBJECT_ONE:  
	            return CONTENT_ITEM_TYPE + TABLE_NAME_MYOBJECT; 
	        default:  
	        	throw new IllegalArgumentException("Unknown URI: " + uri);  
        }  
    }
	
	@Override
	public int delete(Uri uri, String selection, String[] selectionArgs) {
		
        int match = matcher.match(uri);  
        String tablename = null;
		long _id;
		switch (match) {  
	        case INVENTORY_ALL:  
	        	tablename = TABLE_NAME_INVENTORY;  
	            break;  
	        case INVENTORY_ONE:  
	        	tablename = TABLE_NAME_INVENTORY; 
	            _id = ContentUris.parseId(uri);  
	            selection = "_id = ?";  
	            selectionArgs = new String[]{String.valueOf(_id)};  
	            break;  
	        //delete all same inventory_id's myobject,don't support delete all myobject
	        case MYOBJECT_ALL:  
	        	tablename = TABLE_NAME_MYOBJECT;
	        	break;
	        case MYOBJECT_ONE:  
	        	tablename = TABLE_NAME_MYOBJECT;
	            _id = ContentUris.parseId(uri);  
	            selection = "_id = ?";  
	            selectionArgs = new String[]{String.valueOf(_id)};  
	            break;
	        default:  
	            throw new IllegalArgumentException("Unknown URI: " + uri);  
        }
		if( db == null )  db = helper.getWritableDatabase();
		int count = db.delete(tablename, selection, selectionArgs);  
        if (count > 0) {  
            notifyDataChanged(tablename);  
        }  
        return count; 
	}

	@Override
	public Uri insert(Uri uri, ContentValues values) {
		Log.d("Wangd933", "MyContentProvider insert() start: ");
		int match = matcher.match(uri);  
        String tablename = null;
        switch (match) {  
	        case INVENTORY_ALL:  
	        	tablename = TABLE_NAME_INVENTORY;  
	            break;  
	        case MYOBJECT_ALL:  
	        	tablename = TABLE_NAME_MYOBJECT;
	        	break;
	        default:  
	            throw new IllegalArgumentException("Wrong URI: " + uri);  
        }
        if (values == null) {  
        	throw new IllegalArgumentException("Wrong .values is null: " + uri);  
        }  
        if( db == null )  db = helper.getWritableDatabase();
        long rowId = db.insert(tablename, null, values);  
        if (rowId > 0) {  
            notifyDataChanged(tablename);  
            return ContentUris.withAppendedId(uri, rowId);  
        }  
        Log.d("Wangd933", "MyContentProvider insert() end: ");
        return null;
	}

	@Override
	public Cursor query(Uri uri, String[] projection, String selection, String[] selectionArgs,
			String sortOrder) {
		Log.d("Wangd933", "MyContentProvider query() start");
		int match = matcher.match(uri);
		Log.d("Wangd933", "MyContentProvider query() end");
		String tablename = null;
		switch (match) {  
	        case INVENTORY_ALL:  
	        	tablename = TABLE_NAME_INVENTORY;  
	            break;  
	        //look for one record is not used.
//	        case INVENTORY_ONE:  
//	        	tablename = TABLE_NAME_INVENTORY; 
//	            _id = ContentUris.parseId(uri);  
//	            selection = "_id = ?";  
//	            selectionArgs = new String[]{String.valueOf(_id)};  
//	            break;  
	        //look all record in myobject which has same inventory_id
	        case MYOBJECT_ALL:  
	        	tablename = TABLE_NAME_MYOBJECT;

	        	break;
	        //look for one record is not used.
//	        case MYOBJECT_ONE:  
//	        	tablename = TABLE_NAME_MYOBJECT;
//	            _id = ContentUris.parseId(uri);  
//	            selection = "_id = ?";  
//	            selectionArgs = new String[]{String.valueOf(_id)};  
//	            break;
	        default:  
	            throw new IllegalArgumentException("Unknown URI: " + uri);  
        }
		if( db == null )  db = helper.getWritableDatabase();
		Log.d("Wangd933", "MyContentProvider query() start");
		return db.query(tablename, projection, selection, selectionArgs, null, null, sortOrder);
	}

	@Override
	public int update(Uri uri, ContentValues values, String selection, String[] selectionArgs) {
        int match = matcher.match(uri);  
        String tablename = null;
		long _id;
		switch (match) {  
//			//look for one record is not used.
//	        case INVENTORY_ALL:  
//	        	tablename = TABLE_NAME_INVENTORY;  
//	            break;  
	        case INVENTORY_ONE:  
	        	tablename = TABLE_NAME_INVENTORY; 
	            _id = ContentUris.parseId(uri);  
	            selection = "_id = ?";  
	            selectionArgs = new String[]{String.valueOf(_id)};  
	            break;  
	        //may be changed new inventory,so values should be 
	        //column_name=inventory_id and this column's value is new inventory_id 
	        case MYOBJECT_ALL:  
	        	tablename = TABLE_NAME_MYOBJECT;

	        	break;
	        case MYOBJECT_ONE:  
	        	tablename = TABLE_NAME_MYOBJECT;
	            _id = ContentUris.parseId(uri);  
	            selection = "_id = ?";  
	            selectionArgs = new String[]{String.valueOf(_id)};  
	            break;
	        default:  
	            throw new IllegalArgumentException("Unknown URI: " + uri);  
        }  
		if( db == null )  db = helper.getWritableDatabase();
        int count = db.update(tablename, values, selection, selectionArgs);  
        if (count > 0) {  
            notifyDataChanged(tablename);  
        }  
        return count;
	}
	
	//通知指定URI数据已改变  
    private void notifyDataChanged(String  tablename) {  
        getContext().getContentResolver().notifyChange(
        		Uri.parse("content://" + AUTHORITY + "/"+tablename), null);         
    }

}
