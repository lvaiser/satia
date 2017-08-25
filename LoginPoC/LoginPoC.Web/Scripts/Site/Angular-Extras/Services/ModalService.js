(function () {
    window.sevices.service('SATIA.Services.ModalService', ['$uibModal',
        function ($uibModal) {
            var modalDefaults = {
                backdrop: true,
                keyboard: true,
                modalFade: true,
                animation: true,
                templateUrl: 'ConfirmationModal.html'
            };

            var modalOptions = {
                closeButtonText: 'Close',
                actionButtonText: 'OK',
                headerText: '',
                bodyText: ''
            };

            this.showModal = function (customModalDefaults, customModalOptions) {
                if (!customModalDefaults)
                    customModalDefaults = {};

                return this.show(customModalDefaults, customModalOptions);
            };

            this.show = function (customModalDefaults, customModalOptions) {
                //Create temp objects to work with since we're in a singleton service
                var tempModalDefaults = {};
                var tempModalOptions = {};

                //Map angular-ui modal custom defaults to modal defaults defined in service
                angular.extend(tempModalDefaults, modalDefaults, customModalDefaults);

                //Map modal.html $scope custom properties to defaults defined in service
                angular.extend(tempModalOptions, modalOptions, customModalOptions);

                if (!tempModalDefaults.controller) {
                    tempModalDefaults.controller = function ($scope, $uibModalInstance) {

                        $scope.modalOptions = tempModalOptions;

                        $scope.modalOptions.ok = function (result) {
                            $uibModalInstance.close(result);
                        };

                        $scope.modalOptions.close = function (result) {
                            $uibModalInstance.dismiss('cancel');
                        };
                    };
                }

                return $uibModal.open(tempModalDefaults).result;
            };

            this.confirm = function (title, message, cancelText, acceptText) {
                var modalOptions = {
                    closeButtonText: cancelText || 'Cancelar',
                    actionButtonText: acceptText || 'Aceptar',
                    headerText: title,
                    bodyText: message
                };

                return this.showModal({}, modalOptions);
            };
        }]);
})();