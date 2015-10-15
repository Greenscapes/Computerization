function PropertyCreateController($scope, $resource, $location) {
    var propertiesResource = $resource('/api/properties');

    $scope.property = {};
    $( '#datetimepicker' ).datepicker( " setDate", new Date() );
    $scope.property.State = "FL"
    $scope.save = function(property) {
        $scope.buttonsDisabled = true;
       
        propertiesResource.save(property, function () {
           
            $scope.buttonsDisabled = false;
                $scope.back();
            },
            function(error) {
                _showValidationErrors($scope, error)
                $scope.buttonsDisabled = false;
            },

            function () {
                $scope.buttonsDisabled = false;
            }
            );
    };
    
    $scope.back = function() {
        $location.path("/properties");
        if (!$scope.$$phase) $scope.$apply();
    };

    function _showValidationErrors($scope,error)
    {
        $scope.validationErrors = [];
        if (error.data && angular.isObject(error.data)) {
            for (var key in error.data);
            $scope.validationErrors.push(error.data[key]);
        }
        else {
            $scope.validationErrors.push("Could not add property");

        }
    };
    
    $(document).ready(function () {
        $('#datetimepicker').datepicker({
            format: 'mm-dd-yyyy',


        }

        );
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = d.getFullYear() + '/' +
        (month < 10 ? '0' : '') + month + '/' +
        (day < 10 ? '0' : '') + day;

        $("#datetimepicker").val(output + " 00:01:00");

        $('#datetimepicker').on('changeDate', function (ev) {
            ('#datetimepicker').valueOf(ev.target.value);
            $scope.property.ContractDate = ev.target.value;
        });
    });
    }


PropertyCreateController.$inject = ['$scope', '$resource', '$location'];
app.controller('PropertyCreateController', PropertyCreateController);