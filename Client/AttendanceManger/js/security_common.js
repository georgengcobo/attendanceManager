$("#logout").click(function () {
  var r = confirm("Are You Sure That You Want to Log Out");
  if (r == true) {
    localStorage.clear();
    sessionStorage.clear();
  }
});

if (sessionStorage.getItem("isLoggedin") === null) {
  window.location.replace("login.html");
}

if (sessionStorage.getItem("isLoggedin") !== "true") {
  alert("Please log in First");
  window.location.replace("login.html");
}

function ManageException(textStatus, xhr, data, customMSG) {
  console.log("Begin Dumping Error Details");
  console.log(textStatus);
  console.log(xhr);
  console.log(data);
  console.log("End  Error Details");

  if (data.status == 401) {
    alert(
      "The credentials provided are nolonger valid, please login again and Retry"
    );
    window.location.replace("login.html");
  } else {
    alert(customMSG);
  }
}
