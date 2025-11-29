using Microsoft.EntityFrameworkCore;
using SignalApp.Domain.Enums;
using SignalApp.Domain.Interfaces;
using SignalApp.Domain.Models;
using SignalApp.Infrastructure.Database;

namespace SignalApp.Infrastructure.Repositories
{
    public class SignalRepository : ISignalRepository
    {
        private readonly SignalDbContext _db;

        public SignalRepository(SignalDbContext db)
        {
            _db = db;
        }

        public async Task<Signal?> AddAsync(Signal signal)
        {
            await _db.Signals.AddAsync(signal);
            await _db.SaveChangesAsync();
            return signal;
        }

        public async Task<Signal?> GetByIdAsync(int signalId)
        {
            return await _db.Signals
                .Include(s => s.Points)
                .FirstOrDefaultAsync(s => s.SignalId == signalId);
        }

        public async Task<IEnumerable<Signal>> GetAllAsync()
        {
            return await _db.Signals
                .Include(s => s.Points)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Signal>> GetBySignalTypeAsync(SignalTypeEnum signalType)
        {
            return await _db.Signals
            .Include(s => s.Points)
            .Where(s => s.SignalType == signalType)
            .ToListAsync();
        }
    }
}
