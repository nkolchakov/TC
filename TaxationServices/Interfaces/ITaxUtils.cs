using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationServices.Interfaces
{
    public interface ITaxUtils
    {
        void ValidateHandlers(IEnumerable<TaxHandler> handlers);
    }
}
