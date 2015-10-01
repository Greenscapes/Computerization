function PropertyDetailController($scope, $resource, $routeParams, $location, Modal) {
    var propertyResource = $resource('/api/properties/:propertyId');
    var taskListsResource = $resource( '/api/properties/:propertyId/tasklists', { propertyId: $routeParams.propertyId } );

    var eventschedulesResource = $resource( '/api/eventschedules/:propertyId/:allProperty/propertyevents',
   {
       propertyId: $routeParams.propertyId,
       allProperty:true
   },
   {

   } );

    $scope.property = propertyResource.get({ propertyId: $routeParams.propertyId } );
    $scope.taskLists = taskListsResource.query( function () {
        $scope.empEvents = [];
        $scope.eventlist = eventschedulesResource.query( {}, function () {

            for ( var k = 0; k < $scope.eventlist.length; k++ ) {
                var empevent = $scope.eventlist[k];
                var taskEvent = new Object( {
                    start: empevent.StartTime.toString(),
                    end: empevent.EndTime.toString(),
                    text: empevent.Title,
                    id: empevent.Id.toString()
                } );

                $scope.empEvents.push( taskEvent );
                loadEvents();

            }

        } );

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


        }
        dp.init();
        loadEvents();


        function loadEvents() {
            var start = dp.visibleStart();
            var end = dp.visibleEnd();
            dp.events.list = $scope.empEvents;
            dp.update();
        }

    } );

    $scope.newTaskList = function () {
        $location.path('/properties/' + $routeParams.propertyId + '/tasklists/new');
    };

    var deleteFunction = function () {
        $scope.buttonsDisabled = true;
        propertyResource.delete({ propertyId: $routeParams.propertyId }, $scope.property, function () {
                $scope.back();
            },
            function () {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.confirmDelete = function () {
        Modal.showConfirmDelete("property", $scope.property.Name, $scope.property, deleteFunction);
    };

    $scope.back = function () {
        $location.path("/properties");
        if (!$scope.$$phase) $scope.$apply();
    };
}

PropertyDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller('PropertyDetailController', PropertyDetailController);