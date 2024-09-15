using API.Controllers;
using ChatApp.Business.Domains.User.Models;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Data.Entities;
using ChatApp.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.UnitTests
{
    public class AuthControllerUnitTests
    {
        public class FakeAuthService : IAuthService
        {
            public Task<User> LogIn(RegisterDTO registerData)
            {
                throw new NotImplementedException();
            }

            public Task<User> RegisterUser(RegisterDTO registerData)
            {
                return Task.FromResult<User>(null);
            }
        }

        public class FakeChatRepository : IChatRepository
        {
            public Task Add(Chat entity)
            {
                throw new NotImplementedException();
            }

            public Task AddConnectionIdToAllUserChats(User user, string connectionId)
            {
                throw new NotImplementedException();
            }

            public Task AddConnectionIdToChat(Chat chat, string ConnectionId)
            {
                throw new NotImplementedException();
            }

            public Task AddUserIdToChat(Chat chat, Guid userId)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Chat>> GetAll()
            {
                throw new NotImplementedException();
            }

            public Task<Chat> GetById(Guid id)
            {
                throw new NotImplementedException();
            }

            public Task<Chat> GetByName(string name)
            {
                throw new NotImplementedException();
            }

            public Task<List<Chat>> GetUserChats(Guid id)
            {
                throw new NotImplementedException();
            }

            public Task Remove(Chat entity)
            {
                throw new NotImplementedException();
            }

            public Task RemoveConnectionIdInAllChats(string connectionId)
            {
                throw new NotImplementedException();
            }
        }
        //MOQ перезаписать
        // msubstitute
        [Fact]

        public async Task Register_WhenDTOIsValidAndNotEmpty_ReturnOk()
        {
            //Arrange
            var userData = new RegisterDTO("Ilya", "228");
            var controller = new AuthController(new FakeAuthService(), new FakeChatRepository());

            //Act

            var result = await controller.Register(userData);

            //Assert

            Assert.NotNull(result);
            var statusCode = ((ObjectResult)result).StatusCode;

            Assert.Equal(statusCode, (int)HttpStatusCode.OK);
        }


        [Fact]

        public async Task Register_WhenDTOIsEmpty_ReturnBadRequest()
        {
            //Arrange
            var userData = new RegisterDTO("", "");
            var controller = new AuthController(new FakeAuthService(), new FakeChatRepository());

            //Act

            var result = await controller.Register(userData);

            //Assert

            Assert.NotNull(result);
            var statusCode = ((ObjectResult)result).StatusCode;

            Assert.Equal(statusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
