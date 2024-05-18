var StartupOne = {

    MouseOver: function (elemento) {
        $(elemento).animate({ opacity: 0.6 }, 100).css('color', '#000');
        $(elemento).find("p.insc-workshop span").css('border-color', '#000');
    },
    MouserOut: function (elemento) {
        $(elemento).animate({ opacity: 1 }, 100).css('color', '#fff');
        $(elemento).find("p.insc-workshop span").css('border-color', '#fff');
    },
    MouseOver_et_workshop: function (elemento, cor, posicao) {
        $(elemento).css('color', cor);
        $('#bl-etapas-workshop').css('background-position', posicao);
    },
    MouseOut_et_workshop: function (elemento, cor, posicao) {
        $(elemento).css('color', cor);
        $('#bl-etapas-workshop').css('background-position', posicao);
    }

};



var AutoComplete = {
    'Limpar': false,
    'MaximoGrupo': function (input, maximo) {
        if ($("div#divSelecionados label").length == maximo) {
            $(input).attr("disabled", true);
            return;
        }
        $(input).attr("disabled", false);
    },
    'Palavras': [],
    'EmUso': function () {
        var emUso = [];
        $("div#divSelecionados  input[type='hidden']").each(function (index, value) {
            emUso.push($(value).val());
        });
        return emUso;
    },
    'BuscarPalavras': function (palavra, url) {
        var self = this;
        var itensEmUso = self.EmUso();
        self.Palavras = [];

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify({ busca: palavra, selecionados: itensEmUso }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: true,
            success: function (data) {
                if (data != null) {
                    for (var cont = 0; data.length > cont; cont++) {
                        var inserir = true;
                        for (var i = 0; i < itensEmUso.length; i++) {

                            if (itensEmUso[i] == data[cont].RM) {
                                inserir = false;
                            }
                        }
                        if (inserir) {
                            self.Palavras.push({ "label": data[cont].RM + " - " + data[cont].Nome, "value": data[cont].RM });
                        }
                    }
                }
            },
            error: function () {
                self.words = [];
            },
            complete: function () {
                $("img.loading").hide();
            },
            beforeSend: function () {
                $("img.loading").show();
            }
        });
    },
    'autoComplete': function (input, maximo) {
        var self = this;
        $(input).autocomplete({
            source: self.Palavras,
            select: function (event, ui) {

                var name = $("div.formulario").length > 0 ? "EntregaStartupOneData.RM" : "obStartupOnePropostaData.Integrantes";

                var html = "<span><span class='text-bold numero-rm'>" + ui.item.label.split('-')[0] + "</span>";
                html += "<span>" + ui.item.label.split('-')[1] + "</span>";
                html += "<span class='imgRemove btn-remover-integrantes'>x</span></br>";
                html += "<input type='hidden' name=" + name + " value='" + ui.item.value + "' /></span>";
                self.Limpar = true;
                $("div#divSelecionados").append(html);
                self.MaximoGrupo(input, maximo);
                setTimeout(function () {
                    $(input).val('');
                    $(input).focus();
                }, 100);
            },
            close: function (event, ui) {

                if (self.Limpar) {
                    $(input).val("");
                    self.Limpar = false;
                }
            }
        });
    }
};


var StartupOneEntrega = {

    exibir: true,
    anexoWindow: null,
    Upload: function (arquivo, nomeDiretorioPessoa) {

        setTimeout(function () {
            var path = arquivo.split("/");

            $("input#EntregaStartupOneData_Arquivo").val(nomeDiretorioPessoa);
            //$("a#anexo").attr('href', arquivo);
            $("span#arquivoPessoa").html("<a target='_blank' class='i-envio-arquivo-nome' href='" + arquivo + "'>" + unescape(nomeDiretorioPessoa) + "</a>");
            //$("a#anexo").show();
        }, 150);


    },
    AtualizarConfirme: function () {
        if (this.exibir) {
            $("div.entrega input[type!='button']").attr({ "readonly": false, "disabled": false });
            $("div.entrega textarea").attr({ "readonly": false, "disabled": false });
            $("div.entrega img.imgRemove").show();
            $("div.entrega #alerta").hide();
            $("div.entrega input#btnEnviar").val("Próximo Passo");
            $("div.entrega input#anexar").attr("disabled", false);
            return;
        }

        $("div.entrega input[type!='button']").attr({ "readonly": "readonly", "disabled": "disabled" });
        $("div.entrega textarea").attr({ "readonly": "readonly", "disabled": "disabled" });
        $("div.entrega img.imgRemove").hide();
        $("div.entrega #alerta").show();
        $("div.entrega input#btnEnviar").val("Concluir Entrega");
        $("div.entrega input#anexar").attr("disabled", true);

    }

};


