$(document).ready(function () {
    $('#btnLogin').click(function () {
        const apiKey = $('#apiKey').val();
        const apiSecret = $('#apiSecret').val();

        if (!apiKey || !apiSecret) {
            Swal.fire('Uyarı', 'Lütfen API Key ve Secret alanlarını doldurun.', 'warning');
            return;
        }

        const $btn = $(this);
        $btn.prop('disabled', true).html('<span class="spinner-border spinner-border-sm"></span> Giriş Yapılıyor...');

        $.ajax({
            url: `${API_CONFIG.baseUrl}/auth/login`,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                apiKey: apiKey,
                apiSecret: apiSecret
            }),
            success: function (res) {
                if (res.isSuccess) {
                    sessionStorage.setItem('HDI_API_KEY', apiKey);
                    sessionStorage.setItem('HDI_API_SECRET', apiSecret);
                    sessionStorage.setItem('PARTNER_NAME', res.data.name);

                    Swal.fire({
                        icon: 'success',
                        title: 'Hoş Geldiniz!',
                        text: `Sayın ${res.data.name}, yönlendiriliyorsunuz...`,
                        timer: 1500,
                        showConfirmButton: false
                    }).then(() => {
                        window.location.href = '/Home';
                    });
                }
            },
            error: function (xhr) {
                const msg = xhr.responseJSON?.message || "Giriş başarısız. Lütfen bilgilerinizi kontrol edin.";
                Swal.fire('Hata', msg, 'error');
                $btn.prop('disabled', false).text('Giriş Yap');
            }
        });
    });
});