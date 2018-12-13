var selectors = {
    cartBody: '.js-cart-body',
    productsListBody: '.js-order-products-body'
};

var ProductsList = require('./orders/products-list-view-model');
var Cart = require('./orders/cart-view-model');
var cart = new Cart();

ko.applyBindings(cart, $(selectors.cartBody)[0]);
ko.applyBindings(new ProductsList(cart), $(selectors.productsListBody)[0]);