$("#MarkRegistrationFrom").submit(function (event) {
  event.preventDefault();

  var table = $("#MarkRegisterTbl tbody");
  var totalAttendance = [];
  var teacherId;
  var isPresent;
  var registrationId;
  var registrationDate = $("#AttendanceDate").val();

  try {
    table.find("tr").each(function (i, el) {
      var tds = $(this).find("td");

      registrationId = tds[0].innerText;
      teacherId = tds[6].innerText;
      isPresent = tds[7].childNodes[0].value;

      if (isPresent == "-1") {
        alert("You Must choose attendance status for " + tds[3].innerText);
        throw "Break";
      }

      var RequestBody = new Object();
      RequestBody.attendanceDate = registrationDate;
      RequestBody.teacherId = teacherId;
      RequestBody.isPresent = isPresent == "1" ? true : false;
      RequestBody.registrationId = registrationId;
      RequestBody.StudentName = tds[3].innerText;

      totalAttendance.push(RequestBody);
    });
  } catch (e) {
    if (e !== "Break") {
      throw e;
    }
    return;
  }

  var url = GenerateEndpoint("POST_RecordAttendance");
  console.log("Regstistering attendance");

  var token = sessionStorage.getItem("token");

  for (var i = 0; i < totalAttendance.length; i++) {
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
      data: JSON.stringify(totalAttendance[i]),
    };

    $.ajax(userSettings)
      .done(function (response) {
        console.log(response);
        alert("Attendance summitted OK for ");
      })
      .fail(function (data, textStatus, xhr) {
        console.log(data);
        console.log(textStatus);
        console.log(xhr);
        if (data.status == 409) {
          alert(
            "Error Saving attendance for selected Date: " +
              data.responseJSON.message
          );
        }
      });
  }
});
