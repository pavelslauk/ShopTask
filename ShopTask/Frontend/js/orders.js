var selectors = {
    cartBody: '.js-cart-body',
    productsListBody: '.js-order-products-body'
};

var ProductsList = require('./orders/products-list-view-model');
var productsList = new ProductsList()

ko.applyBindings(productsList, $(selectors.productsListBody)[0]);
ko.applyBindings(productsList.cart, $(selectors.cartBody)[0]);
