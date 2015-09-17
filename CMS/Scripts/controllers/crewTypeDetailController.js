function CrewTypeDetailController($scope, $resource, $routeParams, $location, Modal) {
    var crewTypesResource = $resource( '/api/types/crewlists/:crewTypeId',
    { crewTypeId: $routeParams.crewTypeId },
    {
        'update': { method: 'PUT' }
    });

    $scope.crewType = crewTypesResource.get({ crewTypeId: $routeParams.crewTypeId });

    $scope.buttonsDisabled = false;

    $scope.update = function (crewType) {
        $scope.buttonsDisabled = true;
        crewTypesResource.update({ crewTypeId: crewType.Id }, crewType, function () {
            $scope.back();
        });
    };

    $scope.back = function () {
        $location.path( "/types/crewlists" );
        if (!$scope.$$phase) $scope.$apply();
    };

    var deleteFunction = function () {
        $scope.buttonsDisabled = true;
        crewTypesResource.delete({ crewTypeId: $routeParams.crewTypeId }, $scope.crewType, function () {
            $scope.back();
            if (!$scope.$$phase) $scope.$apply();
        },
            function () {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.confirmDelete = function () {
        Modal.showConfirmDelete("crew type", $scope.crewType.Name, $scope.crewType, deleteFunction);
    };
}

CrewTypeDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller('CrewTypeDetailController', CrewTypeDetailController);