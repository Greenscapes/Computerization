(function (angular) {
    'use strict';

    var controllerId = 'PropertyCreateController';

    angular.module('cmsApp').controller(controllerId,
        [
            'propertyService', 'customerService', 'cityService', 'alertService', '$location',
            function (propertyService, customerService, cityService, alertService, $location) {

                var vm = this;

                vm.property = {};
                vm.customers = {};
                vm.cities = {};
                vm.buttonsDisabled = false;
                vm.isUpdate = false;

                vm.save = save;
                vm.back = back;

                activate();

                function activate() {
                    customerService.getCustomers().then(function(data) {
                        vm.customers = data;
                    });

                    cityService.getCities()
                        .then(function(data) {
                            vm.cities = data;
                        });

                    propertyService.getNextReference().then(function (data) {
                        vm.property.PropertyRefNumber = data;
                    });

                    vm.property.State = "FL";
                }

                function save() {
                    vm.buttonsDisabled = true;

                    propertyService.createProperty(vm.property).then(function () {
                        vm.buttonsDisabled = false;
                        vm.back();
                    }, function (error) {
                        alertService.error("Could not add property: " + error);
                        vm.buttonsDisabled = false;
                    });
                };

                function back() {
                    $location.path("/properties");
                };

                angular.element(document).ready(function () {
                    $('#datetimepicker').datepicker({
                    }

                    );
                    var d = new Date();

                    var month = d.getMonth() + 1;
                    var day = d.getDate();
                    var year = d.getFullYear();

                    var output =
                    (month < 10 ? '0' : '') + month + '/' +
                    (day < 10 ? '0' : '') + day + '/' + year;

                    $("#datetimepicker").val(output);

                    $('#datetimepicker').on('changeDate', function (ev) {
                        ('#datetimepicker').valueOf(ev.target.value);
                        vm.property.ContractDate = ev.target.value;
                    });
                });
            }
        ]
    );
})(angular);