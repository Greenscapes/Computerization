(function () {
    'use strict';

    var directiveId = 'tooltip';

    angular.module('cmsApp').directive(directiveId, [function () {
        return {
            restrict: 'A',

            link: function (scope, element, attrs) {
                $(element).hover(function () {
                    // on mouseenter
                    $(element).tooltip('show');
                }, function () {
                    // on mouseleave
                    $(element).tooltip('hide');
                });
            }
        }
    }]);
})();