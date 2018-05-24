(function () {
    'use strict';

    var serviceId = 'customerService';

    angular.module('cmsApp').factory(serviceId,
        [
            '$http',
            function ($http) {

                var service = {
                    getCustomers: getCustomers,
                    getCustomer: getCustomer,
                    updateCustomer: updateCustomer,
                    createCustomer: createCustomer,
                    deleteCustomer: deleteCustomer
                };

                return service;

                function getCustomers() {
                    return $http
                        .get('/api/customers')
                        .then(function (response) {
                            return response.data;
                        });
                }

                function getCustomer(customerId) {
                    return $http
                        .get('/api/customers/' + customerId)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function updateCustomer(customer) {
                    return $http
                        .put('/api/customers', customer)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function createCustomer(customer) {
                    return $http
                        .post('/api/customers', customer)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function deleteCustomer(customerId) {
                    return $http
                        .delete('/api/customers/' + customerId)
                        .then(function (response) {
                            return response.data;
                        });
                }
            }
        ]
    );
})();