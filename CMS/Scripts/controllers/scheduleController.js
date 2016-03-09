function ScheduleController($scope, $resource, $routeParams, $location) {
    var propertyResource = $resource('/api/properties/');
    //var taskResource = $resource('/api/tasks/:taskId',
    //{ taskId: $routeParams.taskId });
    var eventTaskListResource = $resource('/api/eventtasklists');
    //var eventTaskListResourceGet = $resource('/api/eventtasklists/:taskListId');
    //var serviceTemplateResource = $resource('/api/servicetemplates');
    var crewsResource = $resource("/api/crews");
    var today = new Date();

    $scope.selectedCrewId = 0;
    $scope.selectedPropertyId = 0;
    $scope.eventTaskList = { Name: '' };
    $scope.eventTaskLists = [];
    $scope.filteredTaskLists = [];
    //$scope.eventTaskList.EventSchedules = [];
    //$scope.property = propertyResource.get({ propertyId: $routeParams.propertyId });
    //if ($routeParams.eventTaskId) {
    $scope.eventTaskLists = eventTaskListResource.query({ }, function () {
        setSchedulerOptions();
        $scope.filteredTaskLists = $scope.eventTaskLists;
    });
    //}
    $scope.crews = crewsResource.query({});
    $scope.properties = propertyResource.query({});

    //$scope.task = taskResource.get({ taskId: $routeParams.taskId }, function () {
    //    if ($routeParams.eventTaskId) {
    //        $scope.eventTaskList = eventTaskListResourceGet.get({ taskListId: $routeParams.eventTaskId }, function () {
    //            setSchedulerOptions();
    //        });
    //    }
    //});

   // $scope.templates = serviceTemplateResource.query();

   // $scope.taskEvents = [];

    //  loadEvents();

    $scope.getEventsForFilter = function (filter) {
        var tasksLists = [];
        $scope.eventTaskLists.forEach(function(taskList) {
            if (taskList.Name.toLowerCase().indexOf(filter.toLowerCase()) > -1) {
                tasksLists.push(taskList);
            }
        });
        return tasksLists;
    }

    $scope.createSchedule = function() {
        var scheduler = $(".k-scheduler").data("kendoScheduler");
        scheduler.addEvent({
            start: new Date(),
            end: new Date(),
            StartDate: new Date()
        });
    }

    $scope.filter = function(type) {
        switch (type) {
            case 'none':
                $scope.filteredTaskLists = $scope.eventTaskLists;
                break;
            case 'crew':
                $scope.filteredTaskLists = [];
                $scope.eventTaskLists.forEach(function(item) {
                    if (item.CrewId == $scope.selectedCrewId) {
                        $scope.filteredTaskLists.push(item);
                    }
                });
                break;
            case 'property':
                $scope.filteredTaskLists = [];
                $scope.eventTaskLists.forEach(function (item) {
                    if (item.PropertyId == $scope.selectedPropertyId) {
                        $scope.filteredTaskLists.push(item);
                    }
                });
                break;
            case 'eventTaskList':
                $scope.filteredTaskLists = [];
                $scope.eventTaskLists.forEach(function (item) {
                    if (item.Id == $scope.eventTaskList.Id) {
                        $scope.filteredTaskLists.push(item);
                    }
                });
                break;
        }

        var scheduler = $(".k-scheduler").data("kendoScheduler");
        scheduler.dataSource.read();
        scheduler.refresh();
    }

    function setSchedulerOptions() {
        $scope.theContent = kendo.template($("#template").html()),

        $scope.schedulerOptions = {
            allDaySlot: false,
            date: new Date(),
            startTime: new Date(today.getYear(), today.getMonth(), today.getDate(), 6, 30, 0),
            endTime: new Date(today.getYear(), today.getMonth(), today.getDate(), 18, 30, 0),
            height: 600,
            views: [
                "day",
                { type: "workWeek", selected: true },
                "week",
                "month",
            ],
            messages: {
                day: "Date ",
              recurrenceEditor: {
                  monthly: {
                      day: "date "
                  },
                  weekdays: {
                      day: "Date "
                  }
              }  
            },
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
            addEvent: function () {
                $scope.scheduler.addEvent({ title: "" });
            },
            edit: function(e) {
                e.event.set('StartDate', new Date(e.event.start.getFullYear(), e.event.start.getMonth(), e.event.start.getDate()));
                var recurrenceEditor = e.container.find("[data-role=recurrenceeditor]").data("kendoRecurrenceEditor");

                recurrenceEditor.setOptions({
                    start: new Date(e.event.start)
                });

                var startWidget = e.container.find("[data-role=datepicker]").filter("[name=StartDate]").data("kendoDatePicker");
                startWidget.setOptions({
                    change: function (dateEvent) {
                        recurrenceEditor.setOptions({
                            start: new Date(dateEvent.sender._value)
                        });
                    }
                });

                $('input').click(function () {
                 var input_class = $(this).attr('k-recur-weekday-checkbox');

                    $('.' + input_class).prop('checked', false);

                    $(this).prop('checked', true);
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
                            model.OwnerID = $scope.eventTaskList.Id;
                            model.ownerId = $scope.eventTaskList.Id;
                            model.Start = new Date(model.StartDate.getFullYear(), model.StartDate.getMonth(), model.StartDate.getDate(), model.Start.getHours(), model.Start.getMinutes(), 0);
                            model.End = new Date(model.StartDate.getFullYear(), model.StartDate.getMonth(), model.StartDate.getDate(), model.End.getHours(), model.End.getMinutes(), 0);
                            return kendo.stringify(model);
                        }
                        if (operation === "read") {
                            return {
                                crewId: $scope.eventTaskList.CrewId
                            };
                        }
                    }
                },
                filter: {
                    field: "ownerId",
                    operator: function (item, value) {
                        var found = false;
                        for (var i = 0; i < $scope.filteredTaskLists.length; i++) {
                            if (item == $scope.filteredTaskLists[i].Id) {
                                found = true;
                            }
                        }
                        return found;
                    }
                },
                schema: {
                    model: {
                        id: "taskId",
                        fields: {
                            taskId: { from: "TaskID", type: "number" },
                            title: { from: "Title", defaultValue: $scope.eventTaskList.Name },
                            start: { type: "date", from: "Start" },
                            end: { type: "date", from: "End" },
                            StartDate: { type: "date", fromt: "StartDate" },
                            startTimezone: {
                                from: "StartTimezone", defaultValue: "America/New_York"
                            },
                            endTimezone: { from: "EndTimezone", defaultValue: "America/New_York" },
                            description: {
                                from: "Description", defaultValue: ""
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

ScheduleController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('ScheduleController', ScheduleController);