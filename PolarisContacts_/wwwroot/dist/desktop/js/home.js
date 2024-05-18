$document.on('ready', function () {
    /**
  * Mensagem de Informação
  */

    var $info = $('#MensagemInfo');

    if ($info && $info.html()) {
        setTimeout(function () {
            $info.addClass('is-disabled')
        }, 3500);
    }

    /**
     * Mensagem.
     */
    var $novidade = $('#novidade');
    var $botaoAcao = $('#botaoSalvarNovidade');
    // sessão utilizada para controlar abertura do modal no on-load  da pagina
    var modalVisto = sessionStorage.getItem('modalVisto') === 'true';
    if (!modalVisto) {
        if ($novidade && $("#novidade").html()) {
            var $iframe = $('#novidade').find('iframe')

            if ($iframe && $iframe.length > 0) {
                $.magnificPopup.open({
                    modal: $iframe.data("bloquear"),
                    enableEscapeKey: false,
                    closeOnBgClick: false,
                    items: {
                        src: $iframe.attr('src'),
                        type: 'iframe',
                        closeOnBgClick: 'false',
                        enableEscapeKey: 'false'
                    }
                });


            } else {
                if ($botaoAcao && $botaoAcao.length > 0) {
                    $.magnificPopup.open({
                        closeOnBgClick: false,
                        enableEscapeKey: false,
                        items: {
                            src: '#novidade',
                            type: 'inline'
                        }
                    });
                } else {
                    $.magnificPopup.open({
                        closeOnBgClick: true,
                        items: {
                            src: '#novidade',
                            type: 'inline'
                        }
                    });
                }
            }

            $(".mfp-close").remove();
        }
    }

    $("#botaoSalvarNovidade").click(function () {
        $(this).prop("disabled", true);
        $(this).text("Aguarde...");
        $.ajax({
            cache: false,
            type: 'POST',
            url: 'SalvarNovidadeVista',
            success: function (result) {
                window.parent.$.magnificPopup.close();
            },
            error: function (result) {
                console.error(result);
                console.log("Erro aqui");
                window.parent.$.magnificPopup.close();
            }
        });
    });



    /**
     * Tabs.
     */
    var tabsToggle = function (href, back) {
        $('.l-menu-link').removeClass('is-active');
        $('.l-content-tab').removeClass('is-active');

        if (href) {
            $('[href="' + href + '"]').addClass('is-active');
            $(href).addClass('is-active');

            if (!back) {
                history.pushState({}, '', href);
            }
        } else {
            $('.l-menu-link:first').addClass('is-active');
            $('.l-content-tab:first').addClass('is-active');
        }
    };

    if (location.hash) {
        tabsToggle(location.hash);
    } else if (!location.hash && $('#favoritos a').length) {
        tabsToggle('#favoritos');
    } else {
        tabsToggle();
    }

    $('.js-content-toggle').on('click', function (e) {
        tabsToggle($(this).attr('href'), false);

        e.preventDefault();
    });

    $window.on('hashchange', function () {
        if (location.hash) {
            tabsToggle(location.hash, true);
        } else if (!location.hash && $('#favoritos a').length) {
            tabsToggle('#favoritos', true);
        } else {
            tabsToggle();
        }

        return false;
    });



    /**
     * Favoritos.
     */
    $('.js-fav-toggle').on('click', function (e) {
        var $this = $(this);
        var id = $this.attr('id');

        if ($this.hasClass('is-active')) {
            $.ajax({
                cache: false,
                type: 'POST',
                url: 'DeletaMinhaArea',
                data: { id: id },
                success: function (result) {
                    $this.removeClass('is-active');
                }
            });
        } else {
            $.ajax({
                cache: false,
                type: 'POST',
                url: 'GravaMinhaArea',
                data: { id: id },
                success: function (result) {
                    $this.addClass('is-active');
                }
            });
        }

        return false;
    });



    /**
     * Avisos.
     */
    $('.js-ajax-link').on('click', function () {
        var $this = $(this);

        var id = $this.attr('id');
        var url = $this.attr('href');

        if (id != undefined) {
            $.ajax({
                cache: false,
                type: 'POST',
                url: 'AtualizaAvisos',
                data: { 'id': id },
                success: function (result) {
                    window.location.href = url;
                }
            });
        } else {
            window.location.href = url;
        }

        return false;
    });

    $('.js-ajax-modal').magnificPopup({
        type: 'ajax',
        fixedContentPos: false,
        callbacks: {
            parseAjax: function () {
                var $this = $(this.currItem.el);
                if ($this.hasClass('is-new')) {
                    $this.removeClass('is-new');
                    $this.addClass('noscroll');
                    var length = $('.i-avisos-link.is-new').length;

                    if (length === 0) {
                        $('.l-menu-link-alert').remove();
                    } else {
                        $('.l-menu-link-alert').text(length);
                    }
                }
            }
        }
    });



    /**
     * Ordem.
     */
    $('.js-order').sortable({
        connectWith: '.ordem',
        update: function (event, ui) {
            var codigo = {};

            $.each($('.classeMinhaArea'), function (index, valor) {
                codigo[index] = $(valor).attr('id');
            });

            $.ajax({
                cache: false,
                type: 'POST',
                url: 'OrdenarMinhaArea',
                data: { 'codigo': codigo }
            });
        }
    });

    $('.js-order').disableSelection();
});