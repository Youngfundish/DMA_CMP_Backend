using DMA_FinalProject.API.DTO;
using DMA_FinalProject.DAL.Model;

namespace DMA_FinalProject.API.Conversion
{
    public static class CookieToDTOConversion
    {
        public static IEnumerable<CookieDTO> CookieToDtos(this IEnumerable<Cookie> cookies)
        {
            foreach (var cookie in cookies)
            {
                yield return cookie.CookieToDto();
            }
        }

        public static CookieDTO CookieToDto(this Cookie cookie)
        {
            return cookie.CopyPropertiesTo(new CookieDTO());
        }

        public static IEnumerable<Cookie> CookieFromDtos(this IEnumerable<CookieDTO> cookieDTOs)
        {
            foreach (var cookie in cookieDTOs)
            {
                yield return cookie.CookieFromDto();
            }
        }

        public static Cookie CookieFromDto(this CookieDTO cookieDTO)
        {
            return cookieDTO.CopyPropertiesTo(new Cookie());
        }
    }
}
