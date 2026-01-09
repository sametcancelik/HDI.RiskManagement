$(document).ready(function () {
    // Anlaşmaları yükle
    $.ajax({
        url: `${API_CONFIG.baseUrl}/agreements`,
        method: 'GET',
        headers: { 'X-Api-Key': API_CONFIG.key },
        success: (res) => {
            res.data.forEach(a => $('#selAgreement').append(`<option value="${a.id}">${a.title} (Limit: ${a.riskLimit} ₺)</option>`));
        }
    });

    $('#btnRunAnalyze').click(function () {
        const text = $('#txtDescription').val();
        const agreementId = $('#selAgreement').val();

        if (!text || !agreementId) {
            Swal.fire('Uyarı', 'Lütfen metin girin ve bir anlaşma seçin.', 'info');
            return;
        }

        $(this).prop('disabled', true).html('<span class="spinner-border spinner-border-sm"></span> Analiz Ediliyor...');

        $.ajax({
            url: `${API_CONFIG.baseUrl}/risk-analysis/analyze`,
            method: 'POST',
            contentType: 'application/json',
            headers: { 'X-Api-Key': API_CONFIG.key },
            data: JSON.stringify({ agreementId: parseInt(agreementId), description: text }),
            success: (res) => {
                $('#analysisResult').hide().fadeIn();
                $('#resScore').text(res.data + " ₺");
                $('#resMsg').text(res.message);
                
                const isExceeded = res.message.includes("aşıldı");
                $('#resBadge').text(isExceeded ? "LİMİT AŞILDI" : "GÜVENLİ")
                             .attr('class', isExceeded ? 'badge bg-danger' : 'badge bg-success');
                $('#analysisResult').css('background', isExceeded ? '#fff5f5' : '#f0fff4')
                                   .css('border', isExceeded ? '1px solid #feb2b2' : '1px solid #9ae6b4');
            },
            complete: () => {
                $(this).prop('disabled', false).html('Analiz Et');
            }
        });
    });
});