var budowanie = false;
var budowanie_szer = 0;
var budowanie_wys = 0;
var budowanie_id = 0;
var szer_mapa = 0;
var wys_mapa = 0;
var budynki;

function buduj(id, width, height) {
    budowanie = true;
    budowanie_szer = width;
    budowanie_wys = height;
    budowanie_id = id;
    $('body').addClass('buduj');
}

function canBuild(colLeft, rowTop) {
    var colRight = colLeft + budowanie_szer - 1;
    var rowBottom = rowTop + budowanie_wys - 1;
    if (colRight >= szer_mapa || rowBottom >= wys_mapa) {
        return false;
    }
    var acanBuild = true;
    for(var a in budynki)
    {
        var x_left = budynki[a][0];
        var x_right = budynki[a][1];
        var y_top = budynki[a][2];
        var y_bottom = budynki[a][3];
        if ((x_left <= colRight && x_right >= colLeft && y_top <= rowBottom  && y_bottom >= rowTop)) {
            acanBuild = false;
            break;
        }
    }
    return acanBuild;
}

function setBudynki(arr)
{
    budynki = arr;
}

function setMapSize(wys, szer)
{
    wys_mapa = wys;
    szer_mapa = szer;
}

function budujAjax(col, row, id) {
    var data = { id: id, col: col, row: row };
    var me = $(this);

    if (me.data('requestRunning')) {
        return;
    }

    me.data('requestRunning', true);

    $.ajax({
        type: "POST",
        url: 'ajax/build',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ a: data }),
        dataType: "json",
        success: function (data) {
            location.reload();
        },
        complete: function () {
            me.data('requestRunning', false);
        }

    });
}

function burzAjax(id) {
    var data = { id: id };
    $.ajax({
        type: "POST",
        url: 'ajax/destroy',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ a: data }),
        dataType: "json",
        success: function (data) {
            location.reload();
        }
    });
}

function getColRow()
{
    var xMap = $('#map').offset().left;
    var yMap = $('#map').offset().top;
    var xBuild = $('.fake-building').offset().left;
    var yBuild = $('.fake-building').offset().top;
    var col = Math.round(xBuild - xMap) / 50;
    var row = Math.round(yBuild - yMap) / 50;
    return [col, row];
}

$(document).ready(function () {
    $("body").mousedown(function (ev) {
        if (ev.which == 3) {
            $('body').removeClass('buduj');
        }
    });

    $('#map').mousemove(function (e) {
        if (!budowanie) return;
        var pos = [e.pageX, e.pageY];
        var map = [$('#map').offset().left, $('#map').offset().top];
        var cursor = [pos[0] - map[0], pos[1] - map[1]];
        $('.fake-building').css('display', 'block');
        $('.fake-building').css('left', (map[0] + cursor[0] -  cursor[0] % 50));
        $('.fake-building').css('top', (map[1] + cursor[1] - cursor[1] % 50));
        $('.fake-building').css('width', budowanie_szer * 50);
        $('.fake-building').css('height', budowanie_wys * 50);
        if (canBuild(getColRow()[0], getColRow()[1])) {
            $('.fake-building').css('background', 'green');
        } else {
            $('.fake-building').css('background', 'red');
        }
    });

    $('.fake-building').click(function (ev) {
        var xMap = $('#map').offset().left;
        var yMap = $('#map').offset().top;
        var xBuild = $('.fake-building').offset().left;
        var yBuild = $('.fake-building').offset().top;
        var col = Math.round(xBuild - xMap)/50;
        var row = Math.round(yBuild - yMap) / 50;
        if (canBuild(col, row)) {
            budujAjax(col, row, budowanie_id);
        }
    });

    $('body').contextmenu(function (ev) {
        if (budowanie) {
            $('.map-tile').removeClass('canbuild');
            $('.map-tile').removeClass('cantbuild');
            $('.fake-building').css('display', 'none');
            budowanie = false;
            return false;
        }
    });

    $('div[name=remove]').click(function () {
        var ID = $(this).data("buildingid");

        UIkit.modal.confirm("Na pewno chcesz zburzyć ten budynek?", function () {
            burzAjax(ID);
        });

    });
});
