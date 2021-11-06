window.onload = function () {
  sessionStorage.setItem("EndPointsUpdateFlag", true);

  if (
    sessionStorage.getItem("EndPoints") === null ||
    sessionStorage.getItem("EndPointsUpdateFlag") == "true"
  ) {
    console.log("Loading Endpoints into memory");

    const baseUrl = "https://localhost";
    const portNumber = ":44391";
    const subDomain = "";
    const api = "/api";
    const addUser = "/User/Register";
    const login = "/User/Authenticate";
    const getClasses = "/Admin/Classes";
    const addStudent = "/Admin/AddStudent";
    const addClass = "/Admin/AddClass";
    const enrollStudent = "/Admin/EnrollClass";
    const recordAttendance = "/Admin/RecordAttendance";
    var endpointDictionary = {};
    // map menu items to json object here
    endpointDictionary["BaseUrl"] = baseUrl;
    endpointDictionary["PortNumber"] = portNumber;
    endpointDictionary["SubDomain"] = subDomain;
    var APIURL =
      endpointDictionary.BaseUrl +
      endpointDictionary.PortNumber +
      endpointDictionary.SubDomain +
      api;
    endpointDictionary["BaseUri"] = APIURL;
    endpointDictionary["POST_AddUser"] = addUser;
    endpointDictionary["POST_LogIn"] = login;
    endpointDictionary["GET_Classes"] = getClasses;
    endpointDictionary["POST_AddStudent"] = addStudent;
    endpointDictionary["POST_AddClass"] = addClass;
    endpointDictionary["POST_EnrollStudent"] = enrollStudent;
    endpointDictionary["POST_RecordAttendance"] = recordAttendance;
    sessionStorage.setItem("EndPoints", JSON.stringify(endpointDictionary));
  }
};

function GenerateEndpoint(enpoint) {
  console.log("Generating end point Url");
  var endpointDictionary = sessionStorage.getItem("EndPoints");
  var jsonEndpoint = JSON.parse(endpointDictionary);
  var baseUrl = jsonEndpoint["BaseUri"];

  var destEndpoint = jsonEndpoint[enpoint];
  if (destEndpoint == baseUrl) {
    return baseUrl;
  }
  if (destEndpoint) {
    var url = baseUrl + destEndpoint;
    console.log("Generated Url", url);
    return url;
  }
  console.log("error generating url ");
  return false;
}
