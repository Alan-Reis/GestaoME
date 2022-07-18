window.onload =
    function loadEquipamento() {
        fetch('/Equipamento/Equipamento', {
            method: 'GET',
            headers: { 'Content-Type': 'application/json;charset=UTF-8' },
        })
            .then(T => T.json())
            .then(tipo => {
                for (let objJSON of tipo) {
                    //popular select
                    const
                        tiposSelect = document.getElementById("equip");

                    const tiposList = {
                        tipo: objJSON.equipamento
                    };

                    for (tipo in tiposList) {
                        option = new Option(tiposList[tipo], tiposList[tipo]);
                        tiposSelect.options[tiposSelect.options.length] = option;
                    }
                    //fim select
                }
            })
    }

function loadFabricante() {
    clearFabricante();
    clearModel();
    var select = document.getElementById("equip");
    var opcaoTexto = select.options[select.selectedIndex].text;
    var fabri = 'fabri=' + opcaoTexto

    fetch('/Equipamento/Fabricante', {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        body: fabri
    })
        .then(T => T.json())
        .then(fabricante => {

            for (let objJSON of fabricante) {
                //popular select
                const fabricantesSelect = document.getElementById("fabri");

                const fabricantesList = {
                    tipo: objJSON.nomeFabricante
                };

                for (fabri in fabricantesList) {
                    option = new Option(fabricantesList[fabri], fabricantesList[fabri]);
                    fabricantesSelect.options[fabricantesSelect.options.length] = option;
                }
                //fim select
            }
        })
}

function loadModelo() {
    clearModel();
    var select = document.getElementById("fabri");
    var opcaoTexto = select.options[select.selectedIndex].text;
    var model = 'model=' + opcaoTexto

    fetch('/Equipamento/Modelo', {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        body: model
    })
        .then(T => T.json())
        .then(modelo => {

            for (let objJSON of modelo) {
                //popular select
                const modelosSelect = document.getElementById("model");

                const modelosList = {
                    tipo: objJSON.nomeModelo
                };

                for (model in modelosList) {
                    option = new Option(modelosList[model], modelosList[model]);
                    modelosSelect.options[modelosSelect.options.length] = option;
                }
                //fim select
            }
        })
}

function clearFabricante() {
    var fabri = document.getElementById("fabri");

    while (fabri.length) {
        fabri.remove(0);
    }

}

function clearModel() {
    var model = document.getElementById("model");

    while (model.length) {
        model.remove(0);
    }
}



//Selecionar o tipo de gás quando o equipamento for aquecedor à gás ou trocador de calor
//usando na página create
document.getElementById('gas').style.display = 'none';

addEventListener("change", function tipo() {
    var tiposSelect = document.getElementById('equip').value;

    if (tiposSelect == 'Aquecedor à gás' || tiposSelect == 'Trocador de calor') {
        document.getElementById('gas').style.display = '';
    }
    else {
        document.getElementById('gas').style.display = 'none';

    }

});

