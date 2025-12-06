using SignalApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SignalApp.Infrastructure.Services
{
    public class PathProviderService : IPathProviderService
    {
        public string GetBaseDirectory()
        {
            string currentDir = AppContext.BaseDirectory;
            while(!string.IsNullOrEmpty(currentDir))
            {
                var slnx = Directory.GetFiles(currentDir, "*slnx");
                if (slnx.Length > 0)
                    return currentDir;
                currentDir = Directory.GetParent(currentDir)?.FullName;
            }
            return AppContext.BaseDirectory;
        }

        public string GetSignalsDirectory()
        {
            string baseDir = GetBaseDirectory();
            string signalsDir = Path.Combine(baseDir, "Signals");
            if(!Directory.Exists(signalsDir))
                Directory.CreateDirectory(signalsDir);
            return signalsDir;
        }
    }
}
