function TaskListTypeDetailController($scope, $resource, $routeParams, $location, Modal) {
    var taskListTypesResource = $resource('/api/types/tasklists/:taskListTypeId',
    { taskListTypeId: $routeParams.taskListTypeId },
    {
        'update': { method: 'PUT' }
    });
    var taskListCountResource = $resource("/api/types/tasklists/:taskListTypeId/count",
    { taskListTypeId: $routeParams.taskListTypeId });

    var count = taskListCountResource.get({}, function() {
        $scope.taskListCount = count.Count;
    });
    $scope.taskListType = taskListTypesResource.get({taskListTypeId: $routeParams.taskListTypeId});

    $scope.buttonsDisabled = false;

    $scope.update = function(taskListType) {
        $scope.buttonsDisabled = true;
        taskListTypesResource.update({ taskListTypeId: taskListType.Id }, taskListType, function() {
            $scope.back();
        });
    };

    $scope.back = function () {
        $location.path("/types");
        if (!$scope.$$phase) $scope.$apply();
    };

    var deleteFunction = function() {
        $scope.buttonsDisabled = true;
        taskListTypesResource.delete({ taskListTypeId: $routeParams.taskListTypeId }, $scope.taskListType, function() {
                $scope.back();
                if (!$scope.$$phase) $scope.$apply();
            },
            function() {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.confirmDelete = function () {
        Modal.showConfirmDelete("task list type", $scope.taskListType.Name, $scope.taskListType, deleteFunction);
    };
}

TaskListTypeDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller('TaskListTypeDetailController', TaskListTypeDetailController);