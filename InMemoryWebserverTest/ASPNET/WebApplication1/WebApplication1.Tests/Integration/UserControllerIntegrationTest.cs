using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Unity;
using WebApplication1.Business;
using WebApplication1.Models;

namespace WebApplication1.Tests.Integration
{
   [TestClass]
   public class UserControllerIntegrationTest
   {
      private readonly HttpClient _httpClient = new HttpClient();
      private string _url;
      private IUnityContainer _unityContainer;
      private Mock<IUserManager> _userManagerMock;
      private IDisposable _webServer;

      [TestInitialize]
      public void Initialize()
      {
         _unityContainer = DependencyContainer.GetContainer();
         _userManagerMock = new Mock<IUserManager>();
         _unityContainer.RegisterInstance(_userManagerMock.Object);

         int port = TcpUtilities.GetFreeTcpPort();
         _url = $"http://localhost:{port}/";
         _webServer = WebApp.Start(_url, appBuilder =>
         {
            Startup.Configure(appBuilder, _unityContainer);
         });
      }

      [TestCleanup]
      public void Cleanup()
      {
         _userManagerMock.VerifyAll();
         _webServer.Dispose();
      }

      [TestMethod]
      public async Task UserController_GetUser_HappyFlow()
      {
         // arrange
         var user = new User
         {
            Id = 1,
            FirstName = "Piet",
            LastName = "Janssen"
         };

         _userManagerMock
            .Setup(m => m.GetUser(user.Id))
            .Returns(user);

         // act
         string url = $"{_url}users/{user.Id}";
         using (var respone = await _httpClient.GetAsync(url))
         {
            //assert
            Assert.AreEqual(HttpStatusCode.OK, respone.StatusCode);

            string content = await respone.Content.ReadAsStringAsync();
            var resultUser = JsonConvert.DeserializeObject<User>(content);
            Assert.AreEqual(user.Id, resultUser.Id);
            Assert.AreEqual(user.FirstName, resultUser.FirstName);
            Assert.AreEqual(user.LastName, resultUser.LastName);
         }
      }

      [TestMethod]
      public async Task UserController_GetUser_UserNotFound_ShouldReturn404()
      {
         // arrange
         int id = 2;

         // act
         string url = $"{_url}users/{id}";
         using (var respone = await _httpClient.GetAsync(url))
         {
            //assert
            Assert.AreEqual(HttpStatusCode.NotFound, respone.StatusCode);
            _userManagerMock.Verify(m => m.GetUser(id), Times.Once);
         }
      }

      [TestMethod]
      public async Task UserController_GetUsers_HappyFlow()
      {
         // arrange
         var users = new[]
         {
            new User
            {
               Id = 1,
               FirstName = "Duco",
               LastName = "Winterwerp"
            },
            new User
            {
               Id = 2,
               FirstName = "Sinter",
               LastName = "Klaas"
            }
         };

         _userManagerMock
            .Setup(m => m.GetUsers())
            .Returns(users);

         // act
         string url = $"{_url}users";
         using (var respone = await _httpClient.GetAsync(url))
         {
            //assert
            Assert.AreEqual(HttpStatusCode.OK, respone.StatusCode);

            string content = await respone.Content.ReadAsStringAsync();
            var resultUsers = JsonConvert.DeserializeObject<User[]>(content);
            Assert.AreEqual(2, resultUsers.Length);
         }
      }
   }
}
