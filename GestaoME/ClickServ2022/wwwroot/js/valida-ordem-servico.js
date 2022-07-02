function enviar() {
    //Se o campo Ordem de Serviço estiver vazio a função não roda
    if (document.getElementById("os").value != "") {

        //Envia um POST para o Action OrdemServico/ValidarOS
        $.ajax({
            type: 'POST',
            url: '/OrdemServico/ValidarOS',
            //Pega o valor do campo Ordem de Serviço
            data: { "os": $("#os").val() },

            success: function (data) {
                const obj = { data };
                const myJSON = JSON.stringify(obj.data);

                //Caso já tenha a ordem de serviço que foi digitada cadastrada no sistema entra no if
                if (obj.data == 1) {
                    alert("Ordem de serviço já cadastrada!");
                    //Limpa o campo ordem de serviço
                    document.getElementById("os").value = '';
                }
            },
        });
    }

};