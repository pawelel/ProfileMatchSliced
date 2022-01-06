
function changeTheme() {
    var element = document.body;
    if (element.classList.contains("dark-mode")) {
        element.classList.remove("dark-mode");
    } else {
        element.classList.add("dark-mode");
    }
}