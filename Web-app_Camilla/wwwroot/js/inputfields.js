document.addEventListener('DOMContentLoaded', function () {
    var inputFields = document.getElementsByTagName('input');

    for (var i = 0; i < inputFields.length; i++) {
        inputFields[i].classList.add('untouched');

        inputFields[i].addEventListener('focus', function () {
            this.classList.remove('untouched');
        });
    }
});
