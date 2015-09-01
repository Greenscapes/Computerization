function TaskListDetailController($scope, $resource, $routeParams, $location, Modal) {
    var taskListResource = $resource('/api/tasklists/:taskListId');
    var tasksResource = $resource('/api/tasklists/:taskListId/tasks',
    {
        taskListId: $routeParams.taskListId
    });

    $scope.taskList = taskListResource.get({ taskListId: $routeParams.taskListId });
    $scope.tasks = tasksResource.query(function() {});

    $scope.getDetailValueForHeader = function(task, header) {
        for (var i = 0; i < task.PropertyTaskDetails.length; i++) {
            if (task.PropertyTaskDetails[i].PropertyTaskHeaderId === header.Id) {
                return task.PropertyTaskDetails[i].Value;
            }
        }

        return null;
    };

    var deleteFunction = function () {
        $scope.buttonsDisabled = true;
        taskListResource.delete({ taskListId: $routeParams.taskListId }, $scope.taskList, function () {
            $location.path("/properties/" + $routeParams.propertyId);
            if (!$scope.$$phase) $scope.$apply();
        },
            function () {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.confirmDelete = function () {
        Modal.showConfirmDelete("task list", $scope.taskList.Name, $scope.taskList, deleteFunction);
    };

    $scope.newTask = function() {
        $location.path("/properties/" + $routeParams.propertyId + "/tasklists/" + $routeParams.taskListId + "/tasks/new");
        if (!$scope.$$phase) $scope.$apply();
    }

    $scope.back = function() {
        $location.path("/properties/" + $routeParams.propertyId);
        if (!$scope.$$phase) $scope.$apply();
    };
}

TaskListDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller('TaskListDetailController', TaskListDetailController);