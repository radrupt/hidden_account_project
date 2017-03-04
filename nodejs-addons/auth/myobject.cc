#include <node.h>
#include "myobject.h"

using namespace v8;

Persistent<Function> MyObject::constructor;

MyObject::MyObject(){

}

MyObject::~MyObject() {
	if( !this->values.empty() ){
		this->values.clear();
	}
}

void MyObject::Init() {
  Isolate* isolate = Isolate::GetCurrent();
  // Prepare constructor template
  Local<FunctionTemplate> tpl = FunctionTemplate::New(isolate, New);
  tpl->SetClassName(String::NewFromUtf8(isolate, "MyObject"));
  tpl->InstanceTemplate()->SetInternalFieldCount(1);

  // Prototype
  NODE_SET_PROTOTYPE_METHOD(tpl, "add", Add);
  NODE_SET_PROTOTYPE_METHOD(tpl, "get", Get);

  constructor.Reset(isolate, tpl->GetFunction());
}

void MyObject::New(const FunctionCallbackInfo<Value>& args) {
  Isolate* isolate = Isolate::GetCurrent();
  HandleScope scope(isolate);

  if (args.IsConstructCall()) {
    // Invoked as constructor: `new MyObject(...)`
    MyObject* obj = new MyObject();
    obj->Wrap(args.This());
    args.GetReturnValue().Set(args.This());
  } else {
    // Invoked as plain function `MyObject(...)`, turn into construct call.
    const int argc = 1;
    Local<Value> argv[argc] = { args[0] };
    Local<Function> cons = Local<Function>::New(isolate, constructor);
    args.GetReturnValue().Set(cons->NewInstance(argc, argv));
  }
}

void MyObject::NewInstance(const FunctionCallbackInfo<Value>& args) {
  Isolate* isolate = Isolate::GetCurrent();
  HandleScope scope(isolate);

  const unsigned argc = 1;
  Handle<Value> argv[argc] = { args[0] };
  Local<Function> cons = Local<Function>::New(isolate, constructor);
  Local<Object> instance = cons->NewInstance(argc, argv);

  args.GetReturnValue().Set(instance);
}

void MyObject::Add(const FunctionCallbackInfo<Value>& args) {
  Isolate* isolate = Isolate::GetCurrent();
  HandleScope scope(isolate);

  MyObject* obj = ObjectWrap::Unwrap<MyObject>(args.Holder());
  //obj->values.push_back(Local<A>::new Aargs[0]->ToString());
  v8::String::Utf8Value param1(args[0]->ToString());
  string s1 = std::string(*param1);
  v8::String::Utf8Value param2(args[1]->ToString());
  string s2 = std::string(*param2);
  v8::String::Utf8Value param3(args[2]->ToString());
  string s3 = std::string(*param3);

  map<string,A*>::iterator l_it;
  l_it = obj->values.find(s1);
  if( l_it == obj->values.end()){
      A* a = new A(s1,s2,s3);
      obj->values.insert(map<string,A*>::value_type (a->name,a));
  }else{
      l_it->second->Add(s2,s3);
  }



  //args.GetReturnValue().Set(Number::New(isolate, obj->values.size()-1));
  //args.GetReturnValue().Set(args[0]);

  //args.GetReturnValue().Set(String::NewFromUtf8(isolate,obj->values[obj->values.size()-1].name.c_str()));


}
bool MyObject::find(MyObject* obj,string aname,string bname,string cname){
    map<string,A*>::iterator l_it;
    l_it = obj->values.find(aname);
    if( l_it == obj->values.end() ) return false;
    return l_it->second->find(bname,cname);
}
void MyObject::Get(const FunctionCallbackInfo<Value>& args) {
    Isolate* isolate = Isolate::GetCurrent();
    HandleScope scope(isolate);
    v8::String::Utf8Value param1(args[0]->ToString());
    string s1 = std::string(*param1);
    v8::String::Utf8Value param2(args[1]->ToString());
    string s2 = std::string(*param2);
    v8::String::Utf8Value param3(args[2]->ToString());
    string s3 = std::string(*param3);
    MyObject* obj = ObjectWrap::Unwrap<MyObject>(args.Holder());
    bool caplitity = obj->find(obj,s1,s2,s3);

    args.GetReturnValue().Set(Boolean::New(isolate,caplitity));
}

