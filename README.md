PROJE OZETI 

Bu sistem, sigorta poliçeleri ve iş süreçlerindeki risk faktörlerini dinamik olarak analiz etmek amacıyla geliştirilmiş, Multi-Tenant yapıda çalışan bir Risk Engine uygulamasıdır. Metin tabanlı açıklamaları tarayarak, tanımlı riskli kelimeler üzerinden bir maliyet veya risk puanı hesaplar ve limit aşımlarını anlık olarak bildirir.

TEKNIK MIMARI
Backend: .NET 10.0 Web API
Frontend: ASP.NET Core MVC (jQuery & Bootstrap 5)
Real-time: SignalR (WebSocket)
Veritabanı: Entity Framework Core (SQL Server)
Guvenlik: X-Api-Key & X-Api-Secret tabanlı, SHA256 Hash doğrulamalı Multi-Tenant Middleware

KATMANLAR (PROJECT STRUCTURE)
HDI.Domain: Temel varlıklar (Entities) ve Interface'ler.
HDI.Application: İş mantığı, DTO'lar, Service arayüzleri ve Mapping.
HDI.Infrastructure: SignalR Hub yapısı ve harici servis entegrasyonları.
HDI.Persistence: DbContext, Repository implementasyonları ve Migration yönetimi.
HDI.WebAPI: REST Controller'lar ve Tenant doğrulama katmanı.
HDI.WebUI: Kullanıcı arayüzü, JavaScript ve AJAX tabanlı API iletişimi.

KURULUM (INSTALLATION)
Migration & Veritabanı Oluşturma: Terminalde projeyi açın ve şu komutu çalıştırarak tabloları ve seed datayı oluşturun: dotnet ef database update --project HDI.Persistence --startup-project HDI.WebAPI
Backend Başlatma: dotnet run --project ./HDI.WebAPI (Varsayılan Port: 1907)
Frontend Başlatma: dotnet run --project ./HDI.WebUI (Varsayılan Port: 5177)
GIRIS BILGILERI (TEST CREDENTIALS) Sistem Multi-Tenant yapıda olduğu için giriş bilgileri zorunludur. Secret bilgisi veritabanında SHA256 ile hashlenmiş olarak saklanır.
API Key: hdi-test-key-123
API Secret: hdi-secret-789

KULLANIM REHBERI

GIRIS: Partner bilgileriyle giriş yapıldığında Key ve Secret bilgileri sessionStorage'a kaydedilir ve tüm AJAX isteklerine Header (X-Api-Key, X-Api-Secret) olarak eklenir.
DASHBOARD: Anlık limit aşım bildirimlerini SignalR üzerinden görüntüler.
ANLASMALAR: Anlaşmaları listeler. "Kelime Ekle" butonu ile poliçeye özel riskli kelimeler ve ağırlık puanları (RiskWeight) tanımlanır.
RISK ANALIZI: Metin girişi yapıldığında sistem tanımlı kelimeleri tarar ve risk tutarını hesaplar. Tutar limit aşarsa dashboard'lara anlık uyarı gider.
ANALIZ GECMISI: Geçmiş analizlerin detaylarını ve limit durumlarını listeler.

ONEMLI BAGIMLILIKLAR
SweetAlert2 (Bildirimler için)
SignalR JS Client (WebSocket iletişimi için)
FontAwesome (İkonlar için)
NOT: İncelerken ekstra efor sarf ettirmemesi için appsettings.json dosyaları ve veritabanı bağlantı ayarları repoya pushlanmıştır.

HDI Risk Engine - 2026 | Safe & Fast Analysis
