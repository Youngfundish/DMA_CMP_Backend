namespace DMA_FinalProject.API.DTO
{
    public class DomainDTO
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int CookieAmount { get; set; }

        public DomainDTO()
        {

        }

        public DomainDTO(string url, string name, int companyId)
        {
            Url = url;
            Name = name;
            CompanyId = companyId;
        }
    }
}
