using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceApp
{
    [ServiceContract]
    public interface IPaymentService
    {
        [OperationContract]
        bool Transact(
            string customerId, 
            double amount, 
            string currency, 
            string cardId,
            Dictionary<string, string> extraData);
    }
}
