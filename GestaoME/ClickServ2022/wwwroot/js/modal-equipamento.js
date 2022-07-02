$(".edit").click(function () {
    var id = $(this).attr("data-id");

    $("#modal").load("/OrdemServico/Edit/" + id, function () {
        $("#modal").modal();
    })
});

$(".delete").click(function () {
    var id = $(this).attr("data-id");

    $("#modal").load("/OrdemServico/Delete/" + id, function () {
        $("#modal").modal();
    })
});