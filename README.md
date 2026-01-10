🛡️ HDI Risk Yönetim & Analiz Sistemi
Bu sistem, sigorta poliçeleri ve iş süreçlerindeki risk faktörlerini dinamik olarak analiz etmek amacıyla geliştirilmiş, Multi-Tenant yapıda çalışan bir Risk Engine uygulamasıdır. Metin tabanlı açıklamaları tarayarak, tanımlı riskli kelimeler üzerinden bir maliyet veya risk puanı hesaplar ve limit aşımlarını anlık olarak bildirir.

🚀 Teknik Mimari
Backend: .NET 10.0 Web API
Frontend: ASP.NET Core MVC (jQuery & Bootstrap 5)
Real-time: SignalR (WebSocket)
Veritabanı: Entity Framework Core (SQL Server)
Güvenlik: X-Api-Key & X-Api-Secret tabanlı, SHA256 Hash doğrulamalı Multi-Tenant Middleware
📂 Katmanlı Mimari (Project Structure)
HDI.Domain: Temel varlıklar (Entities) ve Interface'ler.
HDI.Application: İş mantığı, DTO'lar, Service arayüzleri ve Mapping.
HDI.Infrastructure: SignalR Hub yapısı ve harici servis entegrasyonları.
HDI.Persistence: DbContext, Repository implementasyonları ve Migration yönetimi.
HDI.WebAPI: REST Controller'lar ve Tenant doğrulama katmanı.
HDI.WebUI: Kullanıcı arayüzü, JavaScript ve AJAX tabanlı API iletişimi.
🛠️ Kurulum ve Çalıştırma
1. Veritabanı ve Migration
Sistem, SeedData içinde bir adet test partneri (HDI Sigorta) barındırır. Veritabanını oluşturmak ve tabloları ilklendirmek için:

Bash
# HDI.Persistence katmanına giderek veya ana dizinden:
dotnet ef database update --project HDI.Persistence --startup-project HDI.WebAPI
2. Projeleri Başlatma
Önce API projesini, ardından UI projesini başlatın:

Bash
# Web API Başlatma (Varsayılan Port: 1907)
dotnet run --project ./HDI.WebAPI

# Web UI Başlatma (Varsayılan Port: 5177)
dotnet run --project ./HDI.WebUI
🔐 Güvenlik ve Giriş Bilgileri
Sistem Multi-Tenant yapıda olduğu için her partner kendi Key ve Secret ikilisiyle giriş yapmalıdır. Giriş anında Secret bilgisi SHA256 ile hashlenerek veritabanında doğrulanır.

Örnek Test Bilgileri (Seed Data):
API Key: hdi-test-key-123
API Secret: hdi-secret-789
📋 Kullanım Rehberi
Giriş (Login): Partner bilgileriyle sisteme giriş yapıldığında, X-Api-Key ve X-Api-Secret bilgileri sessionStorage üzerinde saklanır ve tüm API isteklerine otomatik eklenir.
Dashboard: Anlık risk analiz sonuçlarını ve limit aşımlarını SignalR üzerinden real-time takip eder.
Anlaşmalar: Poliçe türlerine göre risk limitleri belirlenir ve "Kelime Ekle" butonu ile riskli kelimelere (örn: "Yangın", "Hasar") ağırlık puanları atanır.
Risk Analizi: Metin girişi yapıldığında sistem kelimeleri tarar, toplam risk puanını hesaplar ve limit aşımı durumunda dashboard'lara anlık uyarı gönderir.
📦 Önemli Bağımlılıklar
SweetAlert2: Şık ve interaktif bildirimler için.
SignalR JS Client: WebSocket üzerinden anlık haberleşme.
FontAwesome: Profesyonel ikon seti.
NOT: Hızlı test edilebilmesi için appsettings.json dosyaları ve gerekli tüm veritabanı bağlantı ayarları proje dosyalarına dahil edilmiştir.

HDI Risk Engine - 2026 | Safe & Fast Analysis