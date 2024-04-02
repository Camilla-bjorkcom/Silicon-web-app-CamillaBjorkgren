'use strict';

document.addEventListener('DOMContentLoaded', function () {

    handleProfileImageUpload();
});

function handleProfileImageUpload() {
    var uploadFile = document.getElementById("uploadFile");
    if (uploadFile != undefined) {
        uploadFile.addEventListener('change', function () {
            if (this.files.lenght > 0) this.form.submit();
        });
    }
}

