$('.deletelink').click(function (event) {
    var itemData = event.target.dataset;
    var tableData = document.getElementById('js-product-table').dataset;
    if (confirm('Remove ' + itemData.title + '?')) {
        $.post(tableData.deleteUrl, { productId: itemData.id }, function (result) {
            if (result) {
                alert('Success!')
                $.ajax({
                    url: tableData.updateUrl,
                    type: 'GET',
                    dataType: 'html',
                    success: function (resultHtml) {
                        $('#main').html(resultHtml);
                    }
                })
            }
            else {
                alert('Error!')
            }
        });
    }
});