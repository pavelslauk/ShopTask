$(".deletelink").click(function deletefunc(event) {
    var data = event.target.dataset;
    if (confirm('Remove ' + data.title + '?')) {
        $.post(data.deleteurl, { productId: data.id }, function (data) {
            if (data == 'True') {
                alert('Success!')
                $.ajax({
                    url: data.updateurl,
                    type: "GET",
                    dataType: "html",
                    success: function (data) {
                        var result = $('<div />').append(data).find('.tablebody').html();
                        $('#main').html(result);
                    }
                })
            }
            else {
                alert('Error!')
            }
        });
    }
});