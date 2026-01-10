$(document).ready(function () {
    loadAgreementsForAnalysis();

    $('#btnRunAnalyze').click(function () {
        const text = $('#txtDescription').val();
        const agreementId = $('#selAgreement').val();

        if (!text || !agreementId) {
            Swal.fire('Uyarı', 'Lütfen analiz metni girin ve bir anlaşma seçin.', 'info');
            return;
        }

        const $btn = $(this);
        $btn.prop('disabled', true).html('<span class="spinner-border spinner-border-sm"></span> Analiz Ediliyor...');

        $.ajax({
            url: `${API_CONFIG.baseUrl}/risk-analysis/analyze?agreementId=${parseInt(agreementId)}`,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(text),
            success: (res) => {
                if (res.isSuccess) {
                    $('#analysisResult').hide().fadeIn(400);
                    
                    $('#resScore').text(res.data.toLocaleString('tr-TR', { minimumFractionDigits: 2 }) + " ₺");
                    $('#resMsg').text(res.message);
                    
                    const isExceeded = res.message.toLowerCase().includes("aşıldı") || res.message.toLowerCase().includes("limit");
                    
                    $('#resBadge').text(isExceeded ? "LİMİT AŞILDI" : "GÜVENLİ")
                                 .attr('class', isExceeded ? 'badge bg-danger shadow-sm' : 'badge bg-success shadow-sm');
                    
                    $('#analysisResult').css({
                        'background': isExceeded ? '#fff5f5' : '#f0fff4',
                        'border-left': isExceeded ? '5px solid #dc3545' : '5px solid #198754',
                        'padding': '20px',
                        'border-radius': '8px'
                    });
                }
            },
            error: (xhr) => {
                let errorMsg = 'Analiz işlemi sırasında sunucu hatası oluştu.';
                if (xhr.status === 401) errorMsg = "Oturumunuz geçersiz veya API Key/Secret hatalı.";
                
                Swal.fire('Hata', errorMsg, 'error');
            },
            complete: () => {
                $btn.prop('disabled', false).html('<i class="fas fa-search me-1"></i> Analiz Et');
            }
        });
    });
});

function loadAgreementsForAnalysis() {
    const $select = $('#selAgreement');
    
    $.ajax({
        url: `${API_CONFIG.baseUrl}/agreements`,
        method: 'GET',
        success: (res) => {
            if (res.isSuccess) {
                $select.empty().append('<option value="">Analiz Edilecek Anlaşmayı Seçin...</option>');
                res.data.forEach(a => {
                    $select.append(`<option value="${a.id}">${a.title} (Limit: ${a.riskLimit.toLocaleString('tr-TR')} ₺)</option>`);
                });
            }
        },
        error: function(xhr) {
            console.error("Anlaşma listesi yüklenemedi:", xhr);
        }
    });
}