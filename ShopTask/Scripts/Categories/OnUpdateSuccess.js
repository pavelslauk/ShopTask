var selectors = {
    categories: '#categories'
};

function updateComplete(result) {
    if (result) {
        alert('Update success!');
    }
    else {
        alert('Error!');
    }

    var categoriesUrl = $(selectors.categories).attr('data-categories-url');
    $.ajax({
        url: categoriesUrl,
        success: function (result) {
            $(selectors.categories).html(result)
        }
    });
}