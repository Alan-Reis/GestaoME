//Função que enviar msg para o WhatsApp
var tabela = document.getElementById("minhaTabela");
var linhas = tabela.getElementsByTagName("tr");

for (var i = 0; i < linhas.length; i++) {
    var linha = linhas[i];
    linha.addEventListener("click", function () {
        //Adicionar ao atual
        selLinha(this, true); //Selecione quantos quiser
    });
}

function selLinha(linha, multiplos) {
    if (!multiplos) {
        var linhas = linha.parentElement.getElementsByTagName("tr");
        for (var i = 0; i < linhas.length; i++) {
            var linha_ = linhas[i];
            linha_.classList.remove("selecionado");
        }
    }
    linha.classList.toggle("selecionado");
}

//Exemplo de como capturar os dados
var btnEnviarWhatsApp = document.getElementById("EnviarWhatsApp");

var phone = document.getElementById("phone").value;

btnEnviarWhatsApp.addEventListener("click", function () {
    var selecionados = tabela.getElementsByClassName("selecionado");
    //Verificar se eestá selecionado
    if (selecionados.length < 1) {
        alert("Selecione pelo menos uma linha");
        return false;
    }

    var dadosWhatsApp = "";

    for (var i = 0; i < selecionados.length; i++) {
        var selecionado = selecionados[i];
        selecionado = selecionado.getElementsByTagName("td");

        //Edição realizada por mim
        var enter = '%0A'

        //Contato
        var nome = '*Contato:* ' + selecionado[0].innerHTML.trim();
        var celular = selecionado[1].innerHTML.trim();
        var telefone = selecionado[2].innerHTML.trim();

        var contato = nome + enter + celular + enter + telefone + enter;

        //Endereço
        var logradouro = '*Endereço:* ' + selecionado[3].innerHTML.trim();
        var complemento = selecionado[4].innerHTML.trim();
        var bairro = selecionado[5].innerHTML.trim();
        var cidade = selecionado[6].innerHTML.trim();

        var endereco = logradouro + ' ' + bairro + enter + complemento + ' - ' + cidade + enter;

        //Equipamento
        var tipo = '*Equipamento:* ' + selecionado[7].innerHTML.trim();
        var fabricante = selecionado[8].innerHTML.trim();
        var modelo = selecionado[9].innerHTML.trim();
        var defeito = '*Defeito:* ' + selecionado[10].innerHTML.trim();

        var equipamento = tipo + ' ' + fabricante + ' ' + modelo + enter + defeito + enter;

        //Observação
        var observacao = '*Observação:* ' + selecionado[11].innerHTML.trim();

        //Data
        var data = '*Data:* ' + selecionado[12].innerHTML.trim();
        var periodo = selecionado[13].innerHTML.trim();
        var colaborador = selecionado[14].innerHTML.trim();

        var agenda = data + ' - ' + periodo + enter + colaborador;

        mensagem = contato + endereco + equipamento + observacao + enter + agenda + enter + enter + enter;
        //Fim edição

        dadosWhatsApp += mensagem;

    }

    var phone = document.getElementById("phone").value;

    window.open('https://api.whatsapp.com/send?phone=55' + phone + '&text=' + dadosWhatsApp)

});