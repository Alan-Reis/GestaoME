var tipo = document.getElementById('equip').value;
var btn = document.getElementById('editar');
document.getElementById('gas').style.display = 'none';

btn.addEventListener("click", function () {
    if (tipo == 'Aquecedor à gás' || tipo == 'Trocador de calor') {
        document.getElementById('gas').style.display = '';
    }
});

addEventListener("change", function tipo() {
    var tiposSelect = document.getElementById('equip').value;
    if (tiposSelect == 'Aquecedor à gás' || tiposSelect == 'Trocador de calor') {
        document.getElementById('gas').style.display = '';
    }
    else {
        document.getElementById('gas').style.display = 'none';

    }

});
