$(".edit").click(function () {
    var id = $(this).attr("data-id");

    $("#modal").load("/Contato/Edit/" + id, function () {
        $("#modal").modal();
    })
});

$(".delete").click(function () {
    var id = $(this).attr("data-id");

    $("#modal").load("/Contato/Delete/" + id, function () {
        $("#modal").modal();
    })
});