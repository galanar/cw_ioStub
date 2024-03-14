namespace Codewars.Tests;

[TestFixture]
public class IoStubTests
{
    [Test]
    public void CreatingDirs()
    {
        var target = new IoStub();
        Assert.That(target.LsDir("/"), Is.Empty);

        target.MkDir("/t");
        target.MkDir("/t/s2");
        target.MkDir("/t/s3");
        
        // added for deeper structure testing
        target.MkDir("/t/s3/f");
        
        Assert.That(target.LsDir("/"), Is.EqualTo(new[] { "/t" }));
        Assert.That(target.LsDir("/t"), Is.EquivalentTo(new[] { "/t/s2", "/t/s3" }));
        
        // added for deeper structure testing
        Assert.That(target.LsDir("/t/s3"), Is.EquivalentTo(new[] { "/t/s3/f" }));
           
        target.RmDir("/t/s3");
        Assert.That(target.LsDir("/t"), Is.EquivalentTo(new[] { "/t/s2" }));
        Assert.That(target.LsDir("/t/s2/"), Is.Empty);
    }
    
}