function CrewRouteController($scope, $resource, $routeParams, $location, Modal) {

    var propertyResource = $resource('/api/properties/:propertyId');
    var eventNotesResource = $resource('/api/eventnotes');


    var officeAddress = "8000 Fruitville rd, Sarasota, FL 34240";


    $scope.addressList = [];
    var infoWindow = new google.maps.InfoWindow();
    var geocoder = new google.maps.Geocoder();
    var directionsService = new google.maps.DirectionsService();
    var zoom_option = 6;
    var mapOptions = {
        zoom: 4,
        center: new google.maps.LatLng(40.0000, -98.0000),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }
    var path = [];

    $scope.map = new google.maps.Map(document.getElementById('map'), mapOptions);
    var directionsDisplay = new google.maps.DirectionsRenderer({ map: $scope.map });

    $scope.markers = [];

    var month = getDate().getMonth() + 1;
    var year = getDate().getFullYear();
    var date = getDate().getDate();

    function getDate()
    {
        var today = new Date(new Date().setHours(0, 0, 0, 0));
        var day = today.getDay();
        var date = today.getDate() - day + Number($routeParams.routeId);

        // Grabbing Start/End Dates
        return new Date(today.setDate(date));
    }

    var eventsResource = $resource('/api/eventschedules/:year/:month/:date/:crewid/events',
         {
             year: year,
             month: month,
             date: date,
             crewid: $routeParams.crewId
         },
            {

            });

    $scope.eventdetaillist = eventsResource.query({}, function() {
        $scope.addressList.push({ address: officeAddress, starttime: new Date(), endtime: new Date(), isFirstpoint: true });

        var uniqueAddress = [];
        for (var i = 0; i < $scope.eventdetaillist.length; i++) {
            var currentEvent = $scope.eventdetaillist[i];
            uniqueAddress.push(currentEvent.PropertyAddress);
        }

        uniqueAddress = uniqueAddress.unique();
        for (var i = 0; i < uniqueAddress.length; i++) {
            $scope.addressList.push({ address: uniqueAddress[i], isFirstpoint: false });
        }

        var waypts = [];
        for (var i = 0; i < $scope.addressList.length; i++) {
            waypts.push({
                location: $scope.addressList[i].address,
                stopover: false
            });
        }

        calculateAndDisplayRoute(directionsService, directionsDisplay, waypts);

        for (var i = 0; i < $scope.addressList.length; i++) {
            codeAddress($scope.addressList[i]); //comment this during testing
        }
    });

    function IndexByKeyValue(arraytosearch, key, valuetosearch) {

        for (var i = 0; i < arraytosearch.length; i++) {

            if (arraytosearch[i][key] == valuetosearch) {
                return i;
            }
        }
        return -1;
    }

    function codeAddress(addressdetails) {

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
                marker.title = '<div >' + addressdetails.address + '</div>';
                marker.content = '';
               // marker.content = '<div class="infoWindowContent">Start Time :' + addressdetails.starttime + '<br>End Time :' + addressdetails.endtime + '</div>';

                google.maps.event.addListener(marker, 'click', function () {
                    infoWindow.setContent('<h2>' + marker.title + '</h2>' + marker.content);
                    infoWindow.open($scope.map, marker);
                });
                $scope.markers.push(marker);
                var bounds = new google.maps.LatLngBounds();
                for (i = 0; i < $scope.markers.length ; i++) {
                    bounds.extend($scope.markers[i].position);
                }
                $scope.map.fitBounds(bounds);
            } else {
                alert("Geocode was not successful for the following reason: " + status);
            }
        });
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

    function combineAddress(property) {
        return property.Address1 + " " + property.Address1 + " " + property.City + " " + property.State + " " + property.Zip;
    }

    $scope.openInfoWindow = function (e, selectedMarker) {
        e.preventDefault();
        google.maps.event.trigger(selectedMarker, 'click');
    }

    Array.prototype.unique = function () {
        var arr = this;
        return $.grep(arr, function (v, i) {
            return $.inArray(v, arr) === i;
        });
    }
};



CrewRouteController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller('CrewRouteController', CrewRouteController);