using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.API.Model.Utility
{
    public interface IHttpWebClients
    {
        string PostRequest(string URI, string parameterValues, bool isAnonymous = false);
    }
}
