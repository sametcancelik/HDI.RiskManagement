$(document).ready(function () {
    loadWorkItems();

    $('#btnFilter').click(function () {
        loadWorkItems();
    });
});

function loadWorkItems() {
    const filterData = {
        startDate: $('#fStart').val() || null,
        endDate: $('#fEnd').val() || null,
        isLimitExceeded: $('#fExceeded').val() === "" ? null : $('#fExceeded').val() === "true"
    };

    $.ajax({
        url: `${API_CONFIG.baseUrl}/work-items/filter`,
        method: 'POST',
        contentType: 'application/json',
        headers: { 'X-Api-Key': API_CONFIG.key },
        data: JSON.stringify(filterData),
        success: (res) => {
            let html = '';
            res.data.forEach(item => {
                html += `<tr>
                    <td><small>${new Date(item.createdDate).toLocaleString()}</small></td>
                    <td title="${item.description}">${item.description.substring(0, 40)}...</td>
                    <td class="fw-bold">${item.calculatedRiskAmount} ₺</td>
                    <td><span class="badge ${item.isLimitExceeded ? 'bg-danger' : 'bg-success'}">
                        ${item.isLimitExceeded ? 'Limit Aşıldı' : 'Normal'}</span></td>
                </tr>`;
            });
            $('#workItemsBody').html(html || '<tr><td colspan="4" class="text-center">Kayıt bulunamadı.</td></tr>');
        }
    });
}