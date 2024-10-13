using OutWit.Controller.Jobs.Jobs;
using OutWit.Controller.Special.Activities;
using OutWit.Controller.Variables.Variables;
using OutWit.Engine;
using OutWit.Engine.Utils;

namespace OutWit.Controller.Variables.Tests
{
    public class Tests
    {
        [TestFixture]
        public class WitControllerVariablesTests
        {
            [OneTimeSetUp]
            public void SetUp()
            {
                WitEngine.Instance.Reload(null);
            }

            [Test]
            public void VariableDoubleTest()
            {
                var job = new WitJobVoid("testJob");
                var variable = new WitVariableDouble("var1", 3.3);

                Assert.That(variable.Name, Is.EqualTo("var1"));
                Assert.That(variable.Value, Is.EqualTo(3.3));

                var variable1 = (WitVariableDouble)variable.Clone();
                Assert.That(variable, Is.Not.SameAs( variable1));
                Assert.That(variable1.Name, Is.EqualTo("var1"));
                Assert.That(variable1.Value, Is.EqualTo(3.3));

                var variableStr = variable.Serialize("");
                variableStr.Deserialize(job);

                variable1 = (WitVariableDouble)job.Variables.Single();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("var1"));
                Assert.That(variable1.Value, Is.EqualTo(3.3));

                variableStr = "Double:var1 = 3.3;";

                job = new WitJobVoid("testJob");
                variableStr.Deserialize(job);

                variable1 = (WitVariableDouble)job.Variables.Single();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("var1"));
                Assert.That(variable1.Value, Is.EqualTo(3.3));

                variableStr = "Double:var1;";

                job = new WitJobVoid("testJob");
                variableStr.Deserialize(job);

                variable1 = (WitVariableDouble)job.Variables.Single();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("var1"));
                Assert.That(variable1.Value, Is.EqualTo(0.0));

                variableStr = "Double:var1 = Test(\"message\", true);";

                job = new WitJobVoid("testJob");
                variableStr.Deserialize(job);

                variable1 = (WitVariableDouble)job.Variables.Single();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("var1"));
                Assert.That(variable1.Value, Is.EqualTo(0.0));

                var activity1 = (WitActivitySpecialTest)job.Activities.Single();

                Assert.That(activity1.Return, Is.EqualTo("var1"));
                Assert.That(activity1.Message, Is.EqualTo("message"));
                Assert.That(activity1.ThrowException, Is.EqualTo(true));
            }

            [Test]
            public void VariableIntegerTest()
            {
                var job = new WitJobVoid("testJob");
                var variable = new WitVariableInteger("var1", 3);

                Assert.That(variable.Name, Is.EqualTo("var1"));
                Assert.That(variable.Value, Is.EqualTo(3));

                var variable1 = (WitVariableInteger)variable.Clone();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("var1"));
                Assert.That(variable1.Value, Is.EqualTo(3));

                var variableStr = variable.Serialize("");
                variableStr.Deserialize(job);

                variable1 = (WitVariableInteger)job.Variables.Single();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("var1"));
                Assert.That(variable1.Value, Is.EqualTo(3));

                variableStr = "Int:var1 = 3;";

                job = new WitJobVoid("testJob");
                variableStr.Deserialize(job);

                variable1 = (WitVariableInteger)job.Variables.Single();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("var1"));
                Assert.That(variable1.Value, Is.EqualTo(3));

                variableStr = "Int:var1;";

                job = new WitJobVoid("testJob");
                variableStr.Deserialize(job);

                variable1 = (WitVariableInteger)job.Variables.Single();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("var1"));
                Assert.That(variable1.Value, Is.EqualTo(0));

                variableStr = "Int:var1 = Test(\"message\", true);";

                job = new WitJobVoid("testJob");
                variableStr.Deserialize(job);

                variable1 = (WitVariableInteger)job.Variables.Single();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("var1"));
                Assert.That(variable1.Value, Is.EqualTo(0));

                var activity1 = (WitActivitySpecialTest)job.Activities.Single();

                Assert.That(activity1.Return, Is.EqualTo("var1"));
                Assert.That(activity1.Message, Is.EqualTo("message"));
                Assert.That(activity1.ThrowException, Is.EqualTo(true));
            }

            [Test]
            public void VariableColorTest()
            {
                var job = new WitJobVoid("testJob");
                var variable = new WitVariableColor("color");

                Assert.That(variable.Name, Is.EqualTo("color"));
                Assert.That(variable.Value, Is.EqualTo(null));

                var variable1 = (WitVariableColor)variable.Clone();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("color"));
                Assert.That(variable1.Value, Is.EqualTo(null));

                var variableStr = variable.Serialize("");
                variableStr.Deserialize(job);

                variable1 = (WitVariableColor)job.Variables.Single();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("color"));
                Assert.That(variable1.Value, Is.EqualTo(null));

                variableStr = "Color:color;";

                job = new WitJobVoid("testJob");
                variableStr.Deserialize(job);

                variable1 = (WitVariableColor)job.Variables.Single();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("color"));
                Assert.That(variable1.Value, Is.EqualTo(null));

                variableStr = "Color:color = Test(\"message\", true);";

                job = new WitJobVoid("testJob");
                variableStr.Deserialize(job);

                variable1 = (WitVariableColor)job.Variables.Single();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("color"));
                Assert.That(variable1.Value, Is.EqualTo(null));

                var activity1 = (WitActivitySpecialTest)job.Activities.Single();

                Assert.That(activity1.Return, Is.EqualTo("color"));
                Assert.That(activity1.Message, Is.EqualTo("message"));
                Assert.That(activity1.ThrowException, Is.EqualTo(true));
            }

            [Test]
            public void VariableStringTest()
            {
                var job = new WitJobVoid("testJob");
                var variable = new WitVariableString("str", "text");

                Assert.That(variable.Name, Is.EqualTo("str"));
                Assert.That(variable.Value, Is.EqualTo("text"));

                var variable1 = (WitVariableString)variable.Clone();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("str"));
                Assert.That(variable1.Value, Is.EqualTo("text"));

                var variableStr = variable.Serialize("");
                variableStr.Deserialize(job);

                variable1 = (WitVariableString)job.Variables.Single();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("str"));
                Assert.That(variable1.Value, Is.EqualTo("text"));

                variableStr = "String:str;";

                job = new WitJobVoid("testJob");
                variableStr.Deserialize(job);

                variable1 = (WitVariableString)job.Variables.Single();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("str"));
                Assert.That(variable1.Value, Is.EqualTo(""));

                variableStr = "String:str = Test(\"message\", true);";

                job = new WitJobVoid("testJob");
                variableStr.Deserialize(job);

                variable1 = (WitVariableString)job.Variables.Single();
                Assert.That(variable, Is.Not.SameAs(variable1));
                Assert.That(variable1.Name, Is.EqualTo("str"));
                Assert.That(variable1.Value, Is.EqualTo(""));

                var activity1 = (WitActivitySpecialTest)job.Activities.Single();

                Assert.That(activity1.Return, Is.EqualTo("str"));
                Assert.That(activity1.Message, Is.EqualTo("message"));
                Assert.That(activity1.ThrowException, Is.EqualTo(true));
            }
        }
    }
}