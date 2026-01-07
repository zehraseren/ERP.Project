# ERP Project - Clean Architecture Based Monorepo

Bu proje **ASP.NET Core için Clean Architecture** temelli bir backend altyapısı üzerine inşa edilmiş, **Angular tabanlı frontend** ile desteklenen uçtan uca bir **ERP (Enterprise Resource Planning)** uygulamasıdır.

Projenin backend alt yapısı, başlangıçta eğitim amacıyla verilen ve **NuGet üzerinden kurulan** [Clean Architecture template](https://www.nuget.org/packages/TS.Result/10.0.0)'i temel alınarak geliştirilmiş; zamanla iş süreçleri, domain kuralları ve frontend entegrasyonu eklenerek genişletilmiştir.

###### *Bu proje Taner Saydam'ın Udemy'deki [.NET 8 ve Angular 17 ile ERP Uygulaması](https://www.udemy.com/course/angular-17-ve-net-8-ile-erp-uygulamasi/) referans alınarak geliştirilmiştir. Eğitim sonunda alınan [sertifika için tıklayınız](https://drive.google.com/file/d/1-9XlSyskLf0ZpwEJWoHGnQTYnl-UxNiD/view?usp=sharing).*

## 📐 Mimari Yaklaşım
Backend tarafında **Clean Architecture** prensipleri benimsenmiştir:
+ Loose coupling
+ Dependency Injection
+ Domain odaklı tasarım
+ Test edilebilir ve genişletilebilir yapı

## 🗂️ Proje Yapısı (Monorepo)
```
ERP/
│
├── ERPServer/        → ASP.NET Core (.NET 8) Backend
│   ├── Domain
│   ├── Application
│   ├── Infrastructure
│   └── WebAPI
│
├── ERPClient/        → Angular Frontend
│
└── README.md
```

## 🧠 Backend Teknolojileri
Backend projesi .NET 8 ile geliştirilmiştir.

Kullanılan başlıca kütüphaneler:
+ EntityFrameworkCore
+ EntityFrameworkCore.Identity
+ MediatR
+ AutoMapper
+ FluentValidation
+ TS.Result
+ TS.EntityFrameworkCore.GenericRepository

Backend tarafında:
+ CQRS pattern
+ Repository pattern
+ Unit of Work
+ Validation ve Result pattern
  aktif olarak kullanılmaktadır.

## 🎨 Frontend Teknolojileri
Frontend tarafı Angular ile geliştirilmiştir.

+ Standalone component yapısı
+ Template-driven forms
+ Custom pipes
+ Servis bazlı HTTP katmanı
+ Backend API ile tam entegre çalışma

> Frontend, backend’den tamamen bağımsız geliştirilmiş olup aynı repository içinde **monorepo** yaklaşımıyla konumlandırılmıştır.

## 🗄️ Veritabanı
+ Varsayılan veritabanı: **MSSQL**
+ Connection string `appsettings.json` üzerinden yapılandırılabilir
```json
"ConnectionStrings": {
  "SqlServer": "Server=.;Database=ERPDb;Trusted_Connection=True;"
}
```
> Farklı bir veritabanı kullanılmak istenirse: **Infrastructure katmanındaki EF Core provider değiştirilmelidir.**

## 🔐 Kimlik Doğrulama
+ ASP.NET Core Identity altyapısı hazır bulunmaktadır
+ Uygulama ilk çalıştığında otomatik olarak admin kullanıcı oluşturulur
+ Login mekanizması backend tarafında hazır şekilde gelmektedir

## 🚀 Projenin Amacı
Bu proje:
+ Clean Architecture prensiplerini gerçek bir iş senaryosu üzerinde uygulamak
+ Backend ve frontend entegrasyonunu kurumsal ölçekte deneyimlemek
+ ERP sistemlerinin temel yapı taşlarını öğrenmek
  amacıyla geliştirilmiştir.

## Proje Görselleri
###### *Görseller demo amaçlıdır.*
### Login
<img width="1918" height="861" alt="Ekran görüntüsü 2026-01-07 174333" src="https://github.com/user-attachments/assets/aa80ce18-b36f-4fdb-9460-b4f05e260225" />

### Müşteri Listesi
<img width="1919" height="848" alt="Ekran görüntüsü 2026-01-07 182244" src="https://github.com/user-attachments/assets/f6cd13c6-7485-49dd-bacb-cab41eed2307" />

### Sipariş Ekranı
<img width="1919" height="865" alt="Ekran görüntüsü 2026-01-07 174510" src="https://github.com/user-attachments/assets/c8cbb244-203e-4fb6-99d6-f486ddb05407" />

### Fatura Ekranı
<img width="1913" height="848" alt="Ekran görüntüsü 2026-01-07 182509" src="https://github.com/user-attachments/assets/e52d327e-37c0-47a6-a02c-e1c6afdfb7db" />
<img width="1893" height="854" alt="Ekran görüntüsü 2026-01-07 182628" src="https://github.com/user-attachments/assets/487462c1-3d3a-409c-a4d0-0fbbb2291df0" />

### Üretim Ekranı
<img width="1914" height="852" alt="Ekran görüntüsü 2026-01-07 182522" src="https://github.com/user-attachments/assets/de2d45bb-77f3-46a9-ae34-ebdb3ea0e8d9" />
