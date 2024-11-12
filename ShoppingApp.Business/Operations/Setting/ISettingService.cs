using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.Operations.Setting
{
    // Uygulama ayarlarını yöneten arayüz
    public interface ISettingService
    {
        // Bakım modunu açıp kapatmayı sağlayan metot
        Task ToggleMaintenence();

        // Bakım modunun açık veya kapalı olduğunu dönen metot
        bool GetMaintenanceState();


    }
}
