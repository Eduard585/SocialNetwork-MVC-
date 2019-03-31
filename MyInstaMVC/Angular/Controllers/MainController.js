
var MainController = function ($scope, $http) {

    $scope.init = function (loves) {

        $scope.loves = loves;
       
        
    }

    $scope.CreateLoves = function () {
        $http.post('/Home/CreateLoves').
            then(function success(response) {

                if ($scope.loves.indexOf("3") !== -1) {
                    console.log("Пара еще не найдена");
                }
                
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
   
    
    
}

MainController.$inject = ['$scope','$http'];