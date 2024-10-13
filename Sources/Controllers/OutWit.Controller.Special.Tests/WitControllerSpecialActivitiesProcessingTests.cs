using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OutWit.Engine;
using OutWit.Engine.Data.Status;
using OutWit.Engine.Services;
using OutWit.Engine.Utils;

namespace OutWit.Controller.Special.Tests
{
    [TestFixture]
    public class WitControllerSpecialActivitiesProcessingTests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            WitEngine.Instance.Reload(null);
        }
        [Test]
        public void TestTest()
        {
            var jobString = "Job:TestJob()" +
                            "{" +
                            "    Test(\"message\", false);" +
                            "    Test(\"message\", true);" +
                            "}";

            var job = jobString.Deserialize();

            var nSteps = 0;
            var status = WitProcessingStatus.Failed;
            var isPaused = false;

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
                    Console.WriteLine("Mes={0}", mes);
                };

                WitEngine.Instance.ProcessAsync(job);
            });

            Thread.Sleep(1000);

            Assert.That(nSteps, Is.EqualTo(2));

            WitEngine.Instance.ResumeAsync();

            Thread.Sleep(1000);

            Assert.That(nSteps, Is.EqualTo(2));
            Assert.That(status, Is.EqualTo(WitProcessingStatus.Failed));
        }

	    [Test]
	    public void ConcatStringTest()
	    {
		    var jobString = "Job:TestJob()" +
		                    "{" +
		                    "	 String:val = \"ddd\";" +
							"    String:result = ConcatString(\"aaa\", \"bbb\", \"ccc\", val);" +
							"    Return(result);" +
		                    "}";

		    var job = jobString.Deserialize();

		    var nSteps = 0;
		    var status = WitProcessingStatus.Failed;
		    var isPaused = false;
		    var returnVal = "";

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
				    Console.WriteLine("Mes={0}", mes);
			    };
			    WitEngine.Instance.ProcessingReturnValue += (val) =>
			    {
				    returnVal = (string)val[0];
				    Console.WriteLine("Return={0}", val);
			    };

			    WitEngine.Instance.ProcessAsync(job);
		    });

		    Thread.Sleep(1000);

		    Assert.That(nSteps, Is.EqualTo(2));
		    Assert.That(returnVal, Is.EqualTo("aaabbbcccddd"));
		    Assert.That(status, Is.EqualTo(WitProcessingStatus.Completed));
		}

		[Test]
        public void ReturnTest()
        {
            var jobString = "Job:TestJob()" +
                            "{" +
                            "    Double:val = 3.5;" +
                            "    Return(val, \"test\");" +
                            "}";

            var job = jobString.Deserialize();

            var nSteps = 0;
            var status = WitProcessingStatus.Failed;
            var isPaused = false;
            var returnVal1 = 0.0;
            var returnVal2 = "";

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
                    Console.WriteLine("Mes={0}", mes);
                };
                WitEngine.Instance.ProcessingReturnValue += (val) =>
                {
                    returnVal1 = (double)val[0];
                    returnVal2 = (string)val[1];
					Console.WriteLine("Return={0}", val);
                };

                WitEngine.Instance.ProcessAsync(job);
            });

            Thread.Sleep(1000);

            Assert.That(nSteps, Is.EqualTo(1));
            Assert.That(returnVal1, Is.EqualTo(3.5));
            Assert.That(returnVal2, Is.EqualTo("test"));
			Assert.That(status, Is.EqualTo(WitProcessingStatus.Completed));
        }

	    [Test]
	    public void PathTest()
	    {
		    var jobString = "Job:TestJob()" +
		                    "{" +
		                    "	 String:val = \"file.ext\";" +
		                    "    String:result = Path(\"C:\", \"Folder1\", \"Folder2\", val);" +
		                    "    Return(result);" +
		                    "}";

		    var job = jobString.Deserialize();

		    var nSteps = 0;
		    var status = WitProcessingStatus.Failed;
		    var isPaused = false;
		    var returnVal = "";

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
				    Console.WriteLine("Mes={0}", mes);
			    };
			    WitEngine.Instance.ProcessingReturnValue += (val) =>
			    {
				    returnVal = (string)val[0];
				    Console.WriteLine("Return={0}", val);
			    };

			    WitEngine.Instance.ProcessAsync(job);
		    });

		    Thread.Sleep(1000);

		    Assert.That(nSteps, Is.EqualTo(2));
		    Assert.That(returnVal, Is.EqualTo(@"C:\Folder1\Folder2\file.ext"));
		    Assert.That(status, Is.EqualTo(WitProcessingStatus.Completed));
	    }

		[Test]
        public void PauseTest()
        {
            var jobString = "Job:TestJob()" +
                            "{" +
                            "    Test(\"message\", false);" +
                            "    Pause(\"paused\");" +
                            "    Test(\"message\", false);" +
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

            Thread.Sleep(1000);

            Assert.That(nSteps, Is.EqualTo(2));

            WitEngine.Instance.ResumeAsync();

            Thread.Sleep(1000);

            Assert.That(nSteps, Is.EqualTo(job.StagesCount));
            Assert.That(status, Is.EqualTo(WitProcessingStatus.Completed));
            Assert.That(isPaused, Is.True);
            Assert.That(pauseMessage, Is.EqualTo("paused"));
        }
        

        [Test]
        public void SequentialTest()
        {
            var jobString = "Job:TestJob()" +
                            "{" +
                            "    Sequence()" +
                            "    {" +
                            "        Test(\"message\", false);" +
                            "        Test(\"message\", false);" +
                            "        Test(\"message\", false);" +
                            "    }" +
                            "}";

            var job = jobString.Deserialize();

            var nSteps = 0;
            var status = WitProcessingStatus.Failed;
            var isPaused = false;

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
                    Console.WriteLine("Mes={0}", mes);
                };

                WitEngine.Instance.ProcessAsync(job);
            });

            Thread.Sleep(1000);

            Assert.That(nSteps, Is.EqualTo(3));
            Assert.That(status, Is.EqualTo(WitProcessingStatus.Completed));


        }

        [Test]
        public void ParallelTest()
        {
            var jobString = "Job:TestJob()" +
                            "{" +
                            "    Parallel()" +
                            "    {" +
                            "        Test(\"message\", false);" +
                            "        Test(\"message\", false);" +
                            "        Test(\"message\", false);" +
                            "    }" +
                            "}";

            var job = jobString.Deserialize();

            var nSteps = 0;
            var status = WitProcessingStatus.Failed;
            var isPaused = false;

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
                    Console.WriteLine("Mes={0}", mes);
                };

                WitEngine.Instance.ProcessAsync(job);
            });

            Thread.Sleep(1000);

            Assert.That(nSteps, Is.EqualTo(1));
            Assert.That(status, Is.EqualTo(WitProcessingStatus.Completed));
        }

        [Test]
        public void LoopTest()
        {
            var jobString = "Job:TestJob()" +
                            "{" +
                            "    Loop(4)" +
                            "    {" +
                            "        Test(\"message\", false);" +
                            "        Test(\"message\", false);" +
                            "        Test(\"message\", false);" +
                            "    }" +
                            "}";

            var job = jobString.Deserialize();

            var nSteps = 0;
            var status = WitProcessingStatus.Failed;
            var isPaused = false;

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
                    Console.WriteLine("Mes={0}", mes);
                };

                WitEngine.Instance.ProcessAsync(job);
            });

            Thread.Sleep(1000);

            Assert.That(nSteps, Is.EqualTo(12));
            Assert.That(status, Is.EqualTo(WitProcessingStatus.Completed));
        }

        [Test]
        public void TimerTest()
        {
            var jobString = "Job:TestJob()" +
                            "{" +
                            "    Timer(50, 520)" +
                            "    {" +
                            "        Test(\"message\", false);" +
                            "        Test(\"message\", false);" +
                            "        Test(\"message\", false);" +
                            "    }" +
                            "}";

            var job = jobString.Deserialize();

            var nSteps = 0;
            var status = WitProcessingStatus.Failed;
            var isPaused = false;

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
                    Console.WriteLine("Mes={0}", mes);
                };

                WitEngine.Instance.ProcessAsync(job);
            });

            Thread.Sleep(1000);

            Assert.That(nSteps, Is.EqualTo(9));
            Assert.That(status, Is.EqualTo(WitProcessingStatus.Completed));
        }

        [Test]
        public void DelayedTest()
        {
            var jobString = "Job:TestJob()" +
                            "{" +
                            "    Delay(1000)" +
                            "    {" +
                            "        Test(\"message\", false);" +
                            "        Test(\"message\", false);" +
                            "        Test(\"message\", false);" +
                            "    }" +
                            "}";

            var job = jobString.Deserialize();

            var nSteps = 0;
            var status = WitProcessingStatus.Failed;
            var isPaused = false;

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
                    Console.WriteLine("Mes={0}", mes);
                };

                WitEngine.Instance.ProcessAsync(job);
            });

            Thread.Sleep(500);

            Assert.That(nSteps, Is.EqualTo(0));

            Thread.Sleep(1500);
            Assert.That(nSteps, Is.EqualTo(3));
            Assert.That(status, Is.EqualTo(WitProcessingStatus.Completed));
        }

	    [Test]
	    public void UniqueNameTest()
	    {
		    var jobString = "Job:TestJob()" +
		                    "{" +
		                    "    String:name = UniqueName(\".ext\");" +
							"    Return(name);" +
		                    "}";

		    var job = jobString.Deserialize();

		    var nSteps = 0;
		    var status = WitProcessingStatus.Failed;
		    var isPaused = false;
		    var returnVal = "";

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
				    Console.WriteLine("Mes={0}", mes);
			    };
			    WitEngine.Instance.ProcessingReturnValue += (val) =>
			    {
				    returnVal = (string)val[0];
				    Console.WriteLine("Return={0}", val);
			    };

			    WitEngine.Instance.ProcessAsync(job);
		    });

		    Thread.Sleep(1000);

		    Assert.That(nSteps, Is.EqualTo(2));
		    Assert.That(string.IsNullOrEmpty(returnVal), Is.False);
		    Assert.That(returnVal.Contains(".ext"), Is.True);
			Assert.That(status, Is.EqualTo(WitProcessingStatus.Completed));
	    }
	}
}
