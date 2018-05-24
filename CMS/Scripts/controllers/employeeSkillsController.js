function EmployeeSkillsController( $scope, $resource, $location ) {
    var skillsResource = $resource("/api/types/employeeskills");
    var skillResource = $resource('/api/types/employeeskills/:skillId', { skillId: 0 }, { 'update': { method: 'PUT' } });

    $scope.skills = skillsResource.query(function () { });

    $scope.newEmployeeSkill = function () {
        $location.path("/types/employeeskills/new");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.moveUp = function (skill) {
        var displayOrder = skill.DisplayOrder - 1;
        var oldSkill;
        $scope.skills.forEach(function (scopeSkill) {
            if (scopeSkill.DisplayOrder == displayOrder) {
                oldSkill = scopeSkill;
            }
        });
        oldSkill.DisplayOrder = oldSkill.DisplayOrder + 1;
        skill.DisplayOrder = skill.DisplayOrder - 1;
        skillResource.update({ skillId: skill.Id }, skill, function () {
            skillResource.update({ skillId: skill.Id }, skill, function () {
                $scope.buttonsDisabled = true;
            });
        });
    }

    $scope.moveDown = function (skill) {
        var displayOrder = skill.DisplayOrder + 1;
        var oldSkill;
        $scope.skills.forEach(function (scopeSkill) {
            if (scopeSkill.DisplayOrder == displayOrder) {
                oldSkill = scopeSkill;
            }
        });
        oldSkill.DisplayOrder = oldSkill.DisplayOrder - 1;
        skill.DisplayOrder = skill.DisplayOrder + 1;
        skillResource.update({ skillId: skill.Id }, skill, function () {
            skillResource.update({ skillId: oldSkill.Id }, oldSkill, function () {
                $scope.buttonsDisabled = true;
            });
        });
    }
}

EmployeeSkillsController.$inject = ['$scope', '$resource', '$location'];
app.controller('EmployeeSkillsController', EmployeeSkillsController);