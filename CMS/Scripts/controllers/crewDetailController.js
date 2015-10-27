function CrewDetailController($scope, $resource, $routeParams, $location, Modal) {
    var crewResource = $resource('/api/crews/:crewId',
    { crewId: $routeParams.crewId },
    {
        'update': { method: 'PUT' }
    });
    var crewTypesResource = $resource('/api/employees');
    var membersResource = $resource('/api/crews/:crewId/members', { crewId: $routeParams.crewId });
    var memberResource = $resource('/api/crews/:crewId/members/:crewMemberId', { crewId: $routeParams.crewId });

    var allEmployees = [];
    var loadAvailableEmployees = function() {
        $scope.employees = [];
        for (var i = 0; i < allEmployees.length; i++) {
            var alreadyMember = false;
            for (var j = 0; j < $scope.members.length; j++) {
                if ($scope.members[j].EmployeeId === allEmployees[i].Id) {
                    alreadyMember = true;
                    break;
                }
            }

            if (!alreadyMember) {
                $scope.employees.push(allEmployees[i]);
            }
        }
    }

    var loadMembers = function() {
        $scope.members = membersResource.query(function() {
            var leaders = [];
            var others = [];
            for (var i = 0; i < $scope.members.length; i++) {
                if ($scope.members[i].IsCrewLeader) {
                    leaders.push($scope.members[i]);
                } else {
                    others.push($scope.members[i]);
                }
            }

            $scope.members = leaders.concat(others);

            loadAvailableEmployees();
        });
    };

    $scope.crew = crewResource.get({ crewId: $routeParams.crewId }, function() {
        allEmployees = crewTypesResource.query({ }, function() {
            loadMembers();
        });
    });

    $scope.newMember = {
        IsCrewLeader: false
    };

    var deleteFunction = function() {
        $scope.buttonsDisabled = true;
        crewResource.delete({ crewId: $routeParams.crewId }, $scope.crew, function() {
                $scope.back();
            },
            function() {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.confirmDelete = function() {
        Modal.showConfirmDelete("crew", $scope.crew.Name, $scope.crew, deleteFunction);
    };

    $scope.back = function() {
        $location.path("/crews");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.addCrewMember = function() {
        var crewMember = {
            CrewId: $routeParams.crewId,
            EmployeeId: $scope.newMember.EmployeeId,
            IsCrewLeader: $scope.newMember.IsCrewLeader
        };

        $scope.newMember.EmployeeId = 0;
        $scope.newMember.IsCrewLeader = false;

        $scope.buttonsDisabled = true;
        membersResource.save(crewMember, function() {
                $scope.buttonsDisabled = false;
                loadMembers();
            },
            function() {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.removeMember = function(member) {
        $scope.buttonsDisabled = true;
        memberResource.delete({
            crewId: $routeParams.crewId,
            crewMemberId: member.Id
        }, member, function () {
            $scope.buttonsDisabled = false;
            loadMembers();
        });
    };

    $scope.formatName = function (employeeId) {
        var employee = null;
        for (var i = 0; i < allEmployees.length; i++) {
            if (allEmployees[i].Id === employeeId) {
                employee = allEmployees[i];
                break;
            }
        }

        if (!employee) {
            return "(Unknown)";
        }

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

CrewDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller('CrewDetailController', CrewDetailController);