var StartupOneWorkshop = {
    ExibirOficina: function (id, title) {
        ModalPopup(title, 380, 920, false, false, "/StartupOne/Calendario?codigo=" + id, "");
    },
    Auxiliar: function (url, codigo, mensagemErro) {

        $.ajax({
            url: url,
            type: 'POST',
            cache: false,
            data: { 'codigo': codigo },
            success: function (data) {
                if (data.Status) {
                    var link = $("li#" + codigo).find("p[class*=inscrito]");
                    if (link.css("display") != "none")
                        link.css("display", "none");
                    else
                        link.css("display", "block");
                }
                $("div.mensagem").html(data.Mensagem);
                $("div.mensagem").fadeIn(1500).fadeOut(2000);
                return;
            },
            error: function (erro) {
                $("div.mensagem").html(mensagemErro);
                $("div.mensagem").fadeOut(1000);
            }
        });

    },
    Participar: function (codigo) {
        StartupOneWorkshop.Auxiliar('/StartupOne/Participar', codigo, "Não foi possivel realizar sua inscrição, tente novamente");
    },
    Remover: function (codigo) {
        StartupOneWorkshop.Auxiliar('/StartupOne/Remover', codigo, "Não foi possivel remover sua inscrição, tente novamente");
    }
};


var IrAte = function (elemento) {
    $('html,body').animate({ scrollTop: $(elemento).offset().top }, 'slow');
}

var Contador = function (length, cont) {
    var count = (cont - length);
    var frase = "Até 0 caracteres";
    if (count > -1)
        frase = "Até " + count + " caracteres";

    return frase;
}


