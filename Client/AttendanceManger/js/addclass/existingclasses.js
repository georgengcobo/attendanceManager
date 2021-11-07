function GetExistingClasses() {
  var uri = GenerateEndpoint("GET_Classes");

  var token = sessionStorage.getItem("token");

  var ClassesRequestSettings = {
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

  $.get(ClassesRequestSettings, function (response) {
    var t = $("#ExistingClassesTbl").DataTable({
      retrieve: true,
      responsive: true,
    });

    t.clear();
    t.draw();

    $.each(response.classes, function (index, element) {
      t.row
        .add([element.className, element.grade, element.teacherName])
        .draw(false);
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
