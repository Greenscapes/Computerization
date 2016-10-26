function PropertiesController($scope, $resource, $routeParams, $location) {
    var propertiesResource = $resource( '/api/properties/type/:type' );
    $scope.properties = propertiesResource.query( { type: $routeParams.type } );
    //$scope.showClosed = false;

    
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
                return "Unconverted Prospect";
            case 4:
                return "Ex-Client";
        }

        return '';
    }

    $scope.getCustomerType = function (type) {
        switch (type) {
            case 1:
                return "HOA";
            case 2:
                return "Commercial";
            case 3:
                return "Residential";
        }

        return '';
    }
}

PropertiesController.$inject = ['$scope', '$resource','$routeParams', '$location'];
app.controller('PropertiesController', PropertiesController);