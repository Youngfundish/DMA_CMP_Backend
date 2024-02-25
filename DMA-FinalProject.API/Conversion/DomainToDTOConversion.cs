using DMA_FinalProject.API.DTO;
using DMA_FinalProject.DAL.Model;

namespace DMA_FinalProject.API.Conversion
{
    public static class DomainToDTOConversion
    {
        public static IEnumerable<DomainDTO> DomainToDtos(this IEnumerable<Domain> domains)
        {
            foreach (var domain in domains)
            {
                yield return domain.DomainToDto();
            }
        }

        public static DomainDTO DomainToDto(this Domain domain)
        {
            return domain.CopyPropertiesTo(new DomainDTO());
        }

        public static IEnumerable<Domain> DomainFromDtos(this IEnumerable<DomainDTO> domainDTOs)
        {
            foreach (var domain in domainDTOs)
            {
                yield return domain.DomainFromDto();
            }
        }

        public static Domain DomainFromDto(this DomainDTO domainDTO)
        {
            return domainDTO.CopyPropertiesTo(new Domain());
        }
    }
}
