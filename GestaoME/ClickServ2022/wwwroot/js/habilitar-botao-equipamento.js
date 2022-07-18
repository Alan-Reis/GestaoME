document.getElementById("salvar").disabled = true;
document.getElementById("equip").disabled = true;
document.getElementById("fabri").disabled = true;
document.getElementById("model").disabled = true;
document.getElementById("serie").disabled = true;
document.getElementById("gas").disabled = true;

function desabilitar() {
    document.getElementById("salvar").disabled = true;
    document.getElementById("equip").disabled = true;
    document.getElementById("fabri").disabled = true;
    document.getElementById("model").disabled = true;
    document.getElementById("serie").disabled = true;
    document.getElementById("gas").disabled = true;
}

function habilitar() {
    document.getElementById("salvar").disabled = false;
    document.getElementById("equip").disabled = false;
    document.getElementById("fabri").disabled = false;
    document.getElementById("model").disabled = false;
    document.getElementById("serie").disabled = false;
    document.getElementById("gas").disabled = false;

}