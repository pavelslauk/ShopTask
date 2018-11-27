$(function () {

    var selectors = {
        cultureSelect: '.js-culture-select',
    };

    $(selectors.cultureSelect).change(function () {
        var selectedCulture = $(selectors.cultureSelect).val();
        var changeCultureUrl = $(selectors.cultureSelect).attr('data-change-culture-url');
        window.location.href = changeCultureUrl + '?culture=' + selectedCulture; 
    });
});