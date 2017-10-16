(function () {

    app.controller("Process.Edit", ["$scope", "$http", controller]);

    function controller($scope, $http) {
        $scope.inputTextFields = ['String', 'Address', 'Occupation', 'StateProvince', 'City'];
        $scope.noNumbersTextFields = ['FirstName', 'LastName'];
        $scope.textAreaFields = ['TextArea'];
        $scope.inputNumberFields = ['Integer'];
        $scope.decimalNumberFields = ['Decimal'];
        $scope.inputDateFields = ['Date', 'BirthDate'];
        $scope.radioButtonFields = ['Bool'];
        $scope.selectFields = ['Gender', 'MaritalStatus', 'Country'];
 
        $scope.events = {
            onInit: onInit,
            onSaveClicked: onSaveClicked,
            onSendToReviewClicked: onSendToReviewClicked
        };

        function onInit(process) {
            $scope.process = process;
            $scope.preParseValues();
        }

        $scope.preParseValues = function () {
            angular.forEach($scope.process.fields, function (field) {
                if (dateField(field.type)) {
                    field.value = new Date(field.value);
                }
                if (radioField(field.type)) {
                    field.value = field.value == 'True';
                }
                if ($scope.selectType(field.type)) {
                    field.selectedValue = field.selectList.filter(function (item) {
                        return item.value == field.value;
                    })[0];
                }
            });
        };

        $scope.simpleInputType = function (dataType) {
            return textField(dataType) || numberField(dataType) || dateField(dataType) || radioField(dataType);
        };

        $scope.stepInputType = function (dataType) {
            return stepNumberField(dataType);
        };

        $scope.noNumbersInputType = function (dataType) {
            return noNumbersTextField(dataType);
        }

        $scope.textAreaType = function (dataType) {
            return $scope.textAreaFields.indexOf(dataType) !== -1;
        };

        $scope.selectType = function (dataType) {
            return $scope.selectFields.indexOf(dataType) !== -1;
        };

        $scope.inputType = function (data) {
            var dataType = data.type;
            if (textField(dataType) || $scope.noNumbersInputType(data)) {
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

        function noNumbersTextField(dataType) {
            return $scope.noNumbersTextFields.indexOf(dataType) !== -1;
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

        function save()
        {
            var action;
            if ($scope.process.id == 0) {
                action = "Create";
            } else {
                action = "Edit";
            }

            return $http.post("/Common/Process/" + action, $scope.process);
        }

        $scope.setSelectedValue = function (field) {
            field.value = field.selectedValue.value;
        }

        function onSaveClicked() {
            save().then(function (response) {
                    window.location = '/Common/Process/Edit/' + response.data.id + '?_=' + Math.random();
                    $.notify("Los datos se actualizaron exitosamente", "success");
                }, Utils.onAjaxError.bind(this, " al guardar el documento"));
        }

        function onSendToReviewClicked()
        {
            save().then(function (response) {
                return $http.post("/Common/Process/Send/" + response.data.id)
                            .then(function () {
                                window.location = '/Common/Process/Edit/' + response.data.id + '?_=' + Math.random();
                                $.notify("Los datos se actualizaron exitosamente", "success");
                            }, Utils.onAjaxError.bind(this, " al guardar el documento"));
            }, Utils.onAjaxError.bind(this, " al guardar el documento"));
        }
    }

})();