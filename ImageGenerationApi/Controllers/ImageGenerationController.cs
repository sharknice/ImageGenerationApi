using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ImageGenerationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageGenerationController : ControllerBase
    {
        private readonly HttpClient client;

        public ImageGenerationController(HttpClient httpClient)
        {
            client = httpClient;
        }

        [HttpGet]
        public async Task<FileContentResult> GetAsync(string text)
        {
            dynamic request = new System.Dynamic.ExpandoObject();
            request.fn_index = 50;
            request.data = new dynamic[]
                            {
                                text,
                                "",
                                "None",
                                "None",
                                20,
                                "Euler a",
                                true,
                                false,
                                1,
                                1,
                                7,
                                -1,
                                -1,
                                0,
                                0,
                                0,
                                false,
                                768,
                                768,
                                false,
                                0.7,
                                0,
                                0,
                                "None",
                                false,
                                false,
                                false,
                                "",
                                "Seed",
                                "",
                                "Nothing",
                                "",
                                true,
                                false,
                                false,
                                null,
                                ""
                            };

            var jsonString = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://127.0.0.1:7860" + "/run/predict/", httpContent);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            ImageApiResponse result = JsonConvert.DeserializeObject<ImageApiResponse>(jsonResponse);

            var data = JsonConvert.DeserializeObject<ImageApiResponseData>(Convert.ToString(result.data[0]).Replace("\r\n", "").Replace("[", "").Replace("]", ""));
            var path = $"M:\\ImageAI\\stable-diffusion-webui\\outputs\\txt2img-images\\{Path.GetFileName(data.name)}";

            byte[] imageBits = System.IO.File.ReadAllBytes(path);
            return File(imageBits, "image/jpeg");
        }
    }
}