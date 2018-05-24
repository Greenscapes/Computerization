function TaskTemplateController($scope, $resource, $routeParams, $location, Modal) {
    var taskResource = $resource('/api/taskTemplates/');
    var taskTemplateResource = $resource('/api/taskTemplates/:taskId', {taskId: 0}, {'update': {method: 'PUT'}});
    $scope.tasks = taskResource.query({});

    var deleteTaskFunction = function (task) {
        $scope.buttonsDisabled = true;
        taskTemplateResource.delete({ taskId: task.Id }, task, function () {
            $scope.tasks = taskResource.query(function () {
            });
        },
            function () {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.confirmDeleteTask = function (task) {
        Modal.showConfirmDelete("task", task.Description, task, deleteTaskFunction);
    };

    $scope.moveUp = function(task) {
        var displayOrder = task.DisplayOrder - 1;
        var oldTask;
        $scope.tasks.forEach(function(scopeTask) {
            if (scopeTask.DisplayOrder == displayOrder) {
                oldTask = scopeTask;
            }
        });
        oldTask.DisplayOrder = oldTask.DisplayOrder + 1;
        task.DisplayOrder = task.DisplayOrder - 1;
        taskTemplateResource.update({ taskId: task.Id }, task, function () {
            taskTemplateResource.update({ taskId: oldTask.Id }, oldTask, function () {
                $scope.buttonsDisabled = true;
            });
        });
    }

    $scope.moveDown = function (task) {
        var displayOrder = task.DisplayOrder + 1;
        var oldTask;
        $scope.tasks.forEach(function (scopeTask) {
            if (scopeTask.DisplayOrder == displayOrder) {
                oldTask = scopeTask;
            }
        });
        oldTask.DisplayOrder = oldTask.DisplayOrder - 1;
        task.DisplayOrder = task.DisplayOrder + 1;
        taskTemplateResource.update({ taskId: task.Id }, task, function () {
            taskTemplateResource.update({ taskId: oldTask.Id }, oldTask, function () {
                $scope.buttonsDisabled = true;
            });
        });
    }
}

TaskTemplateController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller('TaskTemplateController', TaskTemplateController);