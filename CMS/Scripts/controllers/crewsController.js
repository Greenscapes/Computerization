function CrewsController($scope, $resource, $location) {
    var crewsResource = $resource("/api/crews");
    $scope.crews = crewsResource.query(function () { });

    $scope.newCrew = function () {
        $location.path("/crews/new");
        if (!$scope.$$phase) $scope.$apply();
    };
}

CrewsController.$inject = ['$scope', '$resource', '$location'];
app.controller('CrewsController', CrewsController);