package Model;

import com.example.channel.R;

import Helper.DBHelper;
import android.content.Context;
import android.database.Cursor;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.SimpleCursorAdapter;
import android.widget.TextView;



public class MySimpleCursorAdapter extends SimpleCursorAdapter {

	
	
	public MySimpleCursorAdapter(Context context, int layout, Cursor c,
			String[] from, int[] to, int flags) {
		
		super(context, layout, c, from, to, flags);
	}
	
	@Override
	public View newView(Context context, Cursor cursor, ViewGroup parent) {
		final View view = super.newView(context, cursor, parent);
		ViewHolder holder = new ViewHolder();
		holder.channel_fav = (ImageView) view.findViewById(R.id.channel_fav);
		holder.channel_index = (TextView) view.findViewById(R.id.channel_index);
		holder.channel_name = (TextView) view.findViewById(R.id.channel_name);
		view.setTag(holder);
		return view;
	}

	@Override
	public void bindView(View view, Context context, Cursor cursor) {
		ViewHolder holder = (ViewHolder) view.getTag();
		boolean fav =  cursor.getInt(cursor.getColumnIndex(DBHelper.AtvChannel.COLUMN_NAME_FAV)) > 0;

		setViewImage( holder.channel_fav, (fav ? R.drawable.fav_image : R.drawable.not_fav_image)+"");
		
		setViewText(holder.channel_index, cursor.getString(cursor
				.getColumnIndex(DBHelper.AtvChannel.COLUMN_NAME_INDEX)));
		 
		setViewText(holder.channel_name, cursor.getString(cursor
				.getColumnIndex(DBHelper.AtvChannel.COLUMN_NAME_NAME)));
	}

	final static class ViewHolder {
		public ImageView channel_fav;
		public TextView channel_index;
		public TextView channel_name;
	}
}
