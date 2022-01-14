﻿
var theme = getCookie("theme");

async function changeTheme() {
    if (theme=="light") {
        document.body.classList.remove("light-mode");
        document.body.classList.add("dark-mode");
        setCookie("theme", "dark", 365);
    } else {
        document.body.classList.remove("dark-mode");
        document.body.classList.add("light-mode");
        setCookie("theme", "light", 365);
    }
}


function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
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
