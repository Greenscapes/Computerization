function PropertiesController($scope, $resource, $location) {
    var propertiesResource = $resource('/api/properties');
    $scope.properties = propertiesResource.query(function () { });

    $scope.newProperty = function() {
        $location.path("/properties/new");
        if (!$scope.$$phase) $scope.$apply();
    };
}

PropertiesController.$inject = ['$scope', '$resource', '$location'];
app.controller('PropertiesController', PropertiesController);