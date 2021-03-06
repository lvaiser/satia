(function () {

    app.controller("Agent.ConfirmEmailAndCreatePassword", ["$scope", "$http", controller]);

    function controller($scope, $http) {

        var empty = {
            enum: { key: 0, value: "-" },
            idCero: { id: 0, name: "-" },
            idNull: { id: null, name: "-" },
            culture: { id: null, displayName: "-"}
        };

        $scope.form = {};
        $scope.form.data = { }

        $scope.account = { };

        $scope.events = {
            onInit: onInit,
            onSaveClicked: onSaveClicked
        };

        $scope.validators = {
            selectErrorMessage: selectErrorMessage,
            matchPassword: matchPassword,
            validPassword: validPassword
        };

        function onInit(account) {
            $scope.account = account;
        }
        function onSaveClicked()
        {
            if (!validate($scope.pageForm)) {
                $scope.pageForm.$setDirty();
                $.notify("Existen campos con valores inválidos. Corrija los errores y vuelva a intentarlo", "error");
                return;
            }

            return $http.post("/Security/Account/ConfirmEmailAndCreatePassword", $scope.account)
                        .then(function (response) {
                            $.notify("Los datos se actualizaron exitosamente", "success");
                            window.location = "/Security/Account/Confirmed";
                        }, function (e) {
                            if (e.status == 400 || e.status == 409) {
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
        function matchPassword(value) {
            return value == $scope.account.password;
        }
        function validPassword(value)
        {
            return !!value
                && value.length > 6
                && /[0-9]/g.test(value)
                && /[a-z]/g.test(value)
                && /[A-Z]/g.test(value)
                && /[!@#\-_&]/g.test(value)
        }
    }
})();