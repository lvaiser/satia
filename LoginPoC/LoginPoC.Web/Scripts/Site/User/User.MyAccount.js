(function () {

    app.controller("User.MyAccount", ["$scope", "$http", "Upload", controller]);

    function controller($scope, $http, $uploader) {

        var empty = {
            enum: { key: 0, value: "-" },
            idCero: { id: 0, name: "-" },
            idNull: { id: null, name: "-" },
            culture: { id: null, displayName: "-"}
        };

        $scope.form = {};
        $scope.form.data = {

            genders: [empty.enum],
            maritalStatuses: [empty.enum],
            countries: [empty.idNull],
            cultures: [empty.culture],

            datePickerOptions: {
                "singleDatePicker": true,
                "showDropdowns": true,
                "autoApply": true,
                "maxDate": new Date(),
                "locale": {
                    "format": "DD/MM/YYYY",
                    "separator": " - ",
                    "fromLabel": "Desde",
                    "toLabel": "Hasta",
                    "daysOfWeek": [
                        "Dom",
                        "Lun",
                        "Mar",
                        "Mie",
                        "Jue",
                        "Vie",
                        "Sab"
                    ],
                    "monthNames": [
                        "Enero",
                        "Febrero",
                        "Marzo",
                        "Abril",
                        "Mayo",
                        "Junio",
                        "Julio",
                        "Agosto",
                        "Septiembre",
                        "Octubre",
                        "Noviembre",
                        "Diciembre"
                    ],
                    "firstDay": 1
                }
            }
        }

        $scope.user = {
            id: null
        };

        $scope.events = {
            onInit: onInit,
            onSaveClicked: onSaveClicked,
            onUnsubscribeClicked: onUnsubscribeClicked,
            onFileSelected: onFileSelected
        };

        $scope.validators = {
            selectErrorMessage: selectErrorMessage,
            noCero: noCero
        };

        function loadCountries()
        {
            return $http.get("/Common/Country/ListAll")
                        .then(function (response) {
                            response.data.unshift(empty.idNull);
                            $scope.form.data.countries = response.data;
                            return response.data;
                        }, Utils.onAjaxError.bind(this, " al intentar cargar la lista de paises"));
        }
        function loadCultures() {

            return $http.get("/Common/Culture/ListAll")
                        .then(function (response) {
                            response.data.unshift(empty.culture);
                            $scope.form.data.cultures = response.data;
                            return response.data;
                        }, Utils.onAjaxError.bind(this, " al intentar cargar la lista de culturas"));
        }

        function onInit(user, defaultPictureUrl, genders, maritalStatuses) {
            $scope.user = user;
            $scope.user.birthDate = user.birthDate || "";

            genders.unshift(empty.enum);
            maritalStatuses.unshift(empty.enum);

            $scope.form.data.genders = genders;
            $scope.form.data.maritalStatuses = maritalStatuses;

            $scope.form.pictureUrl = user.pictureUrl || defaultPictureUrl;

            loadCountries();
            loadCultures();
        }
        function onSaveClicked()
        {
            if (!validate($scope.pageForm)) {
                $scope.pageForm.$setDirty();
                $.notify("Existen campos con valores invalidos. Corrija los errores y vuelva a intentarlo", "error");
                return;
            }

            return $http.post("/Common/User/SaveProfile", $scope.user)
                        .then(function (response) {
                            $.notify("Los datos se actualizaron exitosamente", "success");
                        }, Utils.onAjaxError.bind(this, " al actualizar los datos"));
        }
        function onUnsubscribeClicked() {
            window.location = "/Common/User/Unsubscribe"
        }
        function onFileSelected(file)
        {
            if (file) {
                file.upload = $uploader.upload({
                    url: '/File/Upload',
                    data: {
                        file: file,
                        displayName: "profile-picture"
                    }
                });

                file.upload.then(function (response) {
                    $scope.user.pictureId = response.data.id;
                    $scope.form.pictureUrl = "/File/Download/" + response.data.id;
                }, function (response) {
                    window.onAjaxError("actualizar la imagen", response);
                }).then(onSaveClicked);
            }
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