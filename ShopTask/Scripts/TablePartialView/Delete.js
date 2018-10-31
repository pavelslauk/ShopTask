$(function () {

    if (!($('#productTable').length)) {
        $('.table-placeholder').removeClass('hidden');
    }

    $('.deletelink').click(function (event) {
        var table = $('#productTable');
        var itemId = $(this).attr('data-id');
        var deleteUrl = table.attr('data-delete-url');
        var row = $(this).closest('tr');

        if (confirm('Remove ' + row.children('.table-product-title').text() + '?')) {
            $.post(deleteUrl, { productId: itemId }, function (result) {
                if (result) {
                    alert('Success!');
                    row.find('td').fadeOut(1000, function () {
                        if (table.find('tr').length > 1) {
                            row.remove();
                        }
                        else {
                            table.remove();
                            $('.table-placeholder').show('fast');
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
