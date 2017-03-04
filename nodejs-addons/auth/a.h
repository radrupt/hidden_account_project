#ifndef A_H
#define A_H
#include <string>
#include <map>
#include "b.h"

using namespace std;

class A {
 public:
    A(string _name):name(_name){}
    A(string aname,string bname, string cname);
    ~A();
    string name;
    void Add(string bname, string cname);
    bool find(string bname, string cname);
 private:
    map<string,B*> values;
};

#endif
