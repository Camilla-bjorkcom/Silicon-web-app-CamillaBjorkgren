//const toggleMenu = () => {
//    document.getElementById('menu').classList.toggle('hide');
//    document.getElementsByClassName('account-buttons').classList.toggle('hide');
//}
//
//const checkScreenSize = () => {
//    if (window.innerWidth >= 1200) {
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
//
//window.addEventListener('resize', checkScreenSize);
//checkScreenSize();

'use strict';

function _toConsumableArray(arr) { if (Array.isArray(arr)) { for (var i = 0, arr2 = Array(arr.length); i < arr.length; i++) arr2[i] = arr[i]; return arr2; } else { return Array.from(arr); } }

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

//document.addEventListener('click'), function () {
//    handleProfileImageUpload()
//}

function handleProfileImageUpload() {

    var uploadFile = document.getElementById('uploadFile');
    if (uploadFile != undefined) {
        uploadFile.addEventListener('change', function () {
            if (this.files.length > 0) this.form.submit();
        });
    }
}

document.addEventListener('DOMContentLoaded', function () {
    select();
});
function select() {
    var select = document.querySelector('.select');
    var selected = select.querySelector('.selected');
    var selectOptions = select.querySelector('.select-options');

    selected.addEventListener('click', function () {
        selectOptions.style.display = selectOptions.style.display === 'block' ? 'none' : 'block';
    });

    var options = selectOptions.querySelectorAll('.option');
    options.forEach(function (option) {
        option.addEventListener('click', function () {
            selected.innerHTML = this.textContent;
            selectOptions.style.display = 'none';
            var category = this.getAttribute('data-value');
            selected.setAttribute('data-value', category);
            updateCourseByFilter();
        });
    });
}

function updateCourseByFilter() {
    var category = document.querySelector('.select .selected').getAttribute('data-value') || 'all';
    var searchQuery = document.querySelector('#searchQuery').value;

    var url = '/Courses?category=' + encodeURIComponent(category) + '&searchQuery=' + encodeURIComponent(searchQuery);

    fetch(url).then(function (res) {
        return res.text();
    }).then(function (data) {
        var parser = new DOMParser();
        var dom = parser.parseFromString(data, 'text/html');

        var courseList = document.querySelector('.course-list');
        var newCourseListContent = dom.querySelector('.course-list').innerHTML;

        if (courseList && newCourseListContent) {
            courseList.innerHTML = newCourseListContent;
        } else {
            console.error("Failed to update course list: element or content not found.");
        }

        var pagination = dom.querySelector('.pagination') ? dom.querySelector('.pagination').innerHTML : '';
        document.querySelector('.pagination').innerHTML = pagination;
    });
}

var tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
var tooltipList = [].concat(_toConsumableArray(tooltipTriggerList)).map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl);
});

