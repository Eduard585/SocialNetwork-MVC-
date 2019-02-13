var App = angular.module('App', ['ngRoute']);

App.controller('MainController', MainController);
App.controller('GridController', GridController);

var configFunction = function ($routeProvider, $httpProvider) {
    $routeProvider.
        when('/grid', {
            templateUrl: 'Angular/Views/Grid.html',
            controller: GridController
        })
        .otherwise({
            redirectTo: function () {
                return '/grid';
            }
        });
}
configFunction.$inject = ['$routeProvider', '$httpProvider'];

App.config(configFunction)