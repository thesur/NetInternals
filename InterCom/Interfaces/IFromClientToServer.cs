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
    public interface IFromClientToServer
    {
        // [OperationContractAttribute] is necesary for arguments by reference


        [OperationContract(IsOneWay = true)]
         void Log(string text);


        [OperationContractAttribute]
        void HookedCall(ref HookedCall hookInfoCall);
    }
}
