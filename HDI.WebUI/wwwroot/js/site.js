const API_CONFIG = {
    key: sessionStorage.getItem('HDI_API_KEY'),
    secret: sessionStorage.getItem('HDI_API_SECRET'),
    baseUrl: 'http://localhost:1907/api',
    hubUrl: 'http://localhost:1907/risk-hub'
};


$(document).ready(function() {
    const isLoginPage = window.location.pathname.toLowerCase().includes('/account/login');

    if (!API_CONFIG.key && !isLoginPage) {
        window.location.href = '/Account/Login';
        return;
    }

    const partnerName = sessionStorage.getItem('PARTNER_NAME');
    if(partnerName) {
        $('#userNameDisplay').text(partnerName);
    }

    $('#btnLogout').click(function() {
        sessionStorage.clear();
        window.location.href = '/Account/Login';
    });

    $.ajaxSetup({
        beforeSend: function (xhr) {
            if (API_CONFIG.key && API_CONFIG.secret) {
                xhr.setRequestHeader('X-Api-Key', API_CONFIG.key);
                xhr.setRequestHeader('X-Api-Secret', API_CONFIG.secret);
            }
        },
        error: function (xhr) {
            if (xhr.status === 401) {
                sessionStorage.clear();
                window.location.href = '/Account/Login';
            }
        }
    });
});