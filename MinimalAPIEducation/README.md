# PAKETLER

- Asp.Versioning.Mvc.ApiExplorer
- Asp.Versioning.Http

---

# Minimal API Versioning – Neden ve Nasıl?

API geliştirme sürecinde **versiyonlama**, uzun vadeli bakım, geriye dönük uyumluluk ve farklı istemci ihtiyaçlarını
yönetmek için kritik bir pratiktir.

---

## 1. Neden API Versiyonlamaya İhtiyaç Var?

1. **Geriye dönük uyumluluk (Backward Compatibility)**

    * Var olan istemciler, eski API sürümlerine bağlı olabilir.
    * Yeni özellik eklerken veya endpoint’leri değiştirirken eski sürümlerin çalışmasını bozmamak gerekir.

2. **Yeni özelliklerin sunulması**

    * Aynı endpoint üzerinden yeni özellikler sunmak istiyorsanız, farklı versiyon numaralarıyla eski ve yeni
      davranışları ayırabilirsiniz.

3. **Hata yönetimi ve risk azaltma**

    * Mevcut API değişiklikleri doğrudan tüm istemcileri etkilemez.
    * Eski versiyonlar stabil kalır, yeni versiyonlar geliştirme ve test süreçlerine açık olur.

---

## Versiyonlama Yöntemleri

Minimal API’de genellikle iki yöntem kullanılır:

### a) **URL Route ile Versiyonlama**

* Endpoint URL’ine versiyon numarası eklenir:

```
GET /api/v1/products
GET /api/v2/products
```

* Avantajları:

    * Görselle açık: Hangi versiyona çağrı yapıldığı belli
    * Caching ve load balancing sistemleri için kolay anlaşılır

* Dezavantajları:

    * URL değişiklikleri olabilir
    * Bazı istemciler için route güncelleme gerekir

---

### b) **Header ile Versiyonlama**

* API versiyonu header üzerinden gönderilir:

```
GET /api/products
x-api-version: 1.0
x-api-version: 2.0
```

* Avantajları:

    * URL temiz kalır, endpoint’ler değişmez
    * Versiyon kontrolü tamamen client tarafından yapılır

* Dezavantajları:

    * Görünürlük azalır (URL’de versiyon belli olmaz)
    * Swagger gibi araçlarla gösterimi ekstra konfigürasyon ister

---

## 🔹 3. V1, V2 Olmasının Önemi

* **v1** → İlk stabil sürüm, temel özellikler.
* **v2** → Yeni özellikler, iyileştirmeler veya davranış değişiklikleri.

> Örnek:
>
> * v1: `/api/v1/products` → `name` ve `price` dönüyor
> * v2: `/api/v2/products` → `name`, `price` + `category` dönüyor

* Bu sayede eski istemciler sorunsuz çalışırken, yeni istemciler gelişmiş veri alabilir.

---

## 🔹 4. Özet / Önemli Noktalar

* API versiyonlama, **bakım, test ve geliştirme süreçlerini güvenli hale getirir**
* Minimal API’de versiyon numarası **URL veya Header** üzerinden belirlenebilir
* Her yeni versiyon, eski sürümlerin çalışmasını bozmadan geliştirme yapılmasını sağlar
* Swagger ve dokümantasyon ile uyumlu kullanıldığında, tüm sürümler **açık ve takip edilebilir** olur


