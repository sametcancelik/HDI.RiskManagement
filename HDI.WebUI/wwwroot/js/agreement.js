$(document).ready(function () {
    loadAgreements();

    $('#btnRefresh').on('click', () => loadAgreements());

    $(document).on('click', '.btnAddKeyword', function () {
        $('#modalAgreementId').val($(this).data('id'));
        $('#keywordModal').modal('show');
    });

    $('#btnSaveKeyword').on('click', function () {
    const data = {
        agreementId: parseInt($('#modalAgreementId').val()),
        word: $('#keywordWord').val(),
        riskWeight: parseInt($('#keywordWeight').val())
    };

    if (!data.word || isNaN(data.riskWeight)) {
        Swal.fire('Uyarı', 'Lütfen kelime ve ağırlık bilgilerini geçerli girin.', 'warning');
        return;
    }

    const $btn = $(this);
    $btn.prop('disabled', true).html('<span class="spinner-border spinner-border-sm"></span>');

    $.ajax({
        url: `${API_CONFIG.baseUrl}/keywords`, 
        method: 'POST',
        headers: { 
            'X-Api-Key': API_CONFIG.key, 
            'Content-Type': 'application/json' 
        },
        data: JSON.stringify(data), 
        success: function (res) {
            if(res.isSuccess) {
                Swal.fire('Başarılı', 'Riskli kelime sisteme tanımlandı.', 'success');
                $('#keywordModal').modal('hide');
                $('#keywordWord').val('');
                $('#keywordWeight').val('100');
            } else {
                Swal.fire('Hata', res.message, 'error');
            }
        },
        error: function (xhr) {
            const errorMsg = xhr.responseJSON?.message || "Sunucuyla iletişim kurulamadı.";
            Swal.fire('Hata!', errorMsg, 'error');
            console.log("Hata Detayı:", xhr.responseText);
        },
        complete: function() {
            $btn.prop('disabled', false).html('Kaydet');
        }
    });
});
});

function loadAgreements() {
    $.ajax({
        url: `${API_CONFIG.baseUrl}/agreements`,
        method: 'GET',
        headers: { 'X-Api-Key': API_CONFIG.key },
        success: function (res) {
            let html = '';
            res.data.forEach(item => {
                html += `<tr>
                    <td>${item.id}</td>
                    <td class="fw-bold">${item.title}</td>
                    <td><span class="badge bg-light text-dark border">${item.riskLimit.toLocaleString('tr-TR')} ₺</span></td>
                    <td><span class="badge bg-info">HDI-Sigorta</span></td>
                    <td class="text-end">
                        <button class="btn btn-sm btn-outline-success btnAddKeyword" data-id="${item.id}">
                            <i class="fas fa-plus me-1"></i>Kelime Ekle
                        </button>
                    </td>
                </tr>`;
            });
            $('#agreementTableBody').html(html || '<tr><td colspan="5" class="text-center">Kayıt bulunamadı.</td></tr>');
        },
        error: function() {
            $('#agreementTableBody').html('<tr><td colspan="5" class="text-center text-danger">Veriler yüklenirken hata oluştu.</td></tr>');
        }
    });
}