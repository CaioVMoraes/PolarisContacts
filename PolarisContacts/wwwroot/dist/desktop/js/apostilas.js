window.___gcfg = {
    lang: 'pt-BR'
};

$(document).ready(function () {
    var Arquivo = {
        type: "json",
        transport: {
            read: {
                url: function (options) {
                    return "/Apostilas/Arquivo/" + options.codigoDisciplina;
                }
            }
        },
        schema: {
            model: {                
                hasChildren: "temPasta",
                children: "pasta"
            }
        }
    };

    var Disciplina = {
        type: "json",
        transport: {
            read: {
                url: function (options) {
                    return "/Apostilas/Disciplina/" + options.ano + '|' + options.codigoCurso + '|' + options.turma;
                }
            }
        },
        schema: {
            model: {
                id: "codigoDisciplina",
                hasChildren: true,
                children: Arquivo
            }
        }
    };

    var Turma = {
        type: "json",
        transport: {
            read: {
                url: function (options) {
                    return "/Apostilas/Turma/" + options.ano + '|' + options.codigoCurso;
                }
            }
        },
        schema: {
            model: {
                id: "turma",
                hasChildren: true,
                children: Disciplina
            }
        }
    };

    var Curso = {
        type: "json",
        schema: {
            model: {
                id: "codigoCurso",
                hasChildren: true,
                children: Turma
            }
        },
        transport: {
            read: {
                url: function (options) {
                    return "/Apostilas/Curso/" + options.ano;
                }
            }
        }
    };

    var Ano = new kendo.data.HierarchicalDataSource({
        type: "json",
        transport: {
            read: {
                url: "/Apostilas/Ano"
            }
        },
        schema: {
            model: {
                hasChildren: true,
                id: "ano",
                children: Curso
            }
        }
    });

    $("#treeview").kendoTreeView({
        dataSpriteCssClassField: "extensao",
        // dataUrlField: "link",
        dataSource: unidade == "G" || unidade == "T" ? Ano : Curso,
        dataTextField: unidade == "G" || unidade == "T" ? ["ano", "descricaoCurso", "turma", "descricaoDisciplina", "retornaVazio"] : ["descricaoCurso", "turma", "descricaoDisciplina", "nomePasta", "retornaVazio"],
        checkboxes: {
            template: kendo.template($("#treeview-template").html())
        }
        // Removido o upload pelo Dropbox e pelo Drive
        //, checkboxes: {
        //    template: kendo.template($("#treeview-template").html()),
        //    checkChildren: true
        //}
    });   

    $(".k-in").on("click", function (e) {
        var versao = parseInt($.browser.version, 10);

        if (!$.browser.msie || ($.browser.msie && versao > 8)) {
            var treeview = $("#treeview").data("kendoTreeView");

            treeview.toggle($(e.target).closest(".k-item"));

            $(e.target).removeClass("k-state-selected");
        }
    });

    $("#treeview").on("click", "a.linkNovoItem", function () {
        $(this).removeClass("linkNovoItem");
        $(this).find(".imgNovo").css("display", "none");
    });

    // Removido o upload pelo Dropbox e pelo Drive.
    //$("#treeview").data("kendoTreeView").dataSource.bind("change", function () {        
    //    var versao = parseInt($.browser.version, 10);

    //    if ((!$.browser.msie && !$.browser.opera) || ($.browser.msie && versao > 8)) {
    //        gapi.savetodrive.go('treeview');
    //    }
    //});

    // Removido o upload pelo Dropbox e pelo Drive.
    //$(".btDropbox").on("click", function () {
    //    var obj = $(this);
    //    var id = $(this).attr("id");
    //    var local = "http://" + stBase + "/Updown/upload_fiap/pessoas/apostilas/" + encodeURI(id);
    //    var nomeArquivo = id;

    //    if ($(obj).hasClass("dropbox-dropin-success")) {
    //        return false;
    //    }

    //    var browser = Dropbox.isBrowserSupported();
    //    if (!browser) {
    //        ModalAlerta('Aviso', 'Browser não suporta essa funcionalidade! <br /><br />Atualize o seu navegador ou tente com outro!', '', 240, 260);
    //        return false;
    //    }

    //    HabilitaImagem(obj, true);
        
    //    options = {
    //        files: [
    //            { 'url': local, 'filename': nomeArquivo }
    //        ],
    //        success: function () {
    //            HabilitaImagem(obj, false, true);
    //        },
    //        progress: function (progress) {
    //            HabilitaImagem(obj, false, false, true);
    //        },
    //        cancel: function () {
    //            HabilitaImagem(obj, true);
    //        },
    //        error: function (err) {
    //            HabilitaImagem(obj, false, false, false, true);
    //        }
    //    }

    //    Dropbox.save(options);

    //    return false;
    //});
});

// Removido o upload pelo Dropbox e pelo Drive.
//function HabilitaImagem(obj, padrao, sucesso, progresso, erro) {
//    if (padrao) {
//        $(obj).addClass("dropbox-dropin-default");
//        $(obj).removeClass("dropbox-dropin-success");
//        $(obj).removeClass("dropbox-dropin-progress");
//        $(obj).removeClass("dropbox-dropin-error");
//    }
//    else if (sucesso) {
//        $(obj).addClass("dropbox-dropin-success");
//        $(obj).removeClass("dropbox-dropin-default");
//        $(obj).removeClass("dropbox-dropin-progress");
//        $(obj).removeClass("dropbox-dropin-error");
//    }
//    else if (progresso) {
//        $(obj).addClass("dropbox-dropin-progress");
//        $(obj).removeClass("dropbox-dropin-default");
//        $(obj).removeClass("dropbox-dropin-success");
//        $(obj).removeClass("dropbox-dropin-error");
//    }
//    else if (erro) {
//        $(obj).addClass("dropbox-dropin-error");
//        $(obj).removeClass("dropbox-dropin-default");
//        $(obj).removeClass("dropbox-dropin-progress");
//        $(obj).removeClass("dropbox-dropin-success");
//    }
//}