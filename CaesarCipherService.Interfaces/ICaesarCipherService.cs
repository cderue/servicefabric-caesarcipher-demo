using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipherService.Interfaces
{
    public interface ICaesarCipherService : IService
    {
        Task<string> Encrypt(string str, int shift);

        Task<string> Decrypt(string str, int shift);
    }
}
