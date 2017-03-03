package com.example.forgetting;

import android.content.ContentResolver;
import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.net.Uri;
import android.os.Handler;
import android.widget.CursorAdapter;

public class MyObjectCursorLoader extends MyCursorLoader {
	private int inventory_id;
	private String[] mMyObjectProjection={DBHelper.MyObject.COLUMN_NAME_ENTRY_ID,DBHelper.MyObject.COLUMN_NAME_CONTENT,
			DBHelper.MyObject.COLUMN_NAME_SMALLIMAGE,DBHelper.MyObject.COLUMN_NAME_BIGIMAGEPATH};
	private Context context = null;
	private Uri uri = Uri.parse("content://"+MyContentProvider.AUTHORITY+"/"+
			DBHelper.MyObject.TABLE_NAME);
	//数据库操作模式
	public static final String ONE = "";
	public static final String ALL = "/#";
	private ContentResolver resolver; 
	private Cursor cursor =  null;
	
    public MyObjectCursorLoader(Context context,Handler handler,int inventory_id) {
    	super(context);
        this.resolver = context.getContentResolver();
        this.context = context;
        this.inventory_id = inventory_id;
        context.getContentResolver().registerContentObserver(uri ,true, new MyObjectObserver(handler));
    }
    /**
     * 查询数据等操作放在这里执行 
     */
    @Override
    protected Cursor onLoadInBackground() {
        return Query();
    }
    
    public Cursor Query(){
    	
        cursor = resolver.query(uri,mMyObjectProjection, DBHelper.MyObject.COLUMN_NAME_INVENTORY_ID + " = ?", 
        		new String[]{String.valueOf(inventory_id)}, null);
        return cursor;
    }
    
	@Override
	public CursorAdapter UpdateCursorAdapter() {
		return new MySimpleCursorAdapter(context,R.layout.myobject_list_view, 
                Query(), new String[]{DBHelper.MyObject.COLUMN_NAME_CONTENT,DBHelper.MyObject.COLUMN_NAME_SMALLIMAGE},
                new int[]{R.id.text1,R.id.image1},CursorAdapter.FLAG_REGISTER_CONTENT_OBSERVER);
	}
    
    public CursorAdapter GetCursorAdapter(Cursor cursor){
    	return new MySimpleCursorAdapter(context,R.layout.myobject_list_view, 
                cursor, new String[]{DBHelper.MyObject.COLUMN_NAME_CONTENT,DBHelper.MyObject.COLUMN_NAME_SMALLIMAGE},
                new int[]{ R.id.text1,R.id.image1},CursorAdapter.FLAG_REGISTER_CONTENT_OBSERVER);
    }
	
    public void Insert(ContentValues contentValues){
    	resolver.insert(uri, contentValues);
    }
	@Override
	public void Delete(int _id, String selection, String[] selectionArgs) {
		resolver.delete(Uri.parse(uri + (_id == 0?"":("/"+_id))), null, null);
		//或许批量删除有用
//		resolver.delete(Uri.parse(uri + (_id == 0?"":("/"+_id))), DBHelper.MyObject.COLUMN_NAME_INVENTORY_ID + " = ?",
//			new String[]{String.valueOf(inventory_id)});
	}
	//换content里的内容或将inventory_id = new inventory_id
	//如果是更换单个内容,那么ContentValues里的key是name或content
	//如果是将某清单里的内容放入另一个清单里，那么ContentValues里的key是inventory_id,
	//并且此时的（selection=selectionArgs）=（inventory_id = ？）
	//当然实际上都用不到
	@Override
	public void Update(int _id, ContentValues values, String selection,
			String[] selectionArgs) {
		resolver.update(Uri.parse(uri + (_id == 0?"":("/"+_id))),values, null, null);
	}
}
