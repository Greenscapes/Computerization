function NavController($scope, $rootScope) {
    $scope.navCollapsed = true;

}

NavController.$inject = ['$scope', '$rootScope'];
app.controller('NavController', NavController);