function openDados(evt, dados) {

    // Declare todas variaveis
    var i, tabcontent, tablinks;

    // Obter todos os elementos com class="tabcontent" e escondê-los
    tabcontent = document.getElementsByClassName("tab-content");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Pega todos os elementos com class="tablinks" e remove a classe "active"
    tablinks = document.getElementsByClassName("nav-link");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Mostre a guia atual e adicione uma classe "ativa" ao link que abriu a guia
    document.getElementById(dados).style.display = "block";
    evt.currentTarget.className += " active";
}

// Pega o elemento com id="defaultOpen" e clica nele
document.getElementById("defaultOpen").click();