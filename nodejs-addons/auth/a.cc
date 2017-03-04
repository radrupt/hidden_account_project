#include "a.h"

A::A(string aname,string bname, string cname){
    this->name = aname;
    B* b = new B(bname, cname);
    this->values.insert(map<string,B*>::value_type (bname,b));
}

void A::Add(string bname, string cname){
    map<string,B*>::iterator l_it;
    l_it = values.find(bname);
    if( l_it == values.end()){
        B* b = new B(bname, cname);
        values.insert(map<string,B*>::value_type (bname,b));
    }else{
        l_it->second->Add(cname);
    }
}

A::~A(){
    if( !this->values.empty() ){
        this->values.clear();
    }
}

bool A::find(string bname,string cname){
    map<string,B*>::iterator l_it;
    l_it = values.find(bname);
    if( l_it == values.end() ) return false;
    return l_it->second->find(cname);
}
