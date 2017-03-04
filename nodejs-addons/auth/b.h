#ifndef B_H
#define B_H
#include <string>
#include <map>
#include "c.h"

using namespace std;

class B {
 public:
    B(string _name):name(_name){}
    B(string bname, string cname);
    ~B();
    string name;
    bool find(string cname);
    void Add(string cname);
 private:
    map<string,C*> values;
};

#endif
