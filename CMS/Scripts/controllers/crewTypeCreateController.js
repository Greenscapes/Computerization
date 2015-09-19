function CrewTypeCreateController($scope, $resource, $routeParams, $location) {
    var crewTypesResource = $resource( '/api/types/crewlists/' );

    $scope.buttonsDisabled = false;

    $scope.back = function () {
        $location.path("/types/crewlists");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.save = function (crewType) {
        $scope.buttonsDisabled = true;
        crewTypesResource.save(crewType, function () {
            $scope.buttonsDisabled = false;
            $scope.back();
        },
            function () {
                $scope.buttonsDisabled = false;
            });
    };
}

CrewTypeCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('CrewTypeCreateController', CrewTypeCreateController);