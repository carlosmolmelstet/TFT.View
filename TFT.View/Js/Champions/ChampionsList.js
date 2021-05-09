$(document).ready(function () {
    for (var i = 0; i <= 5; i++) {

        if (!$.trim($(`.row-${i}`).html()).length) {
            $(`.row-${i}`).remove();
        }

        var autoH2 = $(".wrapper-listChampions").height();

        if (autoH2 == 0) {
            $(".notFoundResult").removeClass('d-none');
        }
    }
});

