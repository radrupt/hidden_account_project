
var currentplace=[0,0];
var scal = 1;
var rota = 1;
var winWidth = 0; 
var winHeight = 0; 
var step = 1;
var isWebkit = /(webkit)[ \/]([\w.]+)/ig.test(navigator.userAgent);
var devicePixelRatio = window.devicePixelRatio || 1;
var clearIntervalFun = null;

function getWinWidth(){ return winWidth; }	
function getWinHeight(){ return winHeight; }		
function findDimensions(context) //函数：获取尺寸 
{ 
	
	//获取窗口宽度 
	if (window.innerWidth) 
		winWidth = window.innerWidth; 
	else if ((document.body) && (document.body.clientWidth)) 
		winWidth = document.body.clientWidth; 
	//获取窗口高度 
	if (window.innerHeight) 
		winHeight = window.innerHeight; 
	else if ((document.body) && (document.body.clientHeight)) 
		winHeight = document.body.clientHeight; 
	//通过深入Document内部对body进行检测，获取窗口大小 
	
	if (document.documentElement && document.documentElement.clientHeight && document.documentElement.clientWidth) 
	{ 
		winHeight = document.documentElement.clientHeight; 
		winWidth = document.documentElement.clientWidth; 
	} 
	context = getContext();
	
	if(!context) {
		alert('请使用webkit内核浏览器！');
		return;
	}
	clearInterval(clearIntervalFun);
	context.translate(winWidth/2,winHeight/2);
	context.scale(0.5,0.5);
	
	clearIntervalFun = setInterval(function(){track_control(context)}, 100); 	
} 
		
//Track control
function track_control(context){
		
		context.clearRect(-winWidth, -winHeight, winWidth*2, winHeight*2);
		context.save();
		context.translate(currentplace[0]-winWidth+200,currentplace[1]-winHeight+200);
		context.rotate(rota*Math.PI/180);
		draw_maple(context);
		context.restore();
		
		scal++;
		if( currentplace[0] >= winWidth || currentplace[1] == winHeight ){
			currentplace[0] =0;
			currentplace[1] = 0;
		}
			//clearInterval(clearIntervalFun);
		
		rota+=1;
		rota%=360;
		//compute next place

		currentplace[0]+=step;
		currentplace[1]+=step;
}	

//draw maple Edge
function draw_Polyline( pointarr, topplace, context )
{
	var height = 180;
	if( pointarr.length / 2 == 1 ) {
		alert("数组不符合规范");
		return;
	}
	context.lineWidth = 2;

	context.moveTo(topplace[0], topplace[1] - height);
	for( var i = 0; i < pointarr.length; i=i+2 ){
		context.lineTo(topplace[0]+pointarr[i],topplace[1]+pointarr[i+1] - height);
		context.stroke();
	}
	context.quadraticCurveTo(topplace[0] - 35,topplace[1]+120 - height, 
							 topplace[0] - 10,topplace[1]+180 - height);	
	context.stroke();
	context.moveTo(topplace[0], topplace[1] - height);
	for( var i = 0; i < pointarr.length; i=i+2 ){
		context.lineTo(topplace[0]-pointarr[i],topplace[1]+pointarr[i+1] - height);
		context.stroke();
	}
	context.quadraticCurveTo(topplace[0] + 35,topplace[1]+120 - height, 
							 topplace[0] + 10,topplace[1]+180 - height);	
	context.stroke();
	
}
// draw maple
function draw_maple(context)
{
	var arr=[-6, 20, -9, 16,
				 -14, 35, -17, 30,
				 -19, 50, -22, 45,
				 -23, 65, -26, 60,
				 -26, 80, -29, 75,
				 -28, 95, -31, 90];
				 
	var sca=[1.0,0.8,0.8,0.6,0.6,0.4,0.4];
	var rota=[0,40,-40,80,-80,120,-120];
	var maple_y=[0,0,0,15,15,25,25];
	var maple_x=[0,20,-20,20,-20,20,-20];
	var rootplace=[0,0];
	
	context.beginPath();	
	for( var i = 0; i < 7; i++ ){
		context.save();
		
		context.translate(maple_x[i],maple_y[i]);
		context.scale(sca[i],sca[i]);
		context.rotate(Math.PI*2/360*rota[i]);
		draw_Polyline(arr, rootplace, context);
		context.restore();
	}
	
	context.strokeStyle = "rgb(111,0,0)";
	context.moveTo(0,20);
	context.lineTo(0,140);
	context.stroke();
	context.closePath();
}

function getContext() {
	var context;
	if(isWebkit && document.getCSSCanvasContext) {
		context = document.getCSSCanvasContext("2d", "calendar", winWidth * devicePixelRatio, winHeight * devicePixelRatio);
	} else {
		return false;
	}
	return context;
};