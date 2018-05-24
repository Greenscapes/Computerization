function CrewCreateController($scope, $resource, $routeParams, $location) {
    var crewsResource = $resource('/api/crews');
    var employeesResource = $resource( "/api/employees" );

    $scope.employees = employeesResource.query(function () {

    });

    $scope.buttonsDisabled = false;

    $scope.back = function () {
        $location.path("/crews");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.save = function(crew) {
        $scope.buttonsDisabled = true;

        crew.CrewMembers = [];

        if ($scope.employees) {
            for (var i = 0; i < $scope.employees.length; i++) {
                if ($scope.employees[i].checked) {
                    delete $scope.employees[i].checked;

                    var cremember = new Object({
                        EmployeeId: $scope.employees[i].Id,
                        IsCrewLeader: $scope.employees[i].IsCrewLeader
                    }
                    );
                    crew.CrewMembers.push(cremember);
                }
            }
        }
        
        crewsResource.save(crew, function() {
            $scope.buttonsDisabled = false;

            $scope.back();
        },
            function() {
                $scope.buttonsDisabled = false;
            } );

    };
}


CrewCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('CrewCreateController', CrewCreateController);