package com.example.forgetting;

import java.lang.ref.WeakReference;
import java.util.Date;

import android.R.integer;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.app.ListFragment;
import android.app.LoaderManager;
import android.content.ContentValues;
import android.content.Context;
import android.content.CursorLoader;
import android.content.DialogInterface;
import android.content.Loader;
import android.database.Cursor;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.view.animation.Animation;
import android.view.animation.Animation.AnimationListener;
import android.view.animation.AnimationUtils;
import android.widget.CursorAdapter;
import android.widget.EditText;
import android.widget.ListView;

public abstract class ForFragment extends ListFragment implements  LoaderManager.LoaderCallbacks<Cursor> {
	protected OnlineSelectedListener mCallback;
	
	public interface OnlineSelectedListener {
        /** Called by ForListFragment when a list item is selected */
        public void onSelected(int inventory_id);
    }
	
	//Called when a fragment is first attached to its activity.
  	@Override
  	public void onAttach(Activity activity){
  		super.onAttach(activity);
  		// This makes sure that the container activity has implemented
          // the callback interface. If not, it throws an exception.
          try {
              mCallback = (OnlineSelectedListener) activity;
          } catch (ClassCastException e) {
              throw new ClassCastException(activity.toString()
                      + " must implement OnHeadlineSelectedListener");
          }
  	}
	
	protected int inventory_id = -1;
	private String tag = "Wangd933";
	protected boolean istouch = false;	//是否接触到ListView时 
	protected MyCursorLoader cursorLoader = null;
	protected CursorAdapter cursorAdapter = null;
	private ForFragment forFragment;
	
	@Override
	public void onActivityCreated(Bundle savedInstanceState){
		super.onActivityCreated(savedInstanceState);
		Log.d(tag, "ForFragment onActivityCreated start");
		 //load data from database in backgroud.this word very important.
        getLoaderManager().initLoader(0, null, this);
		cursorAdapter = cursorLoader.GetCursorAdapter(cursorLoader.Query());
		setListAdapter(cursorAdapter);
		getListView().setOnTouchListener(new OnTouchListener() {
        	float x, y, upx, upy;
        	String tag = "Wangd933";
        	Date d1 ;
            @Override
            public boolean onTouch(View view, MotionEvent event) {
            	
            	Log.d(tag, "setOnTouchListener start");
            	if (event.getAction() == MotionEvent.ACTION_UP) {  //离开ListView时
            		Log.d(tag, "setOnTouchListener start ACTION_UP: "+ 1);
            		upx = event.getX();  
                    upy = event.getY();
                    int position1 = ((ListView) view).pointToPosition((int) x, (int) y);  
                    int position2 = ((ListView) view).pointToPosition((int) upx,(int) upy);
                    if( Math.abs(x - upx )< 10 ) istouch = false;
                    if (position1 == position2 && (x - upx) < -300 ) { 
                    	View v = ((ListView) view).getChildAt(position1);
                    	Cursor cursor=(Cursor)getListView().getItemAtPosition(position1);
                    	int nameColumnIndex = cursor.getColumnIndex("_id");
                		int _id = cursor.getInt(nameColumnIndex);
                		Log.d(tag, "ForFragment onActivityCreated onTouch^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                    	remove_list_item(v,_id);
                    }
                    else{
                        Date d2 = new Date(System.currentTimeMillis());
                        long diff = d2.getTime() - d1.getTime();
                        if( diff > 1000 ){
                        	View v = ((ListView) view).getChildAt(position1);
                        	Cursor cursor=(Cursor)getListView().getItemAtPosition(position1);
                        	int nameColumnIndex = cursor.getColumnIndex("_id");
                    		int _id = cursor.getInt(nameColumnIndex);
                        	showDialog(_id);
                        	return true;
                        }
                    }
                    
                } 
            	else if (event.getAction() == MotionEvent.ACTION_DOWN) {  //接触到ListView时 
            		d1 = new Date(System.currentTimeMillis());//获取当前时间
            		istouch = true;
            		Log.d(tag, "setOnTouchListener start ACTION_DOWN");
                	x = event.getX();  
                    y = event.getY(); 
                }  
            	else if(event.getAction() == MotionEvent.ACTION_MOVE){	  //用来判断用户是否长按某一项
            		
            		upx = event.getX();  
                    upy = event.getY();
            	}
            	Log.d(tag, "setOnTouchListener end");
            	return false;  
             }			
            
       });
        Log.d(tag, "ForFragment onActivityCreated end");
        this.forFragment = this;
	}
	 /**
	   * 使用静态的内部类，不会持有当前对象的引用
	   */
	private static class MyHandler extends Handler {
	    private final WeakReference<ForFragment> wReferenceforFragment;

	    public MyHandler(ForFragment forFragment) {
	    	wReferenceforFragment = new WeakReference<ForFragment>(forFragment);
	    }

	    @Override
	    public void handleMessage(Message msg) {
	    	ForFragment forFragment = wReferenceforFragment.get();
	        if (forFragment != null) {
	        	forFragment.Update();
	        }
	    }
	  }
	protected final MyHandler handler = new MyHandler(this);
	
    @Override
	public void onCreate(Bundle savedInstanceState){
		super.onCreate(savedInstanceState);
		Log.d(tag, "ForFragment onCreate ");
    }
    
    abstract public void add_item(String text);
    
    abstract public void add_item(String text, String path);
    
    abstract public void add_item(String text, String path, byte[] thumbbail);
    
    abstract public void updateItem(int _id, String content);
    
    public void remove_list_item(View view, final int _id){
    	final Animation animation = (Animation) AnimationUtils.loadAnimation(view.getContext(), R.anim.item_anim);  
        animation.setAnimationListener(new AnimationListener() {  
            public void onAnimationStart(Animation animation) {
            	//要不开一个线程做这件事
            	cursorLoader.Delete(_id, null, null);
            }  
  
            public void onAnimationRepeat(Animation animation) {}  
  
            public void onAnimationEnd(Animation animation) {  
            	Log.d("Wangd933", "remove_list_item");
            	
            }  
        });  
          
        Log.d("Wangd933", "ForFragment remove_list_item start^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
        view.startAnimation(animation); 
        Log.d("Wangd933", "ForFragment remove_list_item end^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
    }
    
    //let subclass to real it.initialize cursorloader
    //return cursorLoader = new ForInventoryFragment(getAppliationContext,handle)
	abstract protected CursorLoader init();
	
	abstract protected void Update();
	
	
	
	@Override
	public Loader<Cursor> onCreateLoader(int id, Bundle args) {
		//运行时机
		Log.d("Wangd933", "ForFragment onCreateLoader");
		return init();
	}

	@Override
	public void onLoadFinished(Loader<Cursor> loader, Cursor cursor) {
		Log.d(tag, "ForFragment onLoadFinished");
		cursorAdapter.swapCursor(cursor);
	}

	@Override
	public void onLoaderReset(Loader<Cursor> arg0) {
		if( cursorAdapter != null )
			cursorAdapter.swapCursor(null);
	}
	private EditText editText;
	private void showDialog(final int _id){
		AlertDialog.Builder builder;  
		AlertDialog alertDialog;   
		Context mContext = this.getActivity() ; //mContext不可用
		builder = new AlertDialog.Builder(mContext); 
		
		builder.setTitle("请输入名字");
		editText = new EditText(this.getActivity());
		
		builder.setView(editText);
		builder.setPositiveButton("确定", new DialogInterface.OnClickListener() {
			
			@Override
			public void onClick(DialogInterface arg0, int arg1) {
				// TODO Auto-generated method stub
				String content = editText.getText().toString();
				forFragment.updateItem(_id, content);
			}
		});
		builder.setNegativeButton("取消", null);
		//builder.setView(view);  
		alertDialog = builder.create(); 
		alertDialog.show();
	}

}
