var ID = 0;
var user_ID = 0;
var product_ID = 0;
var number = 0;
var price = 0;
var typeOffer = true;

function buyoffer(ID, user_ID, product_ID, price, typeOff) {
    ID = ID;
    user_ID = user_ID;
    product_ID = product_ID;
    number = parseInt(document.getElementById('numValue_' + ID).value);
    price = price;
    typeOffer = typeOff;


    marketbuyoffer(ID, user_ID, product_ID, price, typeOffer);
}

function marketbuyoffer(ID, user_ID, product_ID, price, type) {
    var data = { ID: ID, User_ID: user_ID, Product_ID: product_ID, Price: price, Number: number, TypeOffer: typeOffer };
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