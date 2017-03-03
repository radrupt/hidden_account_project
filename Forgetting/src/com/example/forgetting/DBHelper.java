package com.example.forgetting;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.provider.BaseColumns;
import android.util.Log;

public final class DBHelper extends SQLiteOpenHelper {

	// If you change the database schema, you must increment the database version.
    public static final int DATABASE_VERSION = 1;
    public static final String DATABASE_NAME = "forgetting.db";
	
    public static abstract class Inventory implements BaseColumns{
    	public static final String TABLE_NAME = "inventory";
    	public static final String COLUMN_NAME_ENTRY_ID = "_id";
    	public static final String COLUMN_NAME_NAME = "name";
    }
    
    public static abstract class MyObject implements BaseColumns{
    	public static final String TABLE_NAME = "myobject";
    	public static final String COLUMN_NAME_ENTRY_ID = "_id";
    	public static final String COLUMN_NAME_INVENTORY_ID = "inventory_id";
    	public static final String COLUMN_NAME_CONTENT = "content";
    	public static final String COLUMN_NAME_SMALLIMAGE = "smallimage";
    	public static final String COLUMN_NAME_BIGIMAGEPATH = "bigimage_path";
    }

    private static final String SQL_CREATE_INVENTORY_ENTRIES =
        "CREATE TABLE " + Inventory.TABLE_NAME + " (" +
        		Inventory.COLUMN_NAME_ENTRY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
        		Inventory.COLUMN_NAME_NAME + " TEXT " +
        " )";

    private static final String SQL_CREATE_MYOBJECT_ENTRIES =
		"CREATE TABLE " + MyObject.TABLE_NAME + " ( " +
				MyObject.COLUMN_NAME_ENTRY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
				MyObject.COLUMN_NAME_CONTENT + " TEXT ," +
				MyObject.COLUMN_NAME_SMALLIMAGE + " BLOB ," + 
				MyObject.COLUMN_NAME_BIGIMAGEPATH + " TEXT ," +
				MyObject.COLUMN_NAME_INVENTORY_ID + " INTEGER ," +
				" FOREIGN KEY( " + MyObject.COLUMN_NAME_INVENTORY_ID  + " ) REFERENCES " + 
				Inventory.TABLE_NAME + " ( " + Inventory.COLUMN_NAME_ENTRY_ID + " ) ON UPDATE CASCADE"+
        " )";
    
/*    private static final String SQL_DELETE_ENTRIES =
        "DROP TABLE IF EXISTS " + Inventory.TABLE_NAME;*/
	
	
    
	public DBHelper(Context context){
		super(context, DATABASE_NAME, null, DATABASE_VERSION);
	}
    
	@Override
	public void onCreate(SQLiteDatabase db) {
		Log.d("Wangd933", "DBHelper onCreate() start: ");
		db.execSQL(SQL_CREATE_INVENTORY_ENTRIES);
		db.execSQL(SQL_CREATE_MYOBJECT_ENTRIES);
		Log.d("Wangd933", "DBHelper onCreate() end: ");
	}

	@Override
	public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
		onUpgrade(db, oldVersion, newVersion);
		//这边还是不乱动了，否则数据丢失就悲剧了
		/*db.execSQL("DROP TABLE IF EXISTS "+Inventory.TABLE_NAME);
		db.execSQL("DROP TABLE IF EXISTS "+MyObject.TABLE_NAME);
        onCreate(db);*/
	}
	
	@Override
	public void onOpen(SQLiteDatabase db) {
		super.onOpen(db);
		if(!db.isReadOnly()) {
			// Enable foreign key constraints
			db.execSQL("PRAGMA foreign_keys=ON;");
		}
	}
	
}
