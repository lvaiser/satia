(function () {

    app.controller("Agent.Edit", ["$scope", "$http", controller]);

    function controller($scope, $http) {

        var empty = {
            enum: { key: 0, value: "-" },
            idCero: { id: 0, name: "-" },
            idNull: { id: null, name: "-" },
            culture: { id: null, displayName: "-"}
        };

        $scope.form = {};
        $scope.form.data = { }

        $scope.agent = {
            id: null
        };

        $scope.events = {
            onInit: onInit,
            onSaveClicked: onSaveClicked
        };

        $scope.validators = {
            selectErrorMessage: selectErrorMessage,
            noCero: noCero
        };

        function onInit(agent) {
            $scope.agent = agent;
        }
        function onSaveClicked()
        {
            if (!validate($scope.pageForm)) {
                $scope.pageForm.$setDirty();
                $.notify("Existen campos con valores inválidos. Corrija los errores y vuelva a intentarlo", "error");
                return;
            }

            var endpoint = $scope.agent.id ? "Edit" : "Register";

            return $http.post("/Admin/Agent/" + endpoint, $scope.agent)
                        .then(function (response) {
                            $.notify("Los datos se actualizaron exitosamente", "success");
                            if (!$scope.agent.id) {
                                window.location = "/Admin/Agent";
                            }
                        }, function(e)
                        {
                            if (e.status == 400 || e.status == 409)
                            {
                                Utils.showModelErrorCollection(e.data);
                                return;
                            }

                            Utils.onAjaxError(" al guardar los datos del asesor", e);
                        });
        }

        function validate(form) {
            angular.forEach(form, function (control, name) {
                // Excludes internal angular properties
                if (typeof name === 'string' && name.charAt(0) !== '$') {
                    // To display ngMessages
                    control.$setTouched();
                    control.$setDirty();

                    // Runs each of the registered validators
                    control.$validate();
                }
            });

            return form.$valid
        }
        function selectErrorMessage(control, messages) {
            if (!messages || !control)
                return;

            for (var prop in control.$error) {
                if (control.$error.hasOwnProperty(prop)) {
                    return messages[prop] || null;
                }
            }
        }
        function noCero(value) {
            return value != 0;
        }
    }
})();