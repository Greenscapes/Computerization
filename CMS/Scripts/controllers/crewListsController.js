function CrewListsController( $scope, $resource, $location ) {
    var crewTypesResource = $resource( "/api/types/crewlists" );
    $scope.crewTypes = crewTypesResource.query(function () { });

    $scope.newCrewType = function () {
        $location.path( "/types/crewlists/new" );
        if (!$scope.$$phase) $scope.$apply();
    };
}

CrewListsController.$inject = ['$scope', '$resource', '$location'];
app.controller( 'CrewListsController', CrewListsController );