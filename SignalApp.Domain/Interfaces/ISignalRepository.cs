using SignalApp.Domain.Enums;
using SignalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Domain.Interfaces
{
    public interface ISignalRepository
    {
        Task<Signal> AddAsync(Signal signal);
        Task<Signal?> GetByIdAsync(int signalId);
        Task<IEnumerable<Signal?>> GetAllAsync();
        Task<IEnumerable<Signal>> GetBySignalTypeAsync(SignalTypeEnum signalTypeEnum);
    }
}
