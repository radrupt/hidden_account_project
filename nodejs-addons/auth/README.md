执行node-gyp rebuild

执行node test.js
# nodejs-addons

	test.js:
    	reateObject = require('./build/Release/addon');
    	var obj = createObject();
    	obj.add("a1","b1","f1");
    	obj.add("a2","b2","f2");
    	obj.add("a3","b3","f3");
    	console.log( obj.get("a1","b1","f1") ); //true
    	console.log( obj.get("a2","b2","f3") ); // false
