function TaskDetailController($scope, $resource, $routeParams, $location, Modal) {
    var taskListResource = $resource('/api/tasklists/:taskListId',
    {
        taskListId: $routeParams.taskListId
    });
    var taskResource = $resource('/api/tasks/:taskId',
    { taskId: $routeParams.taskId },
    {
        'update': { method: 'PUT' }
    });
    var crewsResource = $resource( "/api/crews" );
    var eventScheduleResource = $resource( '/api/eventschedules',{    });
    

    
    
    $scope.task = taskResource.get( { taskId: $routeParams.taskId }, function () {

        $scope.crews = crewsResource.query( function () {
            for ( var i = 0; i < $scope.crews.length; i++ ) {
                for ( var j = 0; j < $scope.task.Crews.length; j++ ) {
                    if ( $scope.crews[i].Id === $scope.task.Crews[j].Id ) {
                        $scope.crews[i].checked = true;
                        break;
                    }
                }
            }
        } );

        $scope.taskList = taskListResource.get({}, function () {
            for ( var i = 0; i < $scope.taskList.PropertyTaskListType.PropertyTaskHeaders.length; i++ ) {
                for (var j = 0; j < $scope.task.PropertyTaskDetails.length; j++) {
                    if ( $scope.taskList.PropertyTaskListType.PropertyTaskHeaders[i].Id === $scope.task.PropertyTaskDetails[j].PropertyTaskHeaderId ) {
                        $scope.task.PropertyTaskDetails[j].HeaderName = $scope.taskList.PropertyTaskListType.PropertyTaskHeaders[i].Name;
                    }
                }
            }
            if (!$scope.$$phase) $scope.$apply();
        } );
        $scope.taskEvents = [];
        GetEvents( $scope.task.EventSchedules );
        
            loadEvents();
        
    });
     
  
    function GetEvents( data ) {
        for ( var i = 0; i < data.length; i++ ) {

            var event = data[i];
            var newEvent = new Object( {
                taskId: event.Id,
                start: new Date(event.StartTime.toString()),
                end: new Date(event.EndTime.toString()),
                title: event.Title,
                isAllDay: event.IsAllDay,
                startTimezone: event.StartTimezone,
                endTimezone: event.EndTimezone,
                description: event.Description,
                recurrenceId: event.RecurrenceID,
                recurrenceRule: event.RecurrenceRule,
                recurrenceException: event.RecurrenceException

            } );
            $scope.taskEvents.push( newEvent );
            
           // $( "#scheduler" ).data( "kendoScheduler" ).addEvent( newEvent );
        }

    }

    $scope.update = function(task) {
        $scope.buttonsDisabled = true;
        var scheduler = $( "#scheduler" ).data( "kendoScheduler" );
        SetEventSchedules( scheduler._data );
        taskResource.update( { taskId: task.Id }, task, function () {

            
                $scope.buttonsDisabled = false;
                $scope.back();
            },
            function() {
                $scope.buttonsDisabled = false;
            });
    };

    var deleteFunction = function(task) {
        $scope.buttonsDisabled = true;
        taskResource.delete({ taskId: task.Id }, task, function() {
                $scope.back();
            },
            function() {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.confirmDelete = function () {
        Modal.showConfirmDelete("task", $scope.task.Location, $scope.task, deleteFunction);
    };


    function loadEvents() {

        var scheduler = $( "#scheduler" ).kendoScheduler( {
            date: new Date(),
            startTime: new Date(),
            height: 600,
            views: [
                "day",
                { type: "workWeek", selected: true },
                "week",
                "month",
            ],
            dataSource: $scope.taskEvents,
            save: scheduler_save,
            remove: scheduler_remove,
            edit: scheduler_edit,
            cancel: scheduler_cancel,
        } ).data( "kendoScheduler" );

       
    }

    function SetEventSchedules( data ) {
        $scope.task.EventSchedules = [];
        for ( var i = 0; i < data.length; i++ ) {

            var event = data[i];
            var newEvent = new Object( {
                Id: event.taskId,
                clientTaskId: event.uid,
                startTime: event.start,
                endTime: event.end,
                title: event.title,
                isAllDay: event.isAllDay,
                startTimezone: event.startTimezone,
                endTimezone: event.endTimezone,
                description: event.description,
                recurrenceID: event.recurrenceId,
                recurrenceRule: event.recurrenceRule,
                recurrenceException: event.recurrenceException

            } );
            $scope.task.EventSchedules.push( newEvent )
        }

    }
    $scope.back = function() {
        $location.path("/properties/" + $routeParams.propertyId + "/tasklists/" + $routeParams.taskListId);
        if (!$scope.$$phase) $scope.$apply();
    }

    function scheduler_save( e ) {

    }

    function scheduler_remove( e ) {

    }

    function scheduler_cancel( e ) {

    }

    function scheduler_edit( e ) {

    }
};

TaskDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller('TaskDetailController', TaskDetailController);