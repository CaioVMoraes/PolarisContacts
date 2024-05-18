function AcionandoMascaras() {
    $(".datemask").inputmask({ "mask": "99/99/9999" });
    $(".cepmask").inputmask({ "mask": "99999-999", autoUnmask: true });
    $(".dddmask").inputmask({ "mask": "(99)", autoUnmask: true });
    $(".telefonemask").inputmask({ "mask": "9999-9999", autoUnmask: true });
    $(".celularmask").inputmask({ "mask": "99999-9999", autoUnmask: true });
}

function SelecionarOpcaoPorTexto(selector,valor) {
    $("#" + selector.attr('id') + " option").filter(function () { return $(this).text() == valor }).prop("selected", true)
    $("#" + selector.attr('id')).trigger("change");
}
