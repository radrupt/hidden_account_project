main
====
##软件所网站索引页  
包含：<a href="#doc">文档管理(doc)</a>，<a href="#swd">信息发布(swd)</a>    

##<a name="doc">文档管理</a>  
	
&emsp;基于：<a href="https://github.com/kalcaddle/KODExplorer">KodExplore</a>&emsp;v2.73  by 雾渺们  
&emsp;删除添加了一些功能，来满足所内的需求  

	与原版的区别(很细)：  
		1.语言——中文唯一  
		2.非管理员没有自己的空间(home)  
		3.非管理员，默认进入public/  文档/android/目录下,contriller/explorer.class.php
		4.新加用户的配置文件里，文件的显示方式变为了list(原来是icon),config/setting.php
		5.添加对公共目录下文件夹里文件的统计功能
		6.对于列表显示方式增加了几个字段如作者，编号
		...待添加



##<a name="swd">信息发布</a>  

&emsp;基于：wordpress  

	做了些特殊处理  
		1.网站首页，如果某篇文章的开始是图片的话，则处理成点击图片直接进入该篇文章，twentytwelve/content.php。

