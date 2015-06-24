(function ($, window, undefined) {

    var selectors = {
        businesssafecookiecontinue: "#businesssafecookiecontinue",
        businesssafecookiecancel: "#businesssafecookiecancel",
        buttons: "#businesssafecookiecontinue,#businesssafecookiecancel",
        closepopup: "businesssafe",
        businesssafecookieholder: "#businesssafecookieholder"
    };

    displayCookieMessage();

    function displayCookieMessage() {
        var result = readCookie(selectors.closepopup);
        if (result === null) {
            $(selectors.businesssafecookieholder).show();
        }
    }

    $(selectors.buttons).click(function (e) {
        e.preventDefault();
        eraseAndCreateCookie();
    });

    function eraseAndCreateCookie() {
        eraseCookie(selectors.closepopup);
        createCookie(selectors.closepopup, "close", 31536000000);
        $(selectors.businesssafecookieholder).slideUp();
    }

    function createCookie(name, value, millisec) {
        var expires = "";
        if (millisec) {
            var date = new Date();
            date.setTime(date.getTime() + (millisec));
            expires = "; expires=" + date.toGMTString();
        }
        document.cookie = name + "=" + value + expires + "; path=/";
    }

    function eraseCookie(name) {
        createCookie(name, "", -1);
    }

    function readCookie(name) {
        var nameeq = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameeq) === 0) return c.substring(nameeq.length, c.length);
        }
        return null;
    }

})(jQuery, this);