function show() {
    var p = document.getElementById('pwd');
    p.setAttribute('type', 'text');
}

function hide() {
    var p = document.getElementById('pwd');
    p.setAttribute('type', 'password');
}

var pwShown = 0;

document.getElementById("eye").addEventListener("click", function () {
    if (pwShown == 0) {
        pwShown = 1;
        show();
        document.getElementById("eye").classList.remove("fa-eye-slash");
        document.getElementById("eye").classList.add("fa-eye");
    } else {
        pwShown = 0;
        hide();
        document.getElementById("eye").classList.add("fa-eye-slash");
        document.getElementById("eye").classList.remove("fa-eye");
    }
}, false);

