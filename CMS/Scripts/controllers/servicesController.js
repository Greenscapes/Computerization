function ServicesController($scope, $resource, $location) {
    var servicesResource = $resource('/api/services');
    $scope.services = servicesResource.query(function () { });
    $scope.showClosed = false;

    $scope.newService = function () {
        $location.path("/service/");
        if (!$scope.$$phase) $scope.$apply();
    };
}

ServicesController.$inject = ['$scope', '$resource', '$location'];
app.controller('ServicesController', ServicesController);