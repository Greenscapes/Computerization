function EventTaskListCreateController($scope, $resource, $routeParams, $location) {
    var propertyResource = $resource('/api/properties/:propertyId');
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
    $scope.templates = serviceTemplateResource.query();
    $scope.taskEvents = [];

    $scope.back = function () {
        $location.path("/properties/" + $routeParams.propertyId);// + "/tasklists/" + $routeParams.taskListId);
        if (!$scope.$$phase) $scope.$apply();
    };

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