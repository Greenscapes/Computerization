function EmployeeCreateController($scope, $resource, $routeParams, $location) {
    var employeesResource = $resource('/api/employees');
    var crewTypesResource = $resource('/api/types/crewlists');

    $scope.crewTypes = crewTypesResource.query();
    $scope.buttonsDisabled = false;

    $scope.back = function () {
        $location.path("/employees");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.save = function(employee) {
        $scope.buttonsDisabled = true;
        employee.CrewTypes = [];

        for (var i = 0; i < $scope.crewTypes.length; i++) {
            if ($scope.crewTypes[i].checked) {
                delete $scope.crewTypes[i].checked;
                employee.CrewTypes.push($scope.crewTypes[i]);
            }
        }

        employeesResource.save(employee, function() {
                $scope.buttonsDisabled = false;
                $scope.back();
            },
            function() {
                $scope.buttonsDisabled = false;
            });
    };
}

EmployeeCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('EmployeeCreateController', EmployeeCreateController);