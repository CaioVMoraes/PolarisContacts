$.validator.setDefaults({ ignore: [] });

$(function () {

    

    $("input[type=checkbox]").change(function (e) {
        var checkboxes = $(this).closest('div').find("input[type='checkbox']:checked");

        if (checkboxes.length > 0) {
            $(this).closest('div').find(".respondeu").val("true");
        } else {
            $(this).closest('div').find(".respondeu").val("");
        }

        if (checkboxes.length >= 3) {
            $(this).closest('div').find("input[type='checkbox']:unchecked").prop("disabled", "disabled");
            $(this).closest('div').find("input[type='checkbox']:unchecked").addClass("disabled");
        } else {
            $(this).closest('div').find("input[type='checkbox']:unchecked").removeAttr("disabled");
            $(this).closest('div').find("input[type='checkbox']:unchecked").removeClass("disabled");
        }

        if (checkboxes.closest("input[data-outro='True']:checked").length) {
            $(this).closest('div').find("input[type='text']").removeAttr("disabled");
            $(this).closest('div').find("input[type='text']").removeClass("disabled");
        } else {
            $(this).closest('div').find("input[type='text']").prop("disabled", "disabled");
            $(this).closest('div').find("input[type='text']").addClass("disabled");
            $(this).closest('div').find("input[type='text']").val("");
        }
    });

    $('#frmSalvarEnquete').on('submit', function (e) {
        e.preventDefault();

        
        var $this = $(this);


        var naoRespondeu = false;

        if ($(document.activeElement).attr('id') == 'btnNaoResponder')
            naoRespondeu = true;


        if (!$this.valid() && !naoRespondeu) {
            $('html, body').animate({
                scrollTop: ($('.field-validation-error').first().offset().top - 50)
            }, 500);
            return;
        }

        $.ajax({
            cache: false,
            type: 'POST',
            url: 'SalvarEnqueteEntrante',
            data: $this.serialize() + "&naoRespondeu=" + naoRespondeu,
            success: function (result) {
                $(".js-conteudo-enquete").hide(400, () => $(".js-finalizou-enquete").show(400));

                setTimeout(() => window.parent.$.magnificPopup.close(), 5000);
            },
            error: function (result) {
                console.error(result);
            }
        });
    });
});