$(function () {
    $('#student').DataTable();
})
function myFunction(id) {
    $.ajax({
        url: "/Student/GetByID/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (response) {
            $('#firstname').val(response.firstName);
            $('#lastname').val(response.lastName);
            $('#birthdate').val(response.birthDate);
            $('#modal-details').modal('show');

        },
        error: function (response) {
            alert(response.responseText);
        }
    });
    return false;
}

