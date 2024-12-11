using AutoMapper;
using Moq;
using NailStudio.Data.Models;
using NailStudio.Data.Repository.Interfaces;
using NailStudioApp.Services.Data.Interfaces;
using NailStudioApp.Web.ViewModel.Service;
using NailStudioApp.Services.Data;
using NailStudio.Data.Repository.Interfaces;
using AutoMapper;

using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data;

namespace NailStudioApp.Services.Tests
{
    using NailStudio.Data.Repository.Interfaces;
    using AutoMapper;

    using NUnit.Framework;
    using Microsoft.EntityFrameworkCore;
    using NailStudio.Data;
    [TestFixture]
    public class Tests
    {

        private NailStudioApp.Services.Data.ServiceService serviceService;

        private Mock<IRepository<Service, Guid>> serviceRepositoryMock;
        private Mock<IMapper> mapperMock;

        [SetUp]
        public void Setup()
        {
            serviceRepositoryMock = new Mock<IRepository<Service, Guid>>();
            mapperMock = new Mock<IMapper>();

            serviceService = new NailStudioApp.Services.Data.ServiceService(serviceRepositoryMock.Object, mapperMock.Object);

        }
        [Test]
        public async Task AddService_ShouldWorkCorrectly()
        {
            var addServiceModel = new AddServiceFormModel
            {
                Name = "Test Service",
                Price = 50.00m,
                DurationInMinutes = 60,
                Description = "Test Description",
                ImageUrl = "https://example.com/test.jpg"
            };

            var mappedService = new Service
            {
                Id = Guid.NewGuid(),
                Name = addServiceModel.Name,
                Price = addServiceModel.Price,
                DurationInMinutes = addServiceModel.DurationInMinutes,
                Description = addServiceModel.Description,
                ImageUrl = addServiceModel.ImageUrl
            };

            mapperMock.Setup(m => m.Map<Service>(addServiceModel)).Returns(mappedService);

            await serviceService.AddServiceAsync(addServiceModel);

            serviceRepositoryMock.Verify(r => r.AddAsync(mappedService), Times.Once);
            serviceRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }



        [Test]
        public void AddService_ShouldThrowError_WhenModelIsNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await serviceService.AddServiceAsync(null);
            });
        }
        [Test]
        public void Service_ShouldThrowError_WhenRepositoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new NailStudioApp.Services.Data.ServiceService(null, mapperMock.Object);
            });
        }

        [Test]
        public void Service_ShouldThrowError_WhenMapperIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new NailStudioApp.Services.Data.ServiceService(serviceRepositoryMock.Object, null);
            });
        }


        [Test]
        public void GetServiceDetailsById_ShouldThrowException_WhenIdIsInvalid()
        {
            var invalidServiceId = Guid.NewGuid();
            serviceRepositoryMock
                .Setup(r => r.All())
                .Returns(Enumerable.Empty<Service>().AsQueryable());

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await serviceService.GetServiceDetailsByIdAsync(invalidServiceId);
            });
        }

        [Test]
        public void Service_ShouldWork_WhenDependenciesAreValid()
        {
            Assert.Throws<ArgumentNullException>(() => new NailStudioApp.Services.Data.ServiceService(null, mapperMock.Object));
            Assert.Throws<ArgumentNullException>(() => new NailStudioApp.Services.Data.ServiceService(serviceRepositoryMock.Object, null));
        }




        [Test]
        public void Service_WhenDependenciesAreValid()
        {
            var service = new NailStudioApp.Services.Data.ServiceService(serviceRepositoryMock.Object, mapperMock.Object);

            Assert.IsNotNull(service);
        }

        [Test]
        public async Task AddService_ShouldCallMethodsInCorrectOrder()
        {
            var addServiceModel = new AddServiceFormModel { Name = "Service" };
            var mappedService = new Service { Name = "Service" };
            mapperMock.Setup(m => m.Map<Service>(addServiceModel)).Returns(mappedService);

            await serviceService.AddServiceAsync(addServiceModel);

            var inOrder = new MockSequence();
            serviceRepositoryMock.InSequence(inOrder).Setup(r => r.AddAsync(mappedService));
            serviceRepositoryMock.InSequence(inOrder).Setup(r => r.SaveChangesAsync());
        }



        [Test]
        public async Task AddService_ShouldWork_WhenOptionalFieldsAreMissing()
        {
            var addServiceModel = new AddServiceFormModel
            {
                Name = "Test Service",
                Price = 50.00m,
                DurationInMinutes = 60
            };

            var mappedService = new Service
            {
                Id = Guid.NewGuid(),
                Name = addServiceModel.Name,
                Price = addServiceModel.Price,
                DurationInMinutes = addServiceModel.DurationInMinutes
            };

            mapperMock.Setup(m => m.Map<Service>(addServiceModel)).Returns(mappedService);

            await serviceService.AddServiceAsync(addServiceModel);

            serviceRepositoryMock.Verify(r => r.AddAsync(mappedService), Times.Once);
            serviceRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }


        [Test]
        public async Task AddService_ShouldNotSave_WhenErrorOccurs()
        {
            var addServiceModel = new AddServiceFormModel { Name = "Service" };
            var mappedService = new Service { Name = "Service" };
            mapperMock.Setup(m => m.Map<Service>(addServiceModel)).Returns(mappedService);

            serviceRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Service>()))
                .Throws(new InvalidOperationException());

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await serviceService.AddServiceAsync(addServiceModel);
            });

            serviceRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

    }
}