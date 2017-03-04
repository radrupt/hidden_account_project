#ifndef C_H
#define C_H
#include <string>

using namespace std;

class C {
 public:
    C(string _name):name(_name),value(true){}
    ~C();
    string name;
    bool getBool();
 private:
    bool value;
};

#endif
