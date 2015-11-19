function EventTaskListCreateController($scope, $resource, $routeParams, $location) {
    var propertyResource = $resource('/api/properties/:propertyId');
    //var taskResource = $resource('/api/tasks/:taskId',
    //{ taskId: $routeParams.taskId });
    var eventTaskListResource = $resource('/api/eventtasklists');
    var eventTaskListResourceGet = $resource('/api/eventtasklists/:taskListId');
    var serviceTemplateResource = $resource('/api/servicetemplates');
    var crewsResource = $resource("/api/crews");
    var today = new Date();

    $scope.eventTaskList = {};
    $scope.eventTaskList.EventSchedules = [];
    $scope.property = propertyResource.get({ propertyId: $routeParams.propertyId });
    if ($routeParams.eventTaskId) {
        $scope.eventTaskList = eventTaskListResourceGet.get({ taskListId: $routeParams.eventTaskId }, function () {
            setSchedulerOptions();
        });
    }
    $scope.crews = crewsResource.query({});

    //$scope.task = taskResource.get({ taskId: $routeParams.taskId }, function () {
    //    if ($routeParams.eventTaskId) {
    //        $scope.eventTaskList = eventTaskListResourceGet.get({ taskListId: $routeParams.eventTaskId }, function () {
    //            setSchedulerOptions();
    //        });
    //    }
    //});

    $scope.templates = serviceTemplateResource.query();

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

    $scope.back = function () {
        $location.path("/properties/" + $routeParams.propertyId);// + "/tasklists/" + $routeParams.taskListId);
        if (!$scope.$$phase) $scope.$apply();
    };

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

    $scope.save = function (eventTaskList, goBack) {
        $scope.buttonsDisabled = true;
        //      var scheduler = $( "#scheduler" ).data( "kendoScheduler" );
        //       SetEventSchedules(scheduler._data);
        eventTaskList.PropertyId = $routeParams.propertyId;
        eventTaskList.Name = $scope.property.Name + ", " + $('#crewSelect option:selected').text();
      //  eventTaskList.CrewId = $scope.task.Crews[0].Id;

        if (!eventTaskList.ServiceTemplateId) {
            alert("You must select a service tempalte");
            return;
        }

        var response = eventTaskListResource.save(eventTaskList, function () {
            $scope.buttonsDisabled = false;
            $scope.eventTaskList = response;
            $scope.back(goBack);
            if (!$scope.$$phase) $scope.$apply();
        },
            function () {
                $scope.buttonsDisabled = false;
            });

        $scope.back = function (goBack) {
            if (goBack) {
                $location.path("/properties/" + $routeParams.propertyId); // + "/tasklists/" + $routeParams.taskListId);
            } else {
                $location.path("/properties/" + $routeParams.propertyId + "/schedule/" + $scope.eventTaskList.Id);// + "/tasklists/" + $routeParams.taskListId);
            }
           
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

    function setSchedulerOptions() {
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
            editable: {
                template: $("#customEditorTemplate").html()
            },
            timezone: "America/New_York",
            dataBound: function (e) {
                $('div.k-event').removeClass('special-event');
                e.sender._data.forEach(function (eventDetails) {
                    if (eventDetails['ownerId'] === $scope.eventTaskList.Id) {
                        $('div.k-event[data-uid="' + eventDetails['uid'] + '"]').addClass('special-event');
                    }
                });
            },
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
                            return kendo.stringify(model);
                        }
                        if (operation === "read") {
                            return {
                                crewId: $scope.eventTaskList.CrewId
                            };
                        }
                    }
                },
                schema: {
                    model: {
                        id: "taskId",
                        fields: {
                            taskId: { from: "TaskID", type: "number" },
                            title: { from: "Title", defaultValue: $scope.eventTaskList.Name, validation: { required: true } },
                            start: { type: "date", from: "Start" },
                            end: { type: "date", from: "End" },
                            startTimezone: {
                                from: "StartTimezone", defaultValue: "America/New_York"
                            },
                            endTimezone: { from: "EndTimezone", defaultValue: "America/New_York" },
                            description: { from: "Description", defaultValue: "Crew: " + $scope.eventTaskList.Crew.Name
                        },
                            recurrenceId: { from: "RecurrenceID" },
                            recurrenceRule: { from: "RecurrenceRule" },
                            recurrenceException: { from: "RecurrenceException" },
                            ownerId: { from: "OwnerID", defaultValue: $scope.eventTaskList.Id },
                            isAllDay: { type: "boolean", from: "IsAllDay" },

                        }
                    }
                }
            }
        };
    }
}

EventTaskListCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('EventTaskListCreateController', EventTaskListCreateController);