$(document).ready(function () {
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