$(".deleteMessage").click(function () {
    var id = $("#viewModel_ID").val();
    console.log(id);
    $.ajax({
        type: "POST",
        url: 'DeleteMessage',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({a: id }),
        success: function (data) {
            Materialize.toast("Wiadomość usunięta", 8000);
            window.location = "Index";
        }
    });
});

