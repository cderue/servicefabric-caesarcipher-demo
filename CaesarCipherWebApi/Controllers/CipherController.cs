using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using CaesarCipherService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaesarCipherWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CipherController : ControllerBase
    {
        private readonly WcfServiceFabricCommunicationClient<ICaesarCipherService> _wcfClient;

        public CipherController()
        {
            _wcfClient = WcfServiceFabricCommunicationClient<ICaesarCipherService>.GetClient(
                new Uri("fabric:/CaesarCipherApplication/CaesarCipherService"), new BasicHttpBinding());
        }

        [HttpGet]
        public string Encrypt(string str, int shift)
        {
            return _wcfClient.InvokeWithRetryAsync(client => client.Channel.Encrypt(str, shift)).Result;
        }
        [HttpGet]
        public string Decrypt(string str, int shift)
        {
            return _wcfClient.InvokeWithRetryAsync(client => client.Channel.Decrypt(str, shift)).Result;
        }
    }
}
