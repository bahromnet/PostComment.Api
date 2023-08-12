using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Api.Common.Interfaces
{
    public interface IHashPassword
    {
        Task<string> GetHashPasswordAsync(string text); 
    }
}
