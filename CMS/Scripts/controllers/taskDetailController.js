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
        for ( var i = 0; i < $scope.task.EventSchedules.length; i++ ) {
            var event = $scope.task.EventSchedules[i];
            var taskEvent = new Object( {
                start: event.StartTime.toString(),
                end: event.EndTime.toString(),
                title: event.Title,
                id: event.Id.toString()
            } );
          
            $scope.taskEvents.push( taskEvent );
            loadEvents();
        }
    });
     
  
    $scope.crewCheckChanged = function(crew, task) {
        if (crew.checked) {
            task.Crews.push(crew);
        } else {
            task.Crews.pop(crew);
        }
    }

    $scope.update = function(task) {
        $scope.buttonsDisabled = true;
        taskResource.update({ taskId: task.Id }, task, function() {
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



    var nav = new DayPilot.Navigator( "nav" );
    nav.showMonths = 3;
    nav.skipMonths = 3;
    nav.selectMode = "week";
    nav.onTimeRangeSelected = function ( args ) {
        dp.startDate = args.day;
        dp.update();
        loadEvents();
    };
    nav.init();

    var dp = new DayPilot.Calendar( "dp" );
    dp.viewType = "Week";
    dp.headerDateFormat = "dddd <br/>d MMMM yyyy";
    dp.headerHeight = "40";
    dp.columnHeaderHeightAutoFit = "false"

    dp.onTimeRangeSelected = function ( args ) {
        var name = $scope.task.Description;

        var e = new DayPilot.Event( {
            start: args.start,
            end: args.end,
            id: DayPilot.guid(),
            resource: args.resource,
            text: name
        } );
        dp.events.add( e );
        $scope.task.EventSchedules = [];
        var newEvent = new Object( {
            startTime: args.start,
            endTime: args.end,
            title: name
        } );
        $scope.task.EventSchedules.push(newEvent);

    }
    dp.init();
    loadEvents();

    function loadEvents() {
        var start = dp.visibleStart();
        var end = dp.visibleEnd();
        dp.events.list = $scope.taskEvents;
        dp.update();
    }

    $scope.back = function() {
        $location.path("/properties/" + $routeParams.propertyId + "/tasklists/" + $routeParams.taskListId);
        if (!$scope.$$phase) $scope.$apply();
    }
};

TaskDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller('TaskDetailController', TaskDetailController);