module.exports = function () {
    var self = this;
    self.cartItems = ko.observableArray([]);

    self.addToCart = function (cartItem) {
        if (self.cartItems.indexOf(cartItem) == -1) {
            self.cartItems.push(cartItem);
        } else {
            self.increaseItemCount(cartItem);
        }
    };

    self.removeFromCart = function (cartItem) {
        self.cartItems.remove(cartItem);
        cartItem.productsCount(1);
    };

    self.increaseItemCount = function (cartItem) {
        var itemIndex = self.cartItems.indexOf(cartItem);
        self.cartItems()[itemIndex].productsCount(self.cartItems()[itemIndex].productsCount() + 1);
    };

    self.decreaseItemCount = function (cartItem) {
        var itemIndex = self.cartItems.indexOf(cartItem);
        self.cartItems()[itemIndex].productsCount(self.cartItems()[itemIndex].productsCount() - 1);
        if (self.cartItems()[itemIndex].productsCount() == 0) {
            self.removeFromCart(cartItem);
        }
    };

    self.totalCartPrice = ko.computed(function () {
        var total = 0;
        for (var i = 0; i < self.cartItems().length; i++) {
            total += self.cartItems()[i].totalPrice();
        }
        return total.toFixed(2);
    });
};

