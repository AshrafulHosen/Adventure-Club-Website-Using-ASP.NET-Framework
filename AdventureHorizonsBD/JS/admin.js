// ADMIN PANEL — tab switching and upload method toggle
// Tab state is stored in hdnActiveAdminTab (a server HiddenField with ViewState)
// so it survives full-page postbacks.

function switchAdminTab(tabName) {
    document.querySelectorAll('.admin-tab-content').forEach(function(el) {
        el.classList.remove('active');
    });
    document.querySelectorAll('.admin-nav-item').forEach(function(el) {
        el.classList.remove('active');
    });

    var section = document.getElementById(tabName);
    if (section) section.classList.add('active');

    // Highlight matching nav button by its onclick text
    document.querySelectorAll('.admin-nav-item').forEach(function(btn) {
        var oc = btn.getAttribute('onclick') || '';
        if (oc.indexOf("'" + tabName + "'") !== -1) {
            btn.classList.add('active');
        }
    });

    // Persist in the ViewState-backed hidden field
    var hdn = document.getElementById('hdnActiveAdminTab');
    if (hdn) hdn.value = tabName;
}

function switchUploadMethod(method) {
    var fileMethod = document.getElementById('fileUploadMethod');
    var urlMethod  = document.getElementById('urlUploadMethod');
    document.querySelectorAll('.upload-method-btn').forEach(function(btn) {
        btn.classList.remove('active');
    });
    if (event && event.target) event.target.classList.add('active');
    if (method === 'file') {
        if (fileMethod) fileMethod.classList.add('active');
        if (urlMethod)  urlMethod.classList.remove('active');
    } else {
        if (fileMethod) fileMethod.classList.remove('active');
        if (urlMethod)  urlMethod.classList.add('active');
    }
}

// Restore active tab on page load (after postback). Runs after DOM is ready.
(function restoreTab() {
    var hdn = document.getElementById('hdnActiveAdminTab');
    var tab = hdn ? hdn.value : 'overview';
    if (!tab) tab = 'overview';
    switchAdminTab(tab);
})();
