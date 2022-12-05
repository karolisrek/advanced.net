using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.BLL.Entities;

namespace Catalog.BLL.Interfaces.Handlers
{
    public interface IQueueMessagePublisher
    {
        void SendMessage<T>(T message);
    }
}
