namespace NLayer.Service.Exceptions
{
    public class ClientSideException : Exception
    {
        public ClientSideException(string message) /*: base(message)*/ //Response'dan donen hata mesajini, Exception sinifina gondermek icin kullanilir... 
        {
        }
    }
}
