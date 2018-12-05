module.exports = function (data) {
    var self = this;
    self.title = data.Title;
    self.price = data.Price;
    self.description = data.Description;
    self.category = data.Category;
    self.count = ko.observable(1);
    self.totalPrice = ko.computed(function () {
        return Number((self.price * self.count()).toFixed(2));
    });
};