using AutoMapper;
using Moq;
using NailStudio.Data.Models;
using NailStudio.Data.Repository.Interfaces;
using NailStudioApp.Web.ViewModel.Review;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Tests
{
    public class ReviewServiceTests
    {
        private NailStudioApp.Services.Data.ReviewService reviewService;

        private Mock<IRepository<Review, Guid>> reviewRepositotyMock;
        private Mock<IMapper> mapperMock;

        [SetUp]
        public void Setup()
        {
            reviewRepositotyMock = new Mock<IRepository<Review, Guid>>();
            mapperMock = new Mock<IMapper>();
            reviewService = new NailStudioApp.Services.Data.ReviewService(reviewRepositotyMock.Object, mapperMock.Object);

        }

        
        [Test]
        public void AddReviewAsync_ShouldMapViewModelToReview()
        {
            var model = new AddReviewViewModel
            {
                Content = "Excellent service!",
                Rating = 5,
                UserId = Guid.NewGuid()
            };

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Content = model.Content,
                Rating = model.Rating,
                UserId = model.UserId,
                IsDeleted = false
            };

            mapperMock.Setup(m => m.Map<Review>(model)).Returns(review);

            reviewService.AddReviewAsync(model);

            mapperMock.Verify(m => m.Map<Review>(model), Times.Once);
        }
        [Test]
        public async Task A9ddReviewAsync_ShouldAddReviewSuccessfully()
        {
            var model = new AddReviewViewModel
            {
                Content = "Great service!",
                Rating = 5,
                UserId = Guid.NewGuid()
            };

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Content = model.Content,
                Rating = model.Rating,
                UserId = model.UserId,
                Date = DateTime.Now
            };

            mapperMock.Setup(m => m.Map<Review>(model)).Returns(review);
            reviewRepositotyMock.Setup(r => r.AddAsync(It.IsAny<Review>())).Returns(Task.CompletedTask);
            reviewRepositotyMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await reviewService.AddReviewAsync(model);

            reviewRepositotyMock.Verify(r => r.AddAsync(It.IsAny<Review>()), Times.Once);
            reviewRepositotyMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        
        [Test]
        public async Task A8ddReviewAsync_ShouldSaveReviewToRepository()
        {
            var model = new AddReviewViewModel
            {
                Content = "Great service!",
                Rating = 5,
                UserId = Guid.NewGuid()
            };

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Content = model.Content,
                Rating = model.Rating,
                UserId = model.UserId,
                Date = DateTime.Now
            };

            mapperMock.Setup(m => m.Map<Review>(model)).Returns(review);
            reviewRepositotyMock.Setup(r => r.AddAsync(It.IsAny<Review>())).Returns(Task.CompletedTask);
            reviewRepositotyMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await reviewService.AddReviewAsync(model);

            reviewRepositotyMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void A7ddReviewAsync_ShouldThrowException_WhenRepositoryFails()
        {
            var model = new AddReviewViewModel
            {
                Content = "Great service!",
                Rating = 5,
                UserId = Guid.NewGuid()
            };

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Content = model.Content,
                Rating = model.Rating,
                UserId = model.UserId,
                Date = DateTime.Now
            };

            mapperMock.Setup(m => m.Map<Review>(model)).Returns(review);
            reviewRepositotyMock.Setup(r => r.AddAsync(It.IsAny<Review>())).Throws(new Exception("Repository Error"));

            Assert.ThrowsAsync<Exception>(async () => await reviewService.AddReviewAsync(model));
        }

       

        [Test]
        public async Task A4ddReviewAsync_ShouldCallAddOnce()
        {
            var model = new AddReviewViewModel
            {
                Content = "Great service!",
                Rating = 5,
                UserId = Guid.NewGuid()
            };

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Content = model.Content,
                Rating = model.Rating,
                UserId = model.UserId,
                Date = DateTime.Now
            };

            mapperMock.Setup(m => m.Map<Review>(model)).Returns(review);
            reviewRepositotyMock.Setup(r => r.AddAsync(It.IsAny<Review>())).Returns(Task.CompletedTask);

            await reviewService.AddReviewAsync(model);

            reviewRepositotyMock.Verify(r => r.AddAsync(It.IsAny<Review>()), Times.Once);
        }

        [Test]
        public async Task A3ddReviewAsync_ShouldCallSaveChangesOnce()
        {
            var model = new AddReviewViewModel
            {
                Content = "Great service!",
                Rating = 5,
                UserId = Guid.NewGuid()
            };

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Content = model.Content,
                Rating = model.Rating,
                UserId = model.UserId,
                Date = DateTime.Now
            };

            mapperMock.Setup(m => m.Map<Review>(model)).Returns(review);
            reviewRepositotyMock.Setup(r => r.AddAsync(It.IsAny<Review>())).Returns(Task.CompletedTask);
            reviewRepositotyMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await reviewService.AddReviewAsync(model);

            reviewRepositotyMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        

    }
}
