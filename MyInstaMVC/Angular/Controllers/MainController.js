var MainController = function ($scope) {

    $scope.init = function (loves) {
        $scope.loves = loves;
    }
    
    $scope.myVar = 2;
}

MainController.$inject = ['$scope'];