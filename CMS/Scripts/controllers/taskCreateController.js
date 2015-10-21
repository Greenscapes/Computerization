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

    

        $scope.save = function(task) {
            $scope.buttonsDisabled = true;
            var scheduler = $( "#scheduler" ).data( "kendoScheduler" );
            SetEventSchedules( scheduler._data )
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

      
        var scheduler = $( "#scheduler" ).kendoScheduler( {
            date: new Date( ),
            startTime: new Date( ),
            height: 600,
            views: [
                "day",
                { type: "workWeek", selected: true },
                "week",
                "month",
            ],
         
            save: scheduler_save,
            remove: scheduler_remove,
            edit: scheduler_edit,
            cancel: scheduler_cancel,
            dataBinding: scheduler_dataBinding,
            dataBound: scheduler_dataBound,
        } ).data( "kendoScheduler" );

        //var contextMenuOpen = function ( e ) {
        //    var menu = e.sender;
        //    var text = $( e.target ).hasClass( "k-event" ) ? "Edit event" : "Add Event";

        //    menu.remove( ".myClass" );
        //    menu.append( [{ text: text, cssClass: "myClass" }] );
        //};

        //var contextMenuSelect = function ( e ) {
        //    var state = selectState;

        //    if ( state.events.length ) {
        //        scheduler.editEvent( state.events[0] );
        //    } else {
        //        scheduler.addEvent( {
        //            start: state.start,
        //            end: state.end
        //        } );
        //    }
        //};

        //$( "#contextMenu" ).kendoContextMenu( {
        //    filter: ".k-event, .k-scheduler-table",
        //    showOn: "click",
        //    select: contextMenuSelect,
        //    open: contextMenuOpen
        //} );

       
        function SetEventSchedules( data )
        {
            for ( var i = 0; i < data.length; i++ ) {

                var event = data[i];
                var newEvent = new Object( {
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
        function scheduler_save( e ) {
       
        }

        function scheduler_remove( e ) {
          
        }

        function scheduler_cancel( e ) {
            
        }

        function scheduler_edit( e ) {
           
        }
        function scheduler_dataBinding( e ) {
          
        }

    function scheduler_dataBound(e) {
       
    }

    }
 
TaskCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('TaskCreateController', TaskCreateController);