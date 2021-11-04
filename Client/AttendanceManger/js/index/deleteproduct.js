function DeleteProduct(productId){

    var r = confirm("Are you sure you wish to delete this Category ?");
    if (r == true) {

        var url = GenerateEndpoint("GET_DeleteProduct");

        url = url + "/" + productId;

        var token = sessionStorage.getItem('token');

        var deleteProductSettings = {
            "async": true,
            "crossDomain": true,
            "url": url,
            "method": "DELETE",
            "headers": {
                "Access-Control-Allow-Origin": "*",
                "Authorization": token,
            },
        }

        $(".loader").show();

        $.ajax(deleteProductSettings).done(function () {
  
            alert("Product with Deleted ");

            window.location.replace("index.html");

        }).fail(function (data, textStatus, xhr) {

            ManageException(textStatus, xhr, data, "There was an Issue deleting the category");

        });

      
    }

}
