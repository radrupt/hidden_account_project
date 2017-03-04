#ifndef MYOBJECT_H
#define MYOBJECT_H

#include <node.h>
#include <node_object_wrap.h>
#include <map>
#include "a.h"
#include "b.h"
#include "c.h"

using namespace std;

class MyObject : public node::ObjectWrap {
 public:
  static void Init();
  static void NewInstance(const v8::FunctionCallbackInfo<v8::Value>& args);

 private:
  explicit MyObject();
  ~MyObject();

  static void New(const v8::FunctionCallbackInfo<v8::Value>& args);
  static void Add(const v8::FunctionCallbackInfo<v8::Value>& args);
  static void Get(const v8::FunctionCallbackInfo<v8::Value>& args);
  static v8::Persistent<v8::Function> constructor;
  map<string,A*> values;
  static bool find(MyObject* obj,string aname,string bname,string cname);

};

#endif
