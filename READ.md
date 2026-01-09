# HDI Risk Yönetim ve Analiz Sistemi

Bu proje, iş ortaklarının (tenants) protokollerini yönetebileceği, metin tabanlı risk analizi yapabileceği ve anlık limit aşımlarını takip edebileceği uçtan uca bir çözüm sunar.

## Öne Çıkan Özellikler
- **Multi-Tenant Mimari:** Middleware tabanlı `X-Api-Key` izolasyonu ile veri güvenliği.
- **Risk Analiz Motoru:** Anlaşma bazlı anahtar kelime ve ağırlık eşleştirmesi.
- **Real-Time Bildirimler:** SignalR ile limit aşımı durumunda tüm dashboard'lara anlık uyarı.
- **Dinamik Raporlama:** jQuery & AJAX tabanlı, sayfa yenilemeden filtreleme ve listeleme.
- **Veri Bütünlüğü:** FluentValidation ile sunucu taraflı veri doğrulama.

## Teknoloji Yığını
- **Backend:** .NET 8/9 Web API, EF Core, SignalR.
- **Frontend:** .NET MVC, jQuery, Bootstrap 5, SweetAlert2.
- **Mimari:** Clean Architecture (Domain, Application, Infrastructure, Web).

## Kurulum
1. `HDI.WebApi` projesini çalıştırın (Port: 5000).
2. `HDI.WebUI` projesini çalıştırın (Port: 5001).
3. WebUI üzerinden "Risk Analizi" sekmesine giderek bir analiz başlatın.

*Not: Örnek API Key: `7de756a213eb479fac9b33b23fe336fd`*