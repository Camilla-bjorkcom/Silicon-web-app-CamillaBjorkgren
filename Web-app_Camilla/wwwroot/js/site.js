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
    let switchMode = document.querySelector('#switch-mode');

    switchMode.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light";

        fetch(`/sitesettings/changetheme?mode=${theme}`)
            .then(res => {
                if (res.ok) {
                    window.location.reload();
                    console.log("hello wrong");
                }
                else {
                    console.log('something');
                }
            });
    });
});

