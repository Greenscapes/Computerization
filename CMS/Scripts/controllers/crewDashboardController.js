function CrewDashboardController($scope, $resource, $routeParams, $location, Modal, $http, $rootScope) {
    var officeAddress = "8000 Fruitville rd, Sarasota, FL 34240";


    $scope.crewId = $routeParams.crewId;
    var crewsResource = $resource( "/api/crews" );
    $scope.crew = {};
    $scope.crews = crewsResource.query(function() {
        if ($routeParams.crewId) {
            $scope.crews.forEach(function(crew) {
                if (crew.Id == $routeParams.crewId) {
                    $scope.crew = crew;
                    $scope.getevents(true);
                }
            });
        }
    });
    
    $scope.selectedDate = new Date();
    $scope.addressList = [];
    var propertyLists = [];
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

    $scope.map = new google.maps.Map(document.getElementById('map'), mapOptions);
    var directionsDisplay = new google.maps.DirectionsRenderer({ map: $scope.map });

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
    $scope.getevents = function (getMap) {
      
        var month = $scope.selectedDate.getMonth()+1;
        var year = $scope.selectedDate.getFullYear();
        var date = $scope.selectedDate.getDate();

        $scope.crewId = $scope.crew.Id;
        $scope.isSelected = true;
        var eventsResource = $resource( '/api/eventschedules/:year/:month/:date/:crewid/events',
         {
             year: year,
             month: month,
             date: date,
             crewid: $scope.crew.Id

         },
            {

            } );

        $scope.eventdetaillist = eventsResource.query( {}, function ()
        {
            if (getMap) {
                $scope.addressList.push({ address: officeAddress, starttime: new Date(), endtime: new Date(), isFirstpoint: true });


                var uniqueAddress = [];
                for (var i = 0; i < $scope.eventdetaillist.length; i++) {
                    var currentEvent = $scope.eventdetaillist[i];
                    uniqueAddress.push(currentEvent.PropertyAddress);
                    if (IndexByKeyValue(propertyLists, "value", currentEvent.PropertyId) > -1) continue
                    propertyLists.push({ text: currentEvent.PropertyName + " </br>" + currentEvent.PropertyAddress, value: currentEvent.PropertyId })

                }
                uniqueAddress = uniqueAddress.unique();
                for (var i = 0; i < uniqueAddress.length; i++) {
                    $scope.addressList.push({ address: uniqueAddress[i], starttime: currentEvent.StartTime, endtime: currentEvent.EndTime, isFirstpoint: false });


                }
                propertyLists = uniquePropertyList(propertyLists);

                var waypts = [];
                for (var i = 0; i < $scope.addressList.length; i++) {
                    waypts.push({
                        location: $scope.addressList[i].address,
                        stopover: false
                    });
                }

                calculateAndDisplayRoute(directionsService, directionsDisplay, waypts);

                for (var i = 0; i < $scope.addressList.length; i++) {
                    codeAddress($scope.addressList[i]);//comment this during testing
                }
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

    $scope.setTime = function (isStart, event) {
        var date = new Date();
        var dateString = (date.getFullYear() + 1) + '-' + date.getMonth() + '-' + date.getDate() + ' ' + date.getHours() + '%3A' + date.getMinutes() + '%3A' + date.getSeconds();

        if (isStart) {
            $http.put('/api/eventtasklists/' + event.EventTaskListId + "/start/" + dateString)
                .success(function(data, status, headers) {
                    $scope.getevents();
                });
        } else {
            $http.put('/api/eventtasklists/' + event.EventTaskListId + "/finish/" + dateString)
                .success(function (data, status, headers) {
                    $scope.getevents();
                });
        }
    }

    $scope.launchTasks = function (event) {
        $location.path('/creweventtasks/' + event.EventTaskListId + "/" + $scope.crewId);
        if (!$scope.$$phase) $scope.$apply();
    }
   
    $scope.launchTicket = function (event) {
        var today = new Date();
        var ticketDate = today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + (today.getDate());
        $location.path('/servicetickets/' + event.EventTaskListId + "/" + ticketDate + "/");
        if (!$scope.$$phase) $scope.$apply();
    }

    function codeAddress( addressdetails ) {

        geocoder.geocode({ 'address': addressdetails.address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                // $scope.map.setCenter( results[0].geometry.location );
                var marker = new google.maps.Marker({
                    map: $scope.map,
                    position: results[0].geometry.location
                });
                if (addressdetails.isFirstpoint) {
                    marker.icon = '~/Content/action.ico';
                }
                path.push(results[0].geometry.location);
                var enddate =
                marker.title = '<div >' + addressdetails.address + '</div>';
                marker.content = '<div class="infoWindowContent">Start Time :' + dateFormat(addressdetails.starttime, "shortTime", true) + '<br>End Time :' + dateFormat(addressdetails.endtime, "shortTime", true) + '</div>';

                google.maps.event.addListener(marker, 'click', function () {
                    infoWindow.setContent('<h2>' + marker.title + '</h2>' + marker.content);
                    infoWindow.open($scope.map, marker);
                });
                $scope.markers.push(marker);
                var bounds = new google.maps.LatLngBounds();
                for (i = 0; i < $scope.markers.length ; i++) {
                    bounds.extend($scope.markers[i].position)
                }
                //var polyline = new google.maps.Polyline({
                //    map: $scope.map,
                //    path: path,
                //    strokeColor: '#0000FF',
                //    strokeOpacity: 0.7,
                //    strokeWeight: 1
                //});
                $scope.map.fitBounds(bounds);
            } else {
                alert("Geocode was not successful for the following reason: " + status);
            }
        });

        function combineAddress( property ) {
            return property.Address1 + " " + property.Address1 + " " + property.City + " " + property.State + " " + property.Zip;
        }

    }

    function calculateAndDisplayRoute(directionsService, directionsDisplay, waypts) {
        directionsService.route({
            origin: $scope.addressList[0].address,
            destination: $scope.addressList[0].address,
            waypoints: waypts,
            optimizeWaypoints: false,
            travelMode: google.maps.TravelMode.DRIVING
        }, function (response, status) {
            if (status === google.maps.DirectionsStatus.OK) {
                directionsDisplay.setDirections(response);
            } else {
                window.alert('Directions request failed due to ' + status);
            }
        });
    }

    $scope.openInfoWindow = function ( e, selectedMarker ) {
        e.preventDefault();
        google.maps.event.trigger( selectedMarker, 'click' );
    }
};

  

CrewDashboardController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal', '$http', '$rootScope'];
app.controller('CrewDashboardController', CrewDashboardController);