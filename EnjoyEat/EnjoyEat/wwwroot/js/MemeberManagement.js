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


var modalId = document.getElementById('modalId');

modalId.addEventListener('show.bs.modal', function (event) {
    // Button that triggered the modal
    let button = event.relatedTarget;
    // Extract info from data-bs-* attributes
    let recipient = button.getAttribute('data-bs-whatever');

    // Use above variables to manipulate the DOM
});