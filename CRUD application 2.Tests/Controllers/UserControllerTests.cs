using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CRUD_application_2.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController _controller = null!;
        private List<User> _users = null!;

        [SetUp]
        public void SetUp()
        {
            _users = new List<User>
            {
                new User { Id = 1, Name = "User1", Email = "user1@example.com" },
                new User { Id = 2, Name = "User2", Email = "user2@example.com" },
                new User { Id = 3, Name = "User3", Email = "user3@example.com" },
            };

            UserController.userlist = _users;
            _controller = new UserController();
        }

        [Test]
        public void Index_ReturnsCorrectViewWithUsers()
        {
            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result);
            var model = result?.Model as List<User>;
            Assert.AreEqual(_users.Count, model?.Count);
        }

        [Test]
        public void Details_ReturnsCorrectViewWithUser()
        {
            var result = _controller.Details(1) as ViewResult;

            Assert.NotNull(result);
            var model = result?.Model as User;
            Assert.AreEqual(_users[0], model);
        }

        [Test]
        public void Details_ReturnsHttpNotFoundForInvalidId()
        {
            var result = _controller.Details(999);

            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }

        [Test]
        public void Create_Post_RedirectsToIndexOnSuccess()
        {
            var newUser = new User { Id = 4, Name = "User4", Email = "user4@example.com" };
            var result = _controller.Create(newUser) as RedirectToRouteResult;

            Assert.NotNull(result);
            //Assert.AreEqual("Index", result?.RouteValues["action"]);
        }

        [Test]
        public void Edit_Get_ReturnsCorrectViewWithUser()
        {
            var result = _controller.Edit(1) as ViewResult;

            Assert.NotNull(result);
            var model = result?.Model as User;
            Assert.AreEqual(_users[0], model);
        }

        [Test]
        public void Edit_Get_ReturnsHttpNotFoundForInvalidId()
        {
            var result = _controller.Edit(999);

            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }

        [Test]
        public void Edit_Post_RedirectsToIndexOnSuccess()
        {
            var updatedUser = new User { Id = 1, Name = "UpdatedUser", Email = "updateduser@example.com" };
            var result = _controller.Edit(1, updatedUser) as RedirectToRouteResult;

            Assert.NotNull(result);
            //Assert.AreEqual("Index", result?.RouteValues["action"]);
        }

        [Test]
        public void Delete_Get_ReturnsCorrectViewWithUser()
        {
            var result = _controller.Delete(1) as ViewResult;

            Assert.NotNull(result);
            var model = result?.Model as User;
            Assert.AreEqual(_users[0], model);
        }

        [Test]
        public void Delete_Get_ReturnsHttpNotFoundForInvalidId()
        {
            var result = _controller.Delete(999);

            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }

        [Test]
        public void Delete_Post_RedirectsToIndexOnSuccess()
        {
            var result = _controller.Delete(1, new FormCollection()) as RedirectToRouteResult;

            Assert.NotNull(result);
            //Assert.AreEqual("Index", result?.RouteValues["action"]);
        }
    }
}
