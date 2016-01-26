function updateProduct(dataProduct) {
    for (var item in dataProduct) {
        try {
            var id = dataProduct[item][0];
            var value = dataProduct[item][1];
        } catch (e) {

        }
        var product = document.getElementById("product_" + id).innerHTML;

        document.getElementById("product_" + id).innerHTML = parseInt(product) + parseInt(value);
    }
}

function fillWithData(dataProduct, productNames) {
    for (var item in dataProduct) {
        try {
            var n = "";
            var id = dataProduct[item][0];
            for (var name in productNames) {
                if (productNames[name][0] == id) {
                    n = productNames[name][1];
                }
            }
            $("#products").append("<div class='productItem' id='product_label_" + dataProduct[item][0] + "'>" + n + ":</div><div class='productItem' id='product_" + dataProduct[item][0] + "'>" + parseInt(dataProduct[item][2]) + "</div>");
        }
        catch (e) {
        }
    }
}
function TakeValues() {
    $.ajax({
        url: '/Ajax/ShowProduct',
        type: 'Post',
        dataType: 'Json',
        success: function (dataProduct) {
            fillWithData(dataProduct);
            setInterval(function () { updateProduct(dataProduct); }, 1000);
        },
        async: true,
        processData: false
    });
}
