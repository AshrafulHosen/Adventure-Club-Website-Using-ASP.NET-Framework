// ADMIN PANEL - CLIENT-SIDE TAB SWITCHING ONLY
// All login, form submission, and data management is handled server-side by ASP.NET

// ===== TAB SWITCHING =====
function switchAdminTab(tabName) {
    // Hide all tab content sections
    var tabs = document.querySelectorAll('.admin-tab-content');
    for (var i = 0; i < tabs.length; i++) {
        tabs[i].classList.remove('active');
    }

    // Remove active class from all nav items
    var navItems = document.querySelectorAll('.admin-nav-item');
    for (var i = 0; i < navItems.length; i++) {
        navItems[i].classList.remove('active');
    }

    // Show the selected tab
    var targetTab = document.getElementById(tabName);
    if (targetTab) {
        targetTab.classList.add('active');
    }

    // Highlight the clicked nav item
    if (event && event.target) {
        var clicked = event.target.closest('.admin-nav-item');
        if (clicked) {
            clicked.classList.add('active');
        }
    }
}

// ===== GALLERY UPLOAD METHOD SWITCHING =====
function switchUploadMethod(method) {
    var fileMethod = document.getElementById('fileUploadMethod');
    var urlMethod = document.getElementById('urlUploadMethod');
    var buttons = document.querySelectorAll('.upload-method-btn');

    // Remove active class from all buttons
    for (var i = 0; i < buttons.length; i++) {
        buttons[i].classList.remove('active');
    }

    // Add active class to clicked button
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
