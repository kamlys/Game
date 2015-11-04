$(document).ready(function () {
    setInterval(addProducts, 10000);
    addProducts();
});

function addProducts() {
    $.ajax({
        url: '/Ajax/ProductUpdate',
        type: 'Post',

        async: true,
        processData: false
    });
}