var notification;

$(".addButton").click(function () {
    var sender_login = $("#User_Identity_Name").val();
    var theme = $("#tableView_Theme").val();
    var customer_login = $("#tableView_Customer_Login").val();
    notification = 'Nowa wiadomość';
});

$(".addFriend").click(function () {
    var login = $("#UserLogin").val();
    notification = 'Nowe zaproszenie do znajomych';
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

    var user = $("#User_Identity_Name").val();
    var game = $.connection.gameHub;
    game.client.shownotification = function (user, notification) {
        $('#notificationList').append('<li><div class="chip newNotification">' + notification + '<i class="material-icons fa fa-times-circle"></i></div></li>');
        var $toastContent = $('<span>' + notification + '</span>');
        Materialize.toast($toastContent, 10000);
        var div = document.getElementById('notifi');
        div.style.color = "red";
        div.style.fontWeight = "700";

        div.innerHTML = div.innerHTML + '!';

    };
    $.connection.hub.start().done(function () {
        $('.addButton').click(function () {
            if ($("#tableView_Customer_Login").val() == null) {
                game.server.sentNotification($("#tableView_Login").val(), notification);
            }
            else {
                game.server.sentNotification($("#tableView_Customer_Login").val(), notification);
            }
        });
        $('.addFriend').click(function () {
            game.server.sentNotification($("#UserLogin").val(), notification);
        })

    });
});
