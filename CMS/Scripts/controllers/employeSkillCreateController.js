﻿function EmployeeSkillCreateController($scope, $resource, $routeParams, $location) {
    var skillResource = $resource('/api/types/employeeskills/');

    $scope.buttonsDisabled = false;

    $scope.back = function () {
        $location.path("/types/employeeskills");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.save = function (skill) {
        $scope.buttonsDisabled = true;
        skillResource.save(skill, function () {
            $scope.buttonsDisabled = false;
            $scope.back();
        },
            function () {
                $scope.buttonsDisabled = false;
            });
    };
}

EmployeeSkillCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('EmployeeSkillCreateController', EmployeeSkillCreateController);