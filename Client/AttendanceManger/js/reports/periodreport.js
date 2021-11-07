$("#PeriodReport").submit(function (event) {
    event.preventDefault();

    $("#Reporting").hide();

    var reportRequest = new Object();
    reportRequest.StartDateTime = $("#StartDate").val();
    reportRequest.EndDateTime = $("#EndDate").val();

  
    var url = GenerateEndpoint("POST_PeriodReport");
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
        data: JSON.stringify(reportRequest),
      };
  
      $.ajax(reportSettings)
        .done(function (response) {

            var t = $("#PeriodReportTbl").DataTable({
                retrieve: true,
                responsive: true,
              });
          
              t.clear();
              t.draw();
          
              $.each(response.periodReport, function (index, element) {
                t.row.add([element.attendanceDate, element.className, element.grade,element.student,element.idNumber,element.markedBy,element.isPresent]).draw(false);
              });

            alert("Done ! " );

            $("#Reporting").show();

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
  