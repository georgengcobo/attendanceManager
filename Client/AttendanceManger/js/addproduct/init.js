$(document).ready(function () {

    $(document).ajaxStart(function () {
        $(".loader").show();
    }).ajaxStop(function () {
        $(".loader").hide();
    });
    $(".loader").hide();

    // MAP Categories
    var stringCategories = sessionStorage.getItem("ProductCategories");
    var jsonCategories = JSON.parse(stringCategories);

    $('#ProductCategoryId').empty();
    $('#ProductCategoryId').append(new Option("Choose Category", -1));

    $.each(jsonCategories, function (index, element) {

        $('#ProductCategoryId').append(new Option(element.categoryCode, element.categoryId));
    });

});
