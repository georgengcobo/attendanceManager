$(document).ready(function () {
  var StudentId = -1;

  var baseUri = GenerateEndpoint("BaseUri");

  const regUri = `/Admin/Students/${StudentId}`;

  var uri = baseUri + regUri;
  var token = sessionStorage.getItem("token");

  var StudentRequestSettings = {
    async: true,
    crossDomain: true,
    url: uri,
    method: "GET",
    headers: {
      "Access-Control-Allow-Origin": "*",
      Authorization: token,
      "content-type": "application/json",
    },
  };

  $.get(StudentRequestSettings, function (response) {
    console.log("Entire Response : ", response);

    var t = $("#Students").DataTable();
    t.clear();
    t.draw();

    $.each(response.students, function (index, element) {
      var editStudent =
        "<a href='#EnrolStudentModal' data-toggle='modal' data-backdrop='static' onClick='return RegisterStudent(" +
        element.studentId +
        ");'><img src='./images/icon_edit.png' title='Enroll Student' /></a>";
      var studentNames = element.name + " " + element.surname;
      t.row.add([studentNames, element.idNumber, editStudent]).draw(false);
    });
  }).fail(function (data, textStatus, xhr) {
    ManageException(
      textStatus,
      xhr,
      data,
      "There was an Issue loading Active Data"
    );
  });
});
