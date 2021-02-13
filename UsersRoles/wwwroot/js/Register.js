function IsValidEmail(id) {
    var value = document.getElementById(id).value;
    if (value !== null && value !== "") {
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!re.test(String(value).toLowerCase())) {
            return false;
        }
    }
    else {
        return false;
    }
    return true;
}
function ValidatePassword() {
    var pass = document.getElementById("txtPassword").value;
    var confirmPass = document.getElementById("txtConfirmPassword").value;
    return pass == confirmPass;
}
$('#btnSubmit').on('click', function () {
    if (ValidatePassword() && IsValidEmail("txtEmail")) {
        return true
    }
    alert("Please check validations");
    return false;
});
$("#txtPassword, #txtConfirmPassword ").focusout(function () {
    if (!ValidatePassword()) {
        $('#lblException').html('Please check your passwords');
    }
    else {
        $('#lblException').html('');
    }
});
