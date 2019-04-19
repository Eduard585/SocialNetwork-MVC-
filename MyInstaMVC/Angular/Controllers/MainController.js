//Логику Сортировки мужчин и женщин следует переделать и сделать более надежной
var MainController = function ($scope, $http,$modal) {

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
              
                if (response.data.Success == false)
                {
                    console.log(response.data.Result);
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

    function equalTables(love) {
        var manLoves = $scope.manloves;
        var girlLoves = $scope.girlloves;

    }
   
    
    
}

MainController.$inject = ['$scope','$http'];