function EventTaskListCreateController($scope, $resource, $routeParams, $location) {
    var propertyResource = $resource('/api/properties/:propertyId');
    var eventTaskListResource = $resource('/api/eventtasklists');
    var eventTaskListResourceGet = $resource('/api/eventtasklists/:taskListId');
    var serviceTemplateResource = $resource('/api/servicetemplates');
    var crewsResource = $resource("/api/crews");

    $scope.eventTaskList = {};
    $scope.eventTaskList.EventSchedules = [];
    $scope.property = propertyResource.get({ propertyId: $routeParams.propertyId }, function() {
        $scope.eventTaskList.Name = $scope.property.Name;
    });
    if ($routeParams.eventTaskId) {
        $scope.eventTaskList = eventTaskListResourceGet.get({ taskListId: $routeParams.eventTaskId }, function () {
           
        });
    }
    $scope.selectedCrew = {};
    $scope.crews = crewsResource.query({});
    $scope.templates = serviceTemplateResource.query();
    $scope.taskEvents = [];

    $scope.crewChanged = function() {
        $scope.eventTaskList.Name = $scope.property.Name + ", " + $scope.selectedCrew.Name;
        $scope.eventTaskList.CrewId = $scope.selectedCrew.Id;
    }

    $scope.back = function () {
        $location.path("/properties/" + $routeParams.propertyId);// + "/tasklists/" + $routeParams.taskListId);
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.save = function (eventTaskList, goBack) {
        $scope.buttonsDisabled = true;
        //      var scheduler = $( "#scheduler" ).data( "kendoScheduler" );
        //       SetEventSchedules(scheduler._data);
        eventTaskList.PropertyId = $routeParams.propertyId;
      //  eventTaskList.Name = $scope.property.Name + ", " + $('#crewSelect option:selected').text();
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
}

EventTaskListCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('EventTaskListCreateController', EventTaskListCreateController);