namespace ImageGenerationApi
{
    public class ImageApiResponse
    {
        public List<object> data { get; set; }
        public bool is_generating { get; set; }
        public double duration { get; set; }
        public double average_duration { get; set; }
    }
}
