
var theme = getCookie("theme");

function changeTheme() {
    if (theme == "light") {
        document.body.classList.remove("light-mode");
        document.body.classList.add("dark-mode");
        setCookie("theme", "dark", 365);
    } else {
        document.body.classList.remove("dark-mode");
        document.body.classList.add("light-mode");
        setCookie("theme", "light");
    }
}



function setCookie(cname, cvalue) {
    document.cookie = `${cname}=${cvalue}; expires=${new Date(new Date().getTime() + 1000 * 60 * 60 * 24 * 365).toUTCString()}; path=/`;
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
