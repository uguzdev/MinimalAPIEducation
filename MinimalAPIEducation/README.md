# PAKETLER

- Microsoft.Extensions.Caching.StackExchangeRedis

# Redis

.NET projelerinde Redis kullanımı için iki farklı yaklaşımı özetler:  
**`IDistributedCache`** ve **`StackExchange.Redis`**.

## IDistributedCache

- **Microsoft.Extensions.Caching.Distributed** namespace’i altında gelir.
- **Abstraction (soyutlama)** sağlar.
- Redis dışında **SQL Server cache**, **NCache**, **Memory cache** gibi farklı implementasyonlarla da kullanılabilir.
- Basit CRUD tarzı methodlar sunar:
    - `GetStringAsync`
    - `SetStringAsync`
    - `RemoveAsync`

👉 Avantaj: Kolay kullanım, provider bağımsız.  
👉 Dezavantaj: Gelişmiş Redis özelliklerini (pub/sub, list, hash, stream vs.) desteklemez.

---

## StackExchange.Redis

- Redis için **low-level, güçlü bir kütüphane**.
- Microsoft’un önerdiği **resmi Redis client**.
- Çok daha fazla method ve veri yapısını destekler:
    - String, Hash, List, Set, Sorted Set
    - Pub/Sub (mesajlaşma)
    - Transactions, Pipelining
    - Lua scripting

👉 Avantaj: Redis’in tüm özelliklerine erişim.  
👉 Dezavantaj: Redis’e sıkı sıkıya bağlı (abstraction yok). Başka bir cache sağlayıcısına geçmek zor olur.

---

## 3. Ne Zaman Hangisi?

- **Sadece basit cache (key-value) lazımsa**  
  → `IDistributedCache` yeterli olur.

- **Redis’in gelişmiş özelliklerini (listeler, pub/sub, stream) kullanmak istiyorsan**  
  → `StackExchange.Redis` kullanmalısın.

# NOT

- İkisinin de implementasyonu kolaydır, ama şu anda biz `IDistributedCache` kullandık.
- Fark ettiyseniz yine handlerlarımızın içerisinde tekrar eden kodlarımız var bunlardan kurtulmak için yardımcı bir
  service yazabiliriz

