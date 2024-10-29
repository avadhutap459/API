using Newtonsoft.Json;

namespace QuestaAdminApi.ServiceLayer
{
    public class ClsJsonConverter : IJsonConverter
    {
        ~ClsJsonConverter()
        {
            Dispose(false);
        }
        public string JsonSerializeObject<T>(T RequestObject)
        {
            try
            {
                return JsonConvert.SerializeObject(RequestObject);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public T DeserializeObject<T>(string JsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(JsonString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Dispose

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }
                else
                {

                }
                disposedValue = true;
            }
            else
            {

            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
