// USER DASHBOARD — tab switching, upload method toggle, star rating
// Tab state is stored in hdnActiveUserTab (a server HiddenField with ViewState)
// so it survives full-page postbacks.

function switchUserTab(tabName) {
    // Hide all tab content sections
    document.querySelectorAll('.user-tab-content').forEach(function(el) {
        el.classList.remove('active');
    });
    // Remove active from all tab buttons
    document.querySelectorAll('.user-tab').forEach(function(el) {
        el.classList.remove('active');
    });

    // Show the chosen section (IDs follow pattern: galleryTab, reviewTab, etc.)
    var section = document.getElementById(tabName + 'Tab');
    if (section) section.classList.add('active');

    // Highlight the matching button by its onclick text
    document.querySelectorAll('.user-tab').forEach(function(btn) {
        var oc = btn.getAttribute('onclick') || '';
        if (oc.indexOf("'" + tabName + "'") !== -1) {
            btn.classList.add('active');
        }
    });

    // Persist in the ViewState-backed hidden field
    var hdn = document.getElementById('hdnActiveUserTab');
    if (hdn) hdn.value = tabName;
}

function switchUserUploadMethod(method) {
    var fileMethod = document.getElementById('userFileUploadMethod');
    var urlMethod  = document.getElementById('userUrlUploadMethod');
    document.querySelectorAll('.upload-method-tabs .upload-method-btn').forEach(function(btn) {
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

function setRating(rating) {
    var ratingInput = document.getElementById('hdnReviewRating');
    if (ratingInput) ratingInput.value = rating;
    document.querySelectorAll('.star').forEach(function(s, i) {
        s.classList.toggle('active', i < rating);
    });
    var ratingTexts = ['', '1 Star', '2 Stars', '3 Stars', '4 Stars', '5 Stars'];
    var display = document.getElementById('ratingDisplay');
    if (display) display.textContent = ratingTexts[rating] || '';
}

document.addEventListener('DOMContentLoaded', function() {
    var hdn = document.getElementById('hdnActiveUserTab');
    var tab = hdn ? hdn.value : 'gallery';
    if (!tab) tab = 'gallery';
    switchUserTab(tab);
});
