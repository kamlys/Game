var user_login;
var user_friend;
var friend_user;

function initialization() {
    $(".addButton").click(function () {
        var sender_login = $("#User_Identity_Name").val();
        var theme = $("#viewModel_Theme").val();
        var customer_login = $("#viewModel_Customer_Login").val();
        pin = "Nowa wiadomość";
    });

    $(".sendMessageBtn").click(function () {
        var customer_login = $("#Login").val();
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
        console.log(user_friend, friend_user);
        $.ajax({
            type: "POST",
            url: 'User/AcceptFriend',
            data: JSON.stringify({ a: id }),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                window.setTimeout(getFriendList, 2000);
            }
        });
    });

    $(".disagree").click(function () {
        pin = "Zaproszenie odrzucone";
        var id = $(this).data('userid');
        user_friend = $(this).data('userlogin');
        friend_user = $(this).data('friendlogin');
        $.ajax({
            type: "POST",
            url: 'User/DontAcceptFriend',
            data: JSON.stringify({ a: id }),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                window.setTimeout(getFriendList, 2000);
            }
        });
    });
}

function getFriendList() {
    $.ajax({
        type: "GET",
        url: 'User/_FriendList',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var $temp = $(data).filter("#FriendBox");
            $("#FriendBox").html($temp);
            initialization();
            initFriend();
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
            initialization();
            initNot();
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
            initialization();
            initDeal();
        }
    });
}

$(document).ready(function () {
    initialization();
    $(function () {
        var user = $("#User_Identity_Name").val();
        var game = $.connection.gameHub;
        game.client.shownotification = function (user, pin) {
            window.setTimeout(getDeals, 2000);
            window.setTimeout(getFriendList, 2000);
            window.setTimeout(getNotifications, 2000);

            var $toastContent = $('<span>' + pin + '</span>');
            Materialize.toast($toastContent, 10000);

        };
        $.connection.hub.start().done(function () {
            $('.sendMessageBtn').click(function () {
                if ($("#viewModel_Customer_Login").val() == undefined) {
                    game.server.sentNotification($("#Login").val(), pin);
                }
                else{
                    game.server.sentNotification($("#viewModel_Customer_Login").val(), pin);
                }
            });
            $('#friendMessage').click(function () {
                game.server.sentNotification($("#viewModel_Friend_Login").val(), pin);
            });
            $('.addFriend').click(function () {
                game.server.sentNotification($("#UserLogin").val(), pin);
            });
            $('#addDeal').click(function () {
                game.server.sentNotification($("#viewModel_User2_Login").val(), pin);
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
