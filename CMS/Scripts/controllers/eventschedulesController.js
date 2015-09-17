function EventSchedulesController( $scope, $resource, $location ) {
    var eventschedulesResource = $resource( '/api/eventschedules' );
    $scope.eventSchedules = eventschedulesResource.query( function () { } );

    
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
    // event creating
    dp.onTimeRangeSelected = function ( args ) {
        var name = "EVENT ORDER";
        dp.clearSelection();
        if ( !name ) return;
        var e = new DayPilot.Event( {
            start: args.start,
            end: args.end,
            id: DayPilot.guid(),
            resource: args.resource,
            text: name
        } );
        var newEvent = new Object( {
            startTime: args.start,
            endTime: args.end,
            title: name
        } );
        dp.events.add( e );
        eventschedulesResource.save( newEvent, function () {
          
           
        } );

       

    };

    dp.init();
    loadEvents();

    function loadEvents() {
        var start = dp.visibleStart();
        var end = dp.visibleEnd();


        dp.events.list = $scope.eventSchedules;
        dp.update();
    }
   
  }

EventSchedulesController.$inject = ['$scope', '$resource', '$location'];

app.controller( 'EventSchedulesController', EventSchedulesController );






