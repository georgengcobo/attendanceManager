$("#loginform").submit(function (event) {

    event.preventDefault();

    var url = GenerateEndpoint("POST_LogIn");
    console.log("Logging user in");
    
    var loginRequest = new Object();
    loginRequest.Email = $("#Email").val();
    loginRequest.Password = $("#Password").val();
   
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": url,
        "method": "POST",
        "headers": {
            "content-type": "application/json",
        },
        "data": JSON.stringify(loginRequest)
    }

    $.ajax(settings).done(function (response) {
        console.log(response);
        alert("Login Success");
        sessionStorage.setItem('isLoggedin', 'true');
        var bearerToken = "Bearer" + " " + response.token;
        sessionStorage.setItem('token', bearerToken);
        window.location.replace("index.html");

    }).fail(function (data, textStatus, xhr) {
        alert("Login Fail , try again");
    });

});

