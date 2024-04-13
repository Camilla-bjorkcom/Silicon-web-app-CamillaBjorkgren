'use strict';

var formErrorMessage = function formErrorMessage(element, validationResult) {
    var spanElement = document.querySelector('[data-valmsg-for="' + element.name + '"]');

    if (validationResult) {
        element.classList.remove('input-validation-error');
        spanElement.classList.remove('field-validation-error');
        spanElement.classList.add('field-validation-valid');
        spanElement.innerHTML = '';
    } else {
        element.classList.add('input-validation-error');
        spanElement.classList.add('field-validation-error');
        spanElement.classList.remove('field-validation-valid');
        spanElement.innerHTML = element.dataset.valRequired;
    }
};

var textValidator = function textValidator(element) {
    var minLength = arguments.length <= 1 || arguments[1] === undefined ? 2 : arguments[1];

    if (element.value.length >= minLength) formErrorMessage(element, true);else {
        formErrorMessage(element, false);
    }
};

var emailValidator = function emailValidator(element) {
    var regEx = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    formErrorMessage(element, regEx.test(element.value));
};

var passwordValidator = function passwordValidator(element) {
    if (element.dataset.valEqualtoOther !== undefined) {
        var password = document.getElementsByName(element.dataset.valEqualtoOther.replace('*', 'form'))[0].value;

        if (element.value === password) {
            formErrorMessage(element, true);
        } else {
            formErrorMessage(element, false);
        }
    } else {
        var regEx = /^(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[@$!%*?&])[a-zA-Z0-9@$!%*?&]{8,}$/;
        formErrorMessage(element, regEx.test(element.value));
    }
};

var checkboxValidator = function checkboxValidator(element) {
    if (element.checked) formErrorMessage(element, true);else {
        formErrorMessage(element, false);
    }
};

var forms = document.querySelectorAll('form');
forms.forEach(function (form) {
    var inputs = form.querySelectorAll('input');

    inputs.forEach(function (input) {
        if (input.dataset.val === 'true') {
            if (input.type === 'checkbox') {
                input.addEventListener('change', function (e) {
                    checkboxValidator(e.target);
                });
            } else {
                input.addEventListener('keyup', function (e) {
                    switch (e.target.type) {
                        case 'text':
                            textValidator(e.target);
                            break;
                        case 'email':
                            emailValidator(e.target);
                            break;
                        case 'password':
                            passwordValidator(e.target);
                            break;
                    }
                });
            }
        }
    });
});

