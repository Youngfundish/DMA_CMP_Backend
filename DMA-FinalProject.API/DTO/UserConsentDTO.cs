namespace DMA_FinalProject.API.DTO
{
    public class UserConsentDTO
    {
        public string UserId { get; set; }
        public string DomainUrl { get; set; }
        public bool Necessary { get; set; }
        public bool Functionality { get; set; }
        public bool Analytics { get; set; }
        public bool Marketing { get; set; }

        public UserConsentDTO()
        {

        }

        public UserConsentDTO(string userId, bool necessary, bool functionality, bool analytics, bool marketing)
        {
            UserId = userId;
            Necessary = necessary;
            Functionality = functionality;
            Analytics = analytics;
            Marketing = marketing;
        }
    }
}
