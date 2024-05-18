var $document = $(document)
var $window = $(window)

$document.on('ready', function () {
    /**
     * SVG.
     */
    $.get('/dist/desktop/svg/sprites.svg', function (data) {
        var div = document.createElement('div')

        div.innerHTML = new XMLSerializer().serializeToString(data.documentElement)
        div.style.display = 'none'

        document.body.insertBefore(div, document.body.childNodes[0])
    });

    /**
     * Menu.
     */
    $('.l-menu-toggle').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();

        $('.l-menu-list').toggleClass('is-active');
    });

    /**
     * Banners.
     */
    var $banners = $('.l-banner-link');
    var $bannersItem = $('.l-banner-nav-link');
    var bannersLength = $banners.length;

    function bannerChange(slide) {
        $banners.removeClass('is-active').eq(slide).addClass('is-active');
        $bannersItem.removeClass('is-active').eq(slide).addClass('is-active');
    }

    $bannersItem.on('click', function () {
        var index = $(this).parent().index();

        bannerChange(index);

        return false;
    });

    setInterval(function () {
        var index = $banners.filter('.is-active').index();

        index++;

        if (bannersLength > index) {
            bannerChange(index);
        } else {
            bannerChange(0);
        }
    }, 10000);

    bannerChange(0);
}).on('click', function () {
    $('.l-menu-list').removeClass('is-active');
});



function MensagemErro(mensagem, segundos) {
    mensagem = mensagem === undefined ? 'Ops! Ocorreu um erro.' : mensagem
    segundos = segundos === undefined ? 5 : segundos

    var $alertaPortalPessoa = $("#alertaPortalPessoa");

    $alertaPortalPessoa.text(mensagem)

    $alertaPortalPessoa.removeClass();
    $alertaPortalPessoa.addClass('l-alert');
    $alertaPortalPessoa.addClass('is-active');
    $alertaPortalPessoa.addClass('alert-error');


    setTimeout(function () {
        $alertaPortalPessoa.removeClass('is-active');
    }, segundos * 1000);
}

function MensagemSucesso(mensagem) {
    var $alertaPortalPessoa = $("#alertaPortalPessoa");

    $alertaPortalPessoa.text(mensagem);

    $alertaPortalPessoa.removeClass();
    $alertaPortalPessoa.addClass('l-alert');
    $alertaPortalPessoa.addClass('is-active');
    $alertaPortalPessoa.addClass('alert-success');


    setTimeout(function () {
        $alertaPortalPessoa.removeClass('is-active');
    }, 5000);
}

