// jshint esversion:8
// Language: javascript
// Path: ProfileMatch.Web\wwwroot\js\theme.js
var darkswitch = document.getElementById("darkSwitch");
var logo = document.querySelector(".logo");
getTheme();

darkswitch.addEventListener("click", changeTheme);
function setCookie(cname, cvalue) {
    document.cookie = `${cname}=${cvalue}; expires=${new Date(new Date().getTime() + 1000 * 60 * 60 * 24 * 365).toUTCString()}; path=/`;
}


function getTheme() {
    var theme = getCookie("theme");
    if (theme == "dark") {
        document.body.classList.add("dark-mode");
        logo.innerHTML = "<img src='../../images/logo-dark.svg' alt='logo' />";
    
        darkswitch.checked = true; 
    } else {
        document.body.classList.remove("dark-mode");
        logo.innerHTML = "<img src='../../images/logo-light.svg' alt='logo' />";

        darkswitch.checked = false;
    }
}
function changeTheme() {
    if (darkswitch.checked) {
        logo.innerHTML = "<img src='../../images/logo-light.svg' alt='logo' />";
        setCookie("theme", "light");
        darkswitch.checked = false;
        document.body.classList.remove("dark-mode");
    } else {
        logo.innerHTML = "<img src='../../images/logo-dark.svg' alt='logo' />";
        setCookie("theme", "dark");
        darkswitch.checked = true;
        document.body.classList.add("dark-mode");
    }
}
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}