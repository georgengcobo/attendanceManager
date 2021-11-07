$("#PeriodReport").submit(function (event) {
    event.preventDefault();

    var reportRequest = new Object();
    reportRequest.StartDate = $("#StartDate").val();
    reportRequest.EndDate = $("#EndDate").val();

  
    var url = GenerateEndpoint("POST_AddClass");
    var token = sessionStorage.getItem("token");
  
      var reportSettings = {
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
  
      $.ajax(reportSettings)
        .done(function (response) {
            alert("Done ! " );
        })
        .fail(function (data, textStatus, xhr) {
          console.log(data);
          console.log(textStatus);
          console.log(xhr);
          if (data.status == 409) {
            alert("Error Generating report : " + data.responseJSON.message );
            return;
          }
          alert("Error Generating report " );
        });
    
  });
  