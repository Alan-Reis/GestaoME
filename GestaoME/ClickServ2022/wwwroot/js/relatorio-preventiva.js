
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
var btnExpPDF = document.getElementById("expPDF");

btnExpPDF.addEventListener("click", function () {
    var selecionados = tabela.getElementsByClassName("selecionado");
    //Verificar se eestá selecionado
    if (selecionados.length < 1) {
        alert("Selecione pelo menos uma linha");
        return false;
    }


    for (var i = 0; i < selecionados.length; i++) {
        var selecionado = selecionados[i];
        selecionado = selecionado.getElementsByTagName("td");

        //Edição realizada por mim
        var enter = "\n";

        var mes = selecionado[0].innerHTML.trim();
        var ano = selecionado[1].innerHTML.trim();
        var nome = selecionado[2].innerHTML.trim();
        var cnpj = selecionado[3].innerHTML.trim();
        var logradouro = selecionado[4].innerHTML.trim();
        var bairro = selecionado[5].innerHTML.trim();
        var cidade = selecionado[6].innerHTML.trim();

        var linha1 = 'Boletim de Atendimento' + enter
        var linha2 = mes + '/' + ano + enter
        var linha3 = 'Data    de ' + mes + ' de ' + ano + enter
        var linha4 = 'Cliente: ' + nome + ' ' + 'CNPJ: ' + cnpj
        var linha5 = 'Endereço ' + logradouro
        var linha6 = 'Bairro: ' + bairro + ' ' + 'Cidade: ' + cidade

        var nomeArquivo = nome + '.pdf'

        var doc = new jsPDF();
        doc.setFontSize(12);
        doc.addImage(logo, 'JPEG', 80, 5, 60, 30);
        doc.rect(5, 38, 200, 20);
        doc.text(linha1, 100, 42, { align: 'center' });//Largura X Altura
        doc.text(linha2, 100, 46, { align: 'center' });
        doc.text(linha3, 100, 51, { align: 'center' });
        doc.text(linha4, 100, 56, { align: 'center' });
        doc.save(nomeArquivo);
        doc.addPage();
    }

    
   
});



