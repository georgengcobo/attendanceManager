$("#CreateClassForm").submit(function (event) {
    event.preventDefault();

   if ($("#Teacher").val() == "-1"){
    alert("Please select Valid teacher first");
   }
  
    var createClassRequest = new Object();
    createClassRequest.ClassName = $("#ClassName").val();
    createClassRequest.Grade = $("#Grade").val();
    createClassRequest.TeacherId = $("#Teacher").val();
  
    var url = GenerateEndpoint("POST_AddClass");
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
        data: JSON.stringify(createClassRequest),
      };
  
      $.ajax(userSettings)
        .done(function (response) {
          var r = confirm("Would You Like to Create Another Class ?");
          if (r == true) {
            window.location.replace("addclass.html");
          } else {
            window.location.replace("index.html");
          }
        })
        .fail(function (data, textStatus, xhr) {
          console.log(data);
          console.log(textStatus);
          console.log(xhr);
          if (data.status == 409) {
            alert("Error Creating a new class: " + data.responseJSON.message );
            return;
          }
          alert("Error Creating a new class " );
        });
    
  });
  