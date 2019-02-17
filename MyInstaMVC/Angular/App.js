var App = angular.module('App', ['ngRoute']);

App.controller('MainController', MainController);



configFunction.$inject = ['$routeProvider', '$httpProvider'];

App.config(configFunction)