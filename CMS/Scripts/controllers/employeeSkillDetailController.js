function EmployeeSkillDetailController($scope, $resource, $routeParams, $location, Modal) {
    var skillsResource = $resource( '/api/types/crewlists/:skillId',
    { skillId: $routeParams.skillId },
    {
        'update': { method: 'PUT' }
    });

    $scope.employeeSkill = skillsResource.get({ skillId: $routeParams.skillId });

    $scope.buttonsDisabled = false;

    $scope.update = function (employeeSkill) {
        $scope.buttonsDisabled = true;
        skillsResource.update({ skillId: employeeSkill.Id }, employeeSkill, function () {
            $scope.back();
        });
    };

    $scope.back = function () {
        $location.path( "/types/crewlists" );
        if (!$scope.$$phase) $scope.$apply();
    };

    var deleteFunction = function () {
        $scope.buttonsDisabled = true;
        skillsResource.delete({ skillId: $routeParams.skillId }, $scope.employeeSkill, function () {
            $scope.back();
            if (!$scope.$$phase) $scope.$apply();
        },
            function () {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.confirmDelete = function () {
        Modal.showConfirmDelete("skill", $scope.employeeSkill.Name, $scope.employeeSkill, deleteFunction);
    };
}

EmployeeSkillDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller('EmployeeSkillDetailController', EmployeeSkillDetailController);