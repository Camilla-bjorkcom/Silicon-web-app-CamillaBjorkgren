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

document.addEventListener('DOMContentLoaded', function () {
    handleProfileImageUpload()
    let switchMode = document.querySelector('#switch-mode');

    switchMode.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light";

        fetch(`/sitesettings/changetheme?mode=${theme}`)
            .then(res => {
                if (res.ok) {
                    window.location.reload();
                }
                else {
                    console.log('something');
                }
            });
    });
});

//document.addEventListener('click'), function () {
//    handleProfileImageUpload()
//}


function handleProfileImageUpload() {

    let uploadFile = document.getElementById('uploadFile')
    if (uploadFile != undefined) {
        uploadFile.addEventListener('change', function () {
            if (this.files.length > 0)
                this.form.submit()
        })
    }
}

document.addEventListener('DOMContentLoaded', function () {
    select()
    searchQuery()
})
function select() {      
        let select = document.querySelector('.select')
        let selected = select.querySelector('.selected')
    let selectOptions = select.querySelector('.select-options')
    selected.addEventListener('click', function () {
        selectOptions.classList.remove('d-none')
    })
        selected.addEventListener('click', function () {
            selectOptions.style.display = (selectOptions.style.display === 'block') ? 'none' : 'block'
        })

        let options = selectOptions.querySelectorAll('.option')
        options.forEach(function (option) {
            option.addEventListener('click', function () {
                selected.innerHTML = this.textContent
                selectOptions.style.display = 'none'
                let category = this.getAttribute('data-value')
                selected.setAttribute('data-value', category)
                updateCourseByFilter()
            })
        })  
}

function searchQuery() {
    try {
        document.querySelector('#searchQuery').addEventListener('keyup', function () {
            updateCourseByFilter()
        })
    }
    catch { }
}

function updateCourseByFilter() {
    const category = document.querySelector('.select .selected').getAttribute('data-value') || 'all'
    const searchQuery = document.querySelector('#searchQuery').value

    const url = `/Courses/Courses?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchQuery)}`


    fetch(url)
        .then(res => res.text())
        .then(data => {
            const parser = new DOMParser()
            const dom = parser.parseFromString(data, 'text/html')

            const courseList = document.querySelector('.course-list')
            const newCourseListContent = dom.querySelector('.course-list').innerHTML

            if (courseList && newCourseListContent) {
                courseList.innerHTML = newCourseListContent;
            }
            else {
                console.error("Failed to update course list: element or content not found.")
            }

            const pagination = dom.querySelector('.pagination') ? dom.querySelector('.pagination').innerHTML : ''
            document.querySelector('.pagination').innerHTML = pagination
            })
}

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))