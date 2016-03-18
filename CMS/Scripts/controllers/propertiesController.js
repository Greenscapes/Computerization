function PropertiesController($scope, $resource, $location) {
    var propertiesResource = $resource('/api/properties');
    $scope.properties = propertiesResource.query(function () { });
    $scope.showClosed = false;

    $scope.newProperty = function() {
        $location.path("/properties/new");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.getPropertyType = function(type) {
        switch(type) {
            case 1:
                return "Prospect";
            case 2:
                return "Active";
            case 3:
                return "Closed";
        }

        return '';
    }
}

PropertiesController.$inject = ['$scope', '$resource', '$location'];
app.controller('PropertiesController', PropertiesController);