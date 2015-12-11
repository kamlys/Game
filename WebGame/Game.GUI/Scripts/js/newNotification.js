$(".addButton").click(function () {
    var sender_login = $("#User_Identity_Name").val();
    var theme = $("#tableView_Theme").val();
    var customer_login = $("#tableView_Customer_Login").val();
});

$(function () {

    //Nowa wiadomość//
    var user = $("#User_Identity_Name").val();
    var game = $.connection.gameHub;
    game.client.shownotification = function (user) {
        $('#notificationList').append('<li><div class="chip"> Nowa wiadomość <i class="material-icons fa fa-times-circle"></i></div></li>');
        var $toastContent = $('<span>Nowa wiadomość</span>');
        Materialize.toast($toastContent, 10000);
        var div = document.getElementById('notifi');
        div.style.color = "red";
        div.style.fontWeight = "700";

        div.innerHTML = div.innerHTML + '!';

    };
    $.connection.hub.start().done(function () {
        $('.addButton').click(function () {
            if ($("#tableView_Customer_Login").val() == null) {
                game.server.sentNotification($("#tableView_Login").val());
            }
            else {
                game.server.sentNotification($("#tableView_Customer_Login").val());
            }
        });
    });
});



$(".addFriend").click(function () {
    var login = $("#User_Identity_Name").val();

    $.ajax({
        type: "POST",
        url: 'AddFriend',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ a: login }),
        dataType: "json",
        success: function (data) {
            location.reload();
        }
    });


});

$(function () {

    //Nowa wiadomość//
    var user = $("#User_Identity_Name").val();
    var game = $.connection.gameHub;
    game.client.shownotification = function (user) {
        $('#notificationList').append('<li><div class="chip"> Nowe zaproszenie do znajomych <i class="material-icons fa fa-times-circle"></i></div></li>');
        var $toastContent = $('<span>Nowe zaproszenie do znajomych</span>');
        Materialize.toast($toastContent, 10000);
        var div = document.getElementById('notifi');
        div.style.color = "red";
        div.style.fontWeight = "700";

        div.innerHTML = div.innerHTML + '!';

    };
    $.connection.hub.start().done(function () {
        $('.addButton').click(function () {
            game.server.sentNotification($("#User_Identity_Name").val());
        });
    });
});