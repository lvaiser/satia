(function () {

    app.controller("Process.Edit", ["$scope", "$http", controller]);

    function controller($scope, $http) {
        $scope.events = {
            onInit: onInit,
            onSaveClicked: onSaveClicked
        };

        function onInit(process) {
            $scope.process = process;
        }

        function onSaveClicked() {
            var action;
            if (!$scope.process.id) {
                action = "Create";
            } else {
                action = "Edit";
            }

            return $http.post("/Admin/Process/" + action, $scope.process)
                .then(function (response) {
                    window.location = '/Admin/Process/Edit/' + response.data.id + '?_=' + Math.random();
                    $.notify("Los datos se actualizaron exitosamente", "success");
                }, Utils.onAjaxError.bind(this, " al guardar el documento"));
        }
    }

})();