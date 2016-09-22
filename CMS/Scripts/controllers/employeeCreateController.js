function EmployeeCreateController($scope, $resource, $routeParams, $location, $http) {
    var employeesResource = $resource('/api/employees');
    var skillResource = $resource('/api/types/employeeskills');
    var crewsResource = $resource('/api/crews');

    $scope.employeeSkills = skillResource.query();
    $scope.crews = crewsResource.query();

    $scope.buttonsDisabled = false;

    $scope.back = function () {
        $location.path("/employees");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.save = function (employee) {
        $scope.buttonsDisabled = true;
        employee.EmployeeSkills = [];
        var crewIds = [];

        for (var i = 0; i < $scope.employeeSkills.length; i++) {
            if ($scope.employeeSkills[i].checked) {
                delete $scope.employeeSkills[i].checked;
                employee.EmployeeSkills.push($scope.employeeSkills[i]);
            }
        }

        for (var i = 0; i < $scope.crews.length; i++) {
            if ($scope.crews[i].checked) {
                delete $scope.crews[i].checked;
                crewIds.push($scope.crews[i].Id);
            }
        }

        employeesResource.save(employee, function (newEmployee) {
            $http.put('/api/employees/' + newEmployee.Id + "/crews", crewIds)
            .success(function (data) {
                $scope.buttonsDisabled = false;
                $scope.back();
            });
        },
            function () {
                $scope.buttonsDisabled = false;
            });
    };
}

EmployeeCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location', '$http'];
app.controller('EmployeeCreateController', EmployeeCreateController);