﻿using InterCom.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InterCom
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public class Client : WCF
    {
        ServiceHost clientHost;
        ChannelFactory<IFromClientToServer> factory;
        IFromClientToServer channel;

        public Client(IFromServerToClient serverToClient)
        {

            clientHost = new ServiceHost(serverToClient);
            clientHost.AddServiceEndpoint((typeof(IFromServerToClient)), new NetNamedPipeBinding( NetNamedPipeSecurityMode.None), base.PipeClientName);
            factory = new ChannelFactory<IFromClientToServer>(new NetNamedPipeBinding(NetNamedPipeSecurityMode.None), new EndpointAddress(base.PipeServerName));
            channel = factory.CreateChannel();

        }

        public void Connect()
        {
            clientHost.Open();
        }

        public void Close()
        {
            clientHost.Close();
        }

        public void Log(string str)
        {

            try
            {
               
                channel.Log(str);
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); }
        }

        public void HookedCall(ref HookedCall hookedCall)
        {

            try
            {
                channel.HookedCall(ref hookedCall);
                
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); }
        }
    }
}
