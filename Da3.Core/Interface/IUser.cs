using Da3.Core.Entities;
using Da3.Core.Role;

namespace Da3.Core.Interface
{
    public interface IUser
    {
        void Register(Da3User user, RoleConstants role);

        Da3User UserInfo();
    } 
}