console.log("trying to load the menu option");
$(document).ready(function () {

    var menuOptions = '<header class="major">';
    menuOptions += '<h2>Menu</h2><br><button class="btn btn-lg btn-success" id="logout">Log Out <i class="glyphicon glyphicon-log-out"></i></button>';
    menuOptions += '</header>';
    menuOptions += '<ul>';
    menuOptions += '<li>';
    menuOptions += '<a href="index.html" title="Classes"><img src="./images/icons/essen/32/home.png"/>Classes</a></div>';
    menuOptions += '</li>';
    menuOptions += '<li>';
    menuOptions += '<a href="addproduct.html" title="addProduct"><img src="./images/icons/essen/32/shipping.png"/>Add Product</a> </div>';
    menuOptions += '</li>';
    menuOptions += '<li>';
    menuOptions += '<a href="addcategory.html" title="AddCategory"><img src="./images/icons/essen/32/milestone.png"/>Add Category</a> </div>';
    menuOptions += '</li>';
    menuOptions += '</ul>';

    console.log(menuOptions);

    $('#menu').append(menuOptions);


    $("#logout").click(function () {
        var r = confirm("Are You Sure That You Want to Log Out");
        if (r == true) {
            localStorage.clear();
            window.location.replace("login.html");
        }

    });
});