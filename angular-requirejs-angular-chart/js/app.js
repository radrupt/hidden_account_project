/**
 * Created by wangd933 on 2015/6/2.
 */
define(['angular-chart'],function(){

    var app = angular.module('app',['chart.js']);
    app.config(['ChartJsProvider', function(ChartJsProvider) {
        ChartJsProvider.setOptions({
            colours: ['#FF5252', '#FF8A80'],
            responsive: false
        });
        // Configure all line charts
        ChartJsProvider.setOptions('Line', {
            datasetFill: false
        });

    }])
    app.run(['$rootScope', '$timeout',function($rootScope, $timeout){

        function ng_chart(){
            $rootScope.labels = ["January", "February", "March", "April", "May", "June", "July"];
            $rootScope.series = ['Series A', 'Series B'];
            $rootScope.data = [
                [65, 59, 80, 81, 56, 55, 40],
                [28, 48, 40, 19, 86, 27, 90]
            ];
            $rootScope.onClick = function (points, evt) {
                console.log(points, evt);
            };

            // Simulate async data update
            $timeout(function () {
                $scope.data = [
                    [28, 48, 40, 19, 86, 27, 90],
                    [65, 59, 80, 81, 56, 55, 40]
                ];
            }, 3000);
        }
        ng_chart();
    }]);
    angular.bootstrap(document, ['app']);
    return app;
})