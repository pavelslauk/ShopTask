$(function () {

    var selectors = {
        productTable: '#productTable',
        tableProductTitle: '.js-table-product-title',
        emptyTablePlaceholder: '.js-empty-table-placeholder'
    };

    $('.deletelink').click(function () {
        var table = $(selectors.productTable);
        var itemId = $(this).attr('data-id');
        var deleteUrl = table.attr('data-delete-url');
        var row = $(this).closest('tr');

        if (confirm('Remove ' + row.children(selectors.tableProductTitle).text() + '?')) {
            $.post(deleteUrl, { productId: itemId }, function (result) {
                if (result) {
                    alert('Success!');
                    row.find('td').fadeOut(1000, function () {
                        row.remove();
                        if (table.find('tr').length == 1) {
                            table.remove();
                            $(selectors.emptyTablePlaceholder).show('fast');
                        }
                    });                    
                }
                else {
                    alert('Error!');
                }
            });
        }
    });
});
