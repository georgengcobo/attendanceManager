$("#categoryCode").change(function () {
  console.log("Category Validation !");

  var currentInput = $(this).val();

  ValidateCode(currentInput);
});

function ValidateCode(input) {
  if (!IsValidCode(input)) {
    alert("Input Invalid Expected ABC123 format !");
    return false;
  }
  return true;
}

function IsValidCode(input) {
  let regex = /[a-zA-Z][a-zA-Z][a-zA-Z]\d\d\d/gi;
  return regex.test(input);
}
