#android-TV#
*android-TV下存放的是在tcl odm 软件所工作时候的所有涉及到的电视方面的app*
##demo   
*demo是在tcl odm 软件所刚工作时候的周任务*

&emsp;<a href="#child">child</a>:&emsp;&emsp;&ensp;儿童模式&emsp;&emsp;*2014.12.8——2014.12.14*  
&emsp;<a href="#aging">aging</a>:&emsp;&emsp;老化模式&emsp;&emsp;*2014.12.1——2014.12.13*  
&emsp;<a href="#channel">channel</a>:&emsp;TV信源&emsp;&emsp;&ensp;*2014.11.24——2014.11.30*  
&emsp;<a href="#fileview">fileview</a>:&emsp;文件浏览&emsp;&emsp;*2014.11.17——2014.11.23* 
	  
###<a name="child">儿童模式</a>

&ensp;功能：  

	1.状态开关：用来控制儿童模式功能的开启和关闭   
	2.限时观看：设定时间，定时锁屏  
	3.密码修改  
	4.锁屏界面：将原有的界面遮盖住  
	5.开机检测儿童模式是否开启，如果处于开启状态，直接锁屏  
	
&ensp;技术：  

	1.AlarmManager：利用setRepeating来实现定时锁屏  
	2.开机检测：监听开机广播，来唤醒service，由service通过隐式intent来激活锁屏app  

###<a name="aging">老化模式</a>

&ensp;比较简单

###<a name="channel">TV信源</a>  
*用到了一些TV相关的库*  
*在有限的时间内做的还是很人性化的，充分利用了方向键，尽量不使用其它按键*  

&ensp;功能：  
	
	1.收藏节目的管理  
	2.显示节目列表  
	3.列表之间切换  

&ensp;技术：  

	(ListView+PopupWindow)(界面)+SimpleCursorAdapter+CursorLoader+SQLiteOpenHelper：  
	
		1.SimpleCursorAdapter：利用ViewHolder重写newView和bindView来实现item的自定义布局，
		实现布局与数据的绑定  
		2.CursorLoader：既然用到了sqlite，也就自然用到了它，数据的加载，就更新需求很好实现了  
		3.SQLiteOpenHelper：有个坑，索引id名设为index或id在BaseColumns没用  
		4.ListView+PopupWindow：主要做的是事件的监听与重写，当然了这里用了猥琐的方式解决按键
		重复响应的问题，导致代码有点恶心   
		5.SQLiteOpenHelper：使用execSQL时，能加上字符串的尽量用反斜杠加上双引号  

###<a name="fileview">文件浏览</a>

&ensp;功能： 

	1.监听usb的插拔
	2.显示文件
	
&ensp;技术：

	1.BroadcastReceiver：动态广播的监听。由于底层对usb的监听监听做得不到位，导致部分数据没有
	及时更新，导致系统获取到的底层数据是有问题的，所以用了猥琐的方式搞了下。
	2.listview：显示文件