function MarkRegister(classId) {

        var url = GenerateEndpoint("GET_RegisteredStudents");

        url = url + classId;

        var token = sessionStorage.getItem('token');

        var registeredStudentsSettings = {

            "async": true,
            "crossDomain": true,
            "url": url,
            "method": "GET",
            "headers": {
                "Access-Control-Allow-Origin": "*",
                "Authorization": token,
            },
        }

        $(".loader").show();

    $.ajax(registeredStudentsSettings).done(function (response) {

        console.log("Entire Response : ", response);

        var t = $('#MarkRegisterFrom').DataTable();
        t.clear();
        t.draw();

        $.each(response.registeredStudents, function (index, element) {

            var attendaceOptions = '<select name="Reg_'+element.registrationId+'" id="Reg_'+element.registrationId+'"><option value="-1">?</option><option value="1">Yes</option><option value="0">No</option></select>';
   
            t.row.add([element.className, element.grade, element.studentName, element.idNumber, element.teacherName, attendaceOptions]).draw(false);
        });

    }).fail(function (data, textStatus, xhr) {
        ManageException(textStatus, xhr, data, "There was an Issue loading Active Data");
    });

}
