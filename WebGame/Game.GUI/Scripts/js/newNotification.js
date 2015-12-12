var pin;

$(".addButton").click(function () {
    var sender_login = $("#User_Identity_Name").val();
    var theme = $("#tableView_Theme").val();
    var customer_login = $("#tableView_Customer_Login").val();
    pin = "Nowa wiadomość";
});

$(".addFriend").click(function () {
    var login = $("#UserLogin").val();
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

function getNotifications()
{
    $.ajax({
        type: "GET",
        url: 'notification/_Notification',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#NotificationBox").html($(data).html());
            $('.infoBox .tabs').tabs('select_tab', 'Notification');
        }

    });
}

$(document).ready(function () {
    $(function () {

        var user = $("#User_Identity_Name").val();
        var game = $.connection.gameHub;
        game.client.shownotification = function (user, pin) {
            //console.log("Wiadomość: " + notification);
            //console.log("Pin: " + pin);
            //$('#notificationList').append(notification);

            //var div = document.getElementById('notifi');
            //div.style.color = "red";
            //div.style.fontSize = "15px";
            //div.style.fontWeight = "700";
            //div.style.textDecoration = "underline";

            //$("#NotificationBox").load('@(Url.Action("_Notification","Notification"))');

            window.setTimeout(getNotifications, 2000);

            var $toastContent = $('<span>' + pin + '</span>');
            Materialize.toast($toastContent, 10000);

        };
        $.connection.hub.start().done(function () {
            $('.sentMessage').click(function () {
                if ($("#tableView_Customer_Login").val() == null) {
                    game.server.sentNotification($("#tableView_Login").val(),pin);
                }
                else {
                    game.server.sentNotification($("#tableView_Customer_Login").val(),pin);
                }
            });
            $('.addFriend').click(function () {
                game.server.sentNotification($("#UserLogin").val(),pin);
            })

        });
    });
});
