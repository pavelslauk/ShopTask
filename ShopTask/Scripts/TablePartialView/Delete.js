function deletefunc(id, title) {
    if (confirm("Remove " + title + "?")) {
        $.post('/Home/DeleteProduct', { productId: id }, function (data) {
            if (data == "True") {
                alert("Success!")
                var url = '/Home/Index';
                window.location.href = url;
            }
            else {
                alert("Error!")
            }
        });
    }
}