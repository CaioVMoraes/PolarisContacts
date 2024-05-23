function ModalNovoContato() {
    $("#loaderHome").fadeIn()

    $.ajax({
        type: "GET",
        url: "/Home/ModalNovoContato",
        success: function (data) {
            $("#loaderHome").fadeOut()

            if (data) {
                $("#ModalNovoContato").html(data);
                $("#modal-novo-contato").modal("show");
            }
        }
    });
};