var budowanie = false;
var budowanie_szer = 0;
var budowanie_wys = 0;
var budowanie_id = 0;

function buduj(id, width, height) {
    budowanie = true;
    budowanie_szer = width;
    budowanie_wys = height;
    budowanie_id = id;
    $('body').addClass('buduj');
}

function canBuild(col, row) {
    for (var i = col; i < col + budowanie_szer; i++) {
        for (var j = row; j < row + budowanie_wys; j++) {
            if (!$('.tile-' + j.toString() + i.toString()).hasClass('free')) {
                return false;
            }
        }
    }
    return true;
}

function budujAjax(col, row, id) {
    var data = { id: id, col: col, row: row };
    console.log(data);
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

$(document).ready(function () {
    $("body").mousedown(function (ev) {
        if (ev.which == 3) {
            $('body').removeClass('buduj');
        }
    });

    $('map-tile').click(function (ev) {
        if (budowanie) {
            var col = $(this).data('col');
            var row = $(this).data('row');
            if (canBuild(col, row)) {
            }
        }
    });

    $('body').contextmenu(function (ev) {
        console.log(budowanie);
        if (budowanie) {
            $('.map-tile').removeClass('canbuild');
            $('.map-tile').removeClass('cantbuild');
            budowanie = false;
            return false;
        }
    });

    $('.map-tile').hover(function () {
        if (budowanie) {
            $('.map-tile').removeClass('canbuild');
            $('.map-tile').removeClass('cantbuild');
            var col = $(this).data('col');
            var row = $(this).data('row');
            acanBuild = canBuild(col, row);
            var klasa = '';
            if (acanBuild) {
                klasa = 'canbuild';
                $(this).click(function (ev) {
                    budujAjax($(this).data('col'), $(this).data('row'), budowanie_id);
                });
            } else {
                klasa = 'cantbuild';
            }
            for (var i = col; i < col + budowanie_szer; i++) {
                for (var j = row; j < row + budowanie_wys; j++) {
                    $('.tile-' + j.toString() + i.toString()).addClass(klasa);
                }
            }

        }
    });

    $('div[name=remove]').click(function () {
        var ID = $(this).data("buildingid");

        UIkit.modal.confirm("Na pewno chcesz zburzyć ten budynek?", function () {
            burzAjax(ID);
        });

    });
});
