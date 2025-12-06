using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Domain.Interfaces
{
    public interface IPathProviderService
    {
        string GetBaseDirectory();
        string GetSignalsDirectory();
    }
}
