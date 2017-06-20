(function () {

    //Module 
    var app = angular.module('ZooApp', []); 

    // Controller
    app.controller('AnimalsCtrl', function ($scope, $http) {
        //$scope.answered = false;
        $scope.title = "loading...";
        $scope.message = "App is starting, eh?"
        $scope.species = [];
        $scope.animals = [];
        //$scope.correctAnswer = false;
        $scope.working = false;
        $scope.avgAge = 0;

        $scope.loadSpecies = function () {
            $scope.working = true;
            $scope.title = "loading species...";
            $scope.species = [];


            $http.get("/api/Species").then(function (response) {
                //console.log("Species returned:");
                //console.log(response);
                $scope.species = response.data;
                $scope.title = "Species loaded";
                $scope.working = false;
            }, function() {
                $scope.title = "Oops... something went wrong with Species request";
                $scope.working = false;
            });
        };

        $scope.loadAnimals = function () {
            $scope.working = true;
            $scope.title = "loading animals...";
            $scope.animals = [];

            $http.get("/api/Animals").then(function (response) {
                //console.log("Animals returned:");
                //console.log(response);
                $scope.animals = response.data;
                $scope.title = "Animals loaded";
                $scope.avgAge = $scope.calcAvgAge();
                $scope.working = false;
            }, function () {
                $scope.title = "Oops... something went wrong with Animals request";
                $scope.working = false;
            });
        };

        


        $scope.calcAvgAge = function () {
            var sum = 0;

            angular.forEach($scope.animals, function (value, key) {
                sum += value.Age;
            });
            
            return (sum / $scope.animals.length);
        };
    });
})();