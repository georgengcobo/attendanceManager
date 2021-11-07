function RegisterStudent(studentId) {
  var classId = -1;

  document.getElementById("StudentId").value = studentId;
  var token = sessionStorage.getItem("token");

  var baseUri = GenerateEndpoint("BaseUri");

  const regUri = `/Admin/Registrations/Class/${classId}/Student/${studentId}`;

  var uri = baseUri + regUri;

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

  var classesUri = GenerateEndpoint("GET_Classes");

  var classRequestSettings = {
    async: true,
    crossDomain: true,
    url: classesUri,
    method: "GET",
    headers: {
      "Access-Control-Allow-Origin": "*",
      Authorization: token,
      "content-type": "application/json",
    },
  };

  var activeClasses = [];
  $.get(StudentRequestSettings, function (response) {
    console.log("Entire Response : ", response);
    var t = $("#registeredClasses").DataTable({
      retrieve: true,
      responsive: true,
    });
    t.clear();
    t.draw();

    $.each(response.registeredStudents, function (index, element) {
      activeClasses.push(element.classId);
      t.row
        .add([
          element.studentName,
          element.idNumber,
          element.className,
          element.grade,
          element.teacherName,
        ])
        .draw(false);
    });

    $.get(classRequestSettings, function (response) {
      console.log("Entire Response : ", response);

      $.each(response.classes, function (index, element) {
        var classNames = `${element.className} Grade ${element.grade}`;
        if (!activeClasses.includes(element.classId)) {
          $("#AvailClasses").append(new Option(classNames, element.classId));
        }
      });
    }).fail(function (data, textStatus, xhr) {
      ManageException(
        textStatus,
        xhr,
        data,
        "There was an Issue loading Active Data"
      );
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
