using DMA_FinalProject.API.DTO;
using DMA_FinalProject.DAL.Model;

namespace DMA_FinalProject.API.Conversion
{
    public static class UserToDTOConversion
    {
        public static IEnumerable<UserDTO> UserToDtos(this IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                yield return user.UserToDto();
            }
        }

        public static UserDTO UserToDto(this User user)
        {
            return user.CopyPropertiesTo(new UserDTO());
        }

        public static IEnumerable<User> UserFromDtos(this IEnumerable<UserDTO> userDTOs)
        {
            foreach (var user in userDTOs)
            {
                yield return user.UserFromDto();
            }
        }

        public static User UserFromDto(this UserDTO userDTO)
        {
            return userDTO.CopyPropertiesTo(new User());
        }
    }
}
