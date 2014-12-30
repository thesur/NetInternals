using InterCom.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InterCom
{
    public class Server : WCF
    {
        ServiceHost serverHost;
        ChannelFactory<IFromServerToClient> factory;
        IFromServerToClient channel;

        public Server(IFromClientToServer clientToServer)
        {
            serverHost = new ServiceHost(clientToServer);
            serverHost.AddServiceEndpoint((typeof(IFromClientToServer)), new NetNamedPipeBinding(), "net.pipe://localhost/Server");
            factory = new ChannelFactory<IFromServerToClient>(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/Client"));
            channel = factory.CreateChannel();
        }

        public void Listen()
        {
            serverHost.Open();
        }

        public Response Hook(Hook hook)
        {

            IFromServerToClient serverToClientChannel = factory.CreateChannel();
            try
            {
                return channel.Hook(hook);
            }
            catch { }

            return new Response(ResponseType.Error);
        }
    }
}
