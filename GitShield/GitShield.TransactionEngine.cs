using RocksDbSharp;
using System.Data.SQLite;

public class AtomicTransactionEngine
{
    private readonly RocksDb _objectStore;
    private readonly SQLiteConnection _metaDb;
    private readonly IPlatformServices _platform;

    public void ExecuteAtomic(Action operation)
    {
        using (var tx = new SQLiteTransaction(_metaDb))
        using (var rocksTx = _objectStore.BeginTransaction())
        using (_platform.CreateMemoryMappedFile("index.idx", 1024))
        {
            try
            {
                operation();
                tx.Commit();
                rocksTx.Commit();
            }
            catch
            {
                rocksTx.Rollback();
                throw;
            }
        }
    }
}