(function () {
    'use strict';

    var serviceId = 'propertyService';

    angular.module('cmsApp').factory(serviceId,
        [
            '$http',
            function ($http) {

                var service = {
                    getProperties: getProperties,
                    getProperty: getProperty,
                    updateProperty: updateProperty,
                    createProperty: createProperty,
                    deleteProperty: deleteProperty,
                    getNextReference: getNextReference
                };

                return service;

                function getProperties() {
                    return $http
                        .get('/api/properties')
                        .then(function (response) {
                            return response.data;
                        });
                }

                function getProperty(propertyId) {
                    return $http
                        .get('/api/properties/' + propertyId)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function updateProperty(propertyId, property) {
                    return $http
                        .put('/api/properties/' + propertyId, property)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function createProperty(property) {
                    return $http
                        .post('/api/properties', property)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function deleteProperty(propertyId) {
                    return $http
                        .delete('/api/properties/' + propertyId)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function getNextReference() {
                    return $http.get('/api/properties/getNextReference')
                        .then(function (response) {
                            return response.data;
                        });
                }
                
            }
        ]
    );
})();