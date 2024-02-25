using DMA_FinalProject.API.DTO;
using DMA_FinalProject.DAL.Model;

namespace DMA_FinalProject.API.Conversion
{
    public static class UserConsentToDTOConversion
    {
        public static IEnumerable<UserConsentDTO> UserConsentToDtos(this IEnumerable<UserConsent> employees)
        {
            foreach (var employee in employees)
            {
                yield return employee.UserConsentToDto();
            }
        }

        public static UserConsentDTO UserConsentToDto(this UserConsent employee)
        {
            return employee.CopyPropertiesTo(new UserConsentDTO());
        }

        public static IEnumerable<UserConsent> UserConsentFromDtos(this IEnumerable<UserConsentDTO> employeeDTOs)
        {
            foreach (var employee in employeeDTOs)
            {
                yield return employee.UserConsentFromDto();
            }
        }

        public static UserConsent UserConsentFromDto(this UserConsentDTO employeeDTO)
        {
            return employeeDTO.CopyPropertiesTo(new UserConsent());
        }
    }
}
