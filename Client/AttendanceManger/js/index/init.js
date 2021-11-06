$(document).ready(function () {

  var activeclasseUri = GenerateEndpoint("GET_Classes");

  var token = sessionStorage.getItem("token");

  var ClassesRequestSettings = {
    async: true,
    crossDomain: true,

    url: activeclasseUri,
    method: "GET",
    headers: {
      "Access-Control-Allow-Origin": "*",
      Authorization: token,
      "content-type": "application/json",
    },
  };

  GetActiveClasses(ClassesRequestSettings);
});

function GetActiveClasses(ClassesRequestSettings) {
  $.get(ClassesRequestSettings, function (classesResponse) {
    console.log("Entire Response : ", classesResponse);

    var t = $("#RegisteredClasses").DataTable();
    t.clear();
    t.draw();

    $.each(classesResponse.classes, function (index, element) {
      var editProduct =
        "<a href='#AttendanceModal' data-toggle='modal' data-backdrop='static' onClick='return MarkRegister(" +
        element.classId +
        ");'><img src='./images/icon_edit.png' title='Mark Attendance Register' /></a>";

      t.row.add([element.className, element.grade, editProduct]).draw(false);
    });
  }).fail(function (data, textStatus, xhr) {
    ManageException(
      textStatus,
      xhr,
      data,
      "There was an Issue loading Active Data"
    );
  });
}
