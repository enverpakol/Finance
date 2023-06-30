# GFinance Uygulaması Kullanım Kılavuzu

GFinance uygulamasını kullanmak için aşağıdaki adımları izleyebilirsiniz:

## 1. Admin Hesabı ve Token Alma
- Postman API koleksiyonunu kullanarak, admin hesabı bilgileriyle login endpointine istek gönderin ve bir token alın.

## 2. Firma Bilgileri ve Kullanıcı Hesapları
- İlgili endpointleri kullanarak firma bilgilerini oluşturun ve bu firmalara ait kullanıcı hesaplarını tanımlayın.

## 3. Firma Kullanıcısı
- İlgili endpointleri kullanarak firma kullanıcısı rolündeki hesaba giriş yapın.

## 4. Stok ve Cari Bilgileri
- Firma kullanıcıları için stok bilgilerini ve cari bilgilerini ilgili endpointler aracılığıyla girin.

## 5. Fatura Oluşturma
- Fatura oluşturmak için ilgili endpointe ihtiyaç duyulan bilgilerle istekte bulunun.

## 6. Stok Yönetimi
- Stok girişlerini yaparken fiyat ve miktar bilgilerini girin. Bu bilgilerle birlikte stok oluşturulur.
- Stok girişlerini ve çıkışlarını ilgili endpointler aracılığıyla gerçekleştirin.
- Fatura oluşturulduğunda, ilgili stoktan çıkış yapılır.
- Stok hareketleri stok balance bilgisi olarak tutulur ve stokTransaction tablosunda kaydedilir.

## 7. Fatura Yönetimi
- Fatura işlemlerinde iki ödeme tipi bulunur: nakit ve bakiye.
- Nakit ödeme alındığında, carinin BalanceBakiyesi ve paymentTransaction kayıtları olmadan fatura oluşturulur.
- Ancak bakiye tipi seçildiğinde carinin balanceBakiyesi düşer ve bu bilgi paymentTransaction tablosunda tutulur.
- Cari adına ödeme alarak borç bakiyesini kapatma işlemi yapabilirsiniz.

Yukarıdaki adımları takip ederek GFinance uygulamasını kullanabilir ve finansal işlemlerinizi yönetebilirsiniz. Daha fazla ayrıntı ve endpoint belgeleri için API dokümantasyonuna başvurmanız önerilir.
