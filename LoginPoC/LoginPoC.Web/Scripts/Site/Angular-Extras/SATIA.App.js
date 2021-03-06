(function () {

    var modules = ["ui.bootstrap", "SATIA.Services", "SATIA.Directives", "blockUI", "daterangepicker", "ngFileUpload", "angucomplete-alt"].filter(function (module) {
        try {
            return !!angular.module(module);
        } catch (e) {
            //console.warn("Angular module not found: " + module, e);
        }
    });

    window.app = angular.module('app', modules);

    window.app.config(["blockUIConfig", function (blockUIConfig) {        
        blockUIConfig.message = 'Cargando...';
    }]);

    window.app.config(['$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
    }]);

})();

(function () {

    $.notify.defaults({
        // whether to hide the notification on click
        clickToHide: true,
        // whether to auto-hide the notification
        autoHide: true,
        className: 'info'
        //position: 'top center'
    });

    window.Utils = {};
    window.Utils.onAjaxError = onAjaxError;
    window.Utils.showModelErrorCollection = showModelErrorCollection;

    function onAjaxError(tryingTo, error) {
        $.notify("Ocurrio un error" + tryingTo + ". \r\nRecargue la pagina y vuelva a intentarlo", {
            clickToHide: true,
            autoHide: false,
            className: 'error'
        });

        console.error("Ocurrio un error" + tryingTo + "", error);
    }

    function showModelErrorCollection(modelErrorCollection) {

        for (var i = 0; i < modelErrorCollection.length; i++) {
            $.notify(modelErrorCollection[i].errorMessage, {
                className: 'error'
            });
        }
    }

})();