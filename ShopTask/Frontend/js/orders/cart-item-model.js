module.exports = function (data) {
    var self = this;
    self.title = data.Title;
    self.productPrice = data.Price;
    self.description = data.Description;
    self.category = data.Category;
    self.productsCount = ko.observable(1);
    self.totalPrice = ko.computed(function () {
        return Number((self.productPrice * self.productsCount()).toFixed(2));
    });
};