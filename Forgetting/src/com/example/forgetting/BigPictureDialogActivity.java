package com.example.forgetting;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.view.Window;
import android.widget.EditText;
import android.widget.ImageView;

public class BigPictureDialogActivity extends Activity {

	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        this.requestWindowFeature(Window.FEATURE_NO_TITLE);  
        Bundle bundle = getIntent().getExtras();
        String imagepath  = bundle.getString(MyImageView.imagepath);
        Bitmap bitmap = BitmapFactory.decodeFile(imagepath);
        ImageView imageView = new ImageView(this);
        EditText editText = new EditText(this);
        setContentView(imageView);
        imageView.setImageBitmap(bitmap);
    }
	
}
