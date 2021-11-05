function RegisterStudent(studentId){
    var classId = -1;

    var token = sessionStorage.getItem('token');

    var baseUri = GenerateEndpoint("BaseUri");
    
    const regUri = `/Admin/Registrations/Class/${classId}/Student/${studentId}`;
    
    var  uri = baseUri + regUri;

    var StudentRequestSettings = {
        "async": true,
        "crossDomain": true,
        "url": uri,
        "method": "GET",
        "headers": {
            "Access-Control-Allow-Origin": "*",
            "Authorization": token,
            "content-type": "application/json",
        },
    }


    $.get(StudentRequestSettings, function (response) {

        console.log("Entire Response : ", response);

        var t = $('#registeredClasses').DataTable();
        t.clear();
        t.draw();

        $.each(response.registeredStudents, function (index, element) {
            t.row.add([element.studentName, element.idNumber, element.className, element.grade,element.teacherName,]).draw(false);
        });
    }).fail(function (data, textStatus, xhr) {

        ManageException(textStatus, xhr, data, "There was an Issue loading Active Data");

    });
 

}