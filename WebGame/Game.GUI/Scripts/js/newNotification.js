var user_login;
var user_friend;
var friend_user;

$(".addButton").click(function () {
    var sender_login = $("#User_Identity_Name").val();
    var theme = $("#tableView_Theme").val();
    var customer_login = $("#tableView_Customer_Login").val();
    console.log(customer_login);
    pin = "Nowa wiadomość";
});

$(".sentMessage").click(function () {
    var customer_login = $("#tableView_Customer_Login").val();
    console.log(customer_login);
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

$(".agree").click(function () {
    pin = "Zaproszenie zaakceptowane";
    var id = $(this).data('userid');
    user_friend = $(this).data('userlogin');
    friend_user = $(this).data('friendlogin');
    console.log(id);
    $.ajax({
        type: "POST",
        url: 'User/AcceptFriend',
        data: JSON.stringify({ a: id }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
        }
    });
    console.log(user_friend, friend_user)
});

$(".disagree").click(function () {
    pin = "Zaproszenie odrzucone";
    var id = $(this).data('userid');
    user_friend = $(this).data('userlogin');
    friend_user = $(this).data('friendlogin');
    console.log(id);
    $.ajax({
        type: "POST",
        url: 'User/DontAcceptFriend',
        data: JSON.stringify({ a: id }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
        }
    });
    console.log(user_friend, friend_user)
});

function getFriendList() {
    $.ajax({
        type: "GET",
        url: 'User/_FriendList',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#FriendBox").html($(data).html());
        }

    });
}

function getNotifications() {
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

function getDeals() {

    $.ajax({
        type: "GET",
        url: 'Office/_UserDealList',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#dealBox").html($(data).html());
        }

    });
}

$(document).ready(function () {
    $(function () {

        var user = $("#User_Identity_Name").val();
        var game = $.connection.gameHub;
        game.client.shownotification = function (user, pin) {
            window.setTimeout(getDeals, 2000);
            window.setTimeout(getFriendList(), 2000);
            window.setTimeout(getNotifications, 2000);

            var $toastContent = $('<span>' + pin + '</span>');
            Materialize.toast($toastContent, 10000);

        };
        $.connection.hub.start().done(function () {
            $('.sentMessage').click(function () {
                console.log($("#tableView_Customer_Login").val());
                if ($("#tableView_Customer_Login").val() == undefined) {
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
                game.server.sentNotification(user_login, pin);
            });
            $('.agree').click(function () {
                game.server.sentNotification(user_friend, pin);
                game.server.sentNotification(friend_user, pin);
            });
            $('.disagree').click(function () {
                game.server.sentNotification(user_friend, pin);
                game.server.sentNotification(friend_user, pin);
            });
        });
    });
});
