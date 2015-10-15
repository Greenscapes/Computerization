function EventNotesController( $scope, $resource, $routeParams, $location, Modal ) {
    var eventNotesResource = $resource( '/api/eventnotes' );

    var eventNotebyScheduleResource = $resource( '/api/eventnotes/:eventscheduleId', { eventscheduleId: $routeParams.eventid } );
    $scope.eventid = $routeParams.eventid;
    $scope.back = function () {
        //$location.path( "/customerroutes" );
        //if ( !$scope.$$phase ) $scope.$apply();
    };

    $scope.propertyTaskEventNotes = eventNotebyScheduleResource.query( { eventscheduleId: $routeParams.eventid }, function () {
            
        });
    
    $scope.save = function ( propertyTaskEventNote ) {
        $scope.buttonsDisabled = true;
        
        propertyTaskEventNote.EventScheduleId = $scope.eventid;
        eventNotesResource.save( propertyTaskEventNote, function () {
            $scope.buttonsDisabled = false;
            $scope.back();
        },
            function () {
                $scope.buttonsDisabled = false;
            } );
    };

    
}

EventNotesController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller( 'EventNotesController', EventNotesController );