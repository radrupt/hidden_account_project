package Model;

import android.content.ContentValues;
import android.content.Context;
import android.content.CursorLoader;
import android.database.Cursor;
import android.widget.CursorAdapter;

public abstract class MyCursorLoader extends CursorLoader{

	public MyCursorLoader(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}
	
	abstract public void Insert(int inputsource,ContentValues contentValues);

	abstract public void Delete(int inputsource, int id);
	
	abstract public void Update(int inputsource, int id, String newname, int fav);
	
	abstract public Cursor Query(int inputsource);
	
	abstract public Cursor QueryFav(int inputsource);
	
	abstract public CursorAdapter GetCursorAdapter(int inputsource);

}
