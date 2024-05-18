$(function () {
    $(".js-turma-change").on("change", function () {
        $.ajax({
            url: '/Calendario/Visualizar',
            type: 'POST',
            cache: false,
            data: { 'turma': $(this).val() },
            success: function (data) {
                $('div#load').html(data);
                return;
            },
            error: function (erro) {
                window.location.reload();
            }
        });
    });
});