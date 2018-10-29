$('.deletelink').click(function (event) {
    var itemData = event.target.dataset;
    var urls = document.getElementById('productTable').dataset;
    if (confirm('Remove ' + itemData.title + '?')) {
        $.post(urls.deleteUrl, { productId: itemData.id }, function (result) {
            if (result) {
                alert('Success!');
                $('#' + itemData.id).fadeOut(1000);
            }
            else {
                alert('Error!')
            }
        });
    }
});