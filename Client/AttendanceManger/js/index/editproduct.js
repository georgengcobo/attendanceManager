$("#EditProductFrom").submit(function (event) {

    event.preventDefault();

    var url = GenerateEndpoint("POST_EditProduct");

    var token = sessionStorage.getItem('token');

    var Request = new Object();
    Request.ProductId = $("#ProductId").val();
    Request.ProductName = $("#ProductName").val();
    Request.ProductCode = $("#ProductCode").val();
    Request.ProductCategoryId = $("#ProductCategoryId").val();
    Request.ProductPrice = $("#ProductPrice").val();
    Request.ProductDescription = $("#ProductDescription").val();

    $(".loader").show();


    var productSettings = {
        "async": true,
        "crossDomain": true,
        "url": url,
        "method": "POST",
        "headers": {
            "Access-Control-Allow-Origin": "*",
            "Authorization": token,
            "content-type": "application/json",
        },
        "data": JSON.stringify(Request)
    }

    $.ajax(productSettings).done(function (response) {
        
        alert("Product Update Success");

        window.location.replace("index.html");

    }).fail(function (data, textStatus, xhr) {

        ManageException(textStatus, xhr, data, "There was an issue Updating products");

    });

});