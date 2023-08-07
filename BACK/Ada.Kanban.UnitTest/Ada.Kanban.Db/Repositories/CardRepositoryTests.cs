using Ada.Kanban.Common.Exceptions;
using Ada.Kanban.Db.DbContexts;
using Ada.Kanban.Db.Entities;
using Ada.Kanban.Db.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Ada.Kanban.UnitTest
{
    public class CardRepositoryTests
    {
        [Fact]
        public async Task GetCardById_ShouldReturnCard()
        {
            //arrange
            var testCardId = Guid.NewGuid();
            var mockSet = new Mock<DbSet<Card>>();
            mockSet.Setup(m => m.FindAsync(testCardId)).ReturnsAsync(new Card { Id = testCardId });
            var mockContext = new Mock<AdaKanbanDbContext>();
            mockContext.Setup(m => m.Cards).Returns(mockSet.Object);
            var cardRepository = new CardRepository(mockContext.Object);

            //act
            var card = await cardRepository.GetCardByIdAsync(testCardId);

            //assert
            card.Should().NotBeNull();
            card.Id.Should().Be(testCardId);
        }

        [Fact]
        public async Task GetCardById_ShouldThrowException_WhenCardNotFound()
        {
            //arrange
            var testCardId = Guid.NewGuid();
            var mockSet = new Mock<DbSet<Card>>();
            mockSet.Setup(m => m.FindAsync(testCardId)).ReturnsAsync((Card?)null);
            var mockContext = new Mock<AdaKanbanDbContext>();
            mockContext.Setup(m => m.Cards).Returns(mockSet.Object);
            var cardRepository = new CardRepository(mockContext.Object);

            //act
            var act = async () => await cardRepository.GetCardByIdAsync(testCardId);

            //assert
            var exception = await act.Should().ThrowAsync<AdaKanbanException>();
            exception.Which.ExceptionType.Should().Be(AdaKanbanExceptionType.NotFound);
        }
    }
}