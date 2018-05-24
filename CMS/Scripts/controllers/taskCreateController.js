function TaskCreateController($scope, $resource, $routeParams, $location, alertService) {
    var taskListResource = $resource('/api/tasklists/:taskListId',
    {
        taskListId: $routeParams.taskListId
    });
    var propertyResource = $resource('/api/properties/:propertyId');
    var tasksResource = $resource( '/api/tasks' );
    var locationsResource = $resource('/api/tasks/locations/:propertyId');

    var eventschedulesResource = $resource( '/api/eventschedules' );
    var taskTemplateResource = $resource('/api/taskTemplates/');

    $scope.taskTemplates = taskTemplateResource.query({});

    $scope.task = {};
    $scope.selectedTemplate = {};
    $scope.property = propertyResource.get({ propertyId: $routeParams.propertyId });
    $scope.locations = locationsResource.query({ propertyId: $routeParams.propertyId });
    $scope.taskList = taskListResource.get({}, function () {
        $scope.task.PropertyTaskDetails = [];
        //for (var i = 0; i < $scope.taskList.PropertyTaskListType.PropertyTaskHeaders.length; i++) {
        //    var newTaskDetail = {
        //        PropertyTaskHeaderId: $scope.taskList.PropertyTaskListType.PropertyTaskHeaders[i].Id,
        //        HeaderName: $scope.taskList.PropertyTaskListType.PropertyTaskHeaders[i].Name
        //    };

        //    $scope.task.PropertyTaskDetails.push(newTaskDetail);
        //}
        if (!$scope.$$phase) $scope.$apply();
    } );
    $scope.task.EventSchedules = [];
    var crewsResource = $resource( "/api/crews" );
    $scope.crews = crewsResource.query( function () { } );

    

        $scope.save = function(task) {
            $scope.buttonsDisabled = true;

            task.EstimatedDuration = task.Hours * 60 + task.Minutes;

            if (!task.EstimatedDuration || !task.Description) {
                alertService.error('You must enter a description and estimated duration');
                $scope.buttonsDisabled = false;
                return;
            }

            //var scheduler = $( "#scheduler" ).data( "kendoScheduler" );
            //SetEventSchedules( scheduler._data )
            task.PropertyTaskListId = $routeParams.taskListId;
            //task.crews = [];
            //for ( var i = 0; i < $scope.crews.length; i++ ) {
            //    if ( $scope.crews[i].checked ) {
            //        delete $scope.crews[i].checked;
            //        task.crews.push( $scope.crews[i] );
            //    }
            //}
          
            var response = tasksResource.save(task, function() {
                $scope.buttonsDisabled = false;
                $scope.task.Description = '';
                $scope.task.EstimatedDuration = '';
                $scope.task.Notes = '';

                $scope.locations = locationsResource.query({ propertyId: $routeParams.propertyId });
                    //  $scope.back();
                    //  if (!$scope.$$phase) $scope.$apply();
                },
                function() {
                    $scope.buttonsDisabled = false;
                });
      
            $scope.back = function() {
                $location.path("/properties/" + $routeParams.propertyId);// + "/tasklists/" + $routeParams.taskListId);
                if (!$scope.$$phase) $scope.$apply();
            };
        }

        $scope.templateSelected = function() {
            if ($scope.selectedTemplate) {
                $scope.task.Description = $scope.selectedTemplate.Description;
                $scope.task.EstimatedDuration = $scope.selectedTemplate.EstimatedDuration;
                $scope.task.Notes = $scope.selectedTemplate.Notes;
                $scope.task.IsFreeService = false;//$scope.selectedTemplate.IsFreeService;
            }
        }
      
        var scheduler = $( "#taskCreatescheduler" ).kendoScheduler( {
            date: new Date( ),
            startTime: new Date( ),
            height: 600,
            views: [
                "day",
                { type: "workWeek", selected: true },
                "week",
                "month",
            ],
            timezone: "Etc/UTC",
            save: scheduler_save,
            remove: scheduler_remove,
            edit: scheduler_edit,
            cancel: scheduler_cancel,
            dataBinding: scheduler_dataBinding,
            dataBound: scheduler_dataBound,
        } ).data( "kendoScheduler" );

        

       
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
 
TaskCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'alertService'];
app.controller('TaskCreateController', TaskCreateController);