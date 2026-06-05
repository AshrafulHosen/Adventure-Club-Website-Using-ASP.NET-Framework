// USER DASHBOARD - CLIENT-SIDE TAB SWITCHING ONLY
// All login, form submission, and data management is handled server-side by ASP.NET

// ===== USER TAB SWITCHING =====
function switchUserTab(tabName) {
    // Hide all tab content sections
    var tabs = document.querySelectorAll('.user-tab-content');
    for (var i = 0; i < tabs.length; i++) {
        tabs[i].classList.remove('active');
    }

    // Remove active class from all tab buttons
    var buttons = document.querySelectorAll('.user-tab');
    for (var i = 0; i < buttons.length; i++) {
        buttons[i].classList.remove('active');
    }

    // Show the selected tab
    var targetTab = document.getElementById(tabName + 'Tab');
    if (targetTab) {
        targetTab.classList.add('active');
    }

    // Highlight the clicked button
    if (event && event.target) {
        var clicked = event.target.closest('.user-tab');
        if (clicked) {
            clicked.classList.add('active');
        }
    }
}

// ===== USER UPLOAD METHOD SWITCHING =====
function switchUserUploadMethod(method) {
    var fileMethod = document.getElementById('userFileUploadMethod');
    var urlMethod = document.getElementById('userUrlUploadMethod');
    var buttons = document.querySelectorAll('.upload-method-tabs .upload-method-btn');

    for (var i = 0; i < buttons.length; i++) {
        buttons[i].classList.remove('active');
    }
    if (event && event.target) {
        event.target.classList.add('active');
    }

    if (method === 'file') {
        if (fileMethod) fileMethod.classList.add('active');
        if (urlMethod) urlMethod.classList.remove('active');
    } else {
        if (fileMethod) fileMethod.classList.remove('active');
        if (urlMethod) urlMethod.classList.add('active');
    }
}

// ===== STAR RATING =====
function setRating(rating) {
    var ratingInput = document.getElementById('reviewRating');
    if (ratingInput) ratingInput.value = rating;

    var stars = document.querySelectorAll('.star');
    for (var i = 0; i < stars.length; i++) {
        if (i < rating) {
            stars[i].classList.add('active');
        } else {
            stars[i].classList.remove('active');
        }
    }

    var ratingDisplay = document.getElementById('ratingDisplay');
    var ratingTexts = ['', '1 Star', '2 Stars', '3 Stars', '4 Stars', '5 Stars'];
    if (ratingDisplay) ratingDisplay.textContent = ratingTexts[rating];
}
