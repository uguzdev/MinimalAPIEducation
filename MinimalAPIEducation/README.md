# GEREKLİ PAKETLER

- OpenTelemetry.Extensions.Hosting
- OpenTelemetry.Instrumentation.AspNetCore
- OpenTelemetry.Instrumentation.EntityFrameworkCore
- OpenTelemetry.Exporter.Console
- OpenTelemetry.Exporter.OpenTelemetryProtocol

# Jaeger Docker Kurulumu

```shell
docker run --rm --name jaeger  -p 16686:16686 -p 4317:4317 -p 4318:4318 jaegertracing/jaeger:2.5.0
```

- http://localhost:16686 bu url istek attığımızda Jaeger UI gidebiliriz.
- Configuration ayarlarını Program.cs içerisine yaptık

# OpenTelemetry Nedir?

OpenTelemetry, bulut tabanlı yazılımlar için telemetri verisi (metrikler, izler ve loglar) toplamak amacıyla oluşturulmuş açık kaynaklı bir projedir. Farklı dillerde (ASP.NET,
Java,
Python,
Go, Node.js vb.) uygulamalarınızdan telemetri verisi toplamak için standart bir API, SDK ve araçlar sunar. Bu sayede, uygulamanızdan bağımsız olarak telemetri verisi üretebilir ve
istediğiniz bir arka uç (backend) sistemine gönderebilirsiniz. OpenTelemetry'nin temel amacı, uygulamalardaki izlenebilirliği (observability) kolaylaştırmaktır.

## Jaeger Neden Gerekli?

Uygulamalarınızdan topladığınız izleme (tracing) verilerini anlamlandırmak için Jaeger gibi bir araç gerekir. Jaeger, OpenTelemetry tarafından gönderilen izleme verilerini (trace)
alır, depolar ve bu verileri görsel bir şekilde sunar.

- Jaeger'ın başlıca faydaları şunlardır:
    - Dağıtık İzleme (Distributed Tracing): Microservice mimarilerinde bir isteğin birden fazla servisi nasıl dolaştığını gösteren izleri görselleştirir. Bu sayede, performansı
      düşüren veya hataya neden olan servisleri kolayca bulmanızı sağlar.
    - Hata Ayıklama (Troubleshooting): Hataların veya gecikmelerin hangi servis katmanında meydana geldiğini anlamak için izleri analiz etmenizi sağlar.
    - Performans Optimizasyonu: Bir işlemin hangi aşamasının en uzun sürdüğünü görerek sistem performansını optimize etmenize yardımcı olur.

Kısacası, OpenTelemetry veriyi toplar, Jaeger ise bu veriyi anlaşılır bir şekilde sunarak sisteminizin durumunu ve performansını izlemenizi sağlar.

# NOT

- Bizler trace verilerimizi hem Jaegera hemde consola yazdırdık

![img.png](img.png)