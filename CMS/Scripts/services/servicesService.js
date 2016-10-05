(function () {
    'use strict';

    var serviceId = 'servicesService';

    angular.module('cmsApp').factory(serviceId,
        [
            '$http',
            function ($http) {

                var service = {
                    getServices: getServices,
                    getService: getService,
                    updateService: updateService,
                    createService: createService,
                    deleteService: deleteService,
                    getPropertyService: getPropertyService,
                    updatePropertyService: updatePropertyService,
                    createPropertyService: createPropertyService,
                    deletePropertyService: deletePropertyService
                };

                return service;

                function getServices() {
                    return $http
                        .get('/api/services')
                        .then(function (response) {
                            return response.data;
                        });
                }

                function getService(serviceId) {
                    return $http
                        .get('/api/services/' + serviceId)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function updateService(service) {
                    return $http
                        .put('/api/services/' + service.Id, service)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function createService(service) {
                    return $http
                        .post('/api/services', service)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function deleteService(serviceId) {
                    return $http
                        .delete('/api/services/' + serviceId)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function getPropertyService(serviceId) {
                    return $http
                        .get('/api/services/property/' + serviceId)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function updatePropertyService(service) {
                    return $http
                        .put('/api/services/property/' + service.Id, service)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function createPropertyService(service) {
                    return $http
                        .post('/api/services/property/', service)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function deletePropertyService(serviceId) {
                    return $http
                        .delete('/api/services/property/' + serviceId)
                        .then(function (response) {
                            return response.data;
                        });
                }
            }
        ]
    );
})();