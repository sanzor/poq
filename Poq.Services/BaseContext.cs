using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services
{
    internal class BaseContext
    {
        public Error Error { get; set; }
    }
    internal static class ContextExtensions
    {
        public static BaseContext SetError(this BaseContext context, Error error)
        {
            context.Error = error;
            return context;
        }
    }
}
