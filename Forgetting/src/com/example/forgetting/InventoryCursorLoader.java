package com.example.forgetting;

import android.content.ContentResolver;
import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.net.Uri;
import android.os.Handler;
import android.util.Log;
import android.widget.CursorAdapter;
import android.widget.SimpleCursorAdapter;

public class InventoryCursorLoader extends MyCursorLoader{
	
	private String[] mInventoryProjection={DBHelper.Inventory.COLUMN_NAME_ENTRY_ID ,DBHelper.Inventory.COLUMN_NAME_NAME };
	private ContentResolver resolver = null; 
	private Context context = null;
	private Uri uri = Uri.parse("content://"+MyContentProvider.AUTHORITY+"/"+
			DBHelper.Inventory.TABLE_NAME);
    private Cursor cursor =  null;
	
	
    public InventoryCursorLoader(Context context,Handler handler) {
    	
        super(context);
        Log.d("Wangd933", "InventoryCursorLoader InventoryCursorLoader() start: ");
        this.resolver = context.getContentResolver();
        this.context = context;
        this.resolver.registerContentObserver(uri, true, new InventoryObserver(handler));
        Log.d("Wangd933", "InventoryCursorLoader InventoryCursorLoader() end: ");
    }
    /**
     * 查询数据等操作放在这里执行 
     */
    @Override
    protected Cursor onLoadInBackground() {
    	Log.d("Wangd933", "onLoadInBackground*******************************");
        return Query();
    }
    
    public Cursor Query(){
    	/*if( cursor != null && !cursor.isClosed() ) 
    		cursor.close();//这段代码不知究竟有没有用，先隐去
*/    	cursor = resolver.query(uri,mInventoryProjection, null,null, null);
        return cursor;
    }
    
	@Override
	public CursorAdapter UpdateCursorAdapter() {
		return new SimpleCursorAdapter(context,android.R.layout.simple_list_item_1, 
                Query(), new String[]{DBHelper.Inventory.COLUMN_NAME_NAME},
                new int[]{ android.R.id.text1},CursorAdapter.FLAG_REGISTER_CONTENT_OBSERVER);
	}
    
    public CursorAdapter GetCursorAdapter(Cursor cursor){
    	return new SimpleCursorAdapter(context,android.R.layout.simple_list_item_1, 
                cursor, new String[]{DBHelper.Inventory.COLUMN_NAME_NAME},
                new int[]{ android.R.id.text1},CursorAdapter.FLAG_REGISTER_CONTENT_OBSERVER);
    }
    
    public void Insert(ContentValues contentValues){
    	Log.d("Wangd933", "InventoryCursorLoader Insert() start: "+uri);
    	resolver.insert(uri, contentValues);
    	Log.d("Wangd933", "InventoryCursorLoader Insert() end");
    }
    //感觉暂时不太可能需要全部删除
	@Override
	public void Delete(int _id, String selection, String[] selectionArgs) {
		Log.d("Wangd933", "remove_list_item  Delete+"+_id);
		resolver.delete(Uri.parse(uri + (_id == 0?"":("/"+_id))), null, null);
	}
	//values name = new name(换名字)
	@Override
	public void Update(int _id, ContentValues values, String selection,
			String[] selectionArgs) {
		resolver.update(Uri.parse(uri + (_id == 0?"":("/"+_id))),values, null, null);
	}
	
}
