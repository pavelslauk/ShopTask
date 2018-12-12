var ProductItem = require('./cart-item-model');
var Cart = require('./cart-view-model');
getProductsUrl = $('.js-order-products-body').attr('data-products-url');

module.exports = function () {
    var self = this;
    self.cart = new Cart();
    self.productsList = ko.observableArray([]);

    $.getJSON(getProductsUrl, function (allData) {
        var mappedProducts = $.map(allData, function (item) { return new ProductItem(item) });
        self.productsList(mappedProducts);
    });

    self.addToCart = function (product) {
        self.cart.addToCart(product);
    };
};