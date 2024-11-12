using Microsoft.EntityFrameworkCore;
using ShoppingApp.Business.DataProtection;
using ShoppingApp.Business.Operations.User.Dtos;
using ShoppingApp.Business.Types;
using ShoppingApp.Data.Entities;
using ShoppingApp.Data.Enums;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.Operations.User
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtection _protector;

        public UserManager(IUnitOfWork unitOfWork, IRepository<UserEntity> userRepository, IDataProtection protector)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _protector = protector;
        }

        // Yeni bir kullanıcı ekler
        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == user.Email.ToLower());

            if (hasMail.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Email adresi zaten mevcut."
                };

            }

            var userEntity = new UserEntity()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = _protector.Protect(user.Password),
                PhoneNumber = user.PhoneNumber,
                UserType =UserType.Customer

            };

            _userRepository.Add(userEntity);

            try
            {
                await _unitOfWork.SaveChangeAsync();

            }
            catch (Exception)
            {

                throw new Exception("Kullanıcı kaydı sırasında bir hata oluştu.");
            }

            return new ServiceMessage
            {

                IsSucceed = true,
            };


        }

        public async Task<List<UserInfoDto>> GetUsers()
        {
            var users = await _userRepository.GetAll().Select(x => new UserInfoDto
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserType = x.UserType,
            }).ToListAsync();

            return users;
        }

        // Kullanıcı girişini kontrol eder
        public ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user)
        {
            var userEntity = _userRepository.Get(x => x.Email.ToLower() == user.Email.ToLower());

            if (userEntity is null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı ve şifre hatalı."
                };
            }

            var unprotectedPassword = _protector.UnProtect(userEntity.Password);
            if (unprotectedPassword == user.Password)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Data = new UserInfoDto
                    {
                        Email = userEntity.Email,
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        UserType = userEntity.UserType,
                        PhoneNumber = userEntity.PhoneNumber,
                    }

                };
            }

            else {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı ve şifre hatalı."
                };

            }
        }
    }
}