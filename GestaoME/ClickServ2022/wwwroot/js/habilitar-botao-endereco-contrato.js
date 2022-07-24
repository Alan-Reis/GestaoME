
document.getElementById("btnSalvar").disabled = true;
document.getElementById("logradouro").disabled = true;
document.getElementById("complemento").disabled = true;
document.getElementById("bairro").disabled = true;
document.getElementById("cidade").disabled = true;
document.getElementById("uf").disabled = true;
document.getElementById("observacao").disabled = true;

function desabilitarContrato() {
    document.getElementById("btnSalvar").disabled = true;
    document.getElementById("logradouro").disabled = true;
    document.getElementById("complemento").disabled = true;
    document.getElementById("bairro").disabled = true;
    document.getElementById("cidade").disabled = true;
    document.getElementById("uf").disabled = true;
    document.getElementById("observacao").disabled = true;
}

function habilitarContrato() {
    document.getElementById("btnSalvar").disabled = false;
    document.getElementById("logradouro").disabled = false;
    document.getElementById("complemento").disabled = false;
    document.getElementById("bairro").disabled = false;
    document.getElementById("cidade").disabled = false;
    document.getElementById("uf").disabled = false;
    document.getElementById("observacao").disabled = false;
}