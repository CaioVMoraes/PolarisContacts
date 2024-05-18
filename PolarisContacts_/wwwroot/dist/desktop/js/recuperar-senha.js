var $form = $('.a-senha-form');
var $formFeedback = $('.a-senha-error');
var $formButton = $('.a-senha-btn');

$formButton.on('click', function () {
    if ($form.valid()) {
        $formButton.attr('disabled', 'disabled');

        $.ajax({
            cache: false,
            type: 'POST',
            url: 'RecuperarSenhaAlterar',
            data: $form.serialize(),
            success: function (result) {
                if (result.habilitaCampo) {
                    $('.a-senha-row.is-hidden').show();

                    $('#NovaSenha').attr('readonly', true);
                    $('#ConfirmaNovaSenha').attr('readonly', true);

                    $('.a-senha-error').html(result.mensagem);
                } else if (result.titulo == 'Sucesso') {
                    $('.a-senha-row.is-hidden').hide();

                    $('#NovaSenha').val('');
                    $('#ConfirmaNovaSenha').val('');
                    $('#NovaSenha').attr('readonly', false);
                    $('#ConfirmaNovaSenha').attr('readonly', false);
                    $formFeedback.html(result.mensagem);
                    $(".a-senha-error").animate({ opacity: 1 });
                }
                else {
                    $('.a-senha-error').html(result.mensagem);
                    $(".a-senha-error").animate({ opacity: 1 });
                }
                $formButton.removeAttr('disabled');
            },
            error: function (data) { 
                window.location.href = "/Pessoa/PaginaIndisponivel";
            }
        });

        return false;
    }

    return false;
});