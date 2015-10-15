function EventNotesController( $scope, $resource, $routeParams, $location, Modal ) {
    var eventNotesResource = $resource( '/api/eventnotes' );
    var eventschedulesResource = $resource( '/api/eventschedules');
    var eventNotebyScheduleResource = $resource( '/api/eventnotes/:eventscheduleId', { eventscheduleId: $routeParams.eventid } );
    $scope.eventid = $routeParams.eventid;

    $( document ).ready( function () {
        $( '#datetimepickerstart' ).datepicker( {
            format: 'mm-dd-yyyy',


        }

        );
        $( '#datetimepickerend' ).datepicker( {
            format: 'mm-dd-yyyy',


        }

      );
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = d.getFullYear() + '/' +
        ( month < 10 ? '0' : '' ) + month + '/' +
        ( day < 10 ? '0' : '' ) + day;

        $( "#datetimepickerstart" ).val( output + " 00:01:00" );
        $( "#datetimepickerend" ).val( output + " 00:01:00" );

        $( '#datetimepickerstart' ).on( 'changeDate', function ( ev ) {
            ( '#datetimepickerstart' ).valueOf( ev.target.value );
            $scope.propertyTaskEventNote.ActualStartTime = ev.target.value;
        } );

        $( '#datetimepickerend' ).on( 'changeDate', function ( ev ) {
            ( '#datetimepickerend' ).valueOf( ev.target.value );
            $scope.propertyTaskEventNote.ActualEndTime = ev.target.value;
        } );
    } );
    $scope.back = function () {
        //$location.path( "/customerroutes" );
        //if ( !$scope.$$phase ) $scope.$apply();
    };
    
    $scope.eventSchedule = employeesResource.get( {}, function ()
    {
        $scope.propertyTaskEventNotes = eventNotebyScheduleResource.query( { eventscheduleId: $routeParams.eventid }, function () {
            
        });
    } );
    $scope.save = function ( propertyTaskEventNote ) {
        $scope.buttonsDisabled = true;
        
        eventschedulesResource.save( $scope.eventSchedule );

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