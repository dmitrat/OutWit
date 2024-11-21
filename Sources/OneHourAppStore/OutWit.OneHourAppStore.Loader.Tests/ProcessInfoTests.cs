using OutWit.OneHourAppStore.Loader;

namespace OutWit.OneHourAppStore.Loader.Tests
{
    [TestFixture]
    public class ProcessInfoTests
    {

#if DEBUG

        [Test]
        public void ConstructorTest()
        {
            var info = new ProcessInfo(new IntPtr(1), new IntPtr(2), 3, 4, new IntPtr(5));

            Assert.That(info.ProcessHandle, Is.EqualTo(new IntPtr(1)));
            Assert.That(info.ThreadHandle, Is.EqualTo(new IntPtr(2)));
            Assert.That(info.ProcessId, Is.EqualTo(3));
            Assert.That(info.ThreadId, Is.EqualTo(4));
            Assert.That(info.Loader, Is.EqualTo(new IntPtr(5)));
        }

        [Test]
        public void IsTest()
        {
            var info1 = new ProcessInfo(new IntPtr(1), new IntPtr(2), 3, 4, new IntPtr(5));
            var info2 = new ProcessInfo(new IntPtr(1), new IntPtr(2), 3, 4, new IntPtr(5));

            Assert.That(info1.Is(info2), Is.True);

            info2 = new ProcessInfo(new IntPtr(2), new IntPtr(2), 3, 4, new IntPtr(5));
            Assert.That(info1.Is(info2), Is.False);

            info2 = new ProcessInfo(new IntPtr(1), new IntPtr(3), 3, 4, new IntPtr(5));
            Assert.That(info1.Is(info2), Is.False);

            info2 = new ProcessInfo(new IntPtr(1), new IntPtr(2), 4, 4, new IntPtr(5));
            Assert.That(info1.Is(info2), Is.False);

            info2 = new ProcessInfo(new IntPtr(1), new IntPtr(2), 3, 5, new IntPtr(5));
            Assert.That(info1.Is(info2), Is.False);

            info2 = new ProcessInfo(new IntPtr(1), new IntPtr(2), 3, 4, new IntPtr(6));
            Assert.That(info1.Is(info2), Is.False);
        }

        [Test]
        public void CloneTest()
        {
            var info1 = new ProcessInfo(new IntPtr(1), new IntPtr(2), 3, 4, new IntPtr(5));
            var info2 = info1.Clone() as ProcessInfo;

            Assert.That(info2, Is.Not.Null);
            Assert.That(info1, Is.Not.SameAs(info2));

            Assert.That(info2.ProcessHandle, Is.EqualTo(new IntPtr(1)));
            Assert.That(info2.ThreadHandle, Is.EqualTo(new IntPtr(2)));
            Assert.That(info2.ProcessId, Is.EqualTo(3));
            Assert.That(info2.ThreadId, Is.EqualTo(4));
            Assert.That(info2.Loader, Is.EqualTo(new IntPtr(5)));
        }


#endif
    }
}