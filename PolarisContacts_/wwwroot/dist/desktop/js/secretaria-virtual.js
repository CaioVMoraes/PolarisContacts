/// <reference path="https://code.jquery.com/jquery-1.9.1.min.js" />
/// <reference path="jquery.creditCardValidator.js" />
/// <reference path="jquery.maskedinput.min.js" />
/// <reference path="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.9/jquery.validate.min.js" />


// Remover erros do form
var removerErrosForm = function (form) {
    form.validate().resetForm();

    // Remove validation summary
    form.find("[data-valmsg-summary=true]").removeClass("validation-summary-errors").addClass("validation-summary-valid").find("ul").empty();

    // Remove erros no form
    form.find('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
    form.find('.input-validation-error').removeClass('input-validation-error').addClass('valid');
}

var mascara = {
    'numeroDoCartao': '9999 9999 9999 9999',
    'validadeDoCartao': '99/99',
    'cvvDoCartao': '999?9'
};

var actions = {

    'limparPopups': function () {

        $(document).on('click', '#pagar-protocolo', function () {

            removerErrosForm($(document).find('#form-pagamento-com-cartao'));
            actions.msgValidar('', '');

        });

        $(document).on('click', '#carregar-conversas-protocolo', function () {

            removerErrosForm($(document).find('#form-enviar-mensagem-protocolo'));
            $(document).find('textarea[name="ConversaProtocolo.Mensagem"]').val('');
            actions.msgValidar('', '');

        });

        $(document).on('click', '#cancelar-protocolo-btn', function () {

            removerErrosForm($(document).find('#frm-cancelar-protocolo'));
            $(document).find('textarea[name="CancelarProtocolo.MotivoCancelamento"]').val('');
            actions.msgValidar('', '');

        });

        $(document).on('click', '#carregar-arquivos-protocolo', function () {

            $(document).find('input[name="arquivoAnexado"]').val('');
            $(document).find('input[name="descricaoAnexado"]').val('');
            $(document).find('#btn-enviar-arquivo').hide();
            actions.msgValidar('', '');

        });

        $(document).find('#avancar-etapa').on('click', function () {

            removerErrosForm($(document).find('#frm-avancar-etapa'));
            $(document).find('textarea[name="AvancarEtapa.Observacao"]').val('');
            $(document).find('#btn-avancar-etapa').removeAttr('disabled');
            $(document).find('#btn-avancar-etapa').text('Avançar');

        });

    },

    'cancelarProtocolo': function () {

        var btnCancelarProtocolo = $(document).find('#btn-cancelar-protocolo');
        var frmCancelarProtocolo = $(document).find('#frm-cancelar-protocolo');
        var txtMotivoCancelamento = $(document).find('textarea[name="CancelarProtocolo.MotivoCancelamento"]');

        btnCancelarProtocolo.on('click', function () {
            actions.msgValidar('', '');
            if (frmCancelarProtocolo.valid()) {

                var statusCancelamento;

                $.ajax({

                    url: frmCancelarProtocolo.attr('action'),
                    type: frmCancelarProtocolo.attr('method'),
                    data: frmCancelarProtocolo.serialize(),
                    beforeSend: function () {
                        btnCancelarProtocolo.attr('disabled', 'disabled');
                        btnCancelarProtocolo.text('Cancelando solicitação...');
                        txtMotivoCancelamento.attr('readonly', 'readonly');
                        actions.msgValidar('', '');
                        removerErrosForm(frmCancelarProtocolo);
                    },
                    success: function (r) {
                        if (r.Status !== 200) {
                            actions.msgValidar(r.Mensagem, 'erro');
                            btnCancelarProtocolo.removeAttr('disabled');
                            txtMotivoCancelamento.removeAttr('readonly');
                            btnCancelarProtocolo.text('Pronto');
                        } else {
                            statusCancelamento = r.Status;
                        }
                    },
                    error: function (r) {
                        actions.msgValidar('Ocorreu um erro, tente novamente.', 'erro');
                        btnCancelarProtocolo.removeAttr('disabled');
                        btnCancelarProtocolo.text('Pronto');
                    },
                    complete: function () {
                        if (statusCancelamento === 200) {
                            actions.msgValidar('Solicitação cancelada com sucesso.', 'sucesso');
                            setTimeout(function () {
                                window.location.reload();
                            }, 2000);
                        }
                    }

                });

            }

        });

    },

    'popup': function (_close) {
        $(document).find('.show-popup').magnificPopup({
            delegate: 'a',
            removalDelay: 500,
            callbacks: {
                beforeOpen: function () {
                    this.st.mainClass = this.st.el.attr('data-effect');
                },
                close: _close,
            },
            midClick: true
        });
    },

    'msgValidar': function (msg, tipo) {
        tipo = tipo.toLowerCase();
        var msgInfoValidar = $(document).find('.msg-info-validar');

        var cor = (tipo === 'sucesso' ? 'green' : (tipo === 'erro' ? 'red' : '#8a6d3b'));

        msgInfoValidar.text(msg);
        msgInfoValidar.css('color', cor);
    },

    'carregarTimeline': function () {

        $(document).find('.timeline-protocolo').on('click', function () {

            $(document).find('#codigo-protocolo').attr('value', $(this).attr('data-protocolo'));
            $(document).find('#frm-timeline').submit();

        });

    },

    'novoProtocolo': function () {

        var boxTipoProtocolo = $(document).find('#box-tipo-protocolo'),
            boxNovoProtocolo = $(document).find('#box-novo-protocolo');

        $(document).on('click', '#btn-novo-protocolo', function () {
            boxTipoProtocolo.show();
            boxNovoProtocolo.hide();
        });

        $(document).on('click', '#btn-voltar-novo-protocolo', function () {
            boxTipoProtocolo.show();
            boxNovoProtocolo.hide();
            $(document).find('textarea[name="CadastrarNovoProtocolo.Observacao"]').val('');
        });

        $(document).on('click', '.abrir-tipo-protocolo', function () {

            var protocolo = $(this).attr('data-protocolo');

            var viewEtapa = $(this).attr('data-etapa');

            if (viewEtapa !== '') {

                $(document).find('#tipo-solicitacao-hidden-field').attr('value', protocolo);

                $.ajax({
                    url: viewEtapa,
                    type: 'get',
                    data: {
                        'solicitacao': protocolo
                    },
                    async: true,
                    beforeSend: function () {
                        boxNovoProtocolo.html('<p><center>Aguarde, carregando...</center></p>');
                        boxTipoProtocolo.hide();
                        boxNovoProtocolo.show();
                    },
                    success: function (r) {
                        boxNovoProtocolo.html(r);
                    },
                    error: function (e) {
                        boxNovoProtocolo.html('<p><center>Ocorreu um erro. Por favor, tente novamente.</center></p>');
                    }
                }).done(function () {
                    var script = document.getElementById('script-fluxo-alternativo'),
                        body = document.getElementsByTagName('body')[0];
                    if (script) {
                        body.removeChild(script);
                    }

                    script = document.getElementById('script-generic-fluxos');

                    if (script) {
                        var newscript = document.createElement('script');

                        newscript.type = 'text/javascript';
                        newscript.async = true;
                        newscript.src = script.value;
                        newscript.id = 'script-fluxo-alternativo';
                        body.appendChild(newscript);
                    }
                });

            } else {

                $.ajax({
                    url: '/SecretariaVirtual/ObterEtapaProtocoloAsync',
                    type: 'post',
                    async: true,
                    data: {
                        'protocolo': protocolo
                    },
                    beforeSend: function () {
                        boxNovoProtocolo.html('<p><center>Aguarde, carregando...</center></p>');
                        boxTipoProtocolo.hide();
                        boxNovoProtocolo.show();
                    },
                    success: function (r) {
                        boxNovoProtocolo.html(r);
                    },
                    error: function (e) {
                        boxNovoProtocolo.html('<p><center>Ocorreu um erro. Por favor, tente novamente.</center></p>');
                    }
                });

            }

        });

        $(document).on('click', '#btn-concluir-novo-protocolo', function () {
            $(this).attr('disabled', 'disabled');
            $(this).text('Concluindo...');
            $(document).find('textarea[name="CadastrarNovoProtocolo.Observacao"]').attr('readonly', 'readonly');
            $(document).off('click', '#btn-concluir-novo-protocolo').on('click', '#btn-concluir-novo-protocolo', function () { return false; });
            $(document).find('#btn-voltar-novo-protocolo').attr('disabled', 'disabled');
            $(document).off('click', '#btn-voltar-novo-protocolo');
            $(document).find('#frm-novo-protocolo').submit();
            return false;
        });
    },

    'uploadNovoArquivoDoProtocolo': function () {

        $(document).on('change', 'input[name="arquivoAnexado"]', function () {

            var file = this.files[0],
                extensao = file.name.substr(file.name.lastIndexOf('.') + 1),
                btnEnviarArquivo = $(document).find('#btn-enviar-arquivo'),
                tamanho = file.size;

            $.ajax({

                url: '/SecretariaVirtual/UploadIsValidExtension',
                type: 'post',
                data: {
                    'extensao': extensao,
                    'tamanho': tamanho
                },
                beforeSend: function () {
                    actions.msgValidar('Aguarde, estamos verificando o seu arquivo...', 'sucesso');
                    btnEnviarArquivo.hide();
                },
                success: function (r) {
                    if (r.valido) {
                        btnEnviarArquivo.show();
                        actions.msgValidar('', '');
                    } else {
                        actions.msgValidar(r.mensagem, r.tipo);
                        btnEnviarArquivo.hide();
                    }
                }

            });

        });

        $(document).on('click', '#btn-enviar-arquivo', function () {
            $(this).attr('disabled', 'disabled');
            $(this).text('Enviando...');
            $(document).off('click', '#btn-enviar-arquivo').on('click', '#btn-enviar-arquivo', function () { return false; });
            $(document).find('#form-enviar-arquivo-protocolo').submit();
            return false;
        });
    },

    'formaDePagamentoDoProtocolo': function () {

        var btnPagarComCartao = $(document).find('#pagamento-cartao'),
            btnPagarComBoleto = $(document).find('#pagamento-boleto'),
            frmPagamentoComBoleto = $(document).find('#frm-pagamento-boleto'),
            frmPagametoComCartao = $(document).find('#frm-pagamento-cartao'),
            btnPagarComCartaoEnviar = $(document).find('#btn-pagar-com-cartao');


        btnPagarComBoleto.on('click', function () {

            btnPagarComCartao.show();
            btnPagarComBoleto.show();
            frmPagametoComCartao.hide();
            frmPagamentoComBoleto.hide();

        });

        btnPagarComCartao.on('click', function () {

            btnPagarComCartao.hide();
            btnPagarComBoleto.hide();
            frmPagametoComCartao.show();
            frmPagamentoComBoleto.hide();

        });

        $(document).on('click', '.voltar-forma-pagamento', function () {

            frmPagametoComCartao.hide();
            frmPagamentoComBoleto.hide();
            btnPagarComBoleto.show();
            btnPagarComCartao.show();

            $(document).find('input[name="PagamentoCartao.NumeroDoCartao"]').val('');
            $(document).find('input[name="PagamentoCartao.NomeNoCartao"]').val('');
            $(document).find('input[name="PagamentoCartao.ValidadeDoCartao"]').val('');
            $(document).find('input[name="PagamentoCartao.CvvDoCartao"]').val('');

            removerErrosForm($(document).find('#form-pagamento-com-cartao'));
            $(document).find('.msg-info-validar').text('');

        });

        actions.popup(function () {

            btnPagarComBoleto.show();
            btnPagarComCartao.show();
            frmPagamentoComBoleto.hide();
            frmPagametoComCartao.hide();
            btnPagarComCartaoEnviar.removeAttr('disabled');
            btnPagarComCartaoEnviar.text('Pagar');
            $(document).find('.voltar-forma-pagamento').text('Voltar');
            $(document).find('input[name="PagamentoCartao.NumeroDoCartao"]').val('');
            $(document).find('input[name="PagamentoCartao.NomeNoCartao"]').val('');
            $(document).find('input[name="PagamentoCartao.ValidadeDoCartao"]').val('');
            $(document).find('input[name="PagamentoCartao.CvvDoCartao"]').val('');


        });
    },

    'alterarBandeiraPorNumeroDoCartao': function () {

        try {

            var numeroDoCartao = $(document).find('input[name="PagamentoCartao.NumeroDoCartao"]'),
            bandeiraDoCartao = $(document).find('input[name="PagamentoCartao.BandeiraDoCartao"]'),
            iconeDoCartao = $(document).find('#icon-bandeira-do-cartao');

            numeroDoCartao.validateCreditCard(function (result) {

                bandeiraDoCartao.val(result.card_type == null ? '' : result.card_type.name);

                switch (result.card_type == null ? '' : result.card_type.name) {

                    case 'amex':
                        iconeDoCartao.attr('class', 'fa fa-cc-amex');
                        break;

                    case 'diners':
                        iconeDoCartao.attr('class', 'fa fa-cc-discover');
                        break;

                    case 'visa':
                        iconeDoCartao.attr('class', 'fa fa-cc-visa');
                        break;

                    case 'master':
                        iconeDoCartao.attr('class', 'fa fa-cc-mastercard');
                        break;

                    case 'discover':
                        iconeDoCartao.attr('class', 'fa  fa-cc-discover');
                        break;

                    default:
                        break;

                }

            });

        } catch (e) {

        }

    },

    'validarStatusTransacaoDoCartao': function (codigoTransacao) {

        switch (codigoTransacao) {
            case 0:
                actions.msgValidar('Pagamento efetuado com sucesso.', 'sucesso');
                break;

            case 1:
                actions.msgValidar('Este cartão não possui uma afiliação válida.', 'info');
                break;

            case 2:
                actions.msgValidar('Saldo insuficiente para efetuar transação.', 'info');
                break;

            case 3:
                actions.msgValidar('Não foi possível recuperar as informações do seu cartão.', 'info');
                break;

            case 4:
            case 22:
                actions.msgValidar('Erro ao contatar operadora. Tente novamente mais tarde.', 'info');
                break;

            case 5:
                actions.msgValidar('Tipo de transação inválida.', 'info');
                break;

            case 7:
                actions.msgValidar('Transação negada.', 'info');
                break;

            case 8:
                actions.msgValidar('Pagamento efetuado com sucesso.', 'info');
                break;

            case 9:
                actions.msgValidar('Seu pagamento está sendo processado pela operadora do cartão.', 'info');
                break;

            case 12:
                actions.msgValidar('Problemas com o cartão de crédito. Contate sua operadora.', 'erro');
                break;

            case 13:
                actions.msgValidar('Cartão de crédito cancelado ou inválido.', 'erro');
                break;

            case 14:
                actions.msgValidar('Cartão de crédito bloqueado ou inválido.', 'erro');
                break;

            case 15:
                actions.msgValidar('Cartão de crédito expirado ou inválido.', 'erro');
                break;

            case 16:
            case 17:
                actions.msgValidar('A transação foi abortada, pois uma possível tentativa de fraude foi detectada.', 'erro');
                break;

            case 18:
                actions.msgValidar('Não foi possível efetuar a transação. Tente novamente.', 'erro');
                break;

            case 19:
                actions.msgValidar('Valor inválido.', 'erro');
                break;

            case 20:
                actions.msgValidar('Problemas com a operadora.', 'erro');
                break;

            case 21:
                actions.msgValidar('Número de cartão inválido.', 'erro');
                break;

            case 23:
                actions.msgValidar('Cartão de crédito protegido ou desabilitado.', 'erro');
                break;

            default:
                actions.msgValidar('Não foi possível efetuar a transação.', 'erro');
                break;
        }

    },

    'pagarProtocoloComCartao': function () {

        var numeroDoCartao = $(document).find('input[name="PagamentoCartao.NumeroDoCartao"]');
        var validadeDoCartao = $(document).find('input[name="PagamentoCartao.ValidadeDoCartao"]');
        var cvvDoCartao = $(document).find('input[name="PagamentoCartao.CvvDoCartao"]');
        var frmPagarComCartao = $(document).find('#form-pagamento-com-cartao');
        var frmPagarComCartaoInputs = $(document).find('#form-pagamento-com-cartao :input');
        var btnPagarComCartao = $(document).find('#btn-pagar-com-cartao');
        var btnVoltarPagamentoCartao = $(document).find('#btn-voltar-pagamento');

        validadeDoCartao.mask('99/2099');
        cvvDoCartao.mask('999?9');

        btnPagarComCartao.on('click', function () {

            if (frmPagarComCartao.valid()) {

                var codigoTransacao;

                $.ajax({

                    url: frmPagarComCartao.attr('action'),
                    type: frmPagarComCartao.attr('method'),
                    data: frmPagarComCartao.serialize(),
                    beforeSend: function () {
                        btnPagarComCartao.text('Aguarde...');
                        btnPagarComCartao.attr('disabled', 'disabled');
                        frmPagarComCartaoInputs.attr('disabled', 'disabled');
                        btnVoltarPagamentoCartao.hide();
                        actions.msgValidar('', '');

                    },
                    success: function (r) {

                        if (r.tipo === 'Erro') {

                            actions.msgValidar(r.mensagem, r.tipo);
                            frmPagarComCartaoInputs.removeAttr('disabled');
                            btnPagarComCartao.removeAttr('disabled');
                            btnPagarComCartao.text('Pagar');
                            removerErrosForm(frmPagarComCartao);
                            btnVoltarPagamentoCartao.show();

                        } else if (r.tipo === 'Sucesso') {

                            actions.msgValidar(r.mensagem, r.tipo);
                            removerErrosForm(frmPagarComCartao);

                        } else if (r.tipo === 'InternalServerError') {

                            actions.msgValidar('Ocorreu um erro, por favor tente novamente.', 'Erro');
                            frmPagarComCartaoInputs.removeAttr('disabled');
                            btnPagarComCartao.removeAttr('disabled');
                            btnPagarComCartao.text('Pagar');
                            removerErrosForm(frmPagarComCartao);
                            btnVoltarPagamentoCartao.show();

                        }

                        if (r.objeto) {
                            var objeto = JSON.parse(r.objeto);

                            actions.validarStatusTransacaoDoCartao(objeto.Payment.ReasonCode);

                            codigoTransacao = objeto.Payment.ReasonCode;
                        }
                        else {
                            actions.validarStatusTransacaoDoCartao(99);
                        }
                    },
                    error: function () {
                        btnPagarComCartao.text('Pagar');
                        btnPagarComCartao.removeAttr('disabled');
                        frmPagarComCartaoInputs.removeAttr('disabled');
                        actions.msgValidar('Informações de pagamento inválidas. Por favor, corrija-os e tente novamente.', 'erro');
                        removerErrosForm(frmPagarComCartao);
                        btnVoltarPagamentoCartao.show();
                    },
                    complete: function () {
                        if (codigoTransacao === 0 || codigoTransacao === 9) {
                            removerErrosForm(frmPagarComCartao);
                            btnPagarComCartao.text('Atualizando a página...');
                            setTimeout(function () {
                                window.location.reload();
                            }, 5000);

                        } else {
                            removerErrosForm(frmPagarComCartao);
                            btnPagarComCartao.text('Pagar');
                            btnPagarComCartao.removeAttr('disabled');
                            frmPagarComCartaoInputs.removeAttr('disabled');
                            btnVoltarPagamentoCartao.show();
                        }
                    }
                });

            }

        });

    },

    'anexarMensagemAoProtocolo': function () {

        var formEnviarMensagem = $(document).find('#form-enviar-mensagem-protocolo');
        var btnEnviarMensagem = $(document).find('#btn-enviar-mensagem');
        var msgEnviadaComSucesso = $(document).find('#msg-sucesso-enviada');
        var textAreaMensagem = $(document).find('textarea[name="ConversaProtocolo.Mensagem"]');

        btnEnviarMensagem.on('click', function () {

            if (formEnviarMensagem.valid()) {

                $.ajax({

                    url: formEnviarMensagem.attr('action'),
                    type: formEnviarMensagem.attr('method'),
                    data: formEnviarMensagem.serialize(),
                    beforeSend: function () {
                        btnEnviarMensagem.text('Enviando...');
                        btnEnviarMensagem.attr('disabled', 'disabled');
                        removerErrosForm(formEnviarMensagem);
                    },
                    success: function (r) {

                        if (r.status === "Sucesso") {
                            $(document).find('#nenhuma-mensagem').hide();
                            var divItensConversas = $(document).find('.timeline-conversa-body');
                            var fotoDoPessoaBase64 = $(document).find('.foto-pessoa').attr('src');
                            var nomeDoPessoa = $(document).find('#nome-pessoa').text();
                            var msgEnviadaDoPessoa = $(document).find('textarea[name="ConversaProtocolo.Mensagem"]').val();

                            var ultimaMensagemDoPessoa = '<div class="item pessoa"><img class="conversa-foto-pessoa" src="' + fotoDoPessoaBase64 + '"><span class="data"><i class="ion-clock"></i>' + r.dataHora + '</span><p class="mensagem"><b>' + nomeDoPessoa + '</b><br><span>' + msgEnviadaDoPessoa + '</span></p></div>';
                            divItensConversas.append(ultimaMensagemDoPessoa);
                            $(document).find('.timeline-conversa-body').scrollTop($(document).find('.ion-clock').last().offset().top + 5000)

                            msgEnviadaComSucesso.text('Mensagem enviada com sucesso.');
                            msgEnviadaComSucesso.css('color', 'green');
                            msgEnviadaComSucesso.show();

                        } else {

                            msgEnviadaComSucesso.text('Não foi possível enviar sua mensagem, tente novamente.');
                            msgEnviadaComSucesso.css('color', 'red');
                            msgEnviadaComSucesso.show();

                        }

                    },
                    error: function () {
                        msgEnviadaComSucesso.text('Erro ao enviar mensagem, tente novamente.');
                        msgEnviadaComSucesso.css('color', 'red');
                        msgEnviadaComSucesso.show();
                    },
                    complete: function () {
                        btnEnviarMensagem.text('Enviar ').append('<i class="ion-arrow-right-b"></i>');
                        btnEnviarMensagem.removeAttr('disabled');
                        textAreaMensagem.val('');
                        setTimeout(function () {
                            msgEnviadaComSucesso.fadeOut();
                        }, 2000);

                    }
                });

            }

        });

    },
};

$(function () {

    actions.carregarTimeline();
    actions.novoProtocolo();
    actions.uploadNovoArquivoDoProtocolo();
    actions.formaDePagamentoDoProtocolo();
    actions.pagarProtocoloComCartao();
    actions.alterarBandeiraPorNumeroDoCartao();
    actions.anexarMensagemAoProtocolo();
    actions.limparPopups();
    actions.cancelarProtocolo();

    $(document).find('.msg-close').on('click', function () {
        $('.msg-sucesso').hide();
        $('.msg-aviso').hide();
        $('.msg-info').hide();
        $('.msg-erro').hide();
    });

    $('#form-enviar-arquivo-protocolo').on('keyup keypress', function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
            return false;
        }
    });

});