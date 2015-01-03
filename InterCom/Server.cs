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
            serverHost.AddServiceEndpoint((typeof(IFromClientToServer)), new NetNamedPipeBinding( NetNamedPipeSecurityMode.None), base.PipeServerName);
            factory = new ChannelFactory<IFromServerToClient>(new NetNamedPipeBinding(NetNamedPipeSecurityMode.None), new EndpointAddress(base.PipeClientName));
            channel = factory.CreateChannel();
        }

        public Response Listen()
        {
            if (serverHost.State == CommunicationState.Opened)
                return new Response(ResponseType.OK, "It was already Opened");
            else if (serverHost.State == CommunicationState.Created)
            {
                try
                {
                    serverHost.Open();
                    if (serverHost.State == CommunicationState.Opened)
                        return new Response(ResponseType.OK, "Opened successfully");
                }
                catch (Exception ex) { return new Response(ResponseType.Error, ex.Message); }
            }

            return new Response(ResponseType.Error, string.Concat("The current status is ", serverHost.State.ToString()));
        }

        public void Close()
        {
            serverHost.Close();
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
