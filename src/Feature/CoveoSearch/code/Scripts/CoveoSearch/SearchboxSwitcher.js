class CoveoSearchboxSwitcher {
  constructor() {
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
  };

  setCookie(cookieName, cookieValue, expirationDateInDays) {
    var expirationDate = new Date();
    expirationDate.setTime(expirationDate.getTime() + (expirationDateInDays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + expirationDate.toUTCString();
    document.cookie = cookieName + "=" + cookieValue + ";" + expires + ";path=/";
  };

  getCookie(cookieName) {
    var name = cookieName + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var decodedCookieValueList = decodedCookie.split(';');
    for (var i = 0; i < decodedCookieValueList.length; i++) {
      var cookieValue = decodedCookieValueList[i];
      while (cookieValue.charAt(0) === ' ') {
        cookieValue = cookieValue.substring(1);
      };
      if (cookieValue.indexOf(name) === 0) {
        return cookieValue.substring(name.length, cookieValue.length);
      };
    };
    return this.constants.empty;
  };

  activateCoveo() {
    this.searchboxSwitcher.classList.remove(this.constants.lucene);
    this.searchboxSwitcher.classList.add(this.constants.coveo);
    this.searchboxSwitcher.title = this.constants.coveoActive;
    this.coveoSearchbox.classList.remove(this.constants.disabled);
    this.habitatSearchbox.classList.add(this.constants.disabled);
    this.setCookie(this.constants.cookieName, this.constants.coveo, 30);
  };

  activateLucene() {
    this.searchboxSwitcher.classList.remove(this.constants.coveo);
    this.searchboxSwitcher.classList.add(this.constants.lucene);
    this.searchboxSwitcher.title = this.constants.luceneActive;
    this.coveoSearchbox.classList.add(this.constants.disabled);
    this.habitatSearchbox.classList.remove(this.constants.disabled);
    this.setCookie(this.constants.cookieName, this.constants.lucene, 30);
  };

  enableSearchboxSwitcher() {
    this.searchboxSwitcher.classList.remove(this.constants.disabled);
  };

  toggleSearchboxOnPageLoad() {
    this.coveoSearchboxCookie = this.getCookie(this.constants.cookieName);

    if (this.coveoSearchboxCookie === this.constants.coveo || this.coveoSearchboxCookie === this.constants.empty) {
      this.activateCoveo();
    } else {
      this.activateLucene();
    };
  };

  toggleSearchboxOnClick() {
    this.coveoSearchboxCookie = this.getCookie(this.constants.cookieName);

    if (this.coveoSearchboxCookie === this.constants.coveo || this.coveoSearchboxCookie === this.constants.empty) {
      this.activateLucene();
    } else {
      this.activateCoveo();
    };
  };
}

document.addEventListener('DOMContentLoaded', function () {
  coveoSearchboxSwitcher = new CoveoSearchboxSwitcher();
  coveoSearchboxSwitcher.enableSearchboxSwitcher();
  coveoSearchboxSwitcher.toggleSearchboxOnPageLoad();
}, false);