$("#CreateProductForm").submit(function (event) {

    event.preventDefault();

    var url = GenerateEndpoint("POST_CreateProduct");

    var token = sessionStorage.getItem('token');

    console.log("adding new location");

    console.log("Creating product form ");
    let myForm = document.getElementById('CreateProductForm');

    let formData = new FormData(myForm);

    $(".loader").show();

    var settings = {
        "url": url,
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Authorization": token
        },
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": formData
    };

    $.ajax(settings).done(function (response) {

        console.log(response);

        alert("Create Product Success");

        window.location.replace("index.html");

    }).fail(function (data, textStatus, xhr) {

        ManageException(textStatus, xhr, data, "There was an issue creating a new product See log for details");

    });

});



