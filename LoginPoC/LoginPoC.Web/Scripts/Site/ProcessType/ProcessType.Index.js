$(document).ready(function () {
    $("span.fa-caret-down").click(function () {
        var id = $(this).attr("id");
        $("div.tipoTramite-descripcion#" + id).toggle("slow");
    });
});