using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace CaesarCipherService.Interfaces
{
    [ServiceContract]
    public interface ICaesarCipherService : IService
    {
        [OperationContract]
        Task<string> Encrypt(string str, int shift);

        [OperationContract]
        Task<string> Decrypt(string str, int shift);
    }
}
