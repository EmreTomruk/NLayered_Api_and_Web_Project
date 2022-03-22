using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Exceptions
{
    public class ClientSideException : Exception
    {
        public ClientSideException(string message) /*: base(message)*/ //Response'dan donen hata mesajini, Exception sinifina gondermek icin kullanilir... 
        {
        }
    }
}
