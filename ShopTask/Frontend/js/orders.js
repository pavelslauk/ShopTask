var selectors = {
    cartBody: '.js-cart-body',
    productsListBody: '.js-order-products-body'
};

let productsList = require('./ViewModels/productsList');
let cart = require('./ViewModels/cart');
var cartViewModel = new cart();

ko.applyBindings(cartViewModel, $(selectors.cartBody)[0]);
ko.applyBindings(new productsList(cartViewModel), $(selectors.productsListBody)[0]);
