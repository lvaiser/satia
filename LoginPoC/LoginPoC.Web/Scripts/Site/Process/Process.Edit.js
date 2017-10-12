(function () {

    app.controller("Process.Edit", ["$scope", "$http", controller]);

    function controller($scope, $http) {
        $scope.inputTextFields = ['String', 'FirstName', 'LastName', 'Address', 'Occupation'];
        $scope.textAreaFields = ['TextArea'];
        $scope.inputNumberFields = ['Integer'];
        $scope.decimalNumberFields = ['Decimal'];
        $scope.inputDateFields = ['Date', 'BirthDate'];
        $scope.radioButtonFields = ['Bool'];
        $scope.selectFields = ['Gender', 'MaritalStatus', 'Country', 'StateProvince', 'City'];
 
        $scope.events = {
            onInit: onInit,
            onSaveClicked: onSaveClicked
        };

        function onInit(process) {
            $scope.process = process;
        }

        $scope.simpleInputType = function (dataType) {
            return textField(dataType) || numberField(dataType) || dateField(dataType) || radioField(dataType);
        };

        $scope.stepInputType = function (dataType) {
            return stepNumberField(dataType);
        };

        $scope.textAreaType = function (dataType) {
            return $scope.textAreaFields.indexOf(dataType) !== -1;
        };

        $scope.selectType = function (dataType) {
            return $scope.selectFields.indexOf(dataType) !== -1;
        };

        $scope.inputType = function (dataType) {
            if (textField(dataType)) {
                return 'text';
            }
            if (numberField(dataType) || stepNumberField(dataType)) {
                return 'number';
            }
            if (dateField(dataType)) {
                return 'date';
            }
            if (radioField(dataType)) {
                return 'checkbox';
            }
        };

        function textField(dataType) {
            return $scope.inputTextFields.indexOf(dataType) !== -1;
        }

        function numberField(dataType) {
            return $scope.inputNumberFields.indexOf(dataType) !== -1;
        }

        function dateField(dataType) {
            return $scope.inputDateFields.indexOf(dataType) !== -1;
        }

        function radioField(dataType) {
            return $scope.radioButtonFields.indexOf(dataType) !== -1;
        }

        function stepNumberField(dataType) {
            return $scope.decimalNumberFields.indexOf(dataType) !== -1;
        }

        function onSaveClicked() {
            var action;
            if (!$scope.process.id) {
                action = "Create";
            } else {
                action = "Edit";
            }

            return $http.post("/Common/Process/" + action, $scope.process)
                .then(function (response) {
                    window.location = '/Common/Process/Edit/' + response.data.id + '?_=' + Math.random();
                    $.notify("Los datos se actualizaron exitosamente", "success");
                }, Utils.onAjaxError.bind(this, " al guardar el documento"));
        }
    }

})();