﻿var ID = 0;
var user_ID = 0;
var product_ID = 0;
var number = 0;
var price = 0;

function buyoffer(ID, user_ID, product_ID, price) {
    ID = ID;
    user_ID = user_ID;
    product_ID = product_ID;
    number = parseInt(document.getElementById('numValue_'+ID).value);
    price = price;

    console.log(ID, user_ID, product_ID, number, price);

    marketbuyoffer(ID, user_ID, product_ID, price);
}


function marketbuyoffer(ID, user_ID, product_ID, price) {
    var data = { ID : ID, user_ID: user_ID, product_ID: product_ID, price: price, number: number };
    var me = $(this);

    if (me.data('requestRunning')) {
        return;
    }
    me.data('requestRunning', true);
    $.ajax({
        type: "POST",
        url: 'market/buyoffer',
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