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
     * ��һ�β�ѯ���ݵȲ�����������ִ�� 
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
	 * �����ݿ����������
	 * @param:
	 * 	inputsource: ��ǰ��Դ
	 * 	contentValues: ��Ӧ�����ݿ������������ֵ
	 * */
	@Override
	public void Insert(int inputsource,ContentValues contentValues) {//�����ƣ�param�������Դ
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

	//�о�deleteû�ð�
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
	 * �������ݿ��������
	 * @param:
	 * 	inputsource: ��Դ�������жϱ�
	 * 	number	   : ��Ŀ��
	 * 	newname	   : ����
	 * 	fav		   : �û��Ƿ��ղظý�Ŀ
	 * */
	@Override
	public void Update(int inputsource,int number, String newname, int fav) {
		//dws,��߻��������ƿռ�ģ���ʹ��contentValues
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
	 * *����α���������ÿ��ʹ�øú���������ˢ��һ��cursor
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
