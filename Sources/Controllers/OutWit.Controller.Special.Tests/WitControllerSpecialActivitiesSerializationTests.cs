using System.Linq;
using NUnit.Framework;
using OutWit.Common.Collections;
using OutWit.Common.Utils;
using OutWit.Controller.Jobs.Jobs;
using OutWit.Controller.Special.Activities;
using OutWit.Controller.Variables.Variables;
using OutWit.Engine;
using OutWit.Engine.Utils;

namespace OutWit.Controller.Special.Tests
{
    [TestFixture]
    public class WitControllerSpecialActivitiesSerializationTests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            WitEngine.Instance.Reload(null);
        }

	    [Test]
	    public void ConcatStringTest()
	    {
		    var job = new WitJobVoid("testJob");
		    var activity = new WitActivitySpecialConcatString("result", "aaa", "bbb", "ccc");

		    Assert.That(activity.Return, Is.EqualTo("result"));
		    Assert.That(activity.StringParts.Is("aaa", "bbb", "ccc"), Is.True);

		    var activityStr = activity.Serialize("");

		    activityStr.Deserialize(job);

		    var activity1 = (WitActivitySpecialConcatString)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.Return, Is.EqualTo("result"));
		    Assert.That(activity1.StringParts.Is("aaa", "bbb", "ccc"), Is.True);

			job = new WitJobVoid("testJob");
		    activityStr = "String:result = ConcatString(\"aaa\", \"bbb\");";

		    activityStr.Deserialize(job);

		    activity1 = (WitActivitySpecialConcatString)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.Return, Is.EqualTo("result"));
			Assert.That(activity1.StringParts.Is("aaa", "bbb"), Is.True);
	    }

	    [Test]
	    public void CopyTest()
	    {
		    var job = new WitJobVoid("testJob");
		    var activity = new WitActivitySpecialCopy("from", "to");

		    Assert.That(activity.From, Is.EqualTo("from"));
		    Assert.That(activity.To, Is.EqualTo("to"));

		    var activityStr = activity.Serialize("");

		    activityStr.Deserialize(job);

		    var activity1 = (WitActivitySpecialCopy)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.From, Is.EqualTo("from"));
		    Assert.That(activity1.To, Is.EqualTo("to"));

			job = new WitJobVoid("testJob");
		    activityStr = "Copy(from, to);";

		    activityStr.Deserialize(job);

		    activity1 = (WitActivitySpecialCopy)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.From, Is.EqualTo("from"));
		    Assert.That(activity1.To, Is.EqualTo("to"));
		}

	    [Test]
	    public void DelayedTest()
	    {
		    var job = new WitJobVoid("testJob");

		    var activity = new WitActivitySpecialDelayed(10)
		    {
			    Activities =
			    {
				    new WitActivitySpecialTest("", "test1", false),
				    new WitActivitySpecialTest("", "test2", true),
				    new WitActivitySpecialTest("", "test3", false)
			    }
		    };

		    Assert.That(activity.Activities.Count, Is.EqualTo(3));
		    Assert.That(activity.Delay, Is.EqualTo(10));
		    Assert.That(activity.StagesCount, Is.EqualTo(3));

		    var activityStr = activity.Serialize();

		    activityStr.Deserialize(job);
		    var activity1 = (WitActivitySpecialDelayed)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.Activities.Count, Is.EqualTo(3));
		    Assert.That(activity1.Delay, Is.EqualTo(10));
		    Assert.That(activity1.StagesCount, Is.EqualTo(3));

		    Assert.That(((WitActivitySpecialTest)activity1.Activities[0]).Message, Is.EqualTo("test1"));
		    Assert.That(((WitActivitySpecialTest)activity1.Activities[0]).ThrowException, Is.EqualTo(false));

		    Assert.That(((WitActivitySpecialTest)activity1.Activities[1]).Message, Is.EqualTo("test2"));
		    Assert.That(((WitActivitySpecialTest)activity1.Activities[1]).ThrowException, Is.EqualTo(true));

		    Assert.That(((WitActivitySpecialTest)activity1.Activities[2]).Message, Is.EqualTo("test3"));
		    Assert.That(((WitActivitySpecialTest)activity1.Activities[2]).ThrowException, Is.EqualTo(false));
	    }

	    [Test]
	    public void LoopTest()
	    {
		    var job = new WitJobVoid("testJob");

		    var activity = new WitActivitySpecialLoop(4)
		    {
			    Activities =
			    {
				    new WitActivitySpecialTest("", "test1", false),
				    new WitActivitySpecialTest("", "test2", true),
				    new WitActivitySpecialTest("", "test3", false)
			    }
		    };

		    Assert.That(activity.Activities.Count, Is.EqualTo(3));
		    Assert.That(activity.IterationsCount, Is.EqualTo(4));
		    Assert.That(activity.StagesCount, Is.EqualTo(12));

		    var activityStr = activity.Serialize();

		    activityStr.Deserialize(job);
		    var activity1 = (WitActivitySpecialLoop)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.Activities.Count, Is.EqualTo(3));
		    Assert.That(activity1.StagesCount, Is.EqualTo(12));

		    Assert.That(((WitActivitySpecialTest)activity1.Activities[0]).Message, Is.EqualTo("test1"));
		    Assert.That(((WitActivitySpecialTest)activity1.Activities[0]).ThrowException, Is.EqualTo(false));

		    Assert.That(((WitActivitySpecialTest)activity1.Activities[1]).Message, Is.EqualTo("test2"));
		    Assert.That(((WitActivitySpecialTest)activity1.Activities[1]).ThrowException, Is.EqualTo(true));

		    Assert.That(((WitActivitySpecialTest)activity1.Activities[2]).Message, Is.EqualTo("test3"));
		    Assert.That(((WitActivitySpecialTest)activity1.Activities[2]).ThrowException, Is.EqualTo(false));
	    }

	    [Test]
	    public void ParallelTest()
	    {
		    var job = new WitJobVoid("testJob");

		    var activity = new WitActivitySpecialParallel
		    {
			    Activities =
			    {
				    new WitActivitySpecialTest("", "test1", false),
				    new WitActivitySpecialTest("", "test2", true),
				    new WitActivitySpecialTest("", "test3", false)
			    }
		    };

		    Assert.That(activity.Activities.Count, Is.EqualTo(3));
		    Assert.That(activity.StagesCount, Is.EqualTo(1));

		    var activityStr = activity.Serialize();

		    activityStr.Deserialize(job);
		    var activity1 = (WitActivitySpecialParallel)job.Activities.Single();

		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.Activities.Count, Is.EqualTo(3));
		    Assert.That(activity1.StagesCount, Is.EqualTo(1));

		    Assert.That(((WitActivitySpecialTest)activity1.Activities[0]).Message, Is.EqualTo("test1"));
		    Assert.That(((WitActivitySpecialTest)activity1.Activities[0]).ThrowException, Is.EqualTo(false));

		    Assert.That(((WitActivitySpecialTest)activity1.Activities[1]).Message, Is.EqualTo("test2"));
		    Assert.That(((WitActivitySpecialTest)activity1.Activities[1]).ThrowException, Is.EqualTo(true));

		    Assert.That(((WitActivitySpecialTest)activity1.Activities[2]).Message, Is.EqualTo("test3"));
		    Assert.That(((WitActivitySpecialTest)activity1.Activities[2]).ThrowException, Is.EqualTo(false));
	    }

	    [Test]
	    public void PathTest()
	    {
		    var job = new WitJobVoid("testJob");
		    var activity = new WitActivitySpecialPath("result", "C:", "Folder1", "Folder2", "file.ext");

		    Assert.That(activity.Return, Is.EqualTo("result"));
		    Assert.That(activity.PathParts.Is("C:", "Folder1", "Folder2", "file.ext"), Is.True);

		    var activityStr = activity.Serialize("");

		    activityStr.Deserialize(job);

		    var activity1 = (WitActivitySpecialPath)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.Return, Is.EqualTo("result"));
		    Assert.That(activity1.PathParts.Is("C:", "Folder1", "Folder2", "file.ext"), Is.True);

		    job = new WitJobVoid("testJob");
		    activityStr = "String:path = Path(\"C:\", \"Folder\", \"file.ext\");";

		    activityStr.Deserialize(job);

		    activity1 = (WitActivitySpecialPath)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.Return, Is.EqualTo("path"));
		    Assert.That(activity1.PathParts.Is("C:", "Folder", "file.ext"), Is.True);
	    }

		[Test]
	    public void PauseTest()
	    {
		    var job = new WitJobVoid("testJob");
		    var activity = new WitActivitySpecialPause("message", 1000);

		    Assert.That(activity.Message, Is.EqualTo("message"));
		    Assert.That(activity.Timeout, Is.EqualTo(1000));

		    var activityStr = activity.Serialize("");

		    activityStr.Deserialize(job);

		    var activity1 = (WitActivitySpecialPause)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.Message, Is.EqualTo("message"));
		    Assert.That(activity1.Timeout, Is.EqualTo(1000));

		    job = new WitJobVoid("testJob");
		    activityStr = "Pause(\"message\", 1000);";

		    activityStr.Deserialize(job);

		    activity1 = (WitActivitySpecialPause)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.Message, Is.EqualTo("message"));
		    Assert.That(activity1.Timeout, Is.EqualTo(1000));

		    job = new WitJobVoid("testJob");
		    activityStr = "Pause(\"message\");";

		    activityStr.Deserialize(job);

		    activity1 = (WitActivitySpecialPause)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.Message, Is.EqualTo("message"));
		    Assert.That(activity1.Timeout, Is.EqualTo(600000));
	    }

		[Test]
        public void ReturnTest()
        {
            var job = new WitJobVoid("testJob");
            var activity = new WitActivitySpecialReturn("value");

            Assert.That(activity.Value.Length, Is.EqualTo(1));
            Assert.That(activity.Value[0], Is.EqualTo("value"));

			var activityStr = activity.Serialize("");

            activityStr.Deserialize(job);

            var activity1 = (WitActivitySpecialReturn)job.Activities.Single();
            Assert.That(activity, Is.Not.SameAs(activity1));
			Assert.That(activity1.Value.Length, Is.EqualTo(1));
	        Assert.That(activity1.Value[0], Is.EqualTo("value"));

			job = new WitJobVoid("testJob");
            activityStr = "Return(value);";

            activityStr.Deserialize(job);

            activity1 = (WitActivitySpecialReturn)job.Activities.Single();
            Assert.That(activity, Is.Not.SameAs(activity1));
			Assert.That(activity1.Value.Length, Is.EqualTo(1));
	        Assert.That(activity1.Value[0], Is.EqualTo("value"));
		}

        [Test]
        public void SequentialTest()
        {
            var job = new WitJobVoid("testJob");

            var activity = new WitActivitySpecialSequential
            {
                Activities =
                {
                    new WitActivitySpecialTest("", "test1", false),
                    new WitActivitySpecialTest("", "test2", true),
                    new WitActivitySpecialTest("", "test3", false)
                }
            };

            Assert.That(activity.Activities.Count, Is.EqualTo(3));
            Assert.That(activity.StagesCount, Is.EqualTo(3));

            var activityStr = activity.Serialize();

            activityStr.Deserialize(job);
            var activity1 = (WitActivitySpecialSequential)job.Activities.Single();
            Assert.That(activity, Is.Not.SameAs(activity1));
            Assert.That(activity1.Activities.Count, Is.EqualTo(3));
            Assert.That(activity1.StagesCount, Is.EqualTo(3));

            Assert.That(((WitActivitySpecialTest)activity1.Activities[0]).Message, Is.EqualTo("test1"));
            Assert.That(((WitActivitySpecialTest)activity1.Activities[0]).ThrowException, Is.EqualTo(false));

            Assert.That(((WitActivitySpecialTest)activity1.Activities[1]).Message, Is.EqualTo("test2"));
            Assert.That(((WitActivitySpecialTest)activity1.Activities[1]).ThrowException, Is.EqualTo(true));

            Assert.That(((WitActivitySpecialTest)activity1.Activities[2]).Message, Is.EqualTo("test3"));
            Assert.That(((WitActivitySpecialTest)activity1.Activities[2]).ThrowException, Is.EqualTo(false));

        }

	    [Test]
	    public void TestTest()
	    {
		    var job = new WitJobVoid("testJob");
		    var activity = new WitActivitySpecialTest("return", "message", true);

		    Assert.That(activity.Return, Is.EqualTo("return"));
		    Assert.That(activity.Message, Is.EqualTo("message"));
		    Assert.That(activity.ThrowException, Is.EqualTo(true));

		    var activityStr = activity.Serialize("");

		    activityStr.Deserialize(job);

		    var activity1 = (WitActivitySpecialTest)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.Return, Is.EqualTo("return"));
		    Assert.That(activity1.Message, Is.EqualTo("message"));
		    Assert.That(activity1.ThrowException, Is.EqualTo(true));

		    job = new WitJobVoid("testJob");
		    activity = new WitActivitySpecialTest("", "message", true);

		    Assert.That(activity.Return, Is.EqualTo(""));
		    Assert.That(activity.Message, Is.EqualTo("message"));
		    Assert.That(activity.ThrowException, Is.EqualTo(true));

		    activityStr = activity.Serialize("");

		    activityStr.Deserialize(job);

		    activity1 = (WitActivitySpecialTest)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.Return, Is.EqualTo(""));
		    Assert.That(activity1.Message, Is.EqualTo("message"));
		    Assert.That(activity1.ThrowException, Is.EqualTo(true));
	    }

		[Test]
        public void TimerTest()
        {
            var job = new WitJobVoid("testJob");

            var activity = new WitActivitySpecialTimer(300, 400)
            {
                Activities =
                {
                    new WitActivitySpecialTest("", "test1", false),
                    new WitActivitySpecialTest("", "test2", true),
                    new WitActivitySpecialTest("", "test3", false)
                }
            };

            Assert.That(activity.Activities.Count, Is.EqualTo(3));
            Assert.That(activity.StagesCount, Is.EqualTo(3));
            Assert.That(activity.Interval, Is.EqualTo(300));
            Assert.That(activity.Timeout, Is.EqualTo(400));

            var activityStr = activity.Serialize();

            activityStr.Deserialize(job);
            var activity1 = (WitActivitySpecialTimer)job.Activities.Single();

            Assert.That(activity, Is.Not.SameAs(activity1));
            Assert.That(activity1.Activities.Count, Is.EqualTo(3));
            Assert.That(activity1.StagesCount, Is.EqualTo(3));
            Assert.That(activity1.Interval, Is.EqualTo(300));
            Assert.That(activity1.Timeout, Is.EqualTo(400));

            Assert.That(((WitActivitySpecialTest)activity1.Activities[0]).Message, Is.EqualTo("test1"));
            Assert.That(((WitActivitySpecialTest)activity1.Activities[0]).ThrowException, Is.EqualTo(false));

            Assert.That(((WitActivitySpecialTest)activity1.Activities[1]).Message, Is.EqualTo("test2"));
            Assert.That(((WitActivitySpecialTest)activity1.Activities[1]).ThrowException, Is.EqualTo(true));

            Assert.That(((WitActivitySpecialTest)activity1.Activities[2]).Message, Is.EqualTo("test3"));
            Assert.That(((WitActivitySpecialTest)activity1.Activities[2]).ThrowException, Is.EqualTo(false));
        }

	    [Test]
	    public void UniqueNameTest()
	    {
		    var job = new WitJobVoid("testJob");
		    var activity = new WitActivitySpecialUniqueName("name", ".ext");

		    Assert.That(activity.Return, Is.EqualTo("name"));
		    Assert.That(activity.Extension, Is.EqualTo(".ext"));

		    var activityStr = activity.Serialize("");

		    activityStr.Deserialize(job);

		    var activity1 = (WitActivitySpecialUniqueName)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.Return, Is.EqualTo("name"));
		    Assert.That(activity1.Extension, Is.EqualTo(".ext"));

			job = new WitJobVoid("testJob");
		    activityStr = "String:name = UniqueName(\".ext\");";

		    activityStr.Deserialize(job);

		    activity1 = (WitActivitySpecialUniqueName)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
		    Assert.That(activity1.Return, Is.EqualTo("name"));
		    Assert.That(activity1.Extension, Is.EqualTo(".ext"));

			job = new WitJobVoid("testJob");
		    activityStr = "String:name = UniqueName();";

		    activityStr.Deserialize(job);

		    activity1 = (WitActivitySpecialUniqueName)job.Activities.Single();
		    Assert.That(activity, Is.Not.SameAs(activity1));
			Assert.That(activity1.Return, Is.EqualTo("name"));
		    Assert.That(activity1.Extension, Is.EqualTo(""));
		}

		[Test]
        public void CompositeTest()
        {
            var job = new WitJobVoid("testJob");

            var activity = new WitActivitySpecialLoop(3)
            {
                Variables =
                {
                    new WitVariableDouble("var1", 3.3)
                },
                Activities =
                {
                    new WitActivitySpecialTest("", "test1", false),
                    new WitActivitySpecialSequential
                    {
                        Variables =
                        {
                            new WitVariableDouble("var2", 4.4),
                            new WitVariableDouble("var3", 5.5)
                        },
                        Activities =
                        {
                            new WitActivitySpecialTest("var1", "test2", false),
                            new WitActivitySpecialParallel
                            {
                                Variables =
                                {
                                    new WitVariableDouble("var6", 6.6)
                                },
                                Activities =
                                {
                                    new WitActivitySpecialTest("var2", "test3", false),
                                    new WitActivitySpecialTest("", "test4", true),
                                }

                            },
                            new WitActivitySpecialParallel
                            {
                                Activities =
                                {
                                    new WitActivitySpecialTest("","test5", false),
                                    new WitActivitySpecialTest("","test6", true),
                                }

                            },
                        }

                    },
                    new WitActivitySpecialTest("","test7", true),
                    new WitActivitySpecialDelayed(10)
                    {
                        Activities =
                        {
                            new WitActivitySpecialTest("","test8", false),
                            new WitActivitySpecialTest("var3","test9", true),
                        }

                    }
                }

            };

            Assert.That(activity.Activities.Count, Is.EqualTo(4));
            Assert.That(activity.StagesCount, Is.EqualTo(21));

            var activityStr = activity.Serialize();

            activityStr.Deserialize(job);
            var activity1 = (WitActivitySpecialLoop)job.Activities.Single();
            Assert.That(activity, Is.Not.SameAs(activity1));
            Assert.That(activity1.Activities.Count, Is.EqualTo(4));
            Assert.That(activity1.StagesCount, Is.EqualTo(21));


            Assert.That(((WitActivitySpecialTest)activity1.Activities[0]).Message, Is.EqualTo("test1"));
            Assert.That(((WitActivitySpecialTest)activity1.Activities[0]).ThrowException, Is.EqualTo(false));

            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialSequential)activity1.Activities[1]).Activities[0]).Message, Is.EqualTo("test2"));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialSequential)activity1.Activities[1]).Activities[0]).ThrowException, Is.EqualTo(false));

            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)activity1.Activities[1]).Activities[1]).Activities[0]).Message, Is.EqualTo("test3"));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)activity1.Activities[1]).Activities[1]).Activities[0]).ThrowException, Is.EqualTo(false));

            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)activity1.Activities[1]).Activities[1]).Activities[1]).Message, Is.EqualTo("test4"));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)activity1.Activities[1]).Activities[1]).Activities[1]).ThrowException, Is.EqualTo(true));

            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)activity1.Activities[1]).Activities[2]).Activities[0]).Message, Is.EqualTo("test5"));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)activity1.Activities[1]).Activities[2]).Activities[0]).ThrowException, Is.EqualTo(false));

            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)activity1.Activities[1]).Activities[2]).Activities[1]).Message, Is.EqualTo("test6"));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)activity1.Activities[1]).Activities[2]).Activities[1]).ThrowException, Is.EqualTo(true));

            Assert.That(((WitActivitySpecialTest)activity1.Activities[2]).Message, Is.EqualTo("test7"));
            Assert.That(((WitActivitySpecialTest)activity1.Activities[2]).ThrowException, Is.EqualTo(true));

            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialDelayed)activity1.Activities[3]).Activities[0]).Message, Is.EqualTo("test8"));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialDelayed)activity1.Activities[3]).Activities[0]).ThrowException, Is.EqualTo(false));

            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialDelayed)activity1.Activities[3]).Activities[1]).Message, Is.EqualTo("test9"));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialDelayed)activity1.Activities[3]).Activities[1]).ThrowException, Is.EqualTo(true));

        }
    }
}
