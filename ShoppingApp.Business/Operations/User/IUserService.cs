using ShoppingApp.Business.Operations.User.Dtos;
using ShoppingApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.Operations.User
{
    // Kullanıcı işlemlerini yöneten servis arayüzü
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);
        Task<List<UserInfoDto>> GetUsers();


        // Kullanıcıyı giriş yapmasını sağlar ve kullanıcı bilgilerini döner
        ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user);

    }
}
