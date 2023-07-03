## Açıklama
- Bu projede 2 farklı rol bulunmaktadır. Admin rolü ile ilgili şirket bilgileri ve şirkete ait kullanıcı bilgileri oluşturulabilir. 
- Şirket kullanıcısı rolü ile şirketin stok ve cari bilgileri girilebilir. Ayrıca fatura oluşturulabilir ve fatura işlemleri gerçekleştirilebilir.
- Fatura işleminde ilgili ürünlerin stok miktarları azaltılır ve cari hesap bakiyesi güncellenir. Bu bilgiler stok ve cari tablolarında balance olarak tutulur.
Ayrıca bu işlem hareketleri stokTransaction ve paymentTransaction tablolarında tutulur.




## Kurulum

GFinance uygulamasını kullanabilmek için aşağıdaki kurulum adımlarını takip edin:

### 1. MySQL Sunucusu
- GFinance uygulaması, veritabanı için bir MySQL sunucusuna ihtiyaç duyar.
- Yerel bir MySQL sunucusu kullanıyorsanız, MySQL'in yüklü olduğundan ve çalıştığından emin olun.
- Eğer MySQL sunucusu kurulu değilse, bir MySQL sunucusu kurmanız gerekebilir.

### 2. Redis Sunucusu
- GFinance uygulaması, oturum yönetimi ve önbellekleme için bir Redis sunucusuna ihtiyaç duyar.
- Yerel bir Redis sunucusu kullanıyorsanız, Redis'in yüklü olduğundan ve çalıştığından emin olun.
- Eğer Redis sunucusu kurulu değilse, bir Redis sunucusu kurmanız gerekebilir.

### 3. Bağlantı Bilgileri
- GFinance uygulamasının MySQL ve Redis sunucusuna bağlanabilmesi için `appsettings.json` dosyasındaki bağlantı bilgilerini düzenlemeniz gerekmektedir.
- `appsettings.json` dosyasını açın ve MySQL ve Redis bağlantı bilgilerini güncelleyin.



# GFinance Uygulaması Kullanım Kılavuzu

GFinance uygulamasını kullanmak için aşağıdaki adımları izleyebilirsiniz:

## 1. Admin Hesabı ve Token Alma
- Initialize admin hesabı, Username: admin@test.com Password:123456
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
