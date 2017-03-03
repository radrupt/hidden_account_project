/**
 * Created by wangd on 2016/3/11.
 */
import {Component,View,Input,ElementRef} from 'angular2/core';

@Component({
    selector: 'search-form-date',
    templateUrl:'search-form-date.component.html',
    styleUrls:['search-form-date.component.css'],
    moduleId:__moduleName,
})
/*
* input:data:{value:xxx}
* output:data:{value:Date}
* */
export class SearchFormDateComponent{
    @Input('data') data: Object;
    public keyIdentifiers=['U+00BD','U+0030','U+0031','U+0032','U+0033','U+0034','U+0035','U+0036','U+0037','U+0038','U+0039','U+0008'];//-,0-9,enter
    public yearlist:Array = [];
    public monthlist:Array = [1,2,3,4,5,6,7,8,9,10,11,12];
    public datelist:Array = [];
    public inputfocus:boolean = false;
    public timepickfocus:boolean = false;
    public selectyearfocus:boolean = false;
    public selectmonthfocus:boolean = false;
    public yearstart:any = 2015;
    public yearend:any = 2017;
    public inputerr:boolean = false;
    private _yearcurrent:any = this.yearstart;
    private _monthcurrent:any = 1;
    private _daycurrent:any = 1;
    private _timecurrent:any = null;
    constructor(
        _elementRef: ElementRef
    ){
        //_elementRef.nativeElement.style.left=300;
        //_elementRef.nativeElement.style.top=600;
    }
    set timecurrent(value){
        this._timecurrent = value;
        this.yearcurrent = value.getFullYear();
        this.monthcurrent = value.getMonth()+1;
        this.daycurrent = value.getDate();
    }
    get timecurrent(){
        return this._timecurrent;
    }
    set yearcurrent(value){
        this._yearcurrent = value;
        this.makeTable();
    };
    get yearcurrent(){
        return this._yearcurrent;
    }
    set monthcurrent(value){
        this._monthcurrent = value;
        this.makeTable();
    };
    get monthcurrent(){
        return this._monthcurrent;
    }
    set daycurrent(value){
        this._daycurrent = value;
    };
    get daycurrent(){
        return this._daycurrent;
    }
    get showTimePicker(){
        return this.inputfocus||this.timepickfocus||this.selectyearfocus||this.selectmonthfocus;
    }
    set choosedate(value){
        var res = this.checkDateTime(value+'');
        if(!res){
            //alert('日期格式错误');return;
        }else this.timecurrent = res;
    }
    get choosedate(){
        return this.timecurrent.getFullYear()+'-'+(this.timecurrent.getMonth()+1)+'-'+this.timecurrent.getDate();
    }
    ngOnInit() {
        this.initYearList(this.yearstart,this.yearend);
        this._getYearMonthDate(this.initCurrentTime(this.data.value));
        this.timecurrent = new Date(this.yearcurrent,this.monthcurrent-1,this.daycurrent);
        this.data.value = this.timecurrent;
    }
    choose(){
        console.log(this.timepickfocus)
    }
    leave(key){
        var self = this;
        setTimeout(function(){
            self[key] = false
        },100)
    }
    prevMonth(){
        var time = new Date(this.yearcurrent,this.monthcurrent-1-1,this.daycurrent);
        for(var i = 1; i <= 3; i++){
            if(time.getMonth() == this.monthcurrent-1-1) break;
            time = new Date(this.yearcurrent,this.monthcurrent-1-1,this.daycurrent-i);
        }
        if(time.getFullYear()<this.yearstart)return;
        this._getYearMonthDate(time);
    }
    nextMonth(){
        var time = new Date(this.yearcurrent,this.monthcurrent-1+1,this.daycurrent);
        for(var i = 1; i <= 3; i++){
            if(time.getMonth() == this.monthcurrent-1+1) break;
            time = new Date(this.yearcurrent,this.monthcurrent-1+1,this.daycurrent-i);
        }
        if(time.getFullYear()>this.yearend)return;
        this._getYearMonthDate(time);
    }
    dateClick(day){
        this.daycurrent = day;
        this.timecurrent = new Date(this.yearcurrent,this.monthcurrent-1,this.daycurrent);
        this.inputfocus=this.timepickfocus=this.selectyearfocus=this.selectmonthfocus=false;
        this.data.value = this.timecurrent;
    }
    limitInput(event:any){
        var res = this.keyIdentifiers.find(function(value, index, arr) {
            return value == event.keyIdentifier;
        });
        if(!res){
            event.returnValue =false
        }
    }
    private initYearList(start,end){
        for(var v = start; v <= end; v++)
        this.yearlist.push(v);
    }
    private initCurrentTime(value){
        var time = null;
        try{
            time = new Date(value);//data.value format is unixtamp
            if ( isNaN( time.getTime() ) ) time = new Date();
        }catch (e){
            time = new Date();
        }
        return time;
    }
    private _getYearMonthDate(time){
        this.yearcurrent = time.getFullYear();
        this.monthcurrent = time.getMonth()+1;
        this.daycurrent = time.getDate();
        if(this.yearcurrent > this.yearend) this.yearcurrent = this.yearend;
        if(this.yearcurrent < this.yearstart) this.yearcurrent = this.yearstart;
        this.makeTable();
    }
    private makeTable(){
        var firstdayweek = new Date(this.yearcurrent,this.monthcurrent-1,this.daycurrent).getDay();//first day
        firstdayweek = firstdayweek || 7;//sunday:0 to 7
        var maxday = this.getDaysInOneMonth(this.yearcurrent,this.monthcurrent-1);//
        this.datelist = [];//clear datelist
        for(var v = 1; v < firstdayweek; v++)this.datelist.push(0);
        for(var v = 1; v <= maxday;v++)  this.datelist.push(v);
        var currlength = this.datelist.length;
        for(var v = currlength+1; v <= 5*7; v++ ) this.datelist.push(0);
    }
    private getDaysInOneMonth(year,month){
        month = parseInt(month, 10);
        var d= new Date(year, month, 0);
        return d.getDate();
    }
    private checkDateTime(str){
        str = str.replace(/\s+/g,'');
        var reg = /^(\d+)-(\d{1,2})-(\d{1,2})$/;
        var r = str.match(reg);
        if(r==null)return false;
        r[2]=r[2]-1;
        var d= new Date(r[1], r[2],r[3]);
        if(d.getFullYear()!=r[1])return false;
        if(d.getMonth()!=r[2])return false;
        if(d.getDate()!=r[3])return false;
        return d;
    }
}