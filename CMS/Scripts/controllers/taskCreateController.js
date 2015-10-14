function TaskCreateController($scope, $resource, $routeParams, $location) {
    var taskListResource = $resource('/api/tasklists/:taskListId',
    {
        taskListId: $routeParams.taskListId
    });
    var tasksResource = $resource( '/api/tasks' );

    var eventschedulesResource = $resource( '/api/eventschedules' );
  

    $scope.task = {};
    $scope.taskList = taskListResource.get({}, function() {
        $scope.task.PropertyTaskDetails = [];
        for (var i = 0; i < $scope.taskList.PropertyTaskListType.PropertyTaskHeaders.length; i++) {
            var newTaskDetail = {
                PropertyTaskHeaderId: $scope.taskList.PropertyTaskListType.PropertyTaskHeaders[i].Id,
                HeaderName: $scope.taskList.PropertyTaskListType.PropertyTaskHeaders[i].Name
            };

            $scope.task.PropertyTaskDetails.push(newTaskDetail);
        }
        if (!$scope.$$phase) $scope.$apply();
    } );
    $scope.task.EventSchedules = [];
    var crewsResource = $resource( "/api/crews" );
    $scope.crews = crewsResource.query( function () { } );

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
        
        var newEvent = new Object( {
            startTime: args.start,
            endTime: args.end,
            title: name
        } );
        $scope.task.EventSchedules.push( newEvent )

    }
        dp.init();
        loadEvents();

        function loadEvents() {
            var start = dp.visibleStart();
            var end = dp.visibleEnd();
            dp.update();
        }
    

        $scope.save = function(task) {
            $scope.buttonsDisabled = true;
            task.PropertyTaskListId = $routeParams.taskListId;
            task.crews = [];
            for ( var i = 0; i < $scope.crews.length; i++ ) {
                if ( $scope.crews[i].checked ) {
                    delete $scope.crews[i].checked;
                    task.crews.push( $scope.crews[i] );
                }
            }
          
            var response = tasksResource.save(task, function() {
                $scope.buttonsDisabled = false;
                $scope.back();
                if (!$scope.$$phase) $scope.$apply();
            },
                function() {
                    $scope.buttonsDisabled = false;
                });
      
            $scope.back = function() {
                $location.path("/properties/" + $routeParams.propertyId + "/tasklists/" + $routeParams.taskListId);
                if (!$scope.$$phase) $scope.$apply();
            };
        }
    }
 
TaskCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('TaskCreateController', TaskCreateController);