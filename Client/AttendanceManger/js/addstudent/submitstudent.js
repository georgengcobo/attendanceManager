$("#CreateStudntForm").submit(function (event) {
  event.preventDefault();

  var studentRequest = new Object();
  studentRequest.Name = $("#Name").val();
  studentRequest.Surname = $("#Surname").val();
  studentRequest.IdNumber = $("#IdNumber").val();




  var url = GenerateEndpoint("POST_AddStudent");
  console.log("Regstistering attendance");

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
        console.log(response);
        alert("Student Created Ok");
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
