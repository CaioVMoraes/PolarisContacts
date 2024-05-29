var idToDelete;
var typeToDelete;

function confirmDeleteItem(id, type) {
    idToDelete = id;
    typeToDelete = type;
    document.getElementById('itemType').innerText = type.toLowerCase();
    document.getElementById('confirmDeleteMessage').style.display = 'block';
    document.getElementById('deleteSuccessMessage').style.display = 'none';
    document.getElementById('confirmDeleteButton').disabled = false;
    document.getElementById('cancelButton').disabled = false;
    var myModal = new bootstrap.Modal(document.getElementById('confirmDeleteModal'));
    myModal.show();
}

document.getElementById('confirmDeleteButton').addEventListener('click', function () {
    deleteItem(idToDelete, typeToDelete);
});

function deleteItem(id, type) {
    $("#loaderHome").fadeIn()

    $.ajax({
        type: "POST",
        url: `/${type}/Delete${type}`,
        data: { id: id },
        success: function () {
            $(`#${type.toLowerCase()}-${id}`).remove();
            document.getElementById('confirmDeleteMessage').style.display = 'none';
            document.getElementById('deleteSuccessMessage').style.display = 'block';
            document.getElementById('confirmDeleteButton').disabled = true;
            document.getElementById('cancelButton').disabled = true;
            setTimeout(() => {
                var myModal = bootstrap.Modal.getInstance(document.getElementById('confirmDeleteModal'));
                myModal.hide();
            }, 2000); // Esconde a modal após 2 segundos
        },
        error: function () {
            alert('Erro ao deletar o item. Tente novamente.');
        }
    });

    $("#loaderHome").fadeOut()
}