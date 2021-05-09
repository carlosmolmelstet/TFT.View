$(document).ready(function () {
    ChampionsList();
});


function ChampionsList() {
    $.ajax({
        url: '/Champions/ChampionsList',
        type: 'GET',
        success: function (response) {
            $('.wrapper-listChampions').html(response);
        },
        error: function (response) {
            console.log(response)
        }
    });
}

