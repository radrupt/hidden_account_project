package Model;

import java.util.Iterator;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;

import com.example.channel.R;

import Helper.DBHelper;
import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.widget.CursorAdapter;

public class ChannelCursorLoader extends MyCursorLoader{

	
	private Cursor cursor;
	
	private Context context;
	
	private SQLiteDatabase db;
	
	private MySimpleCursorAdapter mySimpleCursorAdapter;
	
	private int inputsource = 1;
	
	public ChannelCursorLoader(Context context,int inputsource) {
    	super(context);
    	DBHelper helper = new DBHelper(getContext()); 
    	db = helper.getWritableDatabase();
    	this.context = context; 
    	this.inputsource = inputsource;
    }

	/**
     * 第一次查询数据等操作放在这里执行 
     */
    @Override
    protected Cursor onLoadInBackground() {
        return Query(inputsource);
    }
	
	@Override
	public Cursor Query(int inputsource) {
		String table_name = null;
		if( inputsource == 1 ) {
			table_name = DBHelper.AtvChannel.TABLE_NAME;
		}else if( inputsource == 3 ) {
			table_name = DBHelper.DtvChannel.TABLE_NAME;
		}
		if( table_name == null ) return null;
		if( db != null )  {
			cursor = db.rawQuery("select * from "+ table_name+" ORDER BY "+DBHelper.DtvChannel.COLUMN_NAME_INDEX+" ASC",null);
		}
		return cursor;
	}
    
	public Cursor QueryFav(int inputsource){
		String table_name = null;
		if( inputsource == 1 ) {
			table_name = DBHelper.AtvChannel.TABLE_NAME;
		}else if( inputsource == 3 ) {
			table_name = DBHelper.DtvChannel.TABLE_NAME;
		}
		if( table_name == null ) return null;
		if( db != null )  {
			cursor = db.rawQuery("select * from "+ table_name +
								 " where "+ DBHelper.DtvChannel.COLUMN_NAME_FAV +" = 1 "+
								 " ORDER BY "+DBHelper.DtvChannel.COLUMN_NAME_INDEX+" ASC",null);
		}
		return cursor;
	}
	
	/**
	 * 向数据库里插入数据
	 * @param:
	 * 	inputsource: 当前信源
	 * 	contentValues: 对应的数据库里的列名及其值
	 * */
	@Override
	public void Insert(int inputsource,ContentValues contentValues) {//待完善，param：添加信源
		String table_name = null;
		if( inputsource == 1 ) {
			table_name = DBHelper.AtvChannel.TABLE_NAME;
		}else if( inputsource == 3 ) {
			table_name = DBHelper.DtvChannel.TABLE_NAME;
		}
		if( table_name == null ) return;
		String sql = "insert into ";
		String columnname = table_name+" ";
		String attr = "("+DBHelper.AtvChannel.COLUMN_NAME_INDEX+","+
				DBHelper.AtvChannel.COLUMN_NAME_NAME+","+
				DBHelper.AtvChannel.COLUMN_NAME_FAV+") ";
		String values = "values('";
		Set<Entry<String, Object>> s=contentValues.valueSet();
	    Iterator itr = s.iterator();
	    Map.Entry me = null;
	    while(itr.hasNext())
	    {	
	         me = (Map.Entry)itr.next(); 
	         if( !itr.hasNext() ) break;
	         String value =  me.getValue().toString();
	         values+=value+"\",\"";
	    }
	    if( me != null ){
	        String value =  me.getValue().toString();
	        values+=value+"\")";
	    }
		if( db != null ){
			db.execSQL(sql+" "+columnname+attr+" "+values);
		}
		
	}

	//感觉delete没用啊
	@Override
	public void Delete(int inputsource,int id) {
		String table_name = null;
		if( inputsource == 1 ) {
			table_name = DBHelper.AtvChannel.TABLE_NAME;
		}else if( inputsource == 3 ) {
			table_name = DBHelper.DtvChannel.TABLE_NAME;
		}
		if( table_name == null ) return;
		String sql = "delete from ";
		String columnname = table_name+" ";
		String where=" where number = "+id;
		
		if( db != null ){
			db.execSQL(sql+columnname+where);
		}
	}

	/**
	 * 更新数据库里的数据
	 * @param:
	 * 	inputsource: 信源，用于判断表
	 * 	number	   : 节目号
	 * 	newname	   : 名字
	 * 	fav		   : 用户是否收藏该节目
	 * */
	@Override
	public void Update(int inputsource,int number, String newname, int fav) {
		//dws,这边还是有完善空间的，即使用contentValues
		String table_name = null;
		if( inputsource == 1 ) {
			table_name = DBHelper.AtvChannel.TABLE_NAME;
		}else if( inputsource == 3 ) {
			table_name = DBHelper.DtvChannel.TABLE_NAME;
		}
		if( table_name == null ) return;
		String sql = "update ";
		String columnname = table_name+" ";
		String set="set "+DBHelper.AtvChannel.COLUMN_NAME_NAME+" = \""+newname+"\" ,"+
				DBHelper.AtvChannel.COLUMN_NAME_FAV+" = "+fav+" ";
		String where = " where number = "+number;
		
		if( db != null ){
			db.execSQL(sql+columnname+set+where);
		}
	}

	/*
	 * *获得游标适配器，每次使用该函数，都会刷新一次cursor
	 * @return: MySimpleCursorAdapter
	 */
	@Override
	public CursorAdapter GetCursorAdapter(int inputsource) {
		mySimpleCursorAdapter = new MySimpleCursorAdapter(context,R.layout.item, 
				Query(inputsource), new String[]{DBHelper.AtvChannel.COLUMN_NAME_FAV,DBHelper.AtvChannel.COLUMN_NAME_INDEX,DBHelper.AtvChannel.COLUMN_NAME_NAME},
                new int[]{R.id.channel_fav, R.id.channel_index,R.id.channel_name},CursorAdapter.FLAG_REGISTER_CONTENT_OBSERVER); 
		return mySimpleCursorAdapter;
	}

	

}
