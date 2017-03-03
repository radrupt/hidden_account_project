/**
 * Created by wangd933 on 2015/6/2.
 */
require.config({
    "baseUrl"   :   "js/",
    "paths"      :  {
        "angular"   :   "lib/angular",
        "chart"     :   "lib/Chart",
        "angular-chart"   :     "lib/angular-chart"
    },

    "shim"  :   {
        'angular' : {'exports' : 'angular'},
        "angualr-chart"     :   ["angular","chart"]
    },

    deps     :  ['app'],

    // Add angular modules that does not support AMD out of the box, put it in a shim

    urlArgs  :  'bust=0.0.6'
})