function updateProduct(dataProduct) {
    for (var item in dataProduct) {
        try {
            var id = dataProduct[item][0];
            var value = dataProduct[item][1];
        } catch (e) {

        }
        var product = document.getElementById("product_" + id).innerHTML;

        document.getElementById("product_" + id).innerHTML = parseInt(product) + parseInt(value);
        //console.log(document.getElementById("product_" + id).innerHTML);
    }

}

function fillWithData(dataProduct) {
    for (var item in dataProduct) {
        try {
            $("#products").append("<div id='product_label_" +  dataProduct[item][0] + "'>:</div><div id='product_" + dataProduct[item][0] + "'>"+parseInt(dataProduct[item][2]) + "</div>");
        } catch (e) {

        }
    }
}

$(document).ready(function () {
//TakeValues();
})

function TakeValues() {
    $.ajax({
        url: '/Ajax/ShowProduct',
        type: 'Post',
        dataType: 'Json',
        success: function (dataProduct) {
            console.log(dataProduct);
            fillWithData(dataProduct);
            setInterval(function () { updateProduct(dataProduct); }, 1000);
        },
        async: true,
        processData: false
    });
}




