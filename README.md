# Online Shopping Platform - ASP.NET Core Web API

Bu proje, çok katmanlı mimariyi ve modern yazılım geliştirme ilkelerini kullanarak geliştirilen bir online alışveriş platformudur. Projede kullanıcı kimlik doğrulama, ürün yönetimi, sipariş işlemleri ve JWT tabanlı yetkilendirme gibi özellikler bulunmaktadır.

## Proje Mimarisi

Proje, katmanlı mimariyi takip ederek aşağıdaki katmanlardan oluşmaktadır:

- **Presentation Layer (API Katmanı)**: API isteklerini yöneten controller'lar burada yer alır.
- **Business Layer (İş Katmanı)**: Uygulamanın iş mantığını içerir.
- **Data Access Layer (Veri Erişim Katmanı)**: Entity Framework Code First yaklaşımı ile veritabanı işlemleri yapılır. Repository ve Unit of Work desenleri kullanılmıştır.

## Veri Modelleri

### Kullanıcı (User)

Müşteri bilgilerini içeren modeldir:

- **Id** (int): Kullanıcının benzersiz kimliği.
- **FirstName** (string): Kullanıcının adı.
- **LastName** (string): Kullanıcının soyadı.
- **Email** (string): Benzersiz e-posta adresi.
- **PhoneNumber** (string): Kullanıcının telefon numarası.
- **Password** (string): Şifre, Data Protection ile şifrelenmiştir.
- **Role** (enum): Kullanıcının rolü (Admin, Customer).

### Ürün (Product)

Satışta olan ürünleri içeren modeldir:

- **Id** (int): Ürünün benzersiz kimliği.
- **ProductName** (string): Ürünün adı.
- **Price** (decimal): Ürünün fiyatı.
- **StockQuantity** (int): Ürünün stok miktarı.

### Sipariş (Order)

Müşterilerin verdikleri siparişleri içeren modeldir:

- **Id** (int): Siparişin benzersiz kimliği.
- **OrderDate** (DateTime): Siparişin verildiği tarih.
- **TotalAmount** (decimal): Toplam sipariş tutarı.
- **CustomerId** (int): Siparişi veren müşteri.

### Sipariş Ürün (OrderProduct)

Siparişler ve ürünler arasında çoka çok ilişkiyi yöneten modeldir:

- **OrderId** (int): Sipariş kimliği.
- **ProductId** (int): Ürün kimliği.
- **Quantity** (int): Siparişte yer alan ürün miktarı.

## Kimlik Doğrulama ve Yetkilendirme

- **JWT Tabanlı Yetkilendirme**: Kullanıcılar, giriş yaptıktan sonra JWT kullanarak kimlik doğrulama gerçekleştirilir. "Admin" ve "Customer" rollerine göre yetkilendirme kontrolleri yapılmaktadır.
- **Data Protection**: Kullanıcı şifreleri Data Protection mekanizması ile güvenli bir şekilde saklanmaktadır.

## Middleware'ler

- **Logging Middleware**: Her gelen istek loglanır. İsteğin URL'si, istek zamanı ve isteği yapan kullanıcının kimliği kaydedilir.
- **Maintenance Middleware**: Uygulama bakım moduna alındığında, API'ye erişim engellenir. Bu durum veritabanında yönetilebilir.

## Action Filter

- **Zaman Kısıtlamalı Erişim**: Belirli API'lere, belirlenen zaman dilimlerinde erişim izni veren bir action filter kullanılmıştır.

## Model Doğrulama

- **Email Doğrulama**: Kullanıcı e-posta adresi formatı doğrulanmaktadır.
- **Zorunlu Alanlar**: Kullanıcı ve ürünler için gerekli olan alanların doğrulaması yapılmaktadır.
- **Stok Miktarı**: Ürünlerin stok miktarına ilişkin iş kuralları kontrol edilir.

## Bağımlılık Enjeksiyonu (Dependency Injection)

- **Servislerin Yönetimi**: Projede servisler ve repository'ler bağımlılık enjeksiyonu aracılığıyla yönetilmektedir.

## Hata Yönetimi

- **Global Exception Handling**: Tüm uygulama hataları global olarak yakalanır ve kullanıcıya uygun yanıtlar döndürülür.

## Proje Kurulumu ve Çalıştırma

### Gereksinimler

- .NET 6 SDK
- SQL Server


