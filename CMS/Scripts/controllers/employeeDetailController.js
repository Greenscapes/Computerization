function EmployeeDetailController($scope, $resource, $routeParams, $location, $http) {
    var employeesResource = $resource('/api/employees/:employeeId',
    { employeeId: $routeParams.employeeId },
    {
        'update': { method: 'PUT' },
        'delete': { method: 'DELETE' }
    });
    var crewsResource = $resource('/api/crews');
    var crewMembersResource = $resource('/api/employee/:employeeId/members',
    { employeeId: $routeParams.employeeId });

    var skillsResource = $resource('/api/types/employeeskills');
    //var eventschedulesResource = $resource( '/api/eventschedules/:employeeId/events',
    //{ employeeId: $routeParams.employeeId },
    //{

    //});

    $scope.employee = employeesResource.get({}, function () {

        $scope.employeeSkills = skillsResource.query(function () {
            for (var i = 0; i < $scope.employeeSkills.length; i++) {
                for (var j = 0; j < $scope.employee.EmployeeSkills.length; j++) {
                    if ($scope.employeeSkills[i].Id === $scope.employee.EmployeeSkills[j].Id) {
                        $scope.employeeSkills[i].checked = true;
                        break;
                    }
                }
            }

        });
        $scope.crews = crewsResource.query({}, function () {
            var crewMembers = crewMembersResource.query(function () {
                for (var i = 0; i < $scope.crews.length; i++) {
                    for (var j = 0; j < crewMembers.length; j++) {
                        if ($scope.crews[i].Id === crewMembers[j].CrewId) {
                            $scope.crews[i].checked = true;
                            break;
                        }
                    }
                }
            });
        });

        $scope.empEvents = [];

    });

    $scope.buttonsDisabled = false;

    $scope.back = function () {
        $location.path("/employees");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.update = function (employee) {
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

        employeesResource.update({ employeeId: employee.Id }, employee, function () {
            $http.put('/api/employees/' + employee.Id + "/crews", crewIds)
                .success(function (data) {
                    $scope.buttonsDisabled = false;
                    $scope.back();
                });
        },
            function () {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.delete = function (employee) {
        if (!confirm('Are you sure you want to delete this employee')) {
            return;
        }

        $scope.buttonsDisabled = true;

        employeesResource.delete({ employeeId: employee.Id }, employee, function () {
            $scope.buttonsDisabled = false;
            $scope.back();
        },
           function () {
               $scope.buttonsDisabled = false;
           });
    }

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

EmployeeDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', '$http'];
app.controller('EmployeeDetailController', EmployeeDetailController);