using System.Collections.Generic;
using System.Numerics;

namespace FlagRoles
{
    public class UserPermissions
    {
        public UserPermissions(IEnumerable<PermissionFlag> permissions)
        {
            Permissions = new PermissionFlag();

            if (permissions is null)
            {
                return;
            }

            foreach (var permission in permissions)
            {
                if (permission is null)
                {
                    continue;
                }

                Permissions |= permission;
            }
        }

        public UserPermissions(params PermissionFlag[] permissions) : this((IEnumerable<PermissionFlag>)permissions)
        {
        }

        public PermissionFlag Permissions { get; set; }

        public BigInteger RawPermissions => Permissions.Value;

        public bool HasAccessTo(PermissionFlag permissionRequired)
        {
            return (Permissions & permissionRequired) == permissionRequired;
        }

        public override string ToString()
        {
            return Permissions?.ToString() ?? string.Empty;
        }
    }
}
