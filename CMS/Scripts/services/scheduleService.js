(function () {
    'use strict';

    var serviceId = 'scheduleService';

    angular.module('cmsApp').factory(serviceId,
        [
            '$http',
            function ($http) {

                var service = {
                    getHolidays: getHolidays,
                    getHoliday: getHoliday,
                    updateHoliday: updateHoliday,
                    createHoliday: createHoliday,
                    deleteHoliday: deleteHoliday
                };

                return service;

                function getHolidays() {
                    return $http
                        .get('/api/holidays')
                        .then(function (response) {
                            return response.data;
                        });
                }

                function getHoliday(holidayId) {
                    return $http
                        .get('/api/holidays/' + holidayId)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function updateHoliday(holiday) {
                    return $http
                        .put('/api/holidays/' + holiday.Id, holiday)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function createHoliday(holiday) {
                    return $http
                        .post('/api/holidays', holiday)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function deleteHoliday(holidayId) {
                    return $http
                        .delete('/api/holidays/' + holidayId)
                        .then(function (response) {
                            return response.data;
                        });
                }
            }
        ]
    );
})();