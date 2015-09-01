function CrewMemberDetailController($scope, $resource, $routeParams, $location) {
    var crewMemberResource = $resource('/api/crewmembers/:memberId',
    { memberId: $routeParams.memberId },
    {
        'update': { method: 'PUT' }
    });

    $scope.members = crewMemberResource.get();

    $scope.buttonsDisabled = false;

    $scope.back = function () {
        $location.path("/crews/" + $routeParams.crewId);
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.update = function(member) {
        $scope.buttonsDisabled = true;
        crewMemberResource.update({ memberId: member.Id }, member, function() {
            $scope.back();
        }, function() {

        });
    };
}

CrewMemberDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('CrewMemberDetailController', CrewMemberDetailController);