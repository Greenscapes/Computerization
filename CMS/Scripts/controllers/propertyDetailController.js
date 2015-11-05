function PropertyDetailController($scope, $resource, $routeParams, $location, $q, Modal) {
    var propertyResource = $resource('/api/properties/:propertyId');
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
//        $scope.empEvents = [];
//        $scope.eventlist = eventschedulesResource.query( {}, function () {
//            $scope.empEvents;
//            GetEvents( $scope.eventlist );
//            loadEvents();

//        });

    } );

       
    function GetEvents( data ) {
        for ( var i = 0; i < data.length; i++ ) {

            var event = data[i];
            var newEvent = new Object( {
                taskId: event.Id,
                start: new Date( event.StartTime.toString() ),
                end: new Date( event.EndTime.toString() ),
                title: event.Title,
                isAllDay: event.IsAllDay,
                startTimezone: event.StartTimezone,
                endTimezone: event.EndTimezone,
                description: event.Description,
                recurrenceId: event.RecurrenceID,
                recurrenceRule: event.RecurrenceRule,
                recurrenceException: event.RecurrenceException

            } );
            $scope.empEvents.push( newEvent );

            // $( "#scheduler" ).data( "kendoScheduler" ).addEvent( newEvent );
        }

    }

    function loadEvents() {
        var scheduler = $( "#scheduler" ).kendoScheduler( {
            date: new Date(),
            startTime: new Date(),
            height: 600,
            editable: false,
            views: [
                "day",
                { type: "workWeek", selected: true },
                "week",
                "month",
            ],
            dataSource: $scope.empEvents,
        } ).data( "kendoScheduler" );
    }


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

    var updateTask = function (task)
    {
        task.EventTaskListId = $scope.eventTaskListId;
        return taskResource.update({ taskId: task.Id }, task, function () {

        });
    }

    var deleteFunction = function () {
        $scope.buttonsDisabled = true;
        propertyResource.delete({ propertyId: $routeParams.propertyId }, $scope.property, function () {
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
}

PropertyDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', '$q', 'Modal'];
app.controller('PropertyDetailController', PropertyDetailController);