namespace FlagPermissions.Console
{
    public static class ExamplePermissions
    {
        public static PermissionFlag Read => PermissionFlag.FromId(0);

        public static PermissionFlag Write => Read | PermissionFlag.FromId(1);

        public static PermissionFlag CreateDelete => Write | PermissionFlag.FromId(2);

        public static PermissionFlag ViewAudit => PermissionFlag.FromId(3); 
    }
}
