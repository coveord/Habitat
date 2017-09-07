var CoveoSearchboxSwitcher = (function() {
    function CoveoSearchboxSwitcher() {
        this.coveoSearchbox = document.getElementsByClassName("navbar-coveo")[0];
        this.habitatSearchbox = document.getElementsByClassName("navbar-activity-search")[0];
        this.searchboxSwitcher = document.getElementsByClassName("searchbox-switcher")[0];
        this.constants = {
            "lucene": "lucene",
            "coveo": "coveo",
            "empty": "",
            "disabled": "disabled",
            "cookieName": "coveoSearchbox",
            "luceneActive": "Lucene is active",
            "coveoActive": "Coveo is active"
        };
    }
    CoveoSearchboxSwitcher.prototype.setCookie = function(cookieName, cookieValue, expirationDateInDays) {
        var expirationDate = new Date();
        expirationDate.setTime(expirationDate.getTime() + (expirationDateInDays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + expirationDate.toUTCString();
        document.cookie = cookieName + "=" + cookieValue + ";" + expires + ";path=/";
    };
    CoveoSearchboxSwitcher.prototype.getCookie = function(cookieName) {
        var name = cookieName + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var decodedCookieValueList = decodedCookie.split(';');
        for (var i = 0; i < decodedCookieValueList.length; i++) {
            var cookieValue = decodedCookieValueList[i];
            while (cookieValue.charAt(0) === ' ') {
                cookieValue = cookieValue.substring(1);
            }
            if (cookieValue.indexOf(name) === 0) {
                return cookieValue.substring(name.length, cookieValue.length);
            }
        }
        return this.constants.empty;
    };
    CoveoSearchboxSwitcher.prototype.activateCoveo = function() {
        this.searchboxSwitcher.classList.remove(this.constants.lucene);
        this.searchboxSwitcher.classList.add(this.constants.coveo);
        this.searchboxSwitcher.title = this.constants.coveoActive;
        this.coveoSearchbox.classList.remove(this.constants.disabled);
        this.habitatSearchbox.classList.add(this.constants.disabled);
        this.setCookie(this.constants.cookieName, this.constants.coveo, 30);
    };
    CoveoSearchboxSwitcher.prototype.activateLucene = function() {
        this.searchboxSwitcher.classList.remove(this.constants.coveo);
        this.searchboxSwitcher.classList.add(this.constants.lucene);
        this.searchboxSwitcher.title = this.constants.luceneActive;
        this.coveoSearchbox.classList.add(this.constants.disabled);
        this.habitatSearchbox.classList.remove(this.constants.disabled);
        this.setCookie(this.constants.cookieName, this.constants.lucene, 30);
    };
    CoveoSearchboxSwitcher.prototype.enableSearchboxSwitcher = function() {
        this.searchboxSwitcher.classList.remove(this.constants.disabled);
    };
    CoveoSearchboxSwitcher.prototype.toggleSearchboxOnPageLoad = function() {
        this.coveoSearchboxCookie = this.getCookie(this.constants.cookieName);
        if (this.coveoSearchboxCookie === this.constants.coveo || this.coveoSearchboxCookie === this.constants.empty) {
            this.activateCoveo();
        } else {
            this.activateLucene();
        }
    };
    CoveoSearchboxSwitcher.prototype.toggleSearchboxOnClick = function() {
        this.coveoSearchboxCookie = this.getCookie(this.constants.cookieName);
        if (this.coveoSearchboxCookie === this.constants.coveo || this.coveoSearchboxCookie === this.constants.empty) {
            this.activateLucene();
        } else {
            this.activateCoveo();
        }
    };
    return CoveoSearchboxSwitcher;
})();
document.addEventListener('DOMContentLoaded', function() {
    coveoSearchboxSwitcher = new CoveoSearchboxSwitcher();
    coveoSearchboxSwitcher.enableSearchboxSwitcher();
    coveoSearchboxSwitcher.toggleSearchboxOnPageLoad();
}, false);