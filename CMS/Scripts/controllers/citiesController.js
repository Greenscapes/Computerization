function CitiesController($scope, $resource, $location) {
    var citiesResource = $resource('/api/cities');
    $scope.cities = citiesResource.query(function () { });
    $scope.showClosed = false;

    $scope.newCity = function () {
        $location.path("/city/");
        if (!$scope.$$phase) $scope.$apply();
    };
}

CitiesController.$inject = ['$scope', '$resource', '$location'];
app.controller('CitiesController', CitiesController);