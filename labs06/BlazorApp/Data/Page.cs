namespace BlazorApp.Data
{
    public class Page
    {
        public string Url { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        
        public string Html
        {
            get
            {
                return $"<a href='{Url}' target='_blank'>{Name}</a>";
            }
        }
    }
}