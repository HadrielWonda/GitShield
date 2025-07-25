[TestFixture]
public class ChaosTests
{
    [Test]
    public void CommitDuringAvScan()
    {
        using (new SimulatedAvLock("repo.git"))
        {
            var result = _shield.Commit("Test during scan");
            Assert.IsTrue(result.Succeeded);
        }
    }
}