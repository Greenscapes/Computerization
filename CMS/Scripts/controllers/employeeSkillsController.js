function CrewListsController( $scope, $resource, $location ) {
    var skillsResource = $resource( "/api/types/crewlists" );
    $scope.skills = skillsResource.query(function () { });

    $scope.newEmployeeSkill = function () {
        $location.path( "/types/crewlists/new" );
        if (!$scope.$$phase) $scope.$apply();
    };
}

CrewListsController.$inject = ['$scope', '$resource', '$location'];
app.controller( 'CrewListsController', CrewListsController );