(function () {
    'use strict';

    var serviceId = 'alertService';

    angular.module('cmsApp').factory(serviceId,
        [
            function () {

                toastr.options = defaultOptions();

                var service = {
                    error: error,
                    info: info,
                    success: success,
                    warning: warning
                };

                return service;

                function defaultOptions() {
                    return {
                        "closeButton": true,
                        "debug": false,
                        "positionClass": "toast-top-center",
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "11000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    };
                }

                //Enables window to stay open until user closes it.
                //Triggered by modal flag from toast call
                function setModal() {
                    toastr.options.timeOut = 0;
                    toastr.options.extendedTimeOut = 0;
                    toastr.options.closeButton = true;
                    toastr.options.closeHtml = '<button><i class="icon-off"></i></button>';
                }

                function error(text, title, modal) {
                    if (modal == true) {
                        setModal();
                    }
                    toastr.error(text, title);
                }

                function info(text, title, modal) {
                    if (modal == true) {
                        setModal();
                    }
                    toastr.info(text, title);
                }

                function success(text, title, modal) {
                    if (modal == true) {
                        setModal();
                    }
                    toastr.success(text, title);
                }

                function warning(text, title, modal) {
                    if (modal == true) {
                        setModal();
                    }
                    toastr.warning(text, title);
                }
            }
        ]
    );
})();