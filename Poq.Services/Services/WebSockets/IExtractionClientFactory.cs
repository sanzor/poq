using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services
{
    public interface IExtractionClientFactory
    {
        Either<Error,IExtractionClient> CreateExtractionClient(WebSocket websocket);
    }
}
