function EventSchedulesController( $scope, $resource, $location ) {
    var eventschedulesResource = $resource( '/api/eventschedules' );
   
        $scope.eventSchedules = eventschedulesResource.query( function () { } );
  }

EventSchedulesController.$inject = ['$scope', '$resource', '$location'];

app.controller( 'EventSchedulesController', EventSchedulesController );






