(function () {

    app.controller("Process.Index", ["$scope", "$http", controller]);

    function controller($scope, $http) {
        $scope.events = {
            onInit: onInit
        };

        function onInit(model) {
            $scope.processes = model.processes;
            $scope.processTypes = model.processTypes;
        }
        
    }

})();