$(function () {
    //EVENTOS STARTUPONE
    

    $('#alterar-arquivo').on('click', function (e) {
        $("#entrega-arquivo").show();
        $("#entrega-arquivo-anexado").hide();
        e.preventDefault();
    });


    $("input#Arquivo").bind("change", function () {
        var input = $("input[type='file']")[0];
        $("#erro-arquivo").text('')
        if ($(input).val() != '') {
            $('input#TamanhoArquivo').val(input.files[0].size);
            var nome = $(input).val().split("\\")[2];
            var token = $('input[name="__RequestVerificationToken"]');
            $("#btnEnviar").hide()

            var form_data = new FormData();
            form_data.append("Arquivo", $('input[type=file]')[0].files[0])
            form_data.append("TamanhoArquivo", input.files[0].size)
            form_data.append("__RequestVerificationToken", token)
            $.ajax({
                url: "/startupone/upload",
                dataType: 'json',
                cache: false,
                contentType: false,
                processData: false,
                data: form_data,
                type: 'post',
                beforeSend: function () {
                    $(".spinner-overlay").css('display', 'block')
                    $(".spinner-content").css('display', 'block')
                },
                xhr: function () {  
                    request = $.ajaxSettings.xhr();
                    if (request.upload) { 
                        request.upload.addEventListener('progress', function (e) {
                            if (e.total > 50000000) {
                                $("#erro-arquivo").text('Arquivo deve ter no máximo 50 MB.')
                                $(".spinner-overlay").css('display', 'none')
                                $(".spinner-content").css('display', 'none')
                                request.abort()
                                return false;
                            }
                        }, false);
                    }
                    return request;
                },
                success: function (data) {
                    var result = jQuery.parseJSON(JSON.stringify(data));
                    if (result.sucesso) {
                        $(".spinner-overlay").css('display', 'none')
                        $(".spinner-content").css('display', 'none')
                        $("#entrega-arquivo").hide();
                        $("#btnEnviar").show()
                        $("#entrega-arquivo-anexado").show();
                        StartupOneEntrega.Upload(result.arquivoSalvo, nome ? nome : "Arquivo.zip");
                    } else {
                        $(".spinner-overlay").css('display', 'none')
                        $(".spinner-content").css('display', 'none')
                        $("#entrega-arquivo").show();
                        $("#entrega-arquivo-anexado").hide();
                        $("#erro-arquivo").text(result.msg)
                    }
                },
                error: function (e) {
                    console.log(e)
                    $(".spinner-overlay").css('display', 'none')
                    $(".spinner-content").css('display', 'none')
                    $("#entrega-arquivo").show();
                    $("#entrega-arquivo-anexado").hide();
                    $("#erro-arquivo").text(JSON.parse(e.responseText))
                }
            });
        }

    });

    $(document).on("change", "select#turmas", function () {

        var form = "<form id='turma' action='/Pessoa/StartupOne' method='POST'><input type='hidden' value='" + $(this).val() + "' name='turma' /></form>";
        $("body").append(form);
        $("form#turma").submit();

    });


    //FAQ 
    $("li#funcionamento").on("click", "span.spanPergunta", function () {
        var reposta = $(this).parents(".liPerguntaResposta").find(".spanResposta");
        if (reposta.css("display") == 'none') {
            reposta.show();
            return false;
        }
        reposta.hide();
    });
    //Colocando opacidade nas caixas de workshop ao passar o mouse
    $('li.workshop-um a').on({
        mouseover: function () {
            StartupOne.MouseOver(('li.workshop-um'));
        },
        mouseout: function () {
            StartupOne.MouserOut(('li.workshop-um'));
        }
    });
    $('li.workshop-dois a').on({
        mouseover: function () {
            StartupOne.MouseOver(('li.workshop-dois'));
        },
        mouseout: function () {
            StartupOne.MouserOut(('li.workshop-dois'));
        }
    });
    $('li.workshop-tres a').on({
        mouseover: function () {
            StartupOne.MouseOver(('li.workshop-tres'));
        },
        mouseout: function () {
            StartupOne.MouserOut(('li.workshop-tres'));
        }
    });
    $('li.workshop-quatro a').on({
        mouseover: function () {
            StartupOne.MouseOver(('li.workshop-quatro'));
        },
        mouseout: function () {
            StartupOne.MouserOut(('li.workshop-quatro'));
        }
    });
    $('li.workshop-cinco a').on({
        mouseover: function () {
            StartupOne.MouseOver(('li.workshop-cinco'));
        },
        mouseout: function () {
            StartupOne.MouserOut(('li.workshop-cinco'));
        }
    });
    $('li.workshop-seis a').on({
        mouseover: function () {
            StartupOne.MouseOver(('li.workshop-seis'));
        },
        mouseout: function () {
            StartupOne.MouserOut(('li.workshop-seis'));
        }
    });
    //VISUALIZAR ITEM DA ROLETA
    $("div#roleta-icons").on("click", " a", function () {
        var _idx = $(this).index();
        $('.txt-roleta > li').fadeOut(0).eq(_idx).fadeIn(0);
        $('#roleta').attr('class', 'roleta').addClass('_' + _idx);
        return false;
    });
    $("ul#bl-etapas-workshop").on("click", "a#inscricao", function () {
        return false;
    });
    //TROCANDO IMAGEM  DOS BATÕES QUADRADOS AO PASSAR O MOUSE
    $("li.et-workshop-tres a").on({
        mouseover: function (e) {
            StartupOne.MouseOver_et_workshop(this, "#6dc2fd", '0 -229px');
        },
        mouseout: function (e) {
            StartupOne.MouseOut_et_workshop(this, "#fff", '0 0');
        }
    });
    $("li.et-workshop-seis a").on({
        mouseover: function (e) {

            StartupOne.MouseOver_et_workshop(this, "#ff0000", '0 -452px');

        },
        mouseout: function (e) {
            StartupOne.MouseOut_et_workshop(this, "#fff", '0 0');
        }
    });
    //Colocando opacidade nas caixas de workshop ao passar o mouse
    $('li.workshop-um a').on({
        mouseover: function () {
            StartupOne.MouseOver(('li.workshop-um'));
        },
        mouseout: function () {
            StartupOne.MouserOut(('li.workshop-um'));
        }
    });
    $('li.workshop-dois a').on({
        mouseover: function () {
            StartupOne.MouseOver(('li.workshop-dois'));
        },
        mouseout: function () {
            StartupOne.MouserOut(('li.workshop-dois'));
        }
    });
    $('li.workshop-tres a').on({
        mouseover: function () {
            StartupOne.MouseOver(('li.workshop-tres'));
        },
        mouseout: function () {
            StartupOne.MouserOut(('li.workshop-tres'));
        }
    });
    $('li.workshop-quatro a').on({
        mouseover: function () {
            StartupOne.MouseOver(('li.workshop-quatro'));
        },
        mouseout: function () {
            StartupOne.MouserOut(('li.workshop-quatro'));
        }
    });
    $('li.workshop-cinco a').on({
        mouseover: function () {
            StartupOne.MouseOver(('li.workshop-cinco'));
        },
        mouseout: function () {
            StartupOne.MouserOut(('li.workshop-cinco'));
        }
    });
    $('li.workshop-seis a').on({
        mouseover: function () {
            StartupOne.MouseOver(('li.workshop-seis'));
        },
        mouseout: function () {
            StartupOne.MouserOut(('li.workshop-seis'));
        }
    });
    $('img.seta-topo').on("click", function (e) {
        return false;
    });
    $('li#premiacoes').on("click", "a#babson", function () {
        $("div.babson").dialog({
            height: 300,
            width: 750,
            modal: true,
            draggable: false,
            resizable: false
        });
        return false;
    });
    //FIM

    //EVENTOS STARTUPONEPROPOSTA
    $(document).on("click", "a.linkProposta", function (e) {
        ModalPopup('  ', 650, 940, false, false, "/StartupOne/PropostaPartial");
        $(".ui-widget-header").removeClass("ui-widget-header");
    });
    $(document).on("keyup", "div.proposta #obStartupOnePropostaData_Ideia", function () {
        $('#spanIdeia').text(Contador($(this).val().length, 1000));
    });
    $(document).on("keyup", "div.proposta #obStartupOnePropostaData_Porque", function () {
        $('#spanComo').text(Contador($(this).val().length, 1000));
    });
    $(document).on("keypress", "div.proposta input#txtIntegrante", function (e) {
        AutoComplete.BuscarPalavras($(this).val(), '/StartupOne/Integrantes');
        AutoComplete.autoComplete(this, 3);
    });
    $(document).on("click", "div.proposta #btnEnviar", function (e) {
        $(".mensagem-erro").removeClass("validation-summary-errors");
        $(".mensagem-erro").addClass("validation-summary-valid");
        $(".mensagem-sucesso").css("display", "none");

        if ($("#frmProposta").valid()) {
            $.ajax({
                type: "POST",
                url: '/StartupOne/PropostaPartial',
                data: $("#frmProposta").serialize(),
                success: function (data) {
                    if (data.sucesso) {
                        $("#frmProposta").closest('form').find("input[type=text], textarea").val("");
                        $("#divSelecionados label").remove();
                        $("div.mensagem-sucesso p").append(data.mensagem);
                        $(".mensagem-sucesso").css("display", "block");
                        $('#spanIdeia').text(Contador($("#obStartupOnePropostaData_Ideia").val().length));
                        $('#spanComo').text(Contador($("#obStartupOnePropostaData_Porque").val().length));
                    }
                    else {
                        $(".mensagem-erro").removeClass("validation-summary-valid");
                        $(".mensagem-erro").addClass("validation-summary-errors");
                        $(".mensagem-erro ul").empty();
                        $(".mensagem-erro ul").append("<li>" + data.mensagem + "</li>");
                    }
                },
                error: function (data) {
                    ModalAlerta("Aviso", "Houve uma falha, tente novamente", '', 200, 300, '');
                }
            });
        }

        return false;
    });
    //FIM

    //EVENTOS STARTUPONEENTREGA


    $(document).on("click", "div.entrega input.proxChk", function () {


        if ($("input#termo").is(':checked')) {

            $("label#msgcheck").hide();

            $("div.lista").hide();
            $("div.formulario").show();
            //setTimeout(function () {
            //    $("div.carrega").hide();

            //}, 800);

            return;
        }

        $("label#msgcheck").show();

    });

    $(document).on("click", "div.entrega input#antChk", function () {
        if (StartupOneEntrega.exibir) {
            $("div.lista").hide();
            $("div.checklist").show();
            
            //setTimeout(function () {
            //    $("div.carrega").hide();

            //}, 800);
            return;
        }
        StartupOneEntrega.exibir = true;
        StartupOneEntrega.AtualizarConfirme();
    });

    $(document).on("click", "div.entrega input#btnEnviar", function () {
        if (!StartupOneEntrega.exibir) {
            StartupOneEntrega.exibir = true;
            StartupOneEntrega.AtualizarConfirme();
            $("div.entrega input[type='button']").hide();
            //$("div.entrega img#carregando").show();
            $("div.entrega form#frmEntrega").submit();
            return;
        }

        $(".validation-summary-errors ul li").remove();

        var bool = $("form").valid();

        //Remivido regra de no minímo dois integrantes
        //if ($("div.entrega div#divSelecionados label").length <= 0) {
        //    if ($("div.entrega .validation-summary-errors").length == 0) {
        //        $("div.entrega .validation-summary-valid").addClass("validation-summary-errors");
        //    }
        //    $("div.entrega .validation-summary-errors ul").append("<li>Grupo deve ter no mínimo 2 integrantes</li>");
        //    bool = false;
        //    IrAte($(".ui-dialog-titlebar"));
        //}

        if ($("#Integrante").val() != "") {
            if ($("div.entrega .validation-summary-errors").length == 0)
                $("div.entrega .validation-summary-valid").addClass("validation-summary-errors");

            $("div.entrega .validation-summary-errors ul").append("<li>O campo de pesquisa por RM está preenchido. Verifique se todos os integrantes foram adicionados e limpe o campo.</li>");
            bool = false;
        }

        //if (bool && $("div.entrega div#divSelecionados label").length > 0) {
        if (bool) {
            StartupOneEntrega.exibir = false;
            StartupOneEntrega.AtualizarConfirme();
            $("div.entrega .validation-summary-errors").addClass("validation-summary-valid").removeClass("validation-summary-errors");
            return;
        }


        return;
    });

    $(document).on("click", "div.entrega input#antConf", function () {
        $("div.entrega div.lista").hide();
        $("div.entrega div.formulario").show();
    });

    $(document).on("click", "div.entrega input#anexar", function () {

        anexoWindow = window.open("/startupone/Upload", "_blank", "width=480,height=480");

        if ($(this).attr('href') == "#" || $(this).attr('href') == "")
            return false;


    });

    $(document).on("submit", "div.entrega form#frmEntrega", function () {

        $("img#carregando").show();

        $.ajax({
            url: $(this).attr("action"),
            type: $(this).attr("method"),
            data: $(this).serialize(),
            cache: false,
            success: function (data) {
                $("div.entrega").html(data);
            },
            error: function (e) {
                $("div").html("<p><h2>Houve uma falha no momento do envio, tente novamente</h2></p>")
            }
        });

        return false;
    });

    $(document).on("click", "a.entrega", function () {
        ModalPopup('  ', 'auto', 940, false, false, "/StartupOne/Entrega");
        $(".ui-widget-header").removeClass("ui-widget-header");
        $("div.ui-dialog").css("top", "100px");
    });
     

    $(document).on('click', '#radios-aceita-competicao', function() {
        $('.radio-competicao').not(':checked').attr('checked', true);
        
      //  if($('#check-competicao').attr('checked') == 'checked'){
      //      $('#check-competicao').attr('checked', false);
      //      $('#hidden-competicao').val('false');
      //  }
      //  else{
      //      $('#check-competicao').attr('checked', true);
      //      $('#hidden-competicao').val('true');
      //  }
    });

    //Click RADIO
    $(document).on("click", "div.entrega input#Pnao", function () {
        $("div.entrega input#Asim").attr({ "disabled": true, "checked": false });
        $("div.entrega input#Csim").attr({ "disabled": true, "checked": false });
        $("div.entrega input#Anao").attr({ "disabled": true, "checked": true });
        $("div.entrega input#Cnao").attr({ "disabled": true, "checked": true });
    });

    $(document).on("click", "div.entrega input#Psim", function () {
        $("div.entrega input#Cnao").attr({ "disabled": false, "checked": false });
        $("div.entrega input#Csim").attr({ "disabled": false, "checked": false });
        $("div.entrega input#Asim").attr({ "disabled": false, "checked": false });
        $("div.entrega input#Anao").attr({ "disabled": false, "checked": false });
    });

    $(document).on("click", "div.entrega input#Asim", function () {
        $("div.entrega input#Cnao").attr({ "disabled": false, "checked": false });
        $("div.entrega input#Csim").attr({ "disabled": false, "checked": false });
    });

    $(document).on("click", "div.entrega input#Anao", function () {
        $("div.entrega input#Csim").attr({ "disabled": true, "checked": false });
        $("div.entrega input#Cnao").attr({ "disabled": true, "checked": true });
    });

    //contador de caracteres
    $(document).on("keyup", "div.entrega #EntregaStartupOneData_Tema", function () {
        $('div.entrega span#spanTema').text(Contador($(this).val().length, 255));
    });


    $(document).on("keyup", "#txtNomeStartup", function () {
        $('span#spanNomeContador').text(Contador($(this).val().length, 50));
    });


    $(document).on("keyup", "#txtProblemaResolve", function () {
        $('span#spanProblemaContador').text(Contador($(this).val().length, 250));
    });


    $(document).on("keyup", "#txtPropostaValor", function () {
        $('span#spanPropostaContador').text(Contador($(this).val().length, 500));
    });


    $(document).on("keyup", "#txtPublicoAlvo", function () {
        $('span#spanPublicoContador').text(Contador($(this).val().length, 500));
    });


    $(document).on("keyup", "div.entrega input#Integrante", function (e) {

        if ($.trim($(this).val()).length > 3)
            AutoComplete.BuscarPalavras($(this).val(), '/StartupOne/IntegrantesEntrega');
        else
            AutoComplete.Palavras = [];


        AutoComplete.autoComplete(this, 3);
    });

    $("div.entrega div.lista").filter(function (index, value) {
        if ($(value).css("display") != 'none') {
            $("div.entrega ol.seguimento li:eq(" + index + ")").addClass("ativo");
        }
    });

    $("div.entrega input#termo").attr("checked", false);

    StartupOneEntrega.AtualizarConfirme();

    //FIM

    //EVENTOS STARTUPONWorkshop
    $(document).on("mouseover", "ul#datas .cor-cinza a", function () {
        $(this).parent().css({
            border: "2px #b1b1b1 solid",
            background: "#fff",
            color: "#b1b1b1",
            width: "130px",
            height: "142px"
        });
        $(this).parent().find('hr').css('background-color', '#b1b1b1')
    });
    $(document).on("mouseout", "ul#datas .cor-cinza a", function () {
        $(this).parent().css({
            border: "0",
            background: "#b1b1b1",
            color: "#fff",
            width: "130px",
            height: "142px"
        });
        $(this).parent().find('hr').css('background-color', '#fff');
    });
    $(document).on("mouseover", "ul#datas .cor-azul a", function () {
        $(this).parent().css({
            border: "2px #6dc2fd solid",
            background: "#fff",
            color: "#6dc2fd",
            width: "130px",
            height: "142px"
        });
        $(this).parent().find('hr').css('background-color', '#6dc2fd');
    });
    $(document).on("mouseout", "ul#datas .cor-azul a", function () {
        $(this).parent().css({
            border: "0",
            background: "#6dc2fd",
            color: "#fff",
            width: "130px",
            height: "142px"
        });
        $(this).parent().find('hr').css('background-color', '#fff');
    });
    $(document).on("mouseover", "ul#datas .cor-vermelho a", function () {
        $(this).parent().css({
            border: "2px #fb696e solid",
            background: "#fff",
            color: "#fb696e",
            width: "130px",
            height: "142px"
        });
        $(this).parent().find('hr').css('background-color', '#fb696e');
    });
    $(document).on("mouseout", "ul#datas .cor-vermelho a", function () {
        $(this).parent().css({
            border: "0",
            background: "#fb696e",
            color: "#fff",
            width: "130px",
            height: "142px"
        });
        $(this).parent().find('hr').css('background-color', '#fff');
    });
    $("ul[id*='canvas']").on("click", "a", function () {
        StartupOneWorkshop.ExibirOficina($(this).attr('id'), "Inscrição Workshop");
        return false;
    });
    $(document).on("click", "ul#datas li", function () {

        if ($(this).attr("Ativo") == "false") {
            ModalAlerta("Aviso", "<label style='color:red;text-align:center'>Inscrições encerradas ! </label>", '', 200, 200, '');
            return;
        }

        if ($(this).find("p[class*=inscrito]").css("display") == 'none') {
            ModalPergunta("Aviso", "Confirme sua participação neste workshop ? ", "", 200, 200, StartupOneWorkshop.Participar, $(this).attr('id'), "");
            return false;
        }

        ModalPergunta("Aviso", "Deseja cancelar sua participação ? ", "", 200, 200, StartupOneWorkshop.Remover, $(this).attr('id'), "");
        return false;

    });

    //FIM

    //EVENTO PARA ENTREGA E PROPOSTA
    $(document).on("click", ".imgRemove", function (e) {
        $(this).parent().remove();
        AutoComplete.MaximoGrupo($("div.formulario").length > 0 ? $("input#Integrante") : $("input#txtIntegrante"), 3);
    });

});