//const toggleMenu = () => {
//    document.getElementById('menu').classList.toggle('hide');
//    document.getElementsByClassName('account-buttons').classList.toggle('hide');
//}

'use strict';

function _toConsumableArray(arr) { if (Array.isArray(arr)) { for (var i = 0, arr2 = Array(arr.length); i < arr.length; i++) arr2[i] = arr[i]; return arr2; } else { return Array.from(arr); } }

function toggleMenu() {
    var menu = document.querySelector('.mobile-menu');
    menu.classList.toggle('show');
}

//const checkScreenSize = () => {
//    if (window.innerWidth >= 900) {
//        document.getElementById('menu').classList.remove('hide');
//        document.getElementsByClassName('account-buttons').classList.remove('hide');
//    }
//    else {
//        if (!document.getElementById('menu').classList.contains('hide')) {
//            document.getElementById('menu').classList.add('hide');
//        }
//        if (!document.getElementsByClassName('account-buttons').classList.contains('hide')) {
//            document.getElementsByClassName('account-buttons').classList.add('hide');
//        }
//    }
//};

//window.addEventListener('resize', checkScreenSize);
//checkScreenSize();

document.addEventListener('DOMContentLoaded', function () {
    handleProfileImageUpload();
    var switchMode = document.querySelector('#switch-mode');

    switchMode.addEventListener('change', function () {
        var theme = this.checked ? "dark" : "light";

        fetch('/sitesettings/changetheme?mode=' + theme).then(function (res) {
            if (res.ok) {
                window.location.reload();
            } else {
                console.log('something');
            }
        });
    });
});

function handleProfileImageUpload() {

    var uploadFile = document.getElementById('uploadFile');
    if (uploadFile != undefined) {
        uploadFile.addEventListener('change', function () {
            if (this.files.length > 0) this.form.submit();
        });
    }
}

var tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
var tooltipList = [].concat(_toConsumableArray(tooltipTriggerList)).map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl);
});

