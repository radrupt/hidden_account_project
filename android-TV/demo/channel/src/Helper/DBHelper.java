package Helper;

import java.util.List;

import Helper.TvManagerHelper.ChannelInformation;
import Log.MyLog;
import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.provider.BaseColumns;

public class DBHelper extends SQLiteOpenHelper  {
	
	private Context context;
	
	public static final int DATABASE_VERSION = 1;
    public static final String DATABASE_NAME = "TvChannel.db";

    public static abstract class AtvChannel implements BaseColumns{
    	public static final String TABLE_NAME = "atv_channel";
    	public static final String COLUMN_NAME_ENTRY_ID = "_id";
    	public static final String COLUMN_NAME_INDEX = "number";
    	public static final String COLUMN_NAME_NAME = "name";
    	public static final String COLUMN_NAME_FAV = "fav";
    } 
    
    public static abstract class DtvChannel implements BaseColumns{
    	public static final String TABLE_NAME = "dtv_channel";
    	public static final String COLUMN_NAME_ENTRY_ID = "_id";
    	public static final String COLUMN_NAME_INDEX = "number";
    	public static final String COLUMN_NAME_NAME = "name";
    	public static final String COLUMN_NAME_FAV = "fav";
    }
    
    private static final String SQL_CREATE_ATVCHANNEL_ENTRIES =
            "CREATE TABLE " + AtvChannel.TABLE_NAME + " (" +
            		AtvChannel.COLUMN_NAME_ENTRY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
            		AtvChannel.COLUMN_NAME_INDEX + " INTEGER, " +
            		AtvChannel.COLUMN_NAME_NAME + " TEXT, " +
            		AtvChannel.COLUMN_NAME_FAV + " BOOLEAN "+
            " )";
    
    private static final String SQL_CREATE_DTVCHANNEL_ENTRIES =
            "CREATE TABLE " + DtvChannel.TABLE_NAME + " (" + 
            		DtvChannel.COLUMN_NAME_ENTRY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
            		DtvChannel.COLUMN_NAME_INDEX + " INTEGER, " +
            		DtvChannel.COLUMN_NAME_NAME + " TEXT, " +
            		DtvChannel.COLUMN_NAME_FAV + " BOOLEAN "+
            " )";
     
	public DBHelper(Context context) {
		super(context, DATABASE_NAME, null, DATABASE_VERSION);
		this.context = context;
	}

	@Override
	public void onCreate(SQLiteDatabase db) {
		MyLog.i("DBHelper onCreate SQL_CREATE_ATVCHANNEL_ENTRIES");
		db.execSQL(SQL_CREATE_ATVCHANNEL_ENTRIES);
		MyLog.i("DBHelper onCreate SQL_CREATE_DTVCHANNEL_ENTRIES");
		db.execSQL(SQL_CREATE_DTVCHANNEL_ENTRIES);
		TvManagerHelper tvManagerHelper = TvManagerHelper.getInstance(context);
		List<ChannelInformation> channelInformationList = tvManagerHelper.getAtvChannelList();
		for(ChannelInformation cinfo : channelInformationList){
			db.execSQL("insert into "+AtvChannel.TABLE_NAME+"("+
					AtvChannel.COLUMN_NAME_INDEX+","+AtvChannel.COLUMN_NAME_NAME+","+
					AtvChannel.COLUMN_NAME_FAV+") "+"values("+cinfo.channelNumber+", \""+
					cinfo.channelName+"\" , "+(int)(cinfo.isFavorite ? 1 : 0)+")");
		} 
		channelInformationList = tvManagerHelper.getDtvChannelList();
		for(ChannelInformation cinfo : channelInformationList){
			db.execSQL("insert into "+DtvChannel.TABLE_NAME+"("+
					DtvChannel.COLUMN_NAME_INDEX+","+DtvChannel.COLUMN_NAME_NAME+","+
					DtvChannel.COLUMN_NAME_FAV+") "+"values("+cinfo.channelNumber+", \""+
					cinfo.channelName+"\" , "+(int)(cinfo.isFavorite ? 1 : 0)+")");
		}
	}

	@Override
	public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
		onUpgrade(db, oldVersion, newVersion);
	}

}
