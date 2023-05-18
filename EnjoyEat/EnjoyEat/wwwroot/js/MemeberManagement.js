let progress = document.getElementById("progress");
let progressText = document.querySelector(".dataContent")
let progressValue = 16000;
let remainingValue = 25000 - progressValue;

progress.value = progressValue;

if (remainingValue > 0) {
    progress.max = 25000;
    progress.setAttribute(
        "data-content",
        `還需消費 ${remainingValue} 元才能升級為黃金會員`
    );

} else {
    progress.max = progressValue;
    progress.setAttribute("data-content", "已升級為黃金會員");
}
progressText.textContent = progress.getAttribute("data-content")