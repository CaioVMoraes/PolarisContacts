$(function () {
    $('.js-curso-change').on('change', function () {
        var $this = $(this);
        var value = $this.val();
        var valueTurma = value.substring(0, value.lastIndexOf("-"));
        var valueAno = value.substring(value.lastIndexOf("-") + 1);

        $.ajax({
            url: '/Boletim/Graduacao',
            type: 'POST',
            cache: false,
            data: {
                'Turma': valueTurma,
                'Ano': valueAno
            },
            success: function (data) {
                $('#turma').html(data.Curso.replace('-', ' de '));
                $('#boletim').html(data.Html);
            },
            error: function (erro) {
                window.location.reload();
            },
            beforeSend: function () {
                $('.i-boletim-wrapper').addClass('is-loading');
            },
            complete: function () {
                $('.i-boletim-wrapper').removeClass('is-loading');
            }
        });
    });



    /**
     * NACs.
     */
    $('#boletim').on('click', '.js-nacs-open', function () {
        var $this = $(this);

        var id = $this.parent().find('td.td-disciplina').attr('id');

        $.ajax({
            cache: false,
            type: 'GET',
            url: '/Boletim/Nacs',
            dataType: 'html',
            data: {
                CodDisciplina: id
            },
            success: function (result) {
                $.magnificPopup.open({
                    items: {
                        src: result,
                        type: 'inline'
                    }
                });
            },
            beforeSend: function () {
                $('.i-boletim-wrapper').addClass('is-loading');
            },
            complete: function () {
                $('.i-boletim-wrapper').removeClass('is-loading');
            }
        });
    });



    /**
     * Faltas.
     */
    $('#boletim').on('click', '.js-faltas-open', function () {
        var $this = $(this);

        var id = $this.parent().find('td.td-disciplina').attr('id');
        var semestre = $this.attr('data-semestre');

        $.ajax({
            cache: false,
            type: 'GET',
            url: '/Boletim/Faltas',
            dataType: 'html',
            data: {
                CodDisciplina: id,
                Semestre: semestre
            },
            success: function (result) {
                $.magnificPopup.open({
                    items: {
                        src: result,
                        type: 'inline'
                    }
                });
            },
            beforeSend: function () {
                $('.i-boletim-wrapper').addClass('is-loading');
            },
            complete: function () {
                $('.i-boletim-wrapper').removeClass('is-loading');
            }
        });
    });
});