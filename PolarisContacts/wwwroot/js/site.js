var idToInativa;
var typeToInativa;

function confirmInativaItem(id, type) {
    idToInativa = id;
    typeToInativa = type;
    document.getElementById('itemType').innerText = type.toLowerCase();
    document.getElementById('confirmInativaMessage').style.display = 'block';
    document.getElementById('inativaSuccessMessage').style.display = 'none';
    document.getElementById('confirmInativaButton').disabled = false;
    document.getElementById('cancelButton').disabled = false;
    var myModal = new bootstrap.Modal(document.getElementById('confirmInativaModal'));
    myModal.show();
}

document.getElementById('confirmInativaButton').addEventListener('click', function () {
    inativaItem(idToInativa, typeToInativa);
});

function inativaItem(id, type) {
    $("#loaderHome").fadeIn()

    $.ajax({
        type: "POST",
        url: `/${type}/Inativa${type}`,
        data: { id: id },
        success: function () {
            $(`#${type.toLowerCase()}-${id}`).remove();
            document.getElementById('confirmInativaMessage').style.display = 'none';
            document.getElementById('inativaSuccessMessage').style.display = 'block';
            document.getElementById('confirmInativaButton').disabled = true;
            document.getElementById('cancelButton').disabled = true;
            setTimeout(() => {
                var myModal = bootstrap.Modal.getInstance(document.getElementById('confirmInativaModal'));
                myModal.hide();
            }, 2000); // Esconde a modal após 2 segundos
        },
        error: function () {
            alert('Erro ao deletar o item. Tente novamente.');
        }
    });

    $("#loaderHome").fadeOut()
}