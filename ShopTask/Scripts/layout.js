$(function () {
    $('.js-culture-select').change(function () {
        var selectedCulture = $(this).val();
        var changeCultureUrl = $(this).attr('data-change-culture-url');
        $.post(changeCultureUrl, { culture: selectedCulture }, function () {
            location.reload(false);
        })
    });
});