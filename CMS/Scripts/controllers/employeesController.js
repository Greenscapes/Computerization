function EmployeesController($scope, $resource, $location) {
    var employeesResource = $resource("/api/employees");
    $scope.employees = employeesResource.query(function() {
        
    });

    $scope.newEmployee = function () {
        $location.path("/employees/new");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.formatName = function (employee) {
        var name = employee.LastName;

        if (employee.FirstName) {
            if (name) {
                name += ", " + employee.FirstName;
            } else {
                name = employee.FirstName;
            }
        }

        if (employee.MiddleName) {
            if (name) {
                name += " " + employee.MiddleName;
            } else {
                name = employee.MiddleName;
            }
        }

        return name;
    };
}

EmployeesController.$inject = ['$scope', '$resource', '$location'];
app.controller('EmployeesController', EmployeesController);