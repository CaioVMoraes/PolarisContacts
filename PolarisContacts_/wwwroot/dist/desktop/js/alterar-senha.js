$(document).ready(function () {
    $('#SenhaAtual').focus();

    $('#frmAlterarSenha').on('submit', function (e) {
        var $this = $(this);

        if ($this.valid()) {
            $.ajax({
                cache: false,
                type: 'POST',
                url: 'SalvarNovaSenha',
                data: $this.serialize(),
                success: function (result) {
                    $this.before('<p>' + result + '</p>');

                    $('#SenhaAtual').val('');
                    $('#NovaSenha').val('');
                    $('#ConfirmaNovaSenha').val('');
                },
                error: function (result) {
                    $this.before('<p>' + result + '</p>');
                }
            });
        }

        e.preventDefault();
    });
});