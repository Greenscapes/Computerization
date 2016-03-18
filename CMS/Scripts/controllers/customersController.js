function CustomersController($scope, $resource, $location) {
    var customersResource = $resource('/api/customers');
    $scope.customers = customersResource.query(function () { });
    $scope.showClosed = false;

    $scope.newCustomer = function () {
        $location.path("/customer/");
        if (!$scope.$$phase) $scope.$apply();
    };
}

CustomersController.$inject = ['$scope', '$resource', '$location'];
app.controller('CustomersController', CustomersController);