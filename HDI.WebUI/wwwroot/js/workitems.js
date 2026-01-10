$(document).ready(function () {
    loadWorkItems();

    $('#btnFilter').on('click', function () {
        loadWorkItems();
    });

    $('#btnReset').on('click', function () {
        $('#fStart, #fEnd').val('');
        $('#fExceeded').val('');
        loadWorkItems();
    });
});

/**
 * İş Ortaklarına ait analiz geçmişini filtreleyerek listeler
 */
function loadWorkItems() {
    const $tableBody = $('#workItemsBody');
    
    // Filtre değerlerini topla
    const filterData = {
        startDate: $('#fStart').val() || null,
        endDate: $('#fEnd').val() || null,
        isLimitExceeded: $('#fExceeded').val() === "" ? null : $('#fExceeded').val() === "true"
    };

    $tableBody.html(`
        <tr>
            <td colspan="4" class="text-center py-4">
                <div class="spinner-border spinner-border-sm text-primary" role="status"></div>
                Veriler sorgulanıyor...
            </td>
        </tr>
    `);

    $.ajax({
        url: `${API_CONFIG.baseUrl}/work-items/filter`,
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(filterData),
        success: function (res) {
            let html = '';

            if (res.isSuccess && res.data && res.data.length > 0) {
                res.data.forEach(item => {
                    const dateStr = new Date(item.createdDate).toLocaleString('tr-TR', {
                        day: '2-digit',
                        month: '2-digit',
                        year: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit'
                    });

                    const badgeClass = item.isLimitExceeded ? 'bg-danger shadow-sm' : 'bg-success shadow-sm';
                    const badgeText = item.isLimitExceeded ? 'Limit Aşıldı' : 'Normal';

                    html += `
                        <tr class="align-middle">
                            <td>
                                <div class="d-flex align-items-center">
                                    <i class="far fa-clock me-2 text-muted"></i>
                                    <span>${dateStr}</span>
                                </div>
                            </td>
                            <td>
                                <span class="d-inline-block text-truncate" style="max-width: 300px;" title="${item.description}">
                                    ${item.description}
                                </span>
                            </td>
                            <td class="fw-bold text-dark">
                                ${item.calculatedRiskAmount.toLocaleString('tr-TR', { minimumFractionDigits: 2 })} ₺
                            </td>
                            <td>
                                <span class="badge ${badgeClass}" style="min-width: 90px;">
                                    <i class="fas ${item.isLimitExceeded ? 'fa-exclamation-triangle' : 'fa-check-circle'} me-1"></i>
                                    ${badgeText}
                                </span>
                            </td>
                        </tr>`;
                });
            } else {
                html = `
                    <tr>
                        <td colspan="4" class="text-center py-5 text-muted">
                            <i class="fas fa-search mb-2 d-block fa-2x"></i>
                            Kriterlere uygun analiz kaydı bulunamadı.
                        </td>
                    </tr>`;
            }

            $tableBody.hide().html(html).fadeIn(300);
        },
        error: function (xhr) {
            let errorMsg = "Analiz geçmişi yüklenirken bir hata oluştu.";
            if (xhr.status === 401) errorMsg = "Oturum süreniz doldu, lütfen tekrar giriş yapın.";
            
            $tableBody.html(`
                <tr>
                    <td colspan="4" class="text-center text-danger py-4">
                        <i class="fas fa-exclamation-circle me-1"></i> ${errorMsg}
                    </td>
                </tr>
            `);
            
            console.error("Filtreleme Hatası:", xhr);
        }
    });
}