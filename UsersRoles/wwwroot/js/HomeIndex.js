$("#btnSubmit").click(function () {
    $.ajax({
        url: "UpdateUser",
        type: 'POST',
        data: $('#adminForm').serialize(),
        dataType: 'json',
        success: function (response) {
            alert(response.result)
        },
        error: function (message) {
            alert("an error has occurred");
        },
    });
})