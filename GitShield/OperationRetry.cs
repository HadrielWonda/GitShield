using System.Security.Policy;

public class OperationRetry
{
    public T ExecuteWithRetry<T>(Func<T> operation, int maxAttempts = 5)
    {
        return Policy
            .Handle<CloudInterferenceException>()
            .WaitAndRetry(maxAttempts, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .Execute(operation);
    }

    private class CloudInterferenceException
    {
        //tbc
    }
}