using ShoppingApp.Business.Operations.User.Dtos;
using ShoppingApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.Operations.User
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);

        ServiceMessage<UserInfoDto> AddUser(UserInfoDto user);

        ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user);
    }
}
