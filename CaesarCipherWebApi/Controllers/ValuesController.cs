using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaesarCipherService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace CaesarCipherWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CipherController : ControllerBase
    {
        [HttpGet]
        public string Encrypt(string str, int shift)
        {
            var caesarCipherClient = ServiceProxy.Create<ICaesarCipherService>(
                new Uri("fabric:/CaesarCipherApplication/CaesarCipherService"));
            return caesarCipherClient.Encrypt(str, shift).Result;
        }
        [HttpGet]
        public string Decrypt(string str, int shift)
        {
            var caesarCipherClient = ServiceProxy.Create<ICaesarCipherService>(
                new Uri("fabric:/CaesarCipherApplication/CaesarCipherService"));
            return caesarCipherClient.Decrypt(str, shift).Result;
        }
    }
}
