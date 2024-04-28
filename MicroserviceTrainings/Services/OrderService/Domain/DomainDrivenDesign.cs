using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.Services.OrderService.Domain
{
    internal class DomainDrivenDesign
    {
        /*
         * karmaşık yazılım projelerini başarılı bir şekilde yönetmek için kullanılan bir yaklaşım ve tasarım metodolojisidir. Temel amacı, işletmenin karmaşık iş kurallarını ve mantığını yazılım modeline yansıtmak ve bu modeli anlaşılır ve sürdürülebilir bir şekilde tasarlamak ve uygulamaktır.

            DDD, yazılımın temelini işletmenin alanı (domain) oluşturur. Bu domain, işletmenin faaliyet gösterdiği alanı temsil eder ve o alandaki kavramları, iş kurallarını ve süreçleri içerir. DDD, bu domain odaklı tasarımı ve geliştirmeyi teşvik eder.

            İşte DDD'nin temel kavramlarından bazıları:

            Entity (Varlık): Gerçek varlıkları temsil eden nesnelerdir. Örneğin, bir müşteri veya bir sipariş gibi.
            
            Value Object (Değer Nesnesi): Sadece değerlerden oluşan ve genellikle birlikte gruplandırılan nesnelerdir. Örneğin, bir tarih aralığı veya bir para miktarı gibi.
            
            Aggregate Root (Kök Kümeler): Birbirine bağlı varlık ve değer nesnelerinin bir araya geldiği ve bir bütün oluşturduğu nesnelerdir. Örneğin, bir sipariş ve bu siparişe ait ürünler gibi.
            
            Repository (Depo): Veritabanı işlemlerini gerçekleştiren nesnelerdir. Domain nesneleriyle veritabanı arasında bir arayüz sağlarlar.
            
            Unit of Work (İş Birimi): Bir veya daha fazla işlemi bir araya getiren, bu işlemleri bir bütün olarak işleyen bir desendir. Örneğin, bir işlem sırasında yapılan tüm veritabanı işlemleri.
            
            Enumeration (Sıralama): Belirli bir kapsamda kullanılan ve sınırlı sayıda değere sahip olan sabit tipteki nesnelerdir. Örneğin, sipariş durumu gibi.
            
            Domain Events (Alan Olayları): Domain içerisinde meydana gelen önemli olayları temsil eden nesnelerdir. Örneğin, bir siparişin oluşturulması veya iptal edilmesi gibi.
         */
    }
}
