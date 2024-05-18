var $form = $('.a-senha-form');
var $formFeedback = $('.a-senha-error');
var $formButton = $('.a-senha-btn');

$formButton.on('click', function () {
    if ($form.valid()) {
        $formButton.attr('disabled', 'disabled');

        $.ajax({
            cache: false,
            type: 'POST',
            url: 'EsqueciSenha',
            data: $form.serialize(),
            success: function (result) {
                $('#recaptcha_reload').click();
                if (result.habilitaCampo) {
                    $('.a-senha-row.is-hidden').show();

                    $('#obEsqueciSenhaData_Nome').attr('readonly', true);
                    $('#obEsqueciSenhaData_Usuario').attr('readonly', true);

                    $('.a-senha-error').html(result.mensagem);
                } else if (result.titulo == 'Sucesso') {
                    $('.a-senha-row.is-hidden').hide();

                    $('#obEsqueciSenhaData_Nome').val('');
                    $('#obEsqueciSenhaData_Usuario').val('');
                    $('#obEsqueciSenhaData_Email').val('');
                    $('#obEsqueciSenhaData_Telefone').val('');
                    $('#obEsqueciSenhaData_Nome').attr('readonly', false);
                    $('#obEsqueciSenhaData_Usuario').attr('readonly', false);

                    $formFeedback.html(result.mensagem);
                    $(".a-senha-error").animate({ opacity: 1 });
                } else {
                    $('.a-senha-error').html(result.mensagem);
                    $(".a-senha-error").animate({ opacity: 1 });
                }

                $formButton.removeAttr('disabled');
            },
            error: function (result) {
                $formButton.removeAttr('disabled');

                $('.a-senha-error').html('Erro ao enviar senha!');
            }
        });

        return false;
    }

    return false;
});