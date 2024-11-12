using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.DataProtection
{
    // IDataProtection arayüzünü uygulayan sınıf
    public class DataProtection : IDataProtection
    {
        // Veri koruma sağlayıcısı için bir koruyucu (protector) nesnesi
        private readonly IDataProtector _protector;

        // Constructor: IDataProtectionProvider kullanarak bir veri koruma sağlayıcısı oluşturur
        public DataProtection(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("ShoppingApp-security-v1");
        }

        // Verilen string veriyi şifreler
        public string Protect(string text)
        {
            return _protector.Protect(text);
        }

        // Şifrelenmiş veriyi orijinal haline geri döndürür
        public string UnProtect(string protectedText)
        {
            return _protector.Unprotect(protectedText);
        }
    }
}
