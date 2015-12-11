var notification;
var pin;

$(".addButton").click(function () {
    var sender_login = $("#User_Identity_Name").val();
    var theme = $("#tableView_Theme").val();
    var customer_login = $("#tableView_Customer_Login").val();
    notification = '<li><div class="chip newNotification onclick="location.href="@Url.Action("Index", "Message")"">Nowa wiadomość<i class="material-icons fa fa-times-circle"></i></div></li>';
    pin = "Nowa wiadomość";
});

$(".addFriend").click(function () {
    var login = $("#UserLogin").val();
    notification = '<li><div class="chip newNotification" onclick="location.href="@Url.Action("Profil", "User", new { User = @item.Login })"">Nowe zaproszenie do znajomych<i class="material-icons fa fa-times-circle"></i></div></li>';
    pin = "Nowe zaproszenie do znajomych";
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
    game.client.shownotification = function (user, notification,pin) {
        console.log("Wiadomość: " + notification);
        console.log("Pin: " + pin);
        $('#notificationList').append(notification);
        var $toastContent = $('<span>'+pin+'</span>');
        Materialize.toast($toastContent, 10000);
        var div = document.getElementById('notifi');
        div.style.color = "red";
        div.style.fontSize = "15px";
        div.style.fontWeight = "700";
        div.style.textDecoration = "underline";

        

    };
    $.connection.hub.start().done(function () {
        $('.addButton').click(function () {
            if ($("#tableView_Customer_Login").val() == null) {
                game.server.sentNotification($("#tableView_Login").val(), notification,pin);
            }
            else {
                game.server.sentNotification($("#tableView_Customer_Login").val(), notification,pin);
            }
        });
        $('.addFriend').click(function () {
            game.server.sentNotification($("#UserLogin").val(), notification,pin);
        })

    });
});
