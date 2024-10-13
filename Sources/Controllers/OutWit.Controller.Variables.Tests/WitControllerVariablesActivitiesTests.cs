using OutWit.Controller.Jobs.Jobs;
using OutWit.Controller.Variables.Activities;
using OutWit.Controller.Variables.Variables;
using OutWit.Engine;
using OutWit.Engine.Data.Status;
using OutWit.Engine.Services;
using OutWit.Engine.Utils;

namespace OutWit.Controller.Variables.Tests
{
    [TestFixture]
    public class WitControllerVariablesActivitiesTests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            WitEngine.Instance.Reload(null);
        }

        [Test]
        public void ActivityColorSerializationTest()
        {
            var job = new WitJobVoid("testJob");
            var activity = new WitActivityColor("color", 1, 2, 3);

            Assert.That(activity.Color, Is.EqualTo("color"));
            Assert.That(activity.Red, Is.EqualTo(1));
            Assert.That(activity.Green, Is.EqualTo(2));
            Assert.That(activity.Blue, Is.EqualTo(3));

            var activityStr = activity.Serialize("");

            activityStr.Deserialize(job);

            var activity1 = (WitActivityColor)job.Activities.Single();
            Assert.That(activity, Is.Not.SameAs(activity1));
            Assert.That(activity1.Color, Is.EqualTo("color"));
            Assert.That(activity1.Red, Is.EqualTo(1));
            Assert.That(activity1.Green, Is.EqualTo(2));
            Assert.That(activity1.Blue, Is.EqualTo(3));
            activityStr = "Color:color = WitColor(2, 3, 4);";

            job = new WitJobVoid("testJob");
            activityStr.Deserialize(job);

            activity1 = (WitActivityColor)job.Activities.Single();
            Assert.That(activity, Is.Not.SameAs(activity1));
            Assert.That(activity1.Color, Is.EqualTo("color"));
            Assert.That(activity1.Red, Is.EqualTo(2));
            Assert.That(activity1.Green, Is.EqualTo(3));
            Assert.That(activity1.Blue, Is.EqualTo(4));
        }

        [Test]
        public void ActivityColorProcessingTest()
        {
            var jobString = "Job:TestJob()" +
                            "{" +
                            "    Color:color = WitColor(1, 2, 3);" +
                            "}";

            var job = jobString.Deserialize();

            var nSteps = 0;
            var status = WitProcessingStatus.Failed;
            var isPaused = false;
            var pauseMessage = "";

            Task.Run(() =>
            {
                WitEngine.Instance.ProcessingProgressChanged += (val, max, mes) =>
                {
                    nSteps++;
                    Console.WriteLine("Val={0}, Max ={1}, Mes={2}", val, max, mes);
                };
                WitEngine.Instance.ProcessingCompleted += (s, mes) =>
                {
                    status = s;
                    Console.WriteLine("Status={0}, Mes={1}", s, mes);
                };
                WitEngine.Instance.ProcessingPaused += (mes) =>
                {
                    isPaused = true;
                    pauseMessage = mes;
                    Console.WriteLine("Mes={0}", mes);
                };

                WitEngine.Instance.ProcessAsync(job);
            });

            Thread.Sleep(500);

            Assert.That(nSteps, Is.EqualTo(job.StagesCount));
            Assert.That(status, Is.EqualTo(WitProcessingStatus.Completed));
            Assert.That(job.Variables.OfType<WitVariableColor>().Single().Name, Is.EqualTo("color"));
            Assert.That(job.Variables.OfType<WitVariableColor>().Single().Value.Red, Is.EqualTo(1));
            Assert.That(job.Variables.OfType<WitVariableColor>().Single().Value.Green, Is.EqualTo(2));
            Assert.That(job.Variables.OfType<WitVariableColor>().Single().Value.Blue, Is.EqualTo(3));
        }
    }
}
