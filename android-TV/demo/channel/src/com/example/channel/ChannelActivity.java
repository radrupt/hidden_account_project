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
	
	private static boolean listView_fav_view = false;//��ǰlistView��ʾ���Ƿ����ղ��б�  
	
	private static int textview_id = 0; //��ǰ�������ڵ�textview
	
	private static boolean from_fav_cancel_tview = false; //����������ȡ���Ƶ��ղ�ʱ���ղص������������
	
	private static boolean rename_repeat = false; //��ֹ�������ĵ���¼��Խ�ȥ����
	
	private static boolean fav_repeat = false;  //ͬ��
	
	private static boolean listview_left_repeat = false; //ͬ��
	
	private static boolean down_buttom = false;
	
	private static boolean fav_left_listView_repeat = false; //���������ղؿؼ����û����������ʱ�򣬷�ֹlistview�ظ���Ӧ
	
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
        	resource_tview.setText("ģ���ź�");
        	currentinputresource = 1;
        }else if( tvManagerHelper.getInputSource() == 3 ){
        	resource_tview.setText("�����ź�");
        	currentinputresource = 3;
        }
        
        resource_tview.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View view) {
			
				String text = ((TextView)view).getText().toString();
				if( text.equals("ģ���ź�") ){
					tvManagerHelper.setInputSource(android.app.TvManager.SOURCE_DTV1);
					cursorAdapter.changeCursor(cursorLoader.Query(3));
					currentinputresource = 3;
					//setInputSource��ʱû����
					//cursorAdapter.changeCursor(cursorLoader.Query(tvManagerHelper.getInputSource()));
					((TextView)view).setText("�����ź�"); 
				}else if( text.equals("�����ź�") ){
					tvManagerHelper.setInputSource(android.app.TvManager.SOURCE_ATV1);
					cursorAdapter.changeCursor(cursorLoader.Query(1));
				//	cursorAdapter.changeCursor(cursorLoader.Query(tvManagerHelper.getInputSource()));
					((TextView)view).setText("ģ���ź�");
					currentinputresource = 1;
				}
			}
        	
        });
        
        //��ȡpopupwindow�Ĳ��֣��ղأ��������Ĳ��ֿؼ�
        LayoutInflater inflater = (LayoutInflater) getSystemService(LAYOUT_INFLATER_SERVICE);
        View popupView = inflater.inflate(R.layout.popupwindow, (ViewGroup)this.findViewById(R.id.popup));
        
        mPopupWindow = new PopupWindow(popupView,400,(int)(55*2),true);	//����PopupWindow������������
		mPopupWindow.setBackgroundDrawable(new PaintDrawable());//��Ӧ���ؼ���������
		
        //�ղ�
        TextView fav_tview = (TextView) popupView.findViewById(R.id.fav);
        //������
        TextView rename_tview = (TextView) popupView.findViewById(R.id.rename);
        
        //Ϊ�ղؿؼ���Ӽ���
        fav_tview.setOnKeyListener(new OnKeyListener(){
			@Override
			public boolean onKey(View v, int keyCode, KeyEvent event) {
				MyLog.i(""+keyCode);
				if( keyCode == KeyEvent.KEYCODE_DPAD_RIGHT ) 	{//�ҷ����
					if( textview_id == 0 )						{
						textview_id = v.getId(); 				//��ǰ�������ڵĿؼ�ID,�˴�Ϊ�ղؿؼ�
					}
					fav_left_listView_repeat = false;
				}else if( keyCode == KeyEvent.KEYCODE_DPAD_LEFT  ) 	{//�����
					if( !from_fav_cancel_tview && textview_id == v.getId() ){
						fav_left_listView_repeat = true;
						mPopupWindow.dismiss(); 
						return true;
					}
					from_fav_cancel_tview = false;
				}else if( keyCode == KeyEvent.KEYCODE_DPAD_CENTER ){ //ң�����ϵ�ok��
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
						((TextView)mPopupWindow.getContentView().findViewById(R.id.fav)).setText(isfav>0?"ȡ���ղ�":"�ղ�");
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
        //Ϊ�������ؼ���Ӽ���
        rename_tview.setOnKeyListener(new OnKeyListener(){
        	 
			@Override
			public boolean onKey(View v, int keyCode, KeyEvent event) {
				MyLog.i(""+keyCode); 
				
				if( keyCode == KeyEvent.KEYCODE_DPAD_RIGHT ) 	{
					if( textview_id == R.id.fav )		{
						from_fav_cancel_tview = false;
						textview_id = v.getId();
					}
				}else if( keyCode == KeyEvent.KEYCODE_DPAD_LEFT ) 	{//�����
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
        
		//ע�����listView���onclick�¼�,��ok����ֱ��ȥ��listview
		listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {  
 
			@Override 
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				
			}  
        });
		
		//ע�����listView��ң�����ҷ�����¼�
        listView.setOnKeyListener(new OnKeyListener(){

			@Override
			public boolean onKey(View v, int keyCode, KeyEvent event) {

				if( keyCode == KeyEvent.KEYCODE_DPAD_DOWN )	{//�·����
					
					if(((ListView)v).getSelectedItemPosition() == ((ListView)v).getCount() - 1 ){
						if( down_buttom )	{
							((ListView)v).setSelection(0); 
							down_buttom = false;
							return true;
						}
						down_buttom = true;
					}
					
				}else if( keyCode == KeyEvent.KEYCODE_DPAD_RIGHT )	{//�Ҽ�
					View view = ((ListView)v).getSelectedView();
					cursor = (Cursor) ((ListView)v).getSelectedItem();
					Listview_item_position = ((ListView)v).getSelectedItemPosition();
					int isfav = cursor.getInt(cursor.getColumnIndex(DBHelper.AtvChannel.COLUMN_NAME_FAV));
					((TextView)mPopupWindow.getContentView().findViewById(R.id.fav)).setText(isfav>0?"ȡ���ղ�":"�ղ�");
					mPopupWindow.showAsDropDown(view, 400,(int)(-55*2));
					return true;
				}else if( keyCode == KeyEvent.KEYCODE_DPAD_LEFT && listView_fav_view == false )	{//�����
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
				}else if( keyCode == KeyEvent.KEYCODE_DPAD_LEFT && listView_fav_view == true )	{//�����
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
        .setTitle("������")
        .setView(textEntryView)
        .setNegativeButton("ȡ��",
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                    	
                    }

                }) 
        .setPositiveButton("ȷ��",
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
     * �������ݿ�������ݺ��Լ�cursoradapter���ָ��number�ŵ�channel���ݣ�����listview��ʾ������
     * @param:
     * 	cursorloader: �α������
     * 	inputSource��  ��Դ
     * 	number: 	   ��Ŀ��Ӧ��̨��
     * 	name: 		   �û����õ��µ�����
     * 	fav: 		   �û��Ƿ��ղظý�Ŀ
     * */
    private void update(MyCursorLoader cursorloader, int inputSource, int number, String name, int fav){
    	//�������ݿ�
    	cursorLoader.Update(inputSource,
			    			number,
							name,
							fav);
    	//�������������α�����
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
