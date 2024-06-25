// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
setTimeout(function () {
    hideAlert();
}, 5000);

function hideAlert() {
    var alertBox = document.getElementById("myAlert");
    alertBox.style.opacity = "0"; // Set opacity to 0 (transparent)
    setTimeout(function () {
        alertBox.style.display = "none"; // Hide the div after fading out
    }, 600); // 600ms to fade out
}