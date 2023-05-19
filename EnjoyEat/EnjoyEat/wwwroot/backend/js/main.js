(function ($) {
    "use strict";

    // Spinner
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner();

    // Sidebar Toggler
    $('.sidebar-toggler').click(function () {
        $('.sidebar, .content').toggleClass("open");
        $('sidebar,.fa fa-bars').toggleClass("fa fa-arrow-right");
        return false;
    });

})(jQuery);

function changeArrow() {
    var arrow = document.getElementById("i-con-changeArrow");
    var icon = arrow.getElementsByTagName("i")[0];

    if (icon.classList.contains("fa-arrow-left")) {
        icon.classList.remove("fa-arrow-left");
        icon.classList.add("fa-arrow-right");
    } else {
        icon.classList.remove("fa-arrow-right");
        icon.classList.add("fa-arrow-left");
    }
}