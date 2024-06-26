﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitolas.Persistence
{
    public interface IStore
    {
        
        Task<IEnumerable<String>> GetFilesAsync();

        
        Task<DateTime> GetModifiedTimeAsync(String name);
    }
}
