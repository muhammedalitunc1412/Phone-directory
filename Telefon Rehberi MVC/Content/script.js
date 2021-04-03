
    var deleteUrl;
    function Delete(url) {
        deleteUrl = url;
    $('#deleteMessage').modal('show');

    }


    function ResultOK() {
        if (deleteUrl != null)
            location.href = deleteUrl;

    }




