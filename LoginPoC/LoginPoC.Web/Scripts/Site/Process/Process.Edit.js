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
        $scope.editEnabled = true;
        $scope.reviewEnabled = false;
        $scope.birdthDateRegex = /(\d{2})\/(\d{2})\/(\d{4})/;
        $scope.completionError = false;
        $scope.uncompleteRequiredFields = [];

        $scope.events = {
            onInit: onInit,
            onSaveClicked: onSaveClicked,
            onSendToReviewClicked: onSendToReviewClicked,
            onRejectClicked: onRejectClicked,
            onApproveClicked: onApproveClicked
        };

        function onInit(process, editEnabled, reviewEnabled) {
            $scope.process = process;

            $scope.editEnabled = editEnabled && (process.status == "Draft" || process.status == "Rejected");
            $scope.reviewEnabled = reviewEnabled && process.status == "Submitted";

            $scope.preParseValues();
        }

        $scope.preParseValues = function () {
            angular.forEach($scope.process.fields, function (field) {
                if ((field.type == 'Date' || field.type == 'BirthDate') && field.value) {
                    var parsedValue = new Date(field.value);
                    if (parsedValue == 'Invalid Date' && field.value !== '') {
                        var birthDateArr = $scope.birdthDateRegex.exec(field.value);
                        field.value = new Date(birthDateArr[3], birthDateArr[2] - 1, birthDateArr[1]);
                    } else {
                        field.value = parsedValue;
                    }
                }
                if (radioField(field.type)) {
                    field.value = field.value == 'True';
                }
                if ($scope.selectType(field.type)) {
                    field.selectedValue = field.selectList.filter(function (item) {
                        return item.value == field.value;
                    })[0];
                }
                if (numberField(field.type) && field.value) {
                    field.value = parseInt(field.value);
                }
                if (stepNumberField(field.type) && field.value) {
                    field.value = parseFloat(field.value.replace(',', '.'));
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

        function save() {
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
            }, Utils.onAjaxError.bind(this, " al guardar los cambios"));
        }

        function onSendToReviewClicked() {
            save().then(function (response) {
                if (!anyRequiredUncomplete()) {
                    return $http.post("/Common/Process/Send/" + response.data.id)
                        .then(function () {
                            window.location = '/Common/Process/Edit/' + response.data.id + '?_=' + Math.random();
                            $.notify("Los datos se actualizaron exitosamente", "success");
                        }, Utils.onAjaxError.bind(this, " al guardar el documento"));
                } else {
                    showCompletionError();
                }
            }, Utils.onAjaxError.bind(this, " al guardar el documento"));
        }

        function anyRequiredUncomplete() {
            return $scope.process.fields.some(function (field) {
                return fieldWithCompletionError(field);
            })
        }

        function showCompletionError() {
            $scope.completionError = true;
            $scope.uncompleteRequiredFields = $scope.process.fields.filter(function (field) {
                return fieldWithCompletionError(field);
            });
        }

        function fieldWithCompletionError(field) {
            return field.isRequired && (angular.isUndefined(field.value) || field.value === '');
        }

        function onApproveClicked()
        {
            var model = {
                reviewNotes: $scope.process.reviewNotes,
                id: $scope.process.id
            };
            $http.post("/Common/Process/Approve", model)
                .then(function (response) {
                    window.location = '/Common/Process/Edit/' + $scope.process.id + '?_=' + Math.random();
                    $.notify("El tramite se aprobo exitosamente", "success");
                }, Utils.onAjaxError.bind(this, " al aprobar el tramite"));
        }

        function onRejectClicked() {

            if (!$scope.process.reviewNotes)
            {
                $.notify("Es necesario que agrege en observaciones el motivo del rechazo.", "error");
                return;
            }

            var model = {
                reviewNotes: $scope.process.reviewNotes,
                id: $scope.process.id
            };
            $http.post("/Common/Process/Reject", model)
                .then(function (response) {
                    window.location = '/Common/Process/Edit/' + $scope.process.id + '?_=' + Math.random();
                    $.notify("El tramite se rechazo exitosamente", "success");
                }, Utils.onAjaxError.bind(this, " al rechazar el tramite"));
        }
    }

})();