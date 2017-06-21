(function () {

    //Module 
    var app = angular.module('ZooApp', []); 

    // Controller
    app.controller('AnimalsCtrl', function ($scope, $http, $window) {
        $scope.species = [];
        $scope.animal = { Id: null, Name: '', Age: null, YearOfBirth: null, CreationDate: null, Species: { Id: null, Name: '' } };
        $scope.animals = [];
        $scope.working = false;
        $scope.avgAge = 0;

        $scope.loadSpecies = function () {
            $scope.working = true;
            $scope.status = "Pärin looma liike...";
            $scope.statusClass = "progress-bar-info";
            $scope.species = [];

            $http.get("/api/Species").then(function (response) {
                $scope.species = response.data;
                $scope.status = "Looma liigid päritud";
                $scope.statusClass = "progress-bar-success";
                $scope.working = false;
            }, function(response) {
                $scope.status = "Loomaliikide pärimine ebaõnnestus!";
                $scope.statusClass = "progress-bar-danger";
                $scope.showAlert(response);
                $scope.working = false;
            });
        };

        $scope.loadAnimals = function () {
            $scope.working = true;
            $scope.status = "Pärin loomade nimekirja...";
            $scope.statusClass = "progress-bar-info";
            $scope.animals = [];

            $http.get("/api/Animals").then(function (response) {
                $scope.animals = response.data;
                $scope.status = "Loomade nimekiri päritud";
                $scope.statusClass = "progress-bar-success";
                $scope.avgAge = $scope.calcAvgAge();
                $scope.working = false;
            }, function (response) {
                $scope.status = "Loomade pärimine ebaõnnestus!";
                $scope.statusClass = "progress-bar-danger";
                $scope.showAlert(response);
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

        $scope.addAnimal = function (animal) {
            $scope.working = true;
            $scope.status = "Lisan uut looma '"+animal.Name+"'...";
            $scope.statusClass = "progress-bar-info";

            var newAnimal = { Name: animal.Name, YearOfBirth: animal.YearOfBirth, SpeciesId: animal.Species.Id }

            $http.post("/api/Animals", newAnimal).then(function (response) {
                $scope.status = "Uus loom '" + newAnimal.Name + "' lisatud!";
                $scope.statusClass = "progress-bar-success";
                $scope.working = false;
                //reload all animals
                $scope.loadAnimals();
            }, function (response) {
                $scope.status = "Looma '" + newAnimal.Name + "' lisamine ebaõnnestus!";
                $scope.statusClass = "progress-bar-danger";
                $scope.showAlert(response);
                $scope.working = false;
            });
        };

        $scope.updateAnimal = function (animal) {
            $scope.working = true;
            $scope.status = "Uuendan looma '"+animal.Name+"' andmeid...";
            $scope.statusClass = "progress-bar-info";

            animal.SpeciesId = animal.Species.Id

            for (var i = 0; i < $scope.species.length; i++) {
                if ($scope.species[i].Id === animal.Species.Id) {
                    animal.Species.Name = $scope.species[i].Name;
                    break;
                }
            }

            $http.put("/api/Animals/"+animal.Id, animal).then(function (response) {
                $scope.status = "Looma '" + animal.Name + "' andmed uuendatud!";
                $scope.statusClass = "progress-bar-success";
                $scope.working = false;
                //reload all animals
                $scope.loadAnimals();
            }, function (response) {
                $scope.status = "Looma '" + animal.Name + "' andmete uuendamine ebaõnnestus!";
                $scope.statusClass = "progress-bar-danger";
                $scope.showAlert(response);
                $scope.working = false;
            });
        };

        $scope.submitAnimal = function () {
            if($scope.animal.Id===null){
                $scope.addAnimal($scope.animal);
            }else{
                $scope.updateAnimal($scope.animal);
            }
            $scope.reset();
        };

        $scope.modifyAnimal = function (id) {
            for (var i = 0; i < $scope.animals.length; i++) {
                if ($scope.animals[i].Id === id) {
                    $scope.animal = angular.copy($scope.animals[i]);
                    break;
                }
            }
        };

        $scope.deleteAnimal = function (animal) {
            if (confirm("Kas oled kindel, et soovid kustutada looma '" + animal.Name + "'?")) {
                $scope.working = true;
                $scope.status = "Looma '" + animal.Name + "' kustutamine...";
                $scope.statusClass = "progress-bar-info";

                if ($scope.animal.Id === animal.Id) {//clean form if the department to be deleted is shown there.
                    $scope.reset();
                }

                $http.delete("/api/Animals/" + animal.Id).then(function (response) {
                    $scope.status = "Loom '" + animal.Name + "' kustutatud!";
                    $scope.statusClass = "progress-bar-success";
                    $scope.working = false;
                    //reload all animals
                    $scope.loadAnimals();
                }, function (response) {
                    $scope.status = "Looma '" + animal.Name + "' kustutamine ebaõnnestus!";
                    $scope.statusClass = "progress-bar-danger";
                    $scope.showAlert(response);
                    $scope.working = false;
                });
            }
            
        };

        $scope.reset = function () {
            $scope.animal = { Id: null, Name: '', Age: null, YearOfBirth: null, CreationDate: null, SpeciesName: '' };
            $scope.animalForm.$setPristine(); //reset Form
        }

        $scope.initController = function () {
            $scope.loadSpecies();
            $scope.loadAnimals();
        }

        $scope.showAlert = function (response) {
            angular.forEach(response.data.ModelState, function (value, key) {
                $window.alert(value);
            });
        }

        $scope.initController();
    });
})();