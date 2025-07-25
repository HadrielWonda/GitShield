using System.Security;
using System.Security.Principal;

public class SecureOperation
{
    public void ExecutePrivileged(Action action)
    {
        if (!WindowsIdentity.GetCurrent().IsAdmin)
            throw new SecurityException("Admin rights required");

        using (new Impersonator(adminCredentials))
        {
            WindowsSecureMemory.Execute(action);
        }
    }
}