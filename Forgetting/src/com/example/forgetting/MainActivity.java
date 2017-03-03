package com.example.forgetting;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.opencv.android.BaseLoaderCallback;
import org.opencv.android.LoaderCallbackInterface;
import org.opencv.android.OpenCVLoader;

import android.R.integer;
import android.R.string;
import android.app.Activity;
import android.app.FragmentTransaction;
import android.content.res.Configuration;
import android.graphics.Bitmap;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.os.Environment;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.Gravity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnTouchListener;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.Toast;

public class MainActivity extends Activity 
	implements ForFragment.OnlineSelectedListener{

	private String tag = "Wangd933";
	public static boolean hastakephoto = false;
	public static boolean is_add = false;//add_menu is visible or not
	private int inventory_id ;
	
	
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		Log.d(tag, "MainActivity onCreate start");
		setContentView(R.layout.activity_main);
		
			
		// However, if we're being restored from a previous state,
        // then we don't need to do anything and should return or else
        // we could end up with overlapping fragments.
		if( savedInstanceState != null ){
			//has stopped
			return;
		}
		
		LinearLayout linearLayout = (LinearLayout)findViewById(R.id.fragment_container);
		
		linearLayout.setOnTouchListener(new OnTouchListener() {

			
			@Override
			public boolean onTouch(View view, MotionEvent event) {
				if (event.getAction() == MotionEvent.ACTION_DOWN){
					((LinearLayout)findViewById(R.id.add_inventory_layout)).setVisibility(View.GONE);
					((LinearLayout)findViewById(R.id.add_myobject_layout)).setVisibility(View.GONE);
					is_add = false;
					return true;
				}
				return true;
			}
			
		});
		
		ImageView imageView = (ImageView)findViewById(R.id.imageview);
		imageView.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				if( !hastakephoto ) {
					take_Photo();
					hastakephoto = true;
				}
			}
		});
		
		// Create an instance of ExampleFragment
		ForFragment inventoryFragment = new ForInventoryFragment();
		
		// In case this activity was started with special instructions from an Intent,
        // pass the Intent's extras to the fragment as arguments
		inventoryFragment.setArguments(getIntent().getExtras());
		
		// Add the fragment to the 'fragment_container' FrameLayout
		getFragmentManager().beginTransaction().add(R.id.fragment_container,inventoryFragment,"inventoryfragment").commit();
		Log.d(tag, "MainActivity onCreate end");
		DisplayMetrics dm=new DisplayMetrics();
        super.getWindowManager().getDefaultDisplay().getMetrics(dm);
        String strOpt="手机屏幕分辨率为："+dm.widthPixels+"*"+dm.heightPixels;
        Log.d(tag, strOpt);
	}
	
	@Override
	public void onSelected(int inventory_id) {
		
		//这边还可以做些事情来限定myobject的fragment数量过多.........................
		
		if( is_add ) {
			((LinearLayout)findViewById(R.id.add_inventory_layout)).setVisibility(View.GONE);
			((LinearLayout)findViewById(R.id.add_myobject_layout)).setVisibility(View.GONE);
			is_add = false;
			return;
		}
		
		this.inventory_id = inventory_id;
		Log.d("Wangd933", "MainActivity onSelected start");
		ForMyObjectFragment myObjectFragment = null;
		if( (getFragmentManager().findFragmentByTag("myobjectfragment"+inventory_id)) != null){
			 myObjectFragment = ((ForMyObjectFragment)(getFragmentManager().findFragmentByTag("myobjectfragment"+inventory_id)));
			Log.d("Wangd933", ">>>>>>>>>>>>>>>>>>>>>>>>>>>>");
		}
		else {
			myObjectFragment = new ForMyObjectFragment();
			Bundle bundle = new Bundle();
			bundle.putInt("inventory_id", inventory_id);
			myObjectFragment.setArguments(bundle);
		}
		FragmentTransaction transaction = getFragmentManager().beginTransaction();
		transaction = transaction.replace(R.id.fragment_container, myObjectFragment,"myobjectfragment"+inventory_id);
		transaction.addToBackStack(null);
		transaction.commit();
		if( (getFragmentManager().findFragmentByTag("myobjectfragment"+inventory_id)) != null){
			Log.d(tag, "myobjectfragment"+inventory_id);
		}
	}
	
	public void ok_Button(View view){
		Log.d(tag, "MainACtivity add_boutton() start " );	
		
		if( getFragmentManager().getBackStackEntryCount() == 0) {//此时实在inventory页面
			EditText editText = (EditText)((LinearLayout)findViewById(R.id.add_inventory_layout)).findViewById(R.id.newinfoinventory);
			if( editText.getText().toString().equals("") ) {
				
				Toast toast = Toast.makeText(getApplicationContext(), "未填写内容",Toast.LENGTH_SHORT);
				toast.setGravity(Gravity.CENTER, 0, -200);
				toast.show();
				return;
			}
			((ForFragment)(getFragmentManager().
					findFragmentByTag("inventoryfragment"))).add_item(editText.getText().toString());
			((LinearLayout)findViewById(R.id.add_inventory_layout)).setVisibility(View.GONE);
			editText.setText(null);
		}
		else if( getFragmentManager().getBackStackEntryCount() == 1){
			EditText editText = (EditText)((LinearLayout)findViewById(R.id.add_myobject_layout)).findViewById(R.id.newinfomyobject);
//			if( editText.getText().toString().equals("") ) {//至少文本内容得填写
//				Toast toast = Toast.makeText(getApplicationContext(), "未填写内容",Toast.LENGTH_SHORT);
//				toast.setGravity(Gravity.CENTER, 0, -200);
//				toast.show();
//				return;
//			}
			
			ImageView imageView = (ImageView)((LinearLayout)findViewById(R.id.add_myobject_layout)).findViewById(R.id.imageview);
			
			if(imageView.getDrawable().getConstantState().equals(getResources().getDrawable(R.drawable.camera).getConstantState())){
				//暂时设置为必须拍照,虽然已有接口允许不拍照
				/*((ForFragment)(getFragmentManager().
						findFragmentByTag("myobjectfragment"+inventory_id))).add_item(editText.getText().toString());*/
				Toast.makeText(getApplicationContext(), "未拍照",Toast.LENGTH_SHORT).show();
				return;
			}
			else{
				String imagepath = (String)imageView.getTag();
				Log.d("wangd933+dsfsdfsd", imagepath);
				//这里的图片就是要分割的，分割之后处理得到的string，获得各个图片加入数据库
				String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss").format(new Date());
				String allSplitedImage = ImageProc.grayProc(imagepath,
						Environment.getExternalStoragePublicDirectory( 
		            			Environment.DIRECTORY_PICTURES).getPath() + File.separator + "IMAGE_" + timeStamp);
				String paths[] = Tools.splitStringToPath(allSplitedImage);
				
				for( int i = 0; i < paths.length; i++ ) {
					Bitmap bitmap =Tools.Thumbnail(paths[i], 44, 60);  
					final ByteArrayOutputStream os = new ByteArrayOutputStream();
					bitmap.compress(Bitmap.CompressFormat.PNG, 100, os);  
					
					((ForFragment)(getFragmentManager().findFragmentByTag("myobjectfragment"+inventory_id)))
						.add_item(editText.getText().toString(),paths[i], os.toByteArray());
				}
			}
		    
		    ((LinearLayout)findViewById(R.id.add_myobject_layout)).setVisibility(View.GONE);
		    editText.setText(null);
		    Drawable drawable = getResources().getDrawable(R.drawable.camera);
		    imageView.setImageDrawable(drawable);
		}
    	Log.d(tag, "MainACtivity add_boutton() end");
    	
	}
	
	public void take_Photo(){
		Log.d(tag, "take_Photo start");
		AndroidCameraFragment androidCameraFragment = new AndroidCameraFragment();
		if( androidCameraFragment != null){
			FragmentTransaction transaction = getFragmentManager().beginTransaction();
			transaction.replace(R.id.fragment_container, androidCameraFragment,"androidCameraFragment");
			transaction.addToBackStack(null);
			transaction.commit();
			Log.d("Wangd933", ">>>>>>>>>>>>>>>>>>>>>>>>>>>>");
		}
		
	}
	
	/* Menu part start*/
	public void add_MenuItem(MenuItem menu){
		this.is_add = true;
		if( getFragmentManager().getBackStackEntryCount() == 0) {
			((LinearLayout)findViewById(R.id.add_inventory_layout)).setVisibility(View.VISIBLE);
		}
		else if( getFragmentManager().getBackStackEntryCount() == 1){
			    ((LinearLayout)findViewById(R.id.add_myobject_layout)).setVisibility(View.VISIBLE);
		}
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}
	/* Menu part end*/

	@Override 
    public void onConfigurationChanged(Configuration config) { 
		super.onConfigurationChanged(config); 
    } 
	
	@Override
	public void onBackPressed(){
		super.onBackPressed();
		this.is_add = false;
		((LinearLayout)findViewById(R.id.add_inventory_layout)).setVisibility(View.GONE);
		((LinearLayout)findViewById(R.id.add_myobject_layout)).setVisibility(View.GONE);
	}
	
	private BaseLoaderCallback  mLoaderCallback = new BaseLoaderCallback(this) {  
        @Override  
        public void onManagerConnected(int status) {  
            switch (status) {  
                case LoaderCallbackInterface.SUCCESS:{  
                    System.loadLibrary("image_proc");  
                } break;  
                default:{  
                    super.onManagerConnected(status);  
                } break;  
            }  
        }  
    }; 
    
    @Override  
    public void onResume(){  
        super.onResume();  
        //通过OpenCV引擎服务加载并初始化OpenCV类库，所谓OpenCV引擎服务即是  
        //OpenCV_2.4.3.2_Manager_2.4_*.apk程序包，存在于OpenCV安装包的apk目录中  
        OpenCVLoader.initAsync(OpenCVLoader.OPENCV_VERSION_2_4_3, this, mLoaderCallback);  
    }
	
}


