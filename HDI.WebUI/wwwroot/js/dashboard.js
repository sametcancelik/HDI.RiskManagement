$(document).ready(function () {
    loadStats();
    initSignalR();
});

function loadStats() {
    $.ajax({
        url: `${API_CONFIG.baseUrl}/dashboard/stats`,
        method: 'GET',
        headers: { 
            'X-Api-Key': API_CONFIG.key,
            'X-Api-Secret': API_CONFIG.secret 
        },
        success: function (res) {
            if (res.isSuccess) {
                $('#totalWorkItems').text(res.data.totalWorkItems);
                $('#exceededCount').text(res.data.totalExceededItems);
                $('#avgRisk').text(res.data.averageRiskScore.toFixed(2) + " ₺");
            }
        },
        error: function(xhr) {
            if(xhr.status === 401) window.location.href = '/Account/Login';
        }
    });
}

function initSignalR() {
    // SignalR bağlantısında secret'ı header veya query string ile göndermelisin
    const connection = new signalR.HubConnectionBuilder()
        .withUrl(API_CONFIG.hubUrl, {
            // SignalR negotiate aşamasında bu header'ları gönderir
            headers: { 
                "X-Api-Key": API_CONFIG.key,
                "X-Api-Secret": API_CONFIG.secret 
            }
        })
        .withAutomaticReconnect()
        .build();

    connection.on("ReceiveRiskAlert", (data) => {
        Swal.fire({
            title: `<span style="color: #d33">${data.title}</span>`,
            html: `<b>Açıklama:</b> ${data.description}<br><b>Skor:</b> ${data.score} ₺ / <b>Limit:</b> ${data.limit} ₺`,
            icon: 'warning',
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 6000,
            timerProgressBar: true
        });
        
        if (typeof loadStats === "function") loadStats();
        if (typeof loadWorkItems === "function") loadWorkItems();
    });

    connection.start().catch(err => console.error("SignalR Error: ", err));
}