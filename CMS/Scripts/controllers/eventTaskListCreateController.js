function EventTaskListCreateController($scope, $resource, $routeParams, $location) {
    var eventScheduleResource = $resource('/api/eventschedules/:crewId/crewevents', {});
    var propertyResource = $resource('/api/properties/:propertyId');
    var taskResource = $resource('/api/tasks/:taskId',
    { taskId: $routeParams.taskId });
    var eventTaskListResource = $resource('/api/eventtasklists');
    var eventTaskListResourceGet = $resource('/api/eventtasklists/:taskListId');

    var today = new Date();

    $scope.eventTaskList = {};
    $scope.eventTaskList.EventSchedules = [];
    $scope.property = propertyResource.get({ propertyId: $routeParams.propertyId });
    $scope.task = taskResource.get({ taskId: $routeParams.taskId }, function () {
        $scope.crewEventSchedules = eventScheduleResource.query({ crewId: $scope.task.Crews[0].Id }, function() {
     //       GetEvents($scope.crewEventSchedules);
        });
    });

    if ($routeParams.eventTaskId) {
        $scope.eventTaskList = eventTaskListResourceGet.get({ taskListId: $routeParams.eventTaskId });
    }

    $scope.taskEvents = [];

  //  loadEvents();

    function GetEvents(data) {
        for (var i = 0; i < data.length; i++) {

            var event = data[i];
            var newEvent = new Object({
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

            });
            $scope.taskEvents.push(newEvent);

            // $( "#scheduler" ).data( "kendoScheduler" ).addEvent( newEvent );
        }

    }

    function loadEvents() {

        var scheduler = $("#scheduler").kendoScheduler({
            date: new Date(),
            startTime: new Date(),
            height: 600,
            views: [
                "day",
                { type: "workWeek", selected: true },
                "week",
                "month",
            ],
            dataSource: $scope.taskEvents
        }).data("kendoScheduler");


    }

    $scope.save = function (eventTaskList) {
        $scope.buttonsDisabled = true;
  //      var scheduler = $( "#scheduler" ).data( "kendoScheduler" );
 //       SetEventSchedules(scheduler._data);
        eventTaskList.PropertyId = $routeParams.propertyId;
        eventTaskList.CrewId = $scope.task.Crews[0].Id;
        
        var response = eventTaskListResource.save(eventTaskList, function () {
            $scope.buttonsDisabled = false;
                $scope.eventTaskList = response;
                // $scope.back();
                // if (!$scope.$$phase) $scope.$apply();
            },
            function () {
                $scope.buttonsDisabled = false;
            });

        $scope.back = function () {
            $location.path("/properties/" + $routeParams.propertyId);// + "/tasklists/" + $routeParams.taskListId);
            if (!$scope.$$phase) $scope.$apply();
        };
    }

    function SetEventSchedules(data) {
        for (var i = 0; i < data.length; i++) {

            var event = data[i];
            var newEvent = new Object({
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

            });
            $scope.eventTaskList.EventSchedules.push(newEvent);
        }

    }

    $scope.schedulerOptions = {
        date: new Date(),
        startTime: new Date(today.getYear(), today.getMonth(), today.getDay(), 8, 0, 0),
        height: 600,
        views: [
            "day",
            { type: "workWeek", selected: true },
            "week",
            "month",
        ],
        timezone: "Etc/UTC",
        dataSource: {
            batch: false,
            transport: {
                read: {
                    url: "/api/event",
                    type: "get",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                },
                update: {
                    url: "/api/event",
                    type: "put",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                },
                create: {
                    url: "/api/event",
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                },
                destroy: {
                    url: "/api/event",
                    type: "delete",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                },
                parameterMap: function (model, operation) {
                    if (operation !== "read" && model) {
                        model.ownerId = $scope.eventTaskList.Id;
                        return kendo.stringify(model) ;
                    }
                }
            },
            schema: {
                model: {
                    id: "taskId",
                    fields: {
                        taskId: { from: "TaskID", type: "number" },
                        title: { from: "Title", defaultValue: "No title", validation: { required: true } },
                        start: { type: "date", from: "Start" },
                        end: { type: "date", from: "End" },
                        startTimezone: { from: "StartTimezone" },
                        endTimezone: { from: "EndTimezone" },
                        description: { from: "Description" },
                        recurrenceId: { from: "RecurrenceID" },
                        recurrenceRule: { from: "RecurrenceRule" },
                        recurrenceException: { from: "RecurrenceException" },
                        ownerId: { from: "OwnerID", defaultValue: $scope.eventTaskList.Id },
                        isAllDay: { type: "boolean", from: "IsAllDay" },

                    }
                }
            },
            filter: {
                logic: "or",
                filters: [
                    { field: "ownerId", operator: "eq", value: 1 },
                    { field: "ownerId", operator: "eq", value: 2 }
                ]
            }
        },
        resources: [
            {
                field: "ownerId",
                title: "Owner",
                dataSource: [
                    { text: "Alex", value: 1, color: "#f8a398" },
                    { text: "Bob", value: 2, color: "#51a0ed" },
                    { text: "Charlie", value: 3, color: "#56ca85" }
                ]
            }
        ]
    };
}

EventTaskListCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('EventTaskListCreateController', EventTaskListCreateController);