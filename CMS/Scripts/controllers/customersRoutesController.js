function CustomersRoutesController($scope, $resource, $routeParams, $location, Modal) {

    var propertiesResource = $resource( '/api/properties' );
    


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



    var directionsDisplay = new google.maps.DirectionsRenderer( { map: $scope.map } );

    $scope.map = new google.maps.Map( document.getElementById( 'map' ), mapOptions );

    $scope.markers = [];

   


    var crewsResource = $resource( "/api/crews" );
    $scope.crews = crewsResource.query( function () { } );
    $scope.selectedDate = new Date();
    $scope.addressList = [];
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
                marker.title = '<div >' + addressdetails.address +'</div>';
                marker.content = '<div class="infoWindowContent">Start Time :' + addressdetails.starttime +  '<br>End Time :' + addressdetails.endtime + '</div>';

                google.maps.event.addListener( marker, 'click', function () {
                    infoWindow.setContent( '<h2>' + marker.title + '</h2>' + marker.content );
                    infoWindow.open( $scope.map, marker );
                } );
                $scope.markers.push( marker );
                var bounds = new google.maps.LatLngBounds();
                for ( var i in $scope.markers ) {
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


    var nav = new DayPilot.Navigator( "nav" );
    nav.showMonths = 3;
    nav.skipMonths = 3;
    nav.orientation = "horizontal"
    nav.selectMode = "week";
    nav.onTimeRangeSelected = function ( args ) {
        $scope.selectedDate = args.day;
      
    };
    nav.init();

    $scope.collapse = function ( event ) {
        $( event.target ).toggleClass( "glyphicon-chevron-down" );
    };
    $scope.collapsecrew = function ( event ) {
        $( event.target ).toggleClass( "glyphicon-arrow-down" );
    };

    $scope.getevents = function () {
       
        var month = $scope.selectedDate.getMonth()+1;
        var year = $scope.selectedDate.getFullYear();
        var date = $scope.selectedDate.getDate();
        var eventsResource = $resource( '/api/eventschedules/:year/:month/:date/:crewid/events',
         {
             year: year,
             month: month,
             date: date,
             crewid: $scope.crew.Id.Id

         },
            {

            } );

        $scope.eventdetaillist = eventsResource.query( {}, function () {
            $scope.addressList.push( { address: "3600 Country Club Dr, Jefferson City, MO 65109", starttime:  new Date(), endtime: new Date(),isFirstpoint:true } );

            for ( var i = 0; i < $scope.eventdetaillist.length; i++ ) {
                var currentEvent = $scope.eventdetaillist[i];
                var found = jQuery.inArray( currentEvent.PropertyAddress, $scope.addressList );
                if ( found >= 0 ) {
                    continue;
                }
                $scope.addressList.push( { address: currentEvent.PropertyAddress, starttime: currentEvent.StartTime, endtime: currentEvent.EndTime ,isFirstpoint:false } );
               
            }
            //Office Address
           
           
            for ( var i = 0; i < $scope.addressList.length; i++ ) {
                codeAddress( $scope.addressList[i] );
            }
        }
   );
    };


    $scope.openInfoWindow = function ( e, selectedMarker ) {
        e.preventDefault();
        google.maps.event.trigger( selectedMarker, 'click' );
    }




};

  

CustomersRoutesController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller( 'CustomersRoutesController', CustomersRoutesController );