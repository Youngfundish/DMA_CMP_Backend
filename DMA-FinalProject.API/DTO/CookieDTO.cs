namespace DMA_FinalProject.API.DTO
{
    public class CookieDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public string DomainURL { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public CookieDTO()
        {

        }

        public CookieDTO(string name, string value, DateTime expDate, string domainURL, string category)
        {
            Name = name;
            Value = value;
            ExpirationDate = expDate;
            DomainURL = domainURL;
            Category = category;
        }
    }

}
