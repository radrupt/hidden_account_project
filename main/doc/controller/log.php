<?php
/**
 * Created by PhpStorm.
 * User: w.di
 * Date: 2014/12/23
 * Time: 9:47
 */

class log {
    public static function l($content){
        $file1 = fopen(DATA_PATH."log.txt","a");
        fwrite($file1,$content."\n");
        fclose($file1);
        return ;
    }
}