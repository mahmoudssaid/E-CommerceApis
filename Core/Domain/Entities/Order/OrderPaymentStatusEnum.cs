using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Order
{
    public enum OrderPaymentStatusEnum
    {
        Pending = 0,
        PaymentRecived = 1,
        PaymentFailed = 2,
    }
}
