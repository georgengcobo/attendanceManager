$(document).ready(function() {

    $(document).ajaxStart(function() {
        $(".loader").show();
    }).ajaxStop(function() {
        $(".loader").hide();
    });
    $(".loader").hide();
});


