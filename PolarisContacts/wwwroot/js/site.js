function ModalDadosContato() {
    $("#loaderHome").fadeIn()

    $.ajax({
        type: "GET",
        url: "/Home/ModalDadosContato",
        success: function (data) {
            $("#loaderHome").fadeOut()

            if (data) {
                $("#ModalDadosContato").html(data);
                $("#modal-novo-contato").modal("show");
            }
        }
    });
};