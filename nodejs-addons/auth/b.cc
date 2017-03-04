#include "b.h"

B::B(string bname, string cname){
    this->name = bname;
    C* c = new C(cname);
    this->values.insert(map<string,C*>::value_type (cname,c));
}

void B::Add(string cname){
    map<string,C*>::iterator l_it;
    l_it = values.find(cname);
    if( l_it == values.end()){
        C* c = new C(cname);
        this->values.insert(map<string,C*>::value_type (cname,c));
    }else{
     //
    }
}
bool B::find(string cname){
    map<string,C*>::iterator l_it;
    l_it = values.find(cname);
    if( l_it == values.end() ) return false;
    return l_it->second->getBool();
}
B::~B(){
    if( !this->values.empty() ){
        this->values.clear();
    }
}
