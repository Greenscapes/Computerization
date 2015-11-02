function CustomersRoutesController($scope, $resource, $routeParams, $location, Modal) {

    var propertiesResource = $resource( '/api/properties' );
    var eventNotesResource = $resource( '/api/eventnotes' );


    var officeAddress = "8000 Fruitville rd, Sarasota, FL 34240";


    var crewsResource = $resource( "/api/crews" );
    $scope.crews = crewsResource.query( function () { } );
    $scope.selectedDate = new Date();
    $scope.addressList = [];
    var propertyLists=[]
    var infoWindow = new google.maps.InfoWindow();
    var geocoder = new google.maps.Geocoder();
    var directionsService = new google.maps.DirectionsService();
    var zoom_option = 6;
    var mapOptions = {
        zoom: 4,
        center: new google.maps.LatLng( 40.0000, -98.0000 ),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }
    var path = [];

    $( "#panelbar" ).kendoPanelBar( {
        expandMode: "single"
    } );

    var directionsDisplay = new google.maps.DirectionsRenderer( { map: $scope.map } );

    $scope.map = new google.maps.Map( document.getElementById( 'map' ), mapOptions );

    $scope.markers = [];

   

    $scope.collapse = function ( event ) {
        $( event.target ).toggleClass( "glyphicon-chevron-down" );
    };
    $scope.collapsecrew = function ( event ) {
        $( event.target ).toggleClass( "glyphicon-arrow-down" );
    };
    $scope.reset = function () {
        $location.path( '/' );
    }
    $scope.getevents = function () {
      
        var month = $scope.selectedDate.getMonth()+1;
        var year = $scope.selectedDate.getFullYear();
        var date = $scope.selectedDate.getDate();

        $scope.isSelected = true;
        var eventsResource = $resource( '/api/eventschedules/:year/:month/:date/:crewid/events',
         {
             year: year,
             month: month,
             date: date,
             crewid: $scope.crew.Id.Id

         },
            {

            } );

        $scope.eventdetaillist = eventsResource.query( {}, function ()
        {
            $scope.addressList.push( { address: officeAddress, starttime: new Date(), endtime: new Date(), isFirstpoint: true } );


            var uniqueAddress = [];
            for ( var i = 0; i < $scope.eventdetaillist.length; i++ ) {
                var currentEvent = $scope.eventdetaillist[i];
                uniqueAddress.push( currentEvent.PropertyAddress );
                if ( IndexByKeyValue(propertyLists,"value",currentEvent.PropertyId) >-1 )continue
                propertyLists.push( { text: currentEvent.PropertyName + " </br>" + currentEvent.PropertyAddress, value: currentEvent.PropertyId } )
               
            }
            uniqueAddress = uniqueAddress.unique();
            for ( var i = 0; i < uniqueAddress.length; i++ ) {
                $scope.addressList.push( { address: uniqueAddress[i], starttime: currentEvent.StartTime, endtime: currentEvent.EndTime, isFirstpoint: false } );

               
            }
            propertyLists = uniquePropertyList(propertyLists);
            
            //Office Address
            GetEvents( $scope.eventdetaillist );
            loadEvents();
           
            for ( var i = 0; i < $scope.addressList.length; i++ ) {
                codeAddress( $scope.addressList[i] );//comment this during testing
            }
        }
   );
    };
    function IndexByKeyValue( arraytosearch, key, valuetosearch ) {

        for ( var i = 0; i < arraytosearch.length; i++ ) {

            if ( arraytosearch[i][key] == valuetosearch ) {
                return i;
            }
        }
        return -1;
    }
    
    function uniquePropertyList( a ) {
        
        a.sort();
        for ( var i = 1; i < a.length; ) {
            if ( a[i - 1].value == a[i].value ) {
                a.splice( i, 1 );
            } else {
                i++;
            }
        }
        return a;
    }
    Array.prototype.unique = function () {
        var arr = this;
        return $.grep( arr, function ( v, i ) {
            return $.inArray( v, arr ) === i;
        } );
    }

    function GetEvents( data ) {
        $scope.crewevents = [];
        for ( var i = 0; i < data.length; i++ ) {

            var event = data[i];
            var newEvent = new Object( {
                id: event.EventId,
                start: new Date( event.StartTime.toString() ),
                end: new Date( event.EndTime.toString() ),
                title: event.Title,
                isAllDay: event.IsAllDay,
                startTimezone: event.StartTimezone,
                endTimezone: event.EndTimezone,
                description: event.Description,
                recurrenceId: event.RecurrenceID,
                recurrenceRule: event.RecurrenceRule,
                recurrenceException: event.RecurrenceException,
                roomId: event.PropertyId

            } );
            $scope.crewevents.push( newEvent );

            // $( "#scheduler" ).data( "kendoScheduler" ).addEvent( newEvent );
        }

    }

    function launchTicket(events) {
        var event = events[0];

    }

    function loadEvents() {

        var CustomAgenda = kendo.ui.AgendaView.extend({
            endDate: function () {
               // var date = kendo.ui.AgendaView.fn.endDate.call(this);
                return new Date();
            }
        });

        var scheduler = $( "#crewscheduler" ).kendoScheduler( {
            date: new Date(),
            startTime: new Date(),
            height: 600,
            //editable: false,
            selectable: true,
            change: function (e) {
                launchTicket(e.events);
                $scope.selectState = e;
            },
            views: [
                 { type: CustomAgenda, title: "Crew Schedule", selected: true },
                
            ],
            
            dataSource: $scope.crewevents,
            group: {
                resources: ["Properties"],
                orientation: "vertical"
            },
            resources: [
                {
                    field: "roomId",
                    name: "Properties",
                    dataSource: propertyLists,
                    title: "Property"
                }
            ]
        } );
    }
   
    function codeAddress( addressdetails ) {

        geocoder.geocode( { 'address': addressdetails.address }, function ( results, status ) {
            if ( status == google.maps.GeocoderStatus.OK ) {
                // $scope.map.setCenter( results[0].geometry.location );
                var marker = new google.maps.Marker( {
                    map: $scope.map,
                    position: results[0].geometry.location
                } );
                if ( addressdetails.isFirstpoint ) {
                    marker.icon = '~/Content/action.ico';
                }
                path.push( results[0].geometry.location );
                var enddate =
                marker.title = '<div >' + addressdetails.address + '</div>';
                marker.content = '<div class="infoWindowContent">Start Time :' + addressdetails.starttime + '<br>End Time :' + addressdetails.endtime + '</div>';

                google.maps.event.addListener( marker, 'click', function () {
                    infoWindow.setContent( '<h2>' + marker.title + '</h2>' + marker.content );
                    infoWindow.open( $scope.map, marker );
                } );
                $scope.markers.push( marker );
                var bounds = new google.maps.LatLngBounds();
                for ( i = 0; i < $scope.markers.length ; i++ ) {
                    bounds.extend( $scope.markers[i].position )
                }
                var polyline = new google.maps.Polyline( {
                    map: $scope.map,
                    path: path,
                    strokeColor: '#0000FF',
                    strokeOpacity: 0.7,
                    strokeWeight: 1
                } );
                $scope.map.fitBounds( bounds );
            } else {
                alert( "Geocode was not successful for the following reason: " + status );
            }
        } );

        function combineAddress( property ) {
            return property.Address1 + " " + property.Address1 + " " + property.City + " " + property.State + " " + property.Zip;
        }

    }
    $scope.openInfoWindow = function ( e, selectedMarker ) {
        e.preventDefault();
        google.maps.event.trigger( selectedMarker, 'click' );
    }
    
    var contextMenuOpen = function (e) {
        var menu = e.sender;
        var event = e.target;
        
        var text = "";
        if ( $( e.target ).hasClass( "k-scheduler-table" ) )
            {
            text = "Add Note";
            
        }
        else
        {
            return;
        }
        menu.remove( ".myClass" );
        menu.append( [{ text: text, cssClass: "myClass" }] );
    };

    var contextMenuSelect = function ( e ) {
        var message = "Notes";
       
        var html =
             '<div id="myDialogWindow"> ' +
             ' <div style="text-align: center; width:100%"> ' +
             
             ' <label>Enter Notes</label><br/> <textarea type="text" id="txtNotes" cols="100" rows="5" />' +
             '   <button class="k-button" id="yesButton"">Save</button> ' +
             '   <button class="k-button" id="noButton"">Cancel</button> ' +
             '   </div> ' +
             '</div> ';

        $( 'body' ).append( html );

        var windowDiv = $( '#myDialogWindow' );
        windowDiv.kendoWindow( {
            width: "450px",
            title: message,
            modal: true,
            visible: false
        } );
        var txtNote = $( '#txtNotes' );
        var dialog = windowDiv.data( "kendoWindow" );

        $( '#yesButton' ).click( function ( e ) {

            var note = $( '#txtNotes' )[0].value;
            var state = $scope.selectState;
            var eventId = state.events[0].id;
            var propertyTaskEventNote = new Object( {
                Notes: note,
                EventScheduleId: eventId
            } );

            eventNotesResource.save( propertyTaskEventNote, function () {         
            } );
       

            dialog.close();
            $( '#myDialogWindow' ).remove();
            
           
        } );

        $( '#noButton' ).click( function ( e ) {
            dialog.close();
            $( '#myDialogWindow' ).remove();
          
            
        } );

        dialog.center();
        dialog.open();
    };

    //var destroyListener = $scope.$on( 'destroyDirective', function () {
    //    $scope.$destroy();
    //} );

    

    //$scope.$on( '$destroy', function () {
    //    destroyListener();
    //   // element.remove();
    //    $( "#crewcontextMenu" ).remove();
    //} );

    //$( "#crewcontextMenu" ).kendoContextMenu( {
    //    filter: ".k-event, .k-scheduler-table",
    //    showOn: "click",
    //    select: contextMenuSelect,
    //    open: contextMenuOpen
    //} );

    function scheduler_edit( e ) {
        alert("edit")
    }
};

  

CustomersRoutesController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller( 'CustomersRoutesController', CustomersRoutesController );