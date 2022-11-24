namespace CatalogService.Dtos
{
    public enum HttpType
    {
        Get, Post, Put, Delete
    }
    public class Link
    {
        public Link(string name, string href, HttpType type)
        {
            Name = name;
            Href = href;
            Type = ToString(type);
        }

        public string Name { get; private set; }
        public string Href { get; private set; }
        public string Type { get; private set; }

        private string ToString(HttpType type) => type switch
        {
            HttpType.Get => "GET",
            HttpType.Post => "POST",
            HttpType.Put => "PUT",
            HttpType.Delete => "DELETE",
            _ => throw new Exception()
        };

    }
}
