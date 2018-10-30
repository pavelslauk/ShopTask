$(function () {
    $('.deletelink').click(function (event) {
        var itemId = event.target.getAttribute('data-id');
        var deleteUrl = document.getElementById('productTable').getAttribute('data-delete-url');
        var row = event.target.parentElement.parentElement;
        if (confirm('Remove ' + $(row.children[0]).text() + '?')) {
            $.post(deleteUrl, { productId: itemId }, function (result) {
                if (result) {
                    alert('Success!');
                    $(row).fadeOut(1000, function () {
                        if (row.parentElement.childElementCount > 2) {
                            $(row).remove();
                        }
                        else {
                            $('#productTable').remove();
                            $('#main').prepend('<p>No products</p>');
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
