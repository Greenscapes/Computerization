(function () {
    'use strict';

    var directiveId = 'sortby';

    angular.module('cmsApp').directive(directiveId, [function () {
        return {
            restrict: 'E',
            transclude: true,
            templateUrl: '/partials/sortBy.html',

            scope: {
                neworder: '=',
                oldorder: '=',
                reverse: '='
            },

            link: function (scope, element, attrs) {
                scope.sortClick = function () {

                    if (scope.neworder === scope.oldorder) {
                        // we are changing order on same column.
                        scope.reverse = !scope.reverse;
                    } else {
                        // change column to sort by
                        scope.oldorder = scope.neworder;

                        // first search is always ascending.
                        scope.reverse = false;
                    }
                }
            }
        }
    }]);
})();