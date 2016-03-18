function PropertyDetailController($scope, $resource, $routeParams, $location, $q, Modal) {
    var propertyResource = $resource('/api/properties/:propertyId', { propertyId: $routeParams.propertyId }, 
        {
            'update': { method: 'PUT' }
        });
    var taskListsResource = $resource( '/api/properties/:propertyId/tasks', { propertyId: $routeParams.propertyId } );
    var eventTasksResource = $resource("/api/properties/:propertyId/eventtasklists");
    var taskResource = $resource('/api/tasks/:taskId',
   { taskId: $routeParams.taskId },
   {
       'update': { method: 'PUT' }
   });

    var eventschedulesResource = $resource( '/api/eventschedules/:propertyId/:allProperty/propertyevents',
   {
       propertyId: $routeParams.propertyId,
       allProperty:true
   },
   {

   } );

    $scope.liSortByField = "Id";
    $scope.itemReverse = false;

    $scope.eventTaskListId = '';
    $scope.property = propertyResource.get({ propertyId: $routeParams.propertyId });
    $scope.eventTaskLists = eventTasksResource.query({ propertyId: $routeParams.propertyId });

    $scope.tasks = taskListsResource.query( function () {
    } );

    $scope.newTask = function () {
        $location.path('/properties/' + $routeParams.propertyId + '/tasklists/' + $scope.property.TaskListId + '/tasks/new');
    };

    $scope.newEventTaskList = function() {
        $location.path('/properties/' + $routeParams.propertyId + '/schedule/new');
    };

    $scope.assignTasks = function () {
        if (!$scope.eventTaskListId) {
            alert("You need to select an event task list");
            return;
        }
        var prom = [];
        for (var i = 0; i < $scope.tasks.length; i++) {
            if ($scope.tasks[i].selected) {
                prom.push(updateTask($scope.tasks[i]));
            }
        }
        $q.all(prom).then(function() {
            $scope.tasks = taskListsResource.query(function() {});
        });
    };

    var updateTask = function (task) {
        var deferred = $q.defer();
        var promise = deferred.promise;
        task.EventTaskListId = $scope.eventTaskListId;
        taskResource.update({ taskId: task.Id }, task, function () {
            deferred.resolve();
        });
        return promise;
    }

    var deleteFunction = function () {
        $scope.buttonsDisabled = true;
        $scope.buttonsDisabled = true;
        $scope.property.PropertyType = 3;

        propertyResource.update({ propertyId: $routeParams.propertyId }, $scope.property, function () {
            $scope.back();
        },
            function () {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.confirmDelete = function () {
        Modal.showConfirmDelete("property", $scope.property.Name, $scope.property, deleteFunction);
    };

    $scope.back = function () {
        $location.path("/properties");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.unallocateTask = function(task) {
        task.EventTaskListId = null;
        taskResource.update({ taskId: task.Id }, task, function () {
            $scope.tasks = taskListsResource.query(function () {
            });
        });
    }

    var deleteTaskFunction = function (task) {
        taskResource.delete({ taskId: task.Id }, task, function () {
            $scope.tasks = taskListsResource.query(function () {
            });
        },
            function () {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.confirmDeleteTask = function (task) {
        Modal.showConfirmDelete("task", task.Location + " - " + task.Description, task, deleteTaskFunction);
    };
}

PropertyDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', '$q', 'Modal'];
app.controller('PropertyDetailController', PropertyDetailController);