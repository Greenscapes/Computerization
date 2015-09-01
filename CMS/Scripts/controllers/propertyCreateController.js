function PropertyCreateController($scope, $resource, $location) {
    var propertiesResource = $resource('/api/properties');

    $scope.property = {};

    $scope.save = function(property) {
        $scope.buttonsDisabled = true;
        propertiesResource.save(property, function() {
                $scope.buttonsDisabled = false;
                $scope.back();
            },
            function() {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.back = function() {
        $location.path("/properties");
        if (!$scope.$$phase) $scope.$apply();
    };
}

PropertyCreateController.$inject = ['$scope', '$resource', '$location'];
app.controller('PropertyCreateController', PropertyCreateController);