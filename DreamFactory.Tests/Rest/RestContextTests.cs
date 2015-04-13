﻿namespace DreamFactory.Tests.Rest
{
    using System.Collections.Generic;
    using System.Linq;
    using DreamFactory.Http;
    using DreamFactory.Model;
    using DreamFactory.Rest;
    using DreamFactory.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Shouldly;

    [TestClass]
    public class RestContextTests
    {
        private const string BaseAddress = "http://localhost";

        [TestMethod]
        public void ShouldCreateHttpFacade()
        {
            // Arrange

            // Act
            RestContext context = new RestContext(BaseAddress);
            
            // Assert
            context.HttpFacade.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldCreateContentSerializer()
        {
            // Arrange

            // Act
            RestContext context = new RestContext(BaseAddress);

            // Assert
            context.ContentSerializer.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldCreateBaseHeaders()
        {
            // Arrange

            // Act
            RestContext context = new RestContext(BaseAddress);

            // Assert
            context.BaseHeaders.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldUseCustomHttpFacade()
        {
            // Arrange
            IHttpFacade facade = Mock.Of<IHttpFacade>();

            // Act
            RestContext context = new RestContext(BaseAddress, facade, new JsonContentSerializer());

            // Assert
            context.HttpFacade.ShouldBeSameAs(facade);
        }

        [TestMethod]
        public void ShouldUseCustomContentSerializer()
        {
            // Arrange
            IContentSerializer serializer = Mock.Of<IContentSerializer>();

            // Act
            RestContext context = new RestContext(BaseAddress, Mock.Of<IHttpFacade>(), serializer);

            // Assert
            context.ContentSerializer.ShouldBeSameAs(serializer);
        }

        [TestMethod]
        public void ShouldGetServicesAsync()
        {
            // Arrange
            IRestContext context = CreateRestContext();

            // Act
            List<Service> services = context.GetServicesAsync().Result.ToList();

            // Assert
            services.ShouldNotBeEmpty();
            services.ShouldContain(x => x.api_name == "files");
            services.ShouldContain(x => x.api_name == "email");
        }

        [TestMethod]
        public void ShouldGetResourcesAsync()
        {
            // Arrange
            IRestContext context = CreateRestContext();

            // Act
            List<Resource> resources = context.GetResourcesAsync("user").Result.ToList();

            // Assert
            resources.ShouldNotBeEmpty();
            resources.ShouldContain(x => x.name == "password");
            resources.ShouldContain(x => x.name == "profile");
            resources.ShouldContain(x => x.name == "session");
        }

        [TestMethod]
        public void ShouldSetApplicationName()
        {
            // Arrange
            IRestContext context = CreateRestContext();

            // Act
            context.SetApplicationName("foo");

            // Assert
            Dictionary<string, object> headers = context.BaseHeaders.Build();
            headers[HttpHeaders.DreamFactoryApplicationHeader].ShouldBe("foo");
        }

        private static IRestContext CreateRestContext()
        {
            IHttpFacade httpFacade = new TestDataHttpFacade();
            IRestContext context = new RestContext(BaseAddress, httpFacade, new JsonContentSerializer());
            return context;
        }
    }
}