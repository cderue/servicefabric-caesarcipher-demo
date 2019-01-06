using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CaesarCipherService.Interfaces;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace CaesarCipherService
{
    /// <summary>
    /// Une instance de cette classe est créée pour chaque instance de service par le runtime Service Fabric.
    /// </summary>
    internal sealed class CaesarCipherService : StatelessService, ICaesarCipherService
    {
        public CaesarCipherService(StatelessServiceContext context)
            : base(context)
        {
        }

        public char EncryptCharacter(char ch, int shift)
        {
            if (!char.IsLetter(ch)) return ch;

            // (A, a) => 0; (B, b) => 1; ... 
            char offset = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((ch + shift - offset) % 26 + offset);
        }

        public Task<string> Decrypt(string str, int shift)
        {
            return Encrypt(str, 26 - shift);
        }

        public Task<string> Encrypt(string str, int shift)
        {
            return Task.FromResult(new string(str.Select(ch => EncryptCharacter(ch, shift)).ToArray()));
        }

        /// <summary>
        /// Substitution facultative pour créer des écouteurs (par exemple, TCP, HTTP) pour ce réplica de service afin de gérer les requêtes des clients ou des utilisateurs.
        /// </summary>
        /// <returns>Collection d'écouteurs.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return this.CreateServiceRemotingInstanceListeners();
        }
    }
}
