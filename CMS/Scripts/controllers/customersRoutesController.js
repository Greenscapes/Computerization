function CustomersRoutesController($scope, $resource, $routeParams, $location, Modal) {

    var propertiesResource = $resource( '/api/properties' );
    
    var crewsResource = $resource( "/api/crews" );
    $scope.crews = crewsResource.query( function () { } );
    $scope.selectedDate = new Date();

    function codeAddress( address ) {

        geocoder.geocode( { 'address': address }, function ( results, status ) {
            if ( status == google.maps.GeocoderStatus.OK ) {
                // $scope.map.setCenter( results[0].geometry.location );
                var marker = new google.maps.Marker( {
                    map: $scope.map,
                    position: results[0].geometry.location
                } );
               

                marker.content = '<div class="infoWindowContent">' + address + '</div>';

                google.maps.event.addListener( marker, 'click', function () {
                    infoWindow.setContent( '<h2>' + marker.title + '</h2>' + marker.content );
                    infoWindow.open( $scope.map, marker );
                } );
                $scope.markers.push( marker );
                var bounds = new google.maps.LatLngBounds();
                for ( var i in $scope.markers ) 
                    bounds.extend( $scope.markers[i].position )
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
            for ( var i = 0; i < $scope.eventdetaillist.length; i++ ) {
                var currentEvent = $scope.eventdetaillist[i];

                codeAddress( currentEvent.PropertyAddress );
            }
        }
   );
    };
    var mapOptions = {
        zoom: 4,
        center: new google.maps.LatLng( 40.0000, -98.0000 ),
        mapTypeId: google.maps.MapTypeId.TERRAIN
    }

    $scope.map = new google.maps.Map( document.getElementById( 'map' ), mapOptions );

    $scope.markers = [];

    var infoWindow = new google.maps.InfoWindow();
    var geocoder = new google.maps.Geocoder();
    var createMarker = function ( info ) {
      

        var marker = new google.maps.Marker( {
            map: $scope.map,
            position: new google.maps.LatLng( info.lat, info.long ),
            title: info.city
        } );
        marker.content = '<div class="infoWindowContent">' + info.desc + '</div>';

        google.maps.event.addListener( marker, 'click', function () {
            infoWindow.setContent( '<h2>' + marker.title + '</h2>' + marker.content );
            infoWindow.open( $scope.map, marker );
        } );

        $scope.markers.push( marker );

    }

   
    $scope.openInfoWindow = function ( e, selectedMarker ) {
        e.preventDefault();
        google.maps.event.trigger( selectedMarker, 'click' );
    }
    };

  



CustomersRoutesController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller( 'CustomersRoutesController', CustomersRoutesController );