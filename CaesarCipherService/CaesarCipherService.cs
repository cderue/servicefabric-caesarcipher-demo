using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CaesarCipherService.Interfaces;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Communication.Wcf;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace CaesarCipherService
{
    /// <summary>
    /// Une instance de cette classe est créée pour chaque instance de service par le runtime Service Fabric.
    /// </summary>
    internal sealed class CaesarCipherService : StatelessService
    {
        public CaesarCipherService(StatelessServiceContext context)
            : base(context)
        {
        }



        /// <summary>
        /// Substitution facultative pour créer des écouteurs (par exemple, TCP, HTTP) pour ce réplica de service afin de gérer les requêtes des clients ou des utilisateurs.
        /// </summary>
        /// <returns>Collection d'écouteurs.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[]
            {
                new ServiceInstanceListener(initParams =>
                    new GrpcCommunicationListener(this.Context.ReplicaOrInstanceId.ToString()))
            };
        }
    }
}
