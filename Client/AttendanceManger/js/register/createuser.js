$("#Registerform").submit(function (event) {
  event.preventDefault();

  var url = GenerateEndpoint("POST_AddUser");
  console.log("Creating New User");

  var Request = new Object();
  Request.Email = $("#Email").val();
  Request.Password = $("#Password").val();

  var userSettings = {
    async: true,
    crossDomain: true,
    url: url,
    method: "POST",
    headers: {
      "Access-Control-Allow-Origin": "*",
      "content-type": "application/json",
    },
    data: JSON.stringify(Request),
  };

  $.ajax(userSettings)
    .done(function (response) {
      console.log(response);
      alert("Registration Success");
      sessionStorage.setItem("isLoggedin", "true");
      var bearerToken = "Bearer" + " " + response.token;
      sessionStorage.setItem("token", bearerToken);
      window.location.replace("index.html");
    })
    .fail(function (data, textStatus, xhr) {
      alert("Error Registering user ! " + data.responseJSON.title);
    });
});
