package com.example.forgetting;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Rect;
import android.util.AttributeSet;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.widget.ImageView;

public class DrawImageView extends ImageView{
	
	public float left = 50;
	public float top = 50;
	public float right = 250;
	public float bottom = 250;
	
	//边界
	public float right_edge = 0;
	public float bottom_edge = 0;
	
	private float left_deviation;
	private float bottom_deviation;
	private float top_deviation;
	private float right_deviation;
	private boolean left_top = false;
	private boolean left_bottom = false;
	private boolean right_top = false; 
	private boolean right_bottom = false; 
	
	private boolean internal = false;
	private float frontx = 0;        
	private float fronty = 0;
	
	private String tag = "Wangd933";
	
	public DrawImageView(Context context, AttributeSet attrs) {
		super(context, attrs);
		
	}
	
	
	public DrawImageView(Context context){
		super(context);
		this.setOnTouchListener(new OnTouchListener() {
			
			@Override
			public boolean onTouch(View view, MotionEvent event) {
				int pointCount = event.getPointerCount();
				if( event.getAction() == MotionEvent.ACTION_DOWN ){
					Log.d(tag, "DrawImageView onTouch ACTION_DOWN ");
					for( int i = 0; i < pointCount; i++ ){
						float rawx = event.getX(i);
						float rawy = event.getY(i);
						if( Math.abs(rawx - left) < 20 ){
							if( Math.abs(rawy - top) < 20 ){
								left_deviation = left - rawx;
								top_deviation = top - rawy;
								left_top = true;
								Log.d(tag, "DrawImageView onTouch ACTION_DOWN left_top + rawx = " + rawx + " + rawy = " + rawy);
							}
							else if( Math.abs(rawy - bottom) < 20 ){
								left_deviation = left - rawx;
								bottom_deviation = bottom - rawy;
								left_bottom = true;
								Log.d(tag, "DrawImageView onTouch ACTION_DOWN left_bottom + rawx = " + rawx + " + rawy = " + rawy);
							}
						}
						else if( Math.abs(rawx - right) < 20 ){
							if( Math.abs(rawy - top) < 20 ){
								right_deviation = right - rawx;
								top_deviation = top - rawy;
								right_top = true;
								Log.d(tag, "DrawImageView onTouch ACTION_DOWN right_top + rawx = " + rawx + " + rawy = " + rawy);
							}
							else if( Math.abs(rawy - bottom) < 20 ){
								right_deviation = right - rawx;
								bottom_deviation =  bottom - rawy;
								right_bottom = true;
								Log.d(tag, "DrawImageView onTouch ACTION_DOWN right_bottom + rawx = " + rawx + " + rawy = " + rawy);
							}
						}
						else if( rawx - left > 20 && rawx - right < 20 && rawy - top > 20 && rawy - bottom < 20 ){
							internal = true;
							frontx = rawx;
							fronty = rawy;
						}
					}
				}
				else if( event.getAction() == MotionEvent.ACTION_MOVE ){
					Log.d(tag, "DrawImageView onTouch ACTION_MOVE ");
					Log.d(tag, ">>>>>>>>>>>>>>>"+right_edge+
							"<<<"+bottom_edge);
					for( int i = 0; i < pointCount; i++ ){
						float rawx = event.getX(i);
						float rawy = event.getY(i);
						float lefttemp = left;
						float righttemp = right;
						float toptemp = top;
						float bottomtemp = bottom;
						if( internal ){
							left += rawx - frontx;
							top += rawy - fronty;
							right += rawx - frontx;
							bottom += rawy - fronty;
							frontx = rawx;
							fronty = rawy;
						}
						else if( Math.abs(rawx - left) < 20 ){
							if( Math.abs(rawy - top) < 20 ){
								left = rawx + left_deviation;
								top = rawy + top_deviation;
							}
							else if( Math.abs(rawy - bottom) < 20 ){
								left = rawx + left_deviation;
								bottom = rawy + bottom_deviation;
							}
						}
						else if( Math.abs(rawx - right) < 20 ){
							if( Math.abs(rawy - top) < 20 ){
								right = rawx + right_deviation;
								top = rawy + top_deviation;
							}
							else if( Math.abs(rawy - bottom) < 20 ){
								right = rawx + right_deviation ;
								bottom = rawy + bottom_deviation;
							}
						}
						float tem;
						if( left > right ){
							tem = left;
							left = right;
							right = tem;
						}
						if( bottom < top ){
							tem = bottom;
							bottom = top;
							top = tem;
						}
						
						/*if( left < 0 || right > right_edge || top < 0 || bottom > bottom_edge  ){
							left = lefttemp;
							right = righttemp;
							top = toptemp;
							bottom = bottomtemp;
						}*/
					}
				}
				else if( event.getAction() == MotionEvent.ACTION_UP ){
					Log.d(tag, "DrawImageView onTouch ACTION_UP ");
					left_top = false;
					left_bottom = false;
					right_top = false;
					right_bottom = false;
					internal = false;
				}
				invalidate();
				return true;
			}
			
		});
//		this.setBackgroundColor(Color.RED);
	}
	
	
	Paint paint = new Paint();
	{
		paint.setAntiAlias(true);
		paint.setStyle(Paint.Style.FILL);//充满  
		paint.setColor(Color.LTGRAY);  
		paint.setAlpha(50);
		paint.setStrokeWidth(2.5f);//设置线宽
	};
	
	@Override
	protected void onDraw(Canvas canvas) {
		super.onDraw(canvas);
		Log.d(tag, "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<,");
		canvas.drawRect(new Rect((int)left, (int)top, (int)right, (int)bottom), paint);//绘制矩形
		
		if( left_top ){
			Log.d(tag, "onDraw left_top");
			canvas.drawCircle(left, top, 20, paint);
		}
		if( left_bottom ){
			Log.d(tag, "onDraw left_bottom");
			canvas.drawCircle(left, bottom, 20, paint);
		}
		if( right_top ){
			Log.d(tag, "onDraw right_top");
			canvas.drawCircle(right, top, 20, paint);
		}
		if( right_bottom ){
			Log.d(tag, "onDraw right_bottom");
			canvas.drawCircle(right, bottom, 20, paint);
		}
	}
}
