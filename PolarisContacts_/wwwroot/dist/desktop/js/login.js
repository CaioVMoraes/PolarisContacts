$(function () {
    var $form = $('.a-login-form');
    var $formSend = $('.a-login-btn');

    $formSend.on('click', function () {
        var usuario = $('#usuario').val()

        if (usuario && isNaN(usuario) && usuario.toLowerCase().substring(0, 2) !== "rm") {
            var login = window.open('about:blank', 'login', 'resizable=1, status=1, scrollbars=1, width=' + window.innerWidth + ', height=' + window.innerHeight + ', top=0, left=0')
            login.focus();
            
            if(location.href.includes('portaldes')){
                //HOMOLOGAÇÃO
                $form.attr('action', 'http://portaldes.fiap.com.br/programas/login/login/verifica.asp');
            }
            else{
                //PRODUÇÃO
                $form.attr('action', 'https://www2.fiap.com.br/programas/login/login/verifica.asp');
            }

            $form.attr('target', 'login');
            $form.submit();
        } else {

            if($("#autenticacao").length > 0)
                $form.attr('action', '/Pessoa/LogOnAutenticado');
            else
                $form.attr('action', '/Pessoa/LogOn');

            $form.removeAttr('target');
        }
    });

    $('#usuario').focus();
});
