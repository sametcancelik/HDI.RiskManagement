🛡️ HDI RISK YONETIM & ANALIZ SISTEMI - README

1. PROJE OZETI
---------------
Bu sistem, sigorta poliçeleri ve iş süreçlerindeki risk faktörlerini 
dinamik olarak analiz etmek için geliştirilmiştir. Metin tabanlı 
açıklamaları tarayarak, tanımlı riskli kelimeler üzerinden bir maliyet 
veya risk puanı hesaplar. Limit aşımlarını SignalR ile anlık bildirir.

2. TEKNIK MIMARI
-----------------
* Backend: .NET 10.0 Web API
* Frontend: ASP.NET Core MVC (jQuery & Bootstrap 5)
* Real-time: SignalR (WebSocket)
* DB: Entity Framework Core (SQL Server)
* Guvenlik: X-Api-Key tabanlı Multi-Tenant Middleware

3. KATMANLAR (PROJECT STRUCTURE)
---------------------------------
- HDI.Domain: Temel varlıklar (Entities) ve Interface'ler.
- HDI.Application: İş mantığı, DTO'lar ve Service arayüzleri.
- HDI.Infrastructure: SignalR Hub yapısı ve servisleri.
- HDI.Persistence: DbContext, Migration ve Repository sınıfları.
- HDI.WebAPI: REST Controller'lar, CORS ve Tenant yönetim katmanı.
- HDI.WebUI: Kullanıcı arayüzü, JavaScript ve AJAX yönetimi.

4. KURULUM (INSTALLATION)
--------------------------
1) Veritabanı Oluşturma:
   Terminalde projeyi açın ve şu komutu çalıştırın:
   dotnet ef database update --project HDI.Persistence --startup-project HDI.WebAPI

2) Backend Başlatma:
   dotnet run --project ./HDI.WebApi
   dotnet run (Varsayılan Port: 1907)

3) Frontend Başlatma:
   dotnet run --project ./HDI.WebUI
   dotnet run (Varsayılan Port: 5177)

5. KULLANIM VE TEST (USER GUIDE)
---------------------------------
- DASHBOARD: Anlık limit aşım bildirimlerini sağ üstte görüntüler.
- ANLASMALAR: Anlaşmaları listeler. "Kelime Ekle" butonu ile poliçeye 
  özel riskli kelimeler (örn: "Yangın", "Hasar") ve ağırlık puanları eklenir.
- RISK ANALIZI: Bir anlaşma seçilir ve açıklama metni girilir. Analiz 
  sonucunda risk tutarı hesaplanır. Eğer tutar anlaşma limitini aşarsa 
  SignalR üzerinden tüm açık dashboard'lara uyarı gider.
- ANALIZ GECMISI: Yapılan tüm analizler tarih ve limit durumuna göre 
  burada listelenir.

6. ONEMLI BAGIMLILIKLAR
------------------------
- SweetAlert2 (Bildirimler için)
- FontAwesome (İkonlar için)
- SignalR JS Client (Anlık haberleşme için)

NOT: İncelerken ekstra efor sarf ettirmemesi için appsettings.json dosyaları repoya pushlanmıştır.

HDI Risk Engine - 2026 | Safe & Fast Analysis
