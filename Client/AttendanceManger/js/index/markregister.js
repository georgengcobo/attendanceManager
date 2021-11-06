function MarkRegister(classId) {
  var token = sessionStorage.getItem("token");

  var baseUri = GenerateEndpoint("BaseUri");
  var studentId = -1;

  const regUri = `${baseUri}/Admin/Registrations/Class/${classId}/Student/${studentId}`;

  var token = sessionStorage.getItem("token");

  var registeredStudentsSettings = {
    async: true,
    crossDomain: true,
    url: regUri,
    method: "GET",
    headers: {
      "Access-Control-Allow-Origin": "*",
      Authorization: token,
    },
  };

  $(".loader").show();

  $.ajax(registeredStudentsSettings)
    .done(function (response) {
      console.log("Entire Response : ", response);

      var t = $("#MarkRegisterTbl").DataTable({
        responsive: true,
      });

      t.clear();
      t.draw();

      $.each(response.registeredStudents, function (index, element) {
        var attendaceOptions =
          '<select name="Reg" id="Reg"><option value="-1">?</option><option value="1">Yes</option><option value="0">No</option></select>';

        t.row
          .add([
            element.registrationId,
            element.className,
            element.grade,
            element.studentName,
            element.idNumber,
            element.teacherName,
            element.teacherId,
            attendaceOptions,
          ])
          .draw(false);
      });
    })
    .fail(function (data, textStatus, xhr) {
      ManageException(
        textStatus,
        xhr,
        data,
        "There was an Issue loading Active Data"
      );
    });
}
