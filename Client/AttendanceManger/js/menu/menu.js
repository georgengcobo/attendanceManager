console.log("trying to load the menu option");
$(document).ready(function () {
  var menuOptions = '<header class="major">';
  menuOptions +=
    '<h2>Menu</h2><br><button class="btn btn-lg btn-success" id="logout">Log Out <i class="glyphicon glyphicon-log-out"></i></button>';
  menuOptions += "</header>";
  menuOptions += "<ul>";
  menuOptions += "<li>";
  menuOptions +=
    '<a href="index.html" title="Classes"><img src="./images/icons/essen/32/calendar.png"/> Attendance</a></div>';
  menuOptions += "</li>";
  menuOptions += "<li>";
  menuOptions +=
    '<a href="addstudent.html" title="Register a new studen"><img src="./images/icons/essen/32/hire-me.png"/> Register Student</a> </div>';
  menuOptions += "</li>";
  menuOptions += "<li>";
  menuOptions +=
    '<a href="addclass.html" title="Create a new class to teach"><img src="./images/icons/essen/32/document-library.png"/> Create Class</a> </div>';
  menuOptions += "</li>";
  menuOptions += "<li>";
  menuOptions +=
    '<a href="enrollstudent.html" title="Enroll student into a class"><img src="./images/icons/essen/32/special-offer.png"/> Enroll in Class</a> </div>';
  menuOptions += "</li>";
  menuOptions += "</ul>";

  console.log(menuOptions);

  $("#menu").append(menuOptions);

  $("#logout").click(function () {
    var r = confirm("Are You Sure That You Want to Log Out");
    if (r == true) {
      localStorage.clear();
      window.location.replace("login.html");
    }
  });
});
