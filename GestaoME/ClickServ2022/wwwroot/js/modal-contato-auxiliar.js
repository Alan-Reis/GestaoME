$(".edit").click(function () {
    var id = $(this).attr("data-id");

    $("#modal").load("/ContatoAuxiliar/Edit/" + id, function () {
        $("#modal").modal();
    })
});

$(".delete").click(function () {
    var id = $(this).attr("data-id");

    $("#modal").load("/ContatoAuxiliar/Delete/" + id, function () {
        $("#modal").modal();
    })
});