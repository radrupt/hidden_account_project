var createObject = require('./build/Release/addon');

var obj = createObject();
obj.add("a1","b1","c1");
obj.add("a2","b2","c2");
obj.add("a3","b3","c3");
console.log( obj.get("a1","b1","c1") ); //true
console.log( obj.get("a2","b2","c3") ); // false
