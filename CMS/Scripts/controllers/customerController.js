(function (angular) {
    'use strict';

    var controllerId = 'CustomerController';

    angular.module('cmsApp').controller(controllerId,
        [
            'customerService', 'alertService', '$location', '$routeParams',
            function (customerService, alertService, $location, $routeParams) {

                var vm = this;

                vm.customer = {};
                vm.buttonsDisabled = false;
                vm.isUpdate = false;

                vm.save = save;
                vm.back = back;

                activate();

                function activate() {
                    var customerId = $routeParams.customerId;
                    if (customerId) {
                        customerService.getCustomer(customerId).then(function (data) {
                            vm.customer = data;
                            vm.isUpdate = true;
                        });
                    }
                }

                function save() {
                    vm.buttonsDisabled = true;

                    if (vm.isUpdate) {
                        customerService.updateCustomer(vm.customer).then(function() {
                            vm.buttonsDisabled = false;
                            vm.back();
                        }, function(error) {
                            _showValidationErrors(error);
                            vm.buttonsDisabled = false;
                        });
                    } else {
                        customerService.createCustomer(vm.customer).then(function () {
                            vm.buttonsDisabled = false;
                            vm.back();
                        }, function (error) {
                            _showValidationErrors(error);
                            vm.buttonsDisabled = false;
                        });
                    }
                };

                function back() {
                    $location.path("/customers");
                };

                function _showValidationErrors($scope, error) {
                    if (error.data && angular.isObject(error.data)) {
                        //for (var key in error.data) {
                        //    $scope.validationErrors.push(error.data[key]);
                        //}
                    }
                    else {
                        alertService.error("Could not add customer");
                    }
                };
            }
        ]
    );
})(angular);