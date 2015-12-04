function EmployeeCreateController($scope, $resource, $routeParams, $location) {
    var employeesResource = $resource('/api/employees');
    var skillResource = $resource('/api/types/crewlists');

    $scope.employeeSkills = skillResource.query();
    $scope.buttonsDisabled = false;

    $scope.back = function () {
        $location.path("/employees");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.save = function(employee) {
        $scope.buttonsDisabled = true;
        employee.EmployeeSkills = [];

        for (var i = 0; i < $scope.employeeSkills.length; i++) {
            if ($scope.employeeSkills[i].checked) {
                delete $scope.employeeSkills[i].checked;
                employee.EmployeeSkills.push($scope.employeeSkills[i]);
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