using OutWit.Controller.Jobs.Jobs;
using OutWit.Controller.Special.Activities;
using OutWit.Controller.Variables.Variables;
using OutWit.Engine;
using OutWit.Engine.Utils;

namespace OutWit.Controller.Jobs.Tests
{
    [TestFixture]
    public class WitControllerJobTests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            WitEngine.Instance.Reload(null);
        }

        [Test]
        public void JobVoidTest()
        {
            var job = new WitJobVoid("TestJob")
            {
                Variables =
                {
                    new WitVariableDouble("var1", 3.3),
                    new WitVariableDouble("var2", 4.4),
                },
                Activities =
                {
                    new WitActivitySpecialSequential
                    {
                        Variables =
                        {
                            new WitVariableDouble("var3", 5.5)
                        },
                        Activities =
                        {
                            new WitActivitySpecialTest("","test2", false),
                            new WitActivitySpecialParallel
                            {
                                Variables =
                                {
                                    new WitVariableDouble("var6", 6.6)
                                },
                                Activities =
                                {
                                    new WitActivitySpecialTest("var1","test3", false),
                                    new WitActivitySpecialTest("","test4", true),
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
                    new WitActivitySpecialTest("var2","test7", true),
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

            Assert.That(job.Name, Is.EqualTo("TestJob"));
            Assert.That(job.Activities.Count, Is.EqualTo(3));
            Assert.That(job.Variables.Count, Is.EqualTo(2));
            Assert.That(job.StagesCount, Is.EqualTo(6));

            var jobStr = job.Serialize();

            var job1 = (WitJobVoid)jobStr.Deserialize();

            Assert.That(job, Is.Not.SameAs(job1));
            Assert.That(job1.Name, Is.EqualTo("TestJob"));
            Assert.That(job.Activities.Count, Is.EqualTo(3));
            Assert.That(job.Variables.Count, Is.EqualTo(2));
            Assert.That(job.StagesCount, Is.EqualTo(6));


            Assert.That(job1.Variables["var1"].Value, Is.EqualTo(3.3));
            Assert.That(job1.Variables["var2"].Value, Is.EqualTo(4.4));
            Assert.That(((WitActivitySpecialSequential)job1.Activities[0]).Variables["var3"].Value, Is.EqualTo(5.5));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialSequential)job1.Activities[0]).Activities[0]).Message, Is.EqualTo("test2"));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialSequential)job1.Activities[0]).Activities[0]).ThrowException, Is.EqualTo(false));
            Assert.That(((WitActivitySpecialParallel)((WitActivitySpecialSequential)job1.Activities[0]).Activities[1]).Variables["var6"].Value, Is.EqualTo(6.6));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)job1.Activities[0]).Activities[1]).Activities[0]).Message, Is.EqualTo("test3"));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)job1.Activities[0]).Activities[1]).Activities[0]).ThrowException, Is.EqualTo(false));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)job1.Activities[0]).Activities[1]).Activities[1]).Message, Is.EqualTo("test4"));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)job1.Activities[0]).Activities[1]).Activities[1]).ThrowException, Is.EqualTo(true));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)job1.Activities[0]).Activities[2]).Activities[0]).Message, Is.EqualTo("test5"));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)job1.Activities[0]).Activities[2]).Activities[0]).ThrowException, Is.EqualTo(false));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)job1.Activities[0]).Activities[2]).Activities[1]).Message, Is.EqualTo("test6"));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialParallel)((WitActivitySpecialSequential)job1.Activities[0]).Activities[2]).Activities[1]).ThrowException, Is.EqualTo(true));
            Assert.That(((WitActivitySpecialTest)job1.Activities[1]).Message, Is.EqualTo("test7"));
            Assert.That(((WitActivitySpecialTest)job1.Activities[1]).ThrowException, Is.EqualTo(true));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialDelayed)job1.Activities[2]).Activities[0]).Message, Is.EqualTo("test8"));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialDelayed)job1.Activities[2]).Activities[0]).ThrowException, Is.EqualTo(false));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialDelayed)job1.Activities[2]).Activities[1]).Message, Is.EqualTo("test9"));
            Assert.That(((WitActivitySpecialTest)((WitActivitySpecialDelayed)job1.Activities[2]).Activities[1]).ThrowException, Is.EqualTo(true));
        }

        [Test]
        public void JobStringTest()
        {
            var job = new WitJobString("TestJob", new WitVariableString("param"))
            {
                Variables =
                {
                    new WitVariableDouble("var1", 3.3),
                },
                Activities =
                {
                    new WitActivitySpecialTest("var1","test7", true),
                }

            };

            Assert.That(job.Name, Is.EqualTo("TestJob"));
            Assert.That(job.Activities.Count, Is.EqualTo(1));
            Assert.That(job.Variables.Count, Is.EqualTo(2));
            Assert.That(job.StagesCount, Is.EqualTo(1));

            var job1 = (WitJobString)job.Clone();

            Assert.That(job, Is.Not.SameAs(job1));
            Assert.That(job1.Name, Is.EqualTo("TestJob"));
            Assert.That(job1.Activities.Count, Is.EqualTo(1));
            Assert.That(job1.Variables.Count, Is.EqualTo(2));
            Assert.That(job1.StagesCount, Is.EqualTo(1));

            var jobStr = job.Serialize();

            job1 = (WitJobString)jobStr.Deserialize();

            Assert.That(job, Is.Not.SameAs(job1));
            Assert.That(job1.Name, Is.EqualTo("TestJob"));
            Assert.That(job1.Activities.Count, Is.EqualTo(1));
            Assert.That(job1.Variables.Count, Is.EqualTo(2));
            Assert.That(job1.StagesCount, Is.EqualTo(1));


            Assert.That(job1.Variables["var1"].Value, Is.EqualTo(3.3));
            Assert.That(((WitActivitySpecialTest)job1.Activities[0]).Return, Is.EqualTo("var1"));
            Assert.That(((WitActivitySpecialTest)job1.Activities[0]).Message, Is.EqualTo("test7"));
            Assert.That(((WitActivitySpecialTest)job1.Activities[0]).ThrowException, Is.EqualTo(true));
        }

        [Test]
        public void JobStringStringTest()
        {
            var job = new WmJobStringString("TestJob", new WitVariableString("param1"), new WitVariableString("param2"))
            {
                Variables =
                {
                    new WitVariableDouble("var1", 3.3),
                },
                Activities =
                {
                    new WitActivitySpecialTest("var1","test7", true),
                }

            };

            Assert.That(job.Name, Is.EqualTo("TestJob"));
            Assert.That(job.Activities.Count, Is.EqualTo(1));
            Assert.That(job.Variables.Count, Is.EqualTo(3));
            Assert.That(job.StagesCount, Is.EqualTo(1));

            var job1 = (WmJobStringString)job.Clone();

            Assert.That(job, Is.Not.SameAs(job1));
            Assert.That(job1.Name, Is.EqualTo("TestJob"));
            Assert.That(job1.Activities.Count, Is.EqualTo(1));
            Assert.That(job1.Variables.Count, Is.EqualTo(3));
            Assert.That(job1.StagesCount, Is.EqualTo(1));


            var jobStr = job.Serialize();

            job1 = (WmJobStringString)jobStr.Deserialize();

            Assert.That(job, Is.Not.SameAs(job1));
            Assert.That(job1.Name, Is.EqualTo("TestJob"));
            Assert.That(job1.Activities.Count, Is.EqualTo(1));
            Assert.That(job1.Variables.Count, Is.EqualTo(3));
            Assert.That(job1.StagesCount, Is.EqualTo(1));


            Assert.That(job1.Variables["var1"].Value, Is.EqualTo(3.3));
            Assert.That(((WitActivitySpecialTest)job1.Activities[0]).Return, Is.EqualTo("var1"));
            Assert.That(((WitActivitySpecialTest)job1.Activities[0]).Message, Is.EqualTo("test7"));
            Assert.That(((WitActivitySpecialTest)job1.Activities[0]).ThrowException, Is.EqualTo(true));
        }



        [Test]
        public void CommentsTest()
        {
            var script = "Job:TestJob()" +
                         "{" +
                         "  ~This is Comment~" +
            "  var = Test(\"message\", true);" +
                         "}";

            var job = script.Deserialize();

            var activity1 = (WitActivitySpecialTest)job.Activities.Single();

            Assert.That(activity1.Return, Is.EqualTo("var"));
            Assert.That(activity1.ThrowException, Is.EqualTo(true));

        }
    }
}