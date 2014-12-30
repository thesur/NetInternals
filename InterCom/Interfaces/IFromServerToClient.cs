using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InterCom.Interfaces
{
     [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IFromServerToClient
    {
          [OperationContract]
         Response Hook(Hook hook);
    }
}
