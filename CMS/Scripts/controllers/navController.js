function NavController($scope) {
    $scope.navCollapsed = true;
}

NavController.$inject = ['$scope'];
app.controller('NavController', NavController);