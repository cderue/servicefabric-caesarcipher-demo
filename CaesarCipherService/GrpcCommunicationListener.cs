using Grpc.Core;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaesarCipherService
{
    public class GrpcCommunicationListener : ICommunicationListener
    {
        Server _grpcServer;
        private readonly string _replicaId;
        public GrpcCommunicationListener(string replicaId)
        {
            _replicaId = replicaId;
        }
        public async void Abort()
        {
            if (_grpcServer != null)
                await _grpcServer.KillAsync();
        }
        public async Task CloseAsync(CancellationToken cancellationToken)
        {
            if (_grpcServer != null)
                await _grpcServer.ShutdownAsync();
        }
        public Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            var endpoint = FabricRuntime.GetActivationContext().
                GetEndpoint("ServiceEndpoint");
            _grpcServer = new Server
            {
                Services = { CaesarCipher.BindService
                    (new CaesarCipherServiceImpl(_replicaId)) },
                Ports = { new ServerPort(endpoint.IpAddressOrFqdn, endpoint.Port,
                    ServerCredentials.Insecure) }
            };
            _grpcServer.Start();
            return Task.FromResult<string>(endpoint.IpAddressOrFqdn + ":"
                + endpoint.Port.ToString());
        }
    }
}
