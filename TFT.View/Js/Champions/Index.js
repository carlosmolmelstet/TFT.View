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


function SearchChampion() {
    setTimeout(function () {
        if ($(".btn-cost-up").hasClass('active')) {
            var cost = true;
        } else {
            var cost = false;
        }
        OrderChampion(cost)
    }, 600);
}


function OrderChampion(cost) {
    if (cost) {
        $(".btn-cost-up").addClass('active');
        $(".btn-cost-down").removeClass('active');

    } else {
        $(".btn-cost-down").addClass('active');
        $(".btn-cost-up").removeClass('active');

    }

    var search = $("#searchInput").val();
    var className = $("#selectClass").val();
    var originName = $("#selectOrigin").val();

    $.ajax({
        url: '/Champions/SearchChampion',
        type: 'POST',
        data: { search: search, biggest: cost, class: className, origin: originName },
        success: function (response) {
            $('.wrapper-listChampions').html(response);
        },
        error: function (response) {
            alert("vbbbb");
        }
    });
}
