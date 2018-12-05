module.exports = function () {
    var self = this;
    self.cart = ko.observableArray([]);

    self.addToCart = function (product) {
        if (self.cart.indexOf(product) == -1) {
            self.cart.push(product);
        } else {
            self.increaseProductCount(product);
        }
    };

    self.removeFromCart = function (product) {
        self.cart.remove(product);
    };

    self.increaseProductCount = function (product) {
        var productIndex = self.cart.indexOf(product);
        self.cart()[productIndex].count(self.cart()[productIndex].count() + 1);
    };

    self.decreaseProductCount = function (product) {
        var productIndex = self.cart.indexOf(product);
        self.cart()[productIndex].count(self.cart()[productIndex].count() - 1);
        if (self.cart()[productIndex].count() == 0) {
            self.removeFromCart(product);
        }
    };

    self.totalCartPrice = ko.computed(function () {
        var total = 0;
        for (var i = 0; i < self.cart().length; i++) {
            total += self.cart()[i].totalPrice();
        }
        return total.toFixed(2);
    });
};

