function EmployeeDetailController($scope, $resource, $routeParams, $location) {
    var employeesResource = $resource('/api/employees/:employeeId',
    { employeeId: $routeParams.employeeId },
    {
        'update': { method: 'PUT' }
    });
    var crewMemberResource = $resource('/api/crews/employee/:employeeId',
    { employeeId: $routeParams.employeeId });

    var skillsResource = $resource('/api/types/crewlists');
    //var eventschedulesResource = $resource( '/api/eventschedules/:employeeId/events',
    //{ employeeId: $routeParams.employeeId },
    //{
          
    //});
  
    $scope.employee = employeesResource.get( {}, function () {
        
        $scope.employeeSkills = skillsResource.query( function () {
            for (var i = 0; i < $scope.employeeSkills.length; i++) {
                for ( var j = 0; j < $scope.employee.EmployeeSkills.length; j++ ) {
                    if ($scope.employeeSkills[i].Id === $scope.employee.EmployeeSkills[j].Id) {
                        $scope.employeeSkills[i].checked = true;
                        break;
                    }
                }
            }
            
        });
        $scope.crew = crewMemberResource.get({}, function() {

        });

        $scope.empEvents = [];
       
    } );
    
    $scope.buttonsDisabled = false;

    $scope.back = function () {
        $location.path("/employees");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.update = function(employee) {
        $scope.buttonsDisabled = true;
        employee.EmployeeSkills = [];

        for (var i = 0; i < $scope.employeeSkills.length; i++) {
            if ($scope.employeeSkills[i].checked) {
                delete $scope.employeeSkills[i].checked;
                employee.EmployeeSkills.push($scope.employeeSkills[i]);
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