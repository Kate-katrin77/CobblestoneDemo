$(document).ready(() => {

    $.get("/home/list").done((data) => {
        $("#emplaoyeeTbl").html(data);
    })

});