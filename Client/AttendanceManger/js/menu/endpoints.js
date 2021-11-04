window.onload = function() {
    sessionStorage.setItem("EndPointsUpdateFlag", true);

    var EP = sessionStorage.getItem("EndPoints");
    var Es = sessionStorage.getItem("EndPointsUpdateFlag");

    if (sessionStorage.getItem("EndPoints") === null || sessionStorage.getItem("EndPointsUpdateFlag") == "true") {
        console.log("Loading Endpoints into memory");

        var baseUrl = "https://localhost";
        var portNumber = ":44391";
        var subDomain = "";
        var api = "/api"
        var addUser = "/User/RegisterUser";
        var login = "/User/Authenticate"
        var getClasses = "/Admin/Classes";
        var getRegisteredStudents = "/Admin/RegisteredStudents/";
        var addStudent = "/Admin/AddStudent";
        var addClass = "/Admin/AddClass";
        var enrollStudent = "/Admin/EnrollClass";
        var recordAttendance = "/Admin/RecordAttendance";
        var endpointDictionary = {};
        // map menu items to json object here
        endpointDictionary["BaseUrl"] = baseUrl;
        endpointDictionary["PortNumber"] = portNumber;
        endpointDictionary["SubDomain"] = subDomain;
        var APIURL = endpointDictionary.BaseUrl + endpointDictionary.PortNumber + endpointDictionary.SubDomain + api;
        endpointDictionary["BaseUri"] = APIURL;
        endpointDictionary["POST_AddUser"] = addUser;
        endpointDictionary["POST_LogIn"] = login;
        endpointDictionary["GET_Classes"] = getClasses;
        endpointDictionary["GET_RegisteredStudents"] = getRegisteredStudents;
        endpointDictionary["POST_AddStudent"] = addStudent;
        endpointDictionary["POST_AddClass"] = addClass;
        endpointDictionary["POST_EnrollStudent"] = enrollStudent;
        endpointDictionary["POST_RecordAttendance"] = recordAttendance;
        sessionStorage.setItem("EndPoints", JSON.stringify(endpointDictionary));

    }
}

function GenerateEndpoint(enpoint) {
    console.log("Generating end point Url");
    var endpointDictionary = sessionStorage.getItem("EndPoints");
    var jsonEndpoint = JSON.parse(endpointDictionary);
    var baseUrl = jsonEndpoint["BaseUri"]

    var destEndpoint = jsonEndpoint[enpoint];
    if (destEndpoint) {

        var url = baseUrl + destEndpoint;
        console.log("Generated Url", url);
        return url;
    }
    console.log("error generating url ");
    return false;
}