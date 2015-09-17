function CrewCreateController($scope, $resource, $routeParams, $location) {
    var crewsResource = $resource('/api/crews');
    var crewTypesResource = $resource("/api/types/crewlists");

    $scope.crewTypes = crewTypesResource.query();

    $scope.buttonsDisabled = false;

    $scope.back = function () {
        $location.path("/crews");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.save = function(crew) {
        $scope.buttonsDisabled = true;
        crewsResource.save(crew, function() {
                $scope.buttonsDisabled = false;
                $scope.back();
            },
            function() {
                $scope.buttonsDisabled = false;
            });
    };
}

CrewCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('CrewCreateController', CrewCreateController);