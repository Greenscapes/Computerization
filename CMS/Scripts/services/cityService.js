(function () {
    'use strict';

    var serviceId = 'cityService';

    angular.module('cmsApp').factory(serviceId,
        [
            '$http',
            function ($http) {

                var service = {
                    getCities: getCities,
                    getCity: getCity,
                    updateCity: updateCity,
                    createCity: createCity,
                    deleteCity: deleteCity
                };

                return service;

                function getCities() {
                    return $http
                        .get('/api/cities')
                        .then(function (response) {
                            return response.data;
                        });
                }

                function getCity(cityId) {
                    return $http
                        .get('/api/cities/' + cityId)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function updateCity(city) {
                    return $http
                        .put('/api/cities/' + city.Id, city)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function createCity(city) {
                    return $http
                        .post('/api/cities', city)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function deleteCity(cityId) {
                    return $http
                        .delete('/api/cities/' + cityId)
                        .then(function (response) {
                            return response.data;
                        });
                }
            }
        ]
    );
})();