let product = require('../Models/product');
getProductsUrl = $('.js-order-products-body').attr('data-products-url');

module.exports = function (cart) {
    var self = this;
    self.productsList = ko.observableArray([]);

    $.getJSON(getProductsUrl, function (allData) {
        var mappedProducts = $.map(allData, function (item) { return new product(item) });
        self.productsList(mappedProducts);
    });

    self.addToCart = function (product) {
        cart.addToCart(product);
    };
};