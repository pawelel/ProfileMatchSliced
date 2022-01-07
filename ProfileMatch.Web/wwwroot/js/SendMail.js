
function SendMail() {
    var body = document.getElementById("Message").value;
    var SubjectLine = document.getElementById("Subject").value;
    window.location.href = "mailto:admin@profilematch.pl?subject=" + SubjectLine + "&body=" + body;
}