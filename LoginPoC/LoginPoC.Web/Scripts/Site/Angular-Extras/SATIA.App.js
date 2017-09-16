(function () {

    var modules = ["ui.bootstrap", "SATIA.Services", "SATIA.Directives", "blockUI", "daterangepicker", "ngFileUpload"].filter(function (module) {
        try {
            return !!angular.module(module);
        } catch (e) {
            //console.warn("Angular module not found: " + module, e);
        }
    });

    window.app = angular.module('app', modules);

    window.app.config(function (blockUIConfig) {        
        blockUIConfig.message = 'Cargando...';
    });    

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

    function onAjaxError(tryingTo, error) {
        $.notify("Ocurrio un error" + tryingTo + ". \r\nRecargue la pagina y vuelva a intentarlo", {
            clickToHide: true,
            autoHide: false,
            className: 'error'
        });

        console.error("Ocurrio un error" + tryingTo + "", error);
    }

})();