using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SignalApp.Domain.Enums;
using SignalApp.Domain.Models;
using SignalApp.Infrastructure.Database;
using SignalApp.Infrastructure.Repositories;

namespace SignalApp.Tests.Integration
{
    [TestFixture]
    public class SignalRepositoryTests
    {
        private SqliteConnection _connection;
        private SignalDbContext _dbContext;
        private SignalRepository _repository;

        [SetUp]
        public void Setup()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<SignalDbContext>()
                .UseSqlite(_connection)
                .Options;

            _dbContext = new SignalDbContext(options);
            _dbContext.Database.EnsureCreated();

            _repository = new SignalRepository(_dbContext);
        }

        [TearDown]
        public void Teardown()
        {
            _connection.Close();
            _dbContext.Dispose();
        }

        private Signal CreateTestSignal()
        {
            var points = new List<SignalPoint>
        {
            new SignalPoint(0, 1),
            new SignalPoint(1, -1)
        };

            return new Signal(
                SignalTypeEnum.Sine,
                amplitude: 1,
                frequency: 2,
                pointsCount: 2,
                points: points
            );
        }

        [Test]
        public async Task AddSignalAsync_ShouldAddSignal_WhenValid()
        {
            var signal = CreateTestSignal();

            var result = await _repository.AddAsync(signal);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Points.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetSignalByIdAsync_ShouldReturnSignal_WhenExist()
        {
            var signal = await _repository.AddAsync(CreateTestSignal());

            var result = await _repository.GetByIdAsync(signal.SignalId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Points.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllSignalsAsync_ShouldReturnAllSignals_WhenExist()
        {
            await _repository.AddAsync(CreateTestSignal());
            await _repository.AddAsync(CreateTestSignal());

            var result = await _repository.GetAllAsync();

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetSignalsBySignalTypeAsync_ShouldReturnSignalsWithType_WhenExist()
        {
            await _repository.AddAsync(CreateTestSignal());
            await _repository.AddAsync(
                new Signal(
                    SignalTypeEnum.Square,
                    amplitude: 3,
                    frequency: 4,
                    pointsCount: 2,
                    points: new List<SignalPoint> { new(0, 5), new(1, -5) }
                )
            );

            var result = await _repository.GetBySignalTypeAsync(SignalTypeEnum.Sine);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().SignalType, Is.EqualTo(SignalTypeEnum.Sine));
        }
    }
}
