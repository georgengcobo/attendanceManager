$("#EnrolStudentFrom").submit(function (event) {
    event.preventDefault();
  
    var enrollRequest = new Object();
    enrollRequest.StudentId = $("#StudentId").val();
    enrollRequest.classId = $("#AvailClasses").val();

    var url = GenerateEndpoint("POST_EnrollStudent");

    var token = sessionStorage.getItem("token");
  
      var regSettings = {
        async: true,
        crossDomain: true,
        url: url,
        method: "POST",
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: token,
          "content-type": "application/json",
        },
        data: JSON.stringify(enrollRequest),
      };
  
      $.ajax(regSettings)
        .done(function (response) {
          console.log(response);
          alert("Registration completed");
          window.location.replace("enrollstudent.html");
        })
        .fail(function (data, textStatus, xhr) {
          console.log(data);
          console.log(textStatus);
          console.log(xhr);
          if (data.status == 409) {
            alert("Error Registering : " + data.responseJSON.message );
          }
        });
    
  });
  