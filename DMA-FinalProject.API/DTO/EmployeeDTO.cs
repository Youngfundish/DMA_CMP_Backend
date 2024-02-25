namespace DMA_FinalProject.API.DTO
{
    public class EmployeeDTO
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int CompanyId { get; set; }
        public string Token { get; set; } = string.Empty;

        public EmployeeDTO()
        {

        }

        public EmployeeDTO(string name, string phone, string email, string passwordHash, int companyId)
        {
            Name = name;
            Phone = phone;
            Email = email;
            PasswordHash = passwordHash;
            CompanyId = companyId;
        }
    }
}
