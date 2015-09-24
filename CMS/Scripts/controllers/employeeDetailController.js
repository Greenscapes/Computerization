function EmployeeDetailController($scope, $resource, $routeParams, $location) {
    var employeesResource = $resource('/api/employees/:employeeId',
    { employeeId: $routeParams.employeeId },
    {
        'update': { method: 'PUT' }
    });
    var crewTypesResource = $resource('/api/types/crewlists');

    $scope.employee = employeesResource.get({}, function() {
        $scope.crewTypes = crewTypesResource.query(function () {
            for (var i = 0; i < $scope.crewTypes.length; i++) {
                for (var j = 0; j < $scope.employee.CrewTypes.length; j++) {
                    if ($scope.crewTypes[i].Id === $scope.employee.CrewTypes[j].Id) {
                        $scope.crewTypes[i].checked = true;
                        break;
                    }
                }
            }
        });
    });
    
    $scope.buttonsDisabled = false;

    $scope.back = function () {
        $location.path("/employees");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.update = function(employee) {
        $scope.buttonsDisabled = true;
        employee.CrewTypes = [];

        for (var i = 0; i < $scope.crewTypes.length; i++) {
            if ($scope.crewTypes[i].checked) {
                delete $scope.crewTypes[i].checked;
                employee.CrewTypes.push($scope.crewTypes[i]);
            }
        }

        employeesResource.update({ employeeId: employee.Id }, employee, function() {
                $scope.buttonsDisabled = false;
                $scope.back();
            },
            function() {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.formatName = function (employee) {
        var name = employee.FirstName;
        if (employee.MiddleName && name) {
            name += " " + employee.MiddleName;
        }
        if (employee.LastName && name) {
            name += " " + employee.LastName;
        }

        return name;
    };
};

EmployeeDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('EmployeeDetailController', EmployeeDetailController);