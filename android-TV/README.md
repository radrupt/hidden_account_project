#android-TV#
*android-TV�´�ŵ�����tcl odm ���������ʱ��������漰���ĵ��ӷ����app*
##demo   
*demo����tcl odm ������չ���ʱ���������*

&emsp;<a href="#child">child</a>:&emsp;&emsp;&ensp;��ͯģʽ&emsp;&emsp;*2014.12.8����2014.12.14*  
&emsp;<a href="#aging">aging</a>:&emsp;&emsp;�ϻ�ģʽ&emsp;&emsp;*2014.12.1����2014.12.13*  
&emsp;<a href="#channel">channel</a>:&emsp;TV��Դ&emsp;&emsp;&ensp;*2014.11.24����2014.11.30*  
&emsp;<a href="#fileview">fileview</a>:&emsp;�ļ����&emsp;&emsp;*2014.11.17����2014.11.23* 
	  
###<a name="child">��ͯģʽ</a>

&ensp;���ܣ�  

	1.״̬���أ��������ƶ�ͯģʽ���ܵĿ����͹ر�   
	2.��ʱ�ۿ����趨ʱ�䣬��ʱ����  
	3.�����޸�  
	4.�������棺��ԭ�еĽ����ڸ�ס  
	5.��������ͯģʽ�Ƿ�����������ڿ���״̬��ֱ������  
	
&ensp;������  

	1.AlarmManager������setRepeating��ʵ�ֶ�ʱ����  
	2.������⣺���������㲥��������service����serviceͨ����ʽintent����������app  

###<a name="aging">�ϻ�ģʽ</a>

&ensp;�Ƚϼ�

###<a name="channel">TV��Դ</a>  
*�õ���һЩTV��صĿ�*  
*�����޵�ʱ�������Ļ��Ǻ����Ի��ģ���������˷������������ʹ����������*  

&ensp;���ܣ�  
	
	1.�ղؽ�Ŀ�Ĺ���  
	2.��ʾ��Ŀ�б�  
	3.�б�֮���л�  

&ensp;������  

	(ListView+PopupWindow)(����)+SimpleCursorAdapter+CursorLoader+SQLiteOpenHelper��  
	
		1.SimpleCursorAdapter������ViewHolder��дnewView��bindView��ʵ��item���Զ��岼�֣�
		ʵ�ֲ��������ݵİ�  
		2.CursorLoader����Ȼ�õ���sqlite��Ҳ����Ȼ�õ����������ݵļ��أ��͸�������ܺ�ʵ����  
		3.SQLiteOpenHelper���и��ӣ�����id����Ϊindex��id��BaseColumnsû��  
		4.ListView+PopupWindow����Ҫ�������¼��ļ�������д����Ȼ��������������ķ�ʽ�������
		�ظ���Ӧ�����⣬���´����е����   
		5.SQLiteOpenHelper��ʹ��execSQLʱ���ܼ����ַ����ľ����÷�б�ܼ���˫����  

###<a name="fileview">�ļ����</a>

&ensp;���ܣ� 

	1.����usb�Ĳ��
	2.��ʾ�ļ�
	
&ensp;������

	1.BroadcastReceiver����̬�㲥�ļ��������ڵײ��usb�ļ����������ò���λ�����²�������û��
	��ʱ���£�����ϵͳ��ȡ���ĵײ�������������ģ�������������ķ�ʽ�����¡�
	2.listview����ʾ�ļ