/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(1);


/***/ }),
/* 1 */
/***/ (function(module, exports, __webpack_require__) {

﻿var selectors = {
    cartBody: '.js-cart-body',
    productsListBody: '.js-order-products-body'
};

var ProductsList = __webpack_require__(2);
var productsList = new ProductsList()

ko.applyBindings(productsList, $(selectors.productsListBody)[0]);
ko.applyBindings(productsList.cart, $(selectors.cartBody)[0]);


/***/ }),
/* 2 */
/***/ (function(module, exports, __webpack_require__) {

﻿var ProductItem = __webpack_require__(3);
var Cart = __webpack_require__(4);
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

/***/ }),
/* 3 */
/***/ (function(module, exports) {

﻿module.exports = function (data) {
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

/***/ }),
/* 4 */
/***/ (function(module, exports) {

﻿module.exports = function () {
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



/***/ })
/******/ ]);