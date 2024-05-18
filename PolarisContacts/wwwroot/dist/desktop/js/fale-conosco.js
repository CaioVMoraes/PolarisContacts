$(function () {
    $(".js-fale-conosco-open").click(function () {
        var codigo = $(this).attr("id");
        var titulo = $(this).children().text();

        $("#hdnCodigo").val(codigo);
        $("#hdnTitulo").val(titulo);
        $("#frmFaleConosco").submit();

        return false;
    });

    $("#btnSalvar").click(function () {
        $(".mensagemSucessoAlterar").remove();

        if ($('#frmFaleConosco').valid()) {
            $.ajax({
                cache: false,
                type: "POST",
                url: $("#frmFaleConosco").attr("action"),
                data: $("#frmFaleConosco").serialize(),
                success: function (result) {
                    $("#mensagemSucessoAlterar").html(result);
                    $("#Titulo").val('');
                    $("#Descricao").val('');
                },
                error: function (result) {
                    ModalAlerta('Erro', 'Erro ao enviar o e-mail.', '', 300, 300, '');
                }
            });
        }

        return false;
    });

    $("#btnVoltar").click(function () {
        window.location.href = "/Pessoa/FaleConosco";

        return false;
    });
})