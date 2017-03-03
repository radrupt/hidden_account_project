package com.example.child;

import com.example.ch.R;

import android.app.Activity;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class LockScreenActivity extends Activity{
	 @Override
	 protected void onCreate(Bundle savedInstanceState) {
		 this.setTheme(getSharedPreferences("cons", MODE_PRIVATE).getInt("theme",android.R.style.Theme_Translucent_NoTitleBar));
		 super.onCreate(savedInstanceState);
		 setContentView(R.layout.lock_screen_layout);
		 PollingUtils.stopPollingService(this, MyService.class, MyService.ACTION);
		 Button unlock_ok = (Button)this.findViewById(R.id.unlock_ok);
		 final EditText pw =  (EditText)this.findViewById(R.id.pw);
		 unlock_ok.setOnClickListener(new OnClickListener(){
			@Override
			public void onClick(View v) { 
			    	if( pw.getText().toString().equals(
			    			"1")){
			    		PollingUtils.startPollingService(LockScreenActivity.this,
			    				LockScreenActivity.this.getSharedPreferences("setting", 0).getLong("time", -1)/60/10,
									    				MyService.class,
									    				MyService.ACTION);
			    		LockScreenActivity.this.finish();
			    	}else{
			    		Toast.makeText(LockScreenActivity.this, "√‹¬Î¥ÌŒÛ,«Î÷ÿ–¬ ‰»Î", Toast.LENGTH_SHORT).show();
			    	}
		    }
		 });
	 }
	 @Override
	 public boolean onKeyDown(int keyCode, KeyEvent event)
     {
             if ( keyCode ==  KeyEvent.KEYCODE_BACK)
             {
            	 Toast.makeText(LockScreenActivity.this, "«Î ‰»Î√‹¬Î", Toast.LENGTH_SHORT).show();
                 return true;
             }
             return super.onKeyDown(keyCode, event);
     }
	 
}
