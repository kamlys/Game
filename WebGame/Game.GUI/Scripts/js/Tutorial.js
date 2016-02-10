$(document).ready(function () {
    tutorialInit();
})

function tutorialInit()
{
    $('.cookieBtn').click(function () {
        var divname = $(this).data('divname');
        $.ajax({
            type: "GET",
            url: '/Tutorial/closeDivTutorial',
            data: { divname: divname },
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $.ajax({
                    type: "POST",
                    url: '/Home/Index',
                    success: function (data) {
                        //$("#cookieDiv").load(document.URL + ' #cookieDiv');
                        $("#cookieDiv").slideUp("slow");
                        initHome();
                        initBuilding();
                        tutorialInit();
                    }
                });
            }
        });
    });

    $('.allDivBtn').click(function () {
        var divname = $(this).data('divname');
        if ($(this).data('choice') == 'on') {
            $.ajax({
                type: "GET",
                url: '/Tutorial/closeDivTutorial',
                data: { divname: divname },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $.ajax({
                        type: "POST",
                        url: '/Home/Index',
                        success: function (data) {
                            location.reload();
                        }
                    });
                }
            });
        }
        else {
            $.ajax({
                type: "GET",
                url: '/Tutorial/closeTutorial',
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $.ajax({
                        type: "POST",
                        url: '/Home/Index',
                        success: function (data) {
                            location.reload();
                        }
                    });
                }
            });
        }
    });

    $('.closeDiv').click(function () {
        var divname = $(this).data('divname');
        console.log(divname);
        if (divname == 'homeDiv') {
            $.ajax({
                type: "GET",
                url: '/Tutorial/closeDivTutorial',
                data: { divname: divname },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $.ajax({
                        type: "POST",
                        url: '/Home/Index',
                        success: function (data) {
                            //$("#homeTutorial").load(document.URL + ' #homeTutorial');
                            $("#homeTutorial").slideUp("slow");
                            initHome();
                            tutorialInit();
                        }
                    });
                }
            });
        }
        else if(divname=='officeDiv')
        {
            $.ajax({
                type: "GET",
                url: '/Tutorial/closeDivTutorial',
                data: { divname: divname },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $.ajax({
                        type: "POST",
                        url: '/Office/Index',
                        success: function (data) {
                            $("#officeTutorial").slideUp("slow");
                            tutorialInit();
                        }
                    });
                }
            });
        }
        else if (divname == 'messageDiv') {
            $.ajax({
                type: "GET",
                url: '/Tutorial/closeDivTutorial',
                data: { divname: divname },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $.ajax({
                        type: "POST",
                        url: '/Message/Index',
                        success: function (data) {
                            $("#messageTutorial").slideUp("slow");
                            tutorialInit();
                        }
                    });
                }
            });
        }
        else if (divname == 'marketDiv') {
            $.ajax({
                type: "GET",
                url: '/Tutorial/closeDivTutorial',
                data: { divname: divname },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $.ajax({
                        type: "POST",
                        url: '/Market/Index',
                        success: function (data) {
                            $("#marketTutorial").slideUp("slow");
                            tutorialInit();
                        }
                    });
                }
            });
        }
        else if (divname == 'casinoDiv') {
            $.ajax({
                type: "GET",
                url: '/Tutorial/closeDivTutorial',
                data: { divname: divname },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $.ajax({
                        type: "POST",
                        url: '/Casino/Index',
                        success: function (data) {
                            $("#casinoTutorial").slideUp("slow");
                            tutorialInit();
                        }
                    });
                }
            });
        }
        else if (divname == 'setDiv') {
            var user = $(this).data('user');
            $.ajax({
                type: "GET",
                url: '/Tutorial/closeDivTutorial',
                data: { divname: divname },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $.ajax({
                        type: "POST",
                        url: '/User/Profil',
                        data: { user: user },
                        success: function (data) {
                            $("#setTutorial").slideUp("slow");
                            tutorialInit();
                        }
                    });
                }
            });
        }
    });
}