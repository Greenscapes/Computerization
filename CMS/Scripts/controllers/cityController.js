(function (angular) {
    'use strict';

    var controllerId = 'CityController';

    angular.module('cmsApp').controller(controllerId,
        [
            'cityService', 'alertService', '$location', '$routeParams',
            function (cityService, alertService, $location, $routeParams) {

                var vm = this;

                vm.city = {};
                vm.buttonsDisabled = false;
                vm.isUpdate = false;

                vm.save = save;
                vm.back = back;

                activate();

                function activate() {
                    var cityId = $routeParams.cityId;
                    if (cityId) {
                        cityService.getCity(cityId).then(function (data) {
                            vm.city = data;
                            vm.isUpdate = true;
                        });
                    }
                }

                function save() {
                    vm.buttonsDisabled = true;

                    if (vm.isUpdate) {
                        cityService.updateCity(vm.city).then(function () {
                            vm.buttonsDisabled = false;
                            vm.back();
                        }, function (error) {
                            _showValidationErrors(error);
                            vm.buttonsDisabled = false;
                        });
                    } else {
                        cityService.createCity(vm.city).then(function () {
                            vm.buttonsDisabled = false;
                            vm.back();
                        }, function (error) {
                            _showValidationErrors(error);
                            vm.buttonsDisabled = false;
                        });
                    }
                };

                function back() {
                    $location.path("/cities");
                };

                function _showValidationErrors($scope, error) {
                    if (error.data && angular.isObject(error.data)) {
                        //for (var key in error.data) {
                        //    $scope.validationErrors.push(error.data[key]);
                        //}
                    }
                    else {
                        alertService.error("Could not add city");
                    }
                };
            }
        ]
    );
})(angular);