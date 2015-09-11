function EventSchedulesController( $scope, $resource, $location ) {
    var eventschedulesResource = $resource( '/api/eventschedules' );
   
    $scope.navigatorConfig = {
        selectMode: "day",
        showMonths: 3,
        skipMonths: 3,
        onTimeRangeSelected: function ( args ) {
            $scope.weekConfig.startDate = args.day;
            $scope.dayConfig.startDate = args.day;
            $scope.eventSchedules = eventschedulesResource.query( function () { } );
        }
    };

    $scope.weekConfig = {

        viewType: "Week"

    };


    $scope.showWeek = function () {
        $scope.dayConfig.visible = false;
        $scope.weekConfig.visible = true;
        $scope.navigatorConfig.selectMode = "week";
    };
    $scope.eventSchedules = eventschedulesResource.query( function () { } );



  }

EventSchedulesController.$inject = ['$scope', '$resource', '$location'];

app.controller( 'EventSchedulesController', EventSchedulesController );






