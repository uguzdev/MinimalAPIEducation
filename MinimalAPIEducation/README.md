# Service Result Pattern

- ServiceResult, handler’ların hem veriyi hem de durum kodunu ve hata bilgisini tek bir obje içinde taşımasını sağlayan
  bir yapıdır.
- Başarılı işlemler için Data ve Status içerir.
- Hatalı işlemler için Fail (ProblemDetails) ve Status içerir.
- Bu sayede endpoint tarafında switch-case ile doğru HTTP status kodu ve response dönmek kolaylaşır.

Kısaca: handler içinde status + data + error yönetimi yapmamıza yarar.

# OZET

## Handler ve ServiceResult Kullanımı

Bu handler’lar artık `ServiceResult` veya `ServiceResult<T>` döndürerek:

- **Başarılı durumda:** `200 OK`, `201 Created`, `204 NoContent`
- **Hatalı durumda:** `400 BadRequest`, `404 NotFound` gibi status kodlarını taşıyor.

### Endpoints

- Tüm CRUD işlemleri endpoint tarafında `switch(result.Status)` ile HTTP response’a dönüştürüldü.
- Swagger için `Produces<T>()` kullanılarak başarılı ve hata response tipleri tanımlandı.

### Validation ve Hata Yönetimi

- `ValidationFilter<T>` pipeline ile request validation kontrol edildi.
- Category veya product yok gibi durumlar handler içinde kontrol edilip `ServiceResult.Error` döndürüldü.

### Avantajlarımız

- Endpoint içinde status kodu veya hata kontrolü yapmak gerekmez, handler bunu taşır.
- Swagger ve client tarafı için tek tip response yönetimi sağlanır.
- Kod daha temiz, okunabilir ve maintainable oldu.

# EKSIKLER

Yeni **versiyonda**, tüm endpointlerde tekrar eden `switch` kontrollerini kaldırıp, ServiceResult’ı uzantı metodları
üzerinden otomatik olarak doğru HTTP cevaba dönüştürebileceğiz; böylece endpoint kodu hem daha temiz hem de tek tip ve
okunabilir olacak.

# NOT

ServiceResult’ı isterseniz kendi yöntemlerinizle manuel olarak oluşturabilirsiniz, isterseniz de hazır NuGet paketleri
ile hızlı ve standart bir şekilde kullanabilirsiniz.
