using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitolas.Persistence
{
    public interface IKitolasDataAccess
    {
        Task SaveAsync(String path, SlideTable table);

        Task<SlideTable> LoadAsync(String path);

    }
}
