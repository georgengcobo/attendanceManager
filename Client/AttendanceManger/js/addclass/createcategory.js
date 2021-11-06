$("#CreateCategoryForm").submit(function (event) {
  event.preventDefault();

  var categoryCode = $("#categoryCode").val();

  var canProceed = ValidateCode(categoryCode);

  if (!canProceed) {
    return false;
  }

  var categoryName = $("#name").val();

  var isActive = $("#isActive").is(":checked");

  var newCategoryRequest = new Object();
  newCategoryRequest.Name = categoryName;
  newCategoryRequest.CategoryCode = categoryCode;

  newCategoryRequest.IsActive = isActive == true ? 1 : 0;

  var url = GenerateEndpoint("POST_CreateCategory");
  var token = sessionStorage.getItem("token");

  $(".loader").show();

  var settings = {
    async: true,
    crossDomain: true,
    url: url,
    method: "POST",
    headers: {
      "Access-Control-Allow-Origin": "*",
      Authorization: token,
      "content-type": "application/json",
    },
    data: JSON.stringify(newCategoryRequest),
  };

  $.ajax(settings)
    .done(function (response) {
      console.log(response);

      CacheNewCategory(response.Category);

      alert("Category Added Ok ");
    })
    .fail(function (data, textStatus, xhr) {
      ManageException(
        textStatus,
        xhr,
        data,
        "There was an Issue creating a new category"
      );
    });
});

function CacheNewCategory(newCategory) {
  var stringCategories = sessionStorage.getItem("ProductCategories");
  var jsonCategories = JSON.parse(stringCategories);

  jsonCategories.push(newCategory);

  sessionStorage.setItem("ProductCategories", JSON.stringify(jsonCategories));
}
