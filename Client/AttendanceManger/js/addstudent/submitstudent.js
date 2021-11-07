$("#CreateStudntForm").submit(function (event) {
  event.preventDefault();

  if($("#IdNumber").val().length !== 13){

    alert("Please provide 13 Digit Id number")
    return;
  }

  var studentRequest = new Object();
  studentRequest.Name = $("#Name").val();
  studentRequest.Surname = $("#Surname").val();
  studentRequest.IdNumber = $("#IdNumber").val();

  var url = GenerateEndpoint("POST_AddStudent");

  var token = sessionStorage.getItem("token");

    var userSettings = {
      async: true,
      crossDomain: true,
      url: url,
      method: "POST",
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: token,
        "content-type": "application/json",
      },
      data: JSON.stringify(studentRequest),
    };

    $.ajax(userSettings)
      .done(function (response) {
        var r = confirm("Would You Like to add another Student ?");
        if (r == true) {
          window.location.replace("addstudent.html");
        } else {
          window.location.replace("index.html");
        }
      })
      .fail(function (data, textStatus, xhr) {
        console.log(data);
        console.log(textStatus);
        console.log(xhr);
        if (data.status == 409) {
          alert("Error Creating student : " + data.responseJSON.message );
        }
      });
  
});
