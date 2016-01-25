$(document).ready(function () {
    speed1 = Math.floor((Math.random() * 140) + 100);
    speed2 = Math.floor((Math.random() * 80) + 100);
    speed3 = Math.floor((Math.random() * 100) + 100);

    $(".productList").hide();
    s1 = $('#slider1').bxSlider({
        mode: 'vertical',
        slideMargin: 50,
        speed: speed1,
        pause: 0,
        pager: false,
        controls: false
    });
    s2 = $('#slider2').bxSlider({
        mode: 'vertical',
        slideMargin: 50,
        speed: speed2,
        pause: 0,
        pager: false,
        controls: false
    });
    s3 = $('#slider3').bxSlider({
        mode: 'vertical',
        slideMargin: 50,
        speed: speed3,
        pause: 0,
        pager: false,
        controls: false
    });
    started = false;
    $("#bet").attr("max", parseInt($("#currDolars").html()));
    $("#start").click(function () {
        var bet = $("#bet").val();
        if (bet == 0 || bet > parseInt($("#currDolars").html())) {
            $("#result").html("Masz za mało pieniędzy!");
            return;
        } else {
            $("#result").html("");
        }
        if (!started) {
            s1.startAuto();
            s2.startAuto();
            s3.startAuto();
            $.ajax({
                type: "POST",
                url: 'Casino/Slots',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ bet: bet }),
                dataType: "json",
                success: function (data) {
                    if (data != "error") {
                        setTimeout(function () {
                            $("#currDolars").html(data[4]);
                            s1.reloadSlider();
                            s2.reloadSlider();
                            s3.reloadSlider();
                            started = false;
                            s1.goToSlide(parseInt(data[0]));
                            s2.goToSlide(parseInt(data[1]));
                            s3.goToSlide(parseInt(data[2]));
                            if (parseInt(data[3]) > 0) {
                                $("#result").html("Wygrywasz: " + data[3] + "!");
                            } else {
                                $("#result").html("Tym razem się nie udało");
                            }
                        }, 2000);
                    } else {
                        s1.reloadSlider();
                        s2.reloadSlider();
                        s3.reloadSlider();
                        $("#result").html("Wystąpił błąd");
                    }
                }
            });
            started = true;
        }
    });

});