function CrewMemberCreateController($scope, $resource, $routeParams, $location) {
    var crewMemberResource = $resource('/api/crewmembers');

    $scope.buttonsDisabled = false;

    $scope.back = function () {
        $location.path("/crews/" + $routeParams.crewId);
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.save = function(member) {
        $scope.buttonsDisabled = true;
        member.CrewId = $routeParams.crewId;
        crewMemberResource.save(member, function() {
                $scope.back();
            },
            function() {
                $scope.buttonsDisabled = false;
            });
    };
}

CrewMemberCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('CrewMemberCreateController', CrewMemberCreateController);