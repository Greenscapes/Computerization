function EventTaskListCreateController($scope, $resource, $routeParams, $location) {
    var eventScheduleResource = $resource('/api/eventschedules', {});

    $scope.taskEvents = [];
 //   GetEvents($scope.task.EventSchedules);

    loadEvents();

    //function GetEvents(data) {
    //    for (var i = 0; i < data.length; i++) {

    //        var event = data[i];
    //        var newEvent = new Object({
    //            taskId: event.Id,
    //            start: new Date(event.StartTime.toString()),
    //            end: new Date(event.EndTime.toString()),
    //            title: event.Title,
    //            isAllDay: event.IsAllDay,
    //            startTimezone: event.StartTimezone,
    //            endTimezone: event.EndTimezone,
    //            description: event.Description,
    //            recurrenceId: event.RecurrenceID,
    //            recurrenceRule: event.RecurrenceRule,
    //            recurrenceException: event.RecurrenceException

    //        });
    //        $scope.taskEvents.push(newEvent);

    //        // $( "#scheduler" ).data( "kendoScheduler" ).addEvent( newEvent );
    //    }

    //}

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
}

EventTaskListCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('EventTaskListCreateController', EventTaskListCreateController);