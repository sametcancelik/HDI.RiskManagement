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

        $.ajax({
            url: `${API_CONFIG.baseUrl}/keywords`,
            method: 'POST',
            headers: { 'X-Api-Key': API_CONFIG.key, 'Content-Type': 'application/json' },
            data: JSON.stringify(data),
            success: function (res) {
                Swal.fire('Başarılı', 'Kelime eklendi.', 'success');
                $('#keywordModal').modal('hide');
                $('#keywordWord').val('');
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
                    <td><span class="badge bg-light text-dark border">${item.riskLimit} ₺</span></td>
                    <td class="text-end">
                        <button class="btn btn-sm btn-outline-success btnAddKeyword" data-id="${item.id}">
                            <i class="fas fa-plus me-1"></i>Kelime Ekle
                        </button>
                    </td>
                </tr>`;
            });
            $('#agreementTableBody').html(html);
        }
    });
}