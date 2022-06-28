//Cliente
document.getElementById("salvar").disabled = true;
document.getElementById("nome").disabled = true;
document.getElementById("cpf").disabled = true;

function desabilitar() {
    document.getElementById("salvar").disabled = true;
    document.getElementById("nome").disabled = true;
    document.getElementById("cpf").disabled = true;
}

function habilitar() {
    document.getElementById("salvar").disabled = false;
    document.getElementById("nome").disabled = false;
    document.getElementById("cpf").disabled = false;
}

