using CaesarCipherService.Interfaces;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace CaesarCipherWebApi
{
    public class WcfServiceFabricCommunicationClient<T> : ServicePartitionClient<WcfCommunicationClient<T>> where T : class
    {
        public WcfServiceFabricCommunicationClient(ICommunicationClientFactory<WcfCommunicationClient<T>> communicationClientFactory, Uri serviceUri, ServicePartitionKey partitionKey = null, TargetReplicaSelector targetReplicaSelector = TargetReplicaSelector.Default, string listenerName = null, OperationRetrySettings retrySettings = null)
            : base(communicationClientFactory, serviceUri, partitionKey, targetReplicaSelector, listenerName, retrySettings)
        {
        }

        public static WcfServiceFabricCommunicationClient<T> GetClient(Uri uri, Binding binding)
        {
            IServicePartitionResolver partitionResolver = ServicePartitionResolver.GetDefault();
            var wcfClientFactory = new WcfCommunicationClientFactory<T>
              (clientBinding: binding, servicePartitionResolver: partitionResolver);
   
            var client = new WcfServiceFabricCommunicationClient<T>(wcfClientFactory, uri, ServicePartitionKey.Singleton);

            return client;
        }
    }
}
