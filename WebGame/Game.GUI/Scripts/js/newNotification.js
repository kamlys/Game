var user_login;
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

$("#acceptDeal").click(function () {
    user_login = user;
    pin = "Umowa zaakceptowana";
});

$("#cancelDeal").click(function () {
    user_login = user;
    pin = "Umowa odrzucona";
})

$("#addDeal").click(function () {
    pin = "Nowa umowa";
});
function getNotifications() {
    $.ajax({
        type: "GET",
        url: 'notification/_Notification',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
            $("#NotificationBox").html($(data).html());
            $('.infoBox .tabs').tabs('select_tab', 'Notification');
        }

    });
}

function getDeals() {
    console.log("getDeals()");
    $.ajax({
        type: "GET",
        url: 'Office/_UserDealList',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
            $("#dealBox").html($(data).html());
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
            window.setTimeout(getDeals, 2000);
            window.setTimeout(getNotifications, 2000);

            var $toastContent = $('<span>' + pin + '</span>');
            Materialize.toast($toastContent, 10000);

        };
        $.connection.hub.start().done(function () {
            $('.sentMessage').click(function () {
                if ($("#tableView_Customer_Login").val() == null) {
                    game.server.sentNotification($("#tableView_Login").val(), pin);
                }
                else {
                    game.server.sentNotification($("#tableView_Customer_Login").val(), pin);
                }
            });
            $('.addFriend').click(function () {
                game.server.sentNotification($("#UserLogin").val(), pin);
            });
            $('#addDeal').click(function () {
                game.server.sentNotification($("#tableView_Login").val(), pin);
            });
            $('#acceptDeal').click(function () {
                game.server.sentNotification(user_login, pin);
            });
            $('#cancelDeal').click(function () {
                console.log("UserLogin: " + user_login);
                game.server.sentNotification(user_login, pin);
            });

        });
    });
});
