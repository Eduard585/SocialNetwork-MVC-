//Логику Сортировки мужчин и женщин следует переделать и сделать более надежной
var MainController = function ($scope, $http) {

    $scope.init = function (loves) {
        var manArray = loves.filter((subject) => subject.Gender == 'Man');
        var girlArray = loves.filter((subject) => subject.Gender == 'Girl');
        var manArrayLength = manArray.length;
        var girlArrayLength = girlArray.length
        if (manArrayLength != girlArrayLength) {
            if (manArrayLength > girlArrayLength) {
                manArray.splice(0, manArrayLength - girlArrayLength);
            }
            else {
                girlArray.splice(0, girlArrayLength - manArrayLength);
            }
        }
        $scope.manloves = manArray;
        $scope.girlloves = girlArray;
        $scope.loves = loves;
    }

    

    $scope.CreateLoves = function () {
        $http.post('/Home/CreateLoves').
            then(function success(response) {

                if ($scope.loves == 'undefined' || $scope.loves.indexOf("3") !== -1) {
                    console.log("Пара еще не найдена");
                    return;
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
        if (data.Gender == "Girl") {
            $scope.girlloves.push(data);
        }
        else {
            $scope.manloves.push(data);
        }
    }
   
    
    
}

MainController.$inject = ['$scope','$http'];