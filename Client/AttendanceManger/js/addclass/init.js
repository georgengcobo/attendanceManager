$(document).ready(function () {
  $("#Teacher").empty();
  $("#Teacher").append(new Option("-- Select Teacher --", -1));

  GetExistingClasses();

  var teacherId = -1;

  var baseUri = GenerateEndpoint("BaseUri");

  const regUri = `/Admin/Teachers/${teacherId}`;

  var uri = baseUri + regUri;

  var token = sessionStorage.getItem("token");

  var TeachersRequestSettings = {
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

  $.get(TeachersRequestSettings, function (response) {
    console.log("Entire Response : ", response);

    $.each(response.teachers, function (index, element) {
      $("#Teacher").append(new Option(element.fullName, element.teacherId));
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
