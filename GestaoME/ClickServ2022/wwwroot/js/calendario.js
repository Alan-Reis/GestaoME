
//Função que coleta os dados da tabela
function loadData() {
    let eventsArr = [];

    //Pega os valores da tabela id="todoTable"
    let todoTable = document.getElementById("todoTable");
    let trElem = todoTable.getElementsByTagName("tr");

    for (let tr of trElem) {
        let tdElems = tr.getElementsByTagName("td");
        let eventObj = {
            title: tdElems[0].innerText,
            start: tdElems[1].innerText,
        }
        eventsArr.push(eventObj);
    }

    return eventsArr;
}

let eventsArr = loadData();
let calendar = initCalendar();


function initCalendar() {
    document.addEventListener('DOMContentLoaded', function () {
        var calendarEl = document.getElementById('calendar');
        var calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'dayGridMonth',
            locale: 'pt-br',


            events: eventsArr,

        });
        calendar.render();

    });
}