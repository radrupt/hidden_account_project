package com.example.channel;


import android.app.AlertDialog;

import Helper.DBHelper;
import Helper.TvManagerHelper;
import Log.MyLog;
import Model.ChannelCursorLoader;
import Model.MyCursorLoader;
import android.app.Activity;
import android.app.LoaderManager;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Loader;
import android.database.Cursor;
import android.graphics.drawable.PaintDrawable;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnKeyListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.CursorAdapter;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.PopupWindow;
import android.widget.TextView;

public class ChannelActivity extends Activity implements LoaderManager.LoaderCallbacks<Cursor>{
	
	private TvManagerHelper tvManagerHelper;
	
	private PopupWindow mPopupWindow;
	
	private  MyCursorLoader cursorLoader;
	
	private  CursorAdapter cursorAdapter;
	
	private Cursor cursor;
	
	private ListView listView;
	
	private int currentinputresource = 1;
	
	private TextView resource_tview;
	
	private static int Listview_item_position = -1;
	
	private static boolean listView_fav_view = false;//当前listView显示的是否是收藏列表  
	
	private static int textview_id = 0; //当前焦点所在的textview
	
	private static boolean from_fav_cancel_tview = false; //解决当焦点从取消移到收藏时，收藏的左键监听问题
	
	private static boolean rename_repeat = false; //防止重命名的点击事件吃进去两次
	
	private static boolean fav_repeat = false;  //同上
	
	private static boolean listview_left_repeat = false; //同上
	
	private static boolean down_buttom = false;
	
	private static boolean fav_left_listView_repeat = false; //当焦点在收藏控件，用户按左方向键的时候，防止listview重复响应
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_channel);
        
        tvManagerHelper = TvManagerHelper.getInstance(this.getApplicationContext());
        getLoaderManager().initLoader(0, null, this);
        
        cursorAdapter = cursorLoader.GetCursorAdapter(tvManagerHelper.getInputSource());
        listView = (ListView) this.findViewById(R.id.channellist);
        listView.setAdapter(cursorAdapter);
        
        resource_tview = (TextView) this.findViewById(R.id.resource);
        if( tvManagerHelper.getInputSource() == 1 ){
        	resource_tview.setText("模拟信号");
        	currentinputresource = 1;
        }else if( tvManagerHelper.getInputSource() == 3 ){
        	resource_tview.setText("数字信号");
        	currentinputresource = 3;
        }
        
        resource_tview.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View view) {
			
				String text = ((TextView)view).getText().toString();
				if( text.equals("模拟信号") ){
					tvManagerHelper.setInputSource(android.app.TvManager.SOURCE_DTV1);
					cursorAdapter.changeCursor(cursorLoader.Query(3));
					currentinputresource = 3;
					//setInputSource有时没有用
					//cursorAdapter.changeCursor(cursorLoader.Query(tvManagerHelper.getInputSource()));
					((TextView)view).setText("数字信号"); 
				}else if( text.equals("数字信号") ){
					tvManagerHelper.setInputSource(android.app.TvManager.SOURCE_ATV1);
					cursorAdapter.changeCursor(cursorLoader.Query(1));
				//	cursorAdapter.changeCursor(cursorLoader.Query(tvManagerHelper.getInputSource()));
					((TextView)view).setText("模拟信号");
					currentinputresource = 1;
				}
			}
        	
        });
        
        //获取popupwindow的布局，收藏，重命名的布局控件
        LayoutInflater inflater = (LayoutInflater) getSystemService(LAYOUT_INFLATER_SERVICE);
        View popupView = inflater.inflate(R.layout.popupwindow, (ViewGroup)this.findViewById(R.id.popup));
        
        mPopupWindow = new PopupWindow(popupView,400,(int)(55*2),true);	//创建PopupWindow，待弹出窗体
		mPopupWindow.setBackgroundDrawable(new PaintDrawable());//响应返回键必须的语句
		
        //收藏
        TextView fav_tview = (TextView) popupView.findViewById(R.id.fav);
        //重命名
        TextView rename_tview = (TextView) popupView.findViewById(R.id.rename);
        
        //为收藏控件添加监听
        fav_tview.setOnKeyListener(new OnKeyListener(){
			@Override
			public boolean onKey(View v, int keyCode, KeyEvent event) {
				MyLog.i(""+keyCode);
				if( keyCode == KeyEvent.KEYCODE_DPAD_RIGHT ) 	{//右方向键
					if( textview_id == 0 )						{
						textview_id = v.getId(); 				//当前焦点所在的控件ID,此处为收藏控件
					}
					fav_left_listView_repeat = false;
				}else if( keyCode == KeyEvent.KEYCODE_DPAD_LEFT  ) 	{//左方向键
					if( !from_fav_cancel_tview && textview_id == v.getId() ){
						fav_left_listView_repeat = true;
						mPopupWindow.dismiss(); 
						return true;
					}
					from_fav_cancel_tview = false;
				}else if( keyCode == KeyEvent.KEYCODE_DPAD_CENTER ){ //遥控器上的ok键
					if( !fav_repeat ){
						cursorLoader.Update(tvManagerHelper.getInputSource(),
								cursor.getInt(cursor.getColumnIndex(DBHelper.AtvChannel.COLUMN_NAME_INDEX)),
								cursor.getString(cursor.getColumnIndex(DBHelper.AtvChannel.COLUMN_NAME_NAME)),
								cursor.getInt(cursor.getColumnIndex(DBHelper.AtvChannel.COLUMN_NAME_FAV)) > 0 ? 0 : 1);
						if( !listView_fav_view )	{
							cursorAdapter.changeCursor(cursorLoader.Query(tvManagerHelper.getInputSource()));
						}else if( listView_fav_view ) {
							Cursor cursors = cursorLoader.QueryFav(tvManagerHelper.getInputSource());

							if( cursors.getCount() == 0 ) {
								listView_fav_view = false;
								mPopupWindow.dismiss(); 
								cursorAdapter.changeCursor(cursorLoader.Query(tvManagerHelper.getInputSource()));
								
							}else{
								cursorAdapter.changeCursor(cursors);
								Listview_item_position = Listview_item_position > (cursors.getCount() - 1) ? 
											cursors.getCount() - 1 : Listview_item_position ;
							}
							
						}
						cursor =(Cursor) listView.getItemAtPosition(Listview_item_position);
						int isfav = cursor.getInt(cursor.getColumnIndex(DBHelper.AtvChannel.COLUMN_NAME_FAV));
						((TextView)mPopupWindow.getContentView().findViewById(R.id.fav)).setText(isfav>0?"取消收藏":"收藏");
						if( listView_fav_view ) {
							mPopupWindow.dismiss(); 
						}
					}
					fav_repeat = !fav_repeat;
					return true;
				}
				return false;
			}
        	
        });
        //为重命名控件添加监听
        rename_tview.setOnKeyListener(new OnKeyListener(){
        	 
			@Override
			public boolean onKey(View v, int keyCode, KeyEvent event) {
				MyLog.i(""+keyCode); 
				
				if( keyCode == KeyEvent.KEYCODE_DPAD_RIGHT ) 	{
					if( textview_id == R.id.fav )		{
						from_fav_cancel_tview = false;
						textview_id = v.getId();
					}
				}else if( keyCode == KeyEvent.KEYCODE_DPAD_LEFT ) 	{//左方向键
					if( textview_id == v.getId() ){
						textview_id = R.id.fav;
						from_fav_cancel_tview = true;
					}
				}else if( keyCode == KeyEvent.KEYCODE_DPAD_CENTER ){
					if( !rename_repeat ){
						rename(tvManagerHelper, cursorLoader, cursor, ChannelActivity.this);
					}
					rename_repeat = !rename_repeat;
					return true;
				}
				return false;
			}
        	
        });
        
		//注册监听listView里的onclick事件,按ok键，直接去掉listview
		listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {  
 
			@Override 
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				
			}  
        });
		
		//注册监听listView上遥控器右方向键事件
        listView.setOnKeyListener(new OnKeyListener(){

			@Override
			public boolean onKey(View v, int keyCode, KeyEvent event) {

				if( keyCode == KeyEvent.KEYCODE_DPAD_DOWN )	{//下方向键
					
					if(((ListView)v).getSelectedItemPosition() == ((ListView)v).getCount() - 1 ){
						if( down_buttom )	{
							((ListView)v).setSelection(0); 
							down_buttom = false;
							return true;
						}
						down_buttom = true;
					}
					
				}else if( keyCode == KeyEvent.KEYCODE_DPAD_RIGHT )	{//右键
					View view = ((ListView)v).getSelectedView();
					cursor = (Cursor) ((ListView)v).getSelectedItem();
					Listview_item_position = ((ListView)v).getSelectedItemPosition();
					int isfav = cursor.getInt(cursor.getColumnIndex(DBHelper.AtvChannel.COLUMN_NAME_FAV));
					((TextView)mPopupWindow.getContentView().findViewById(R.id.fav)).setText(isfav>0?"取消收藏":"收藏");
					mPopupWindow.showAsDropDown(view, 400,(int)(-55*2));
					return true;
				}else if( keyCode == KeyEvent.KEYCODE_DPAD_LEFT && listView_fav_view == false )	{//左键键
					if( !fav_left_listView_repeat )			{
						if( !listview_left_repeat)	{
							
							Cursor cursor = cursorLoader.QueryFav(currentinputresource);
							if( cursor.getCount() > 0 )	{
								cursorAdapter.changeCursor(cursorLoader.QueryFav(currentinputresource));
								listView_fav_view = true;
							}
						    //cursorAdapter.changeCursor(cursorLoader.QueryFav(tvManagerHelper.getInputSource()));
						}
						listview_left_repeat = !listview_left_repeat;
					}
					fav_left_listView_repeat = false;
					return true;
				}else if( keyCode == KeyEvent.KEYCODE_DPAD_LEFT && listView_fav_view == true )	{//左键键
					if( !fav_left_listView_repeat )			{
						if( !listview_left_repeat)	{
							listView_fav_view = false;
							cursorAdapter.changeCursor(cursorLoader.Query(currentinputresource));
							//cursorAdapter.changeCursor(cursorLoader.Query(tvManagerHelper.getInputSource()));
						}
						listview_left_repeat = !listview_left_repeat;
					}
					fav_left_listView_repeat = false;
					return true;
				}
				return false;
			}
        	
        });
        
    }
    
    private void rename(final TvManagerHelper tvManagerHelper, final MyCursorLoader cursorloader, final Cursor cursor, Context context){
    	LayoutInflater factory = LayoutInflater.from(context);
    	View textEntryView = factory.inflate(R.layout.rename_layout, null);
        final EditText mname_edit = (EditText) textEntryView.findViewById(R.id.newname);
        
        new AlertDialog.Builder(context)
        .setTitle("重命名")
        .setView(textEntryView)
        .setNegativeButton("取消",
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                    	
                    }

                }) 
        .setPositiveButton("确定",
                new DialogInterface.OnClickListener() {

                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                    	update(	cursorloader,
                    		    tvManagerHelper.getInputSource(),
    							cursor.getInt(cursor.getColumnIndex(DBHelper.AtvChannel.COLUMN_NAME_INDEX)),
    							mname_edit.getText().toString(),
    							cursor.getInt(cursor.getColumnIndex(DBHelper.AtvChannel.COLUMN_NAME_FAV)) );

                    	dialog.dismiss();
                    }

                }).show();
        
    }
    
    
    /**
     * 更新数据库里的数据和以及cursoradapter里的指定number号的channel数据，更新listview显示的数据
     * @param:
     * 	cursorloader: 游标加载器
     * 	inputSource：  信源
     * 	number: 	   节目对应的台号
     * 	name: 		   用户设置的新的名字
     * 	fav: 		   用户是否收藏该节目
     * */
    private void update(MyCursorLoader cursorloader, int inputSource, int number, String name, int fav){
    	//更新数据库
    	cursorLoader.Update(inputSource,
			    			number,
							name,
							fav);
    	//更新适配器的游标数据
    	cursorAdapter.changeCursor(cursorLoader.Query(tvManagerHelper.getInputSource()));
    }
    
	@Override
	public Loader<Cursor> onCreateLoader(int arg0, Bundle arg1) {
		cursorLoader = new ChannelCursorLoader(this.getApplicationContext(),tvManagerHelper.getInputSource());
    	return cursorLoader;
	}

	@Override
	public void onLoadFinished(Loader<Cursor> loader, Cursor cursor) {
		cursorAdapter.swapCursor(cursor);
	}

	@Override
	public void onLoaderReset(Loader<Cursor> arg0) {
		if( cursorAdapter != null )
			cursorAdapter.swapCursor(null);
	}
    
	
	
}
