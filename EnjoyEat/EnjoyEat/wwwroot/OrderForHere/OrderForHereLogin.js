$(document).ready(function () {
    const registerBtn = $("#register-btn");
    const dialogOverlay = $("#dialog-overlay");
    const dialog = $("#dialog");
    const closeBtn = $("#close-btn");
    const successMessage = $("#success-message");

    registerBtn.on("click", function (event) {
        event.preventDefault();
        dialogOverlay.css("display", "block");
        dialog.css("display", "block");
    });

    closeBtn.on("click", function (event) {
        event.preventDefault();
        dialogOverlay.css("display", "none");
        dialog.css("display", "none");
    });

    $("form").on("submit", function (event) {
        event.preventDefault();
        // 執行註冊成功後的相關處理
        successMessage.addClass("show");
        setTimeout(function () {
            successMessage.removeClass("show");
            dialogOverlay.css("display", "none");
            dialog.css("display", "none");
        }, 1000); // 停留 1 秒後跳轉
    });
});