
var MainController = function ($scope, $http) {

    $scope.init = function (loves) {

        $scope.loves = loves;
        
        
    }

    $scope.CreateLoves = function (loves) {
        $http.post('/Home/CreateLoves').
            then(function success(response) {
                $scope.AddLoves(response.data.Result);
                console.log("Все ок");
            }, function error(response) {
                console.log("Возникла ошибка");
            }
            );
    }

    $scope.AddLoves = function (data) {
        $scope.loves.push(data);
    }
   
    
    $scope.myVar = 2;
}

MainController.$inject = ['$scope','$http'];