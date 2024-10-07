using Application.Common.Interfaces;
using Domain.Entities;
using Newtonsoft.Json;

namespace Infrastructure.Repositories
{
    public class ExternalService<T> : IExternalService<T>
    where T : BaseEntity
    {
        public async Task<bool> Create(T model)
        {
            HttpClient client = new HttpClient();
            var path = "https://apicosmos-adcmabgphha3fyfk.brazilsouth-01.azurewebsites.net/api/Log/Create";
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(path, content);

            if (responseMessage != null && responseMessage.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
