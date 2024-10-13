using OutWit.WitEngine.Shared.Utils;

namespace OutWit.WitEngine.Shared.Tests
{
    [TestFixture]
    public class UtilsTests
    {
        [Test]
        public void ReadJobNameTest()
        {
            var str = "Job:TestJob1(String:param,Double:param){somecode}";
            Assert.That(StringOperations.ReadJobName(str), Is.EqualTo("TestJob1"));
        }

        [Test]
        public void FindBodyTest()
        {
            var str = "operator1(param){operator2(param){operator3();}operator4();}";
            StringOperations.FindBody(str, out var header, out var body);

            Assert.That(header, Is.EqualTo("operator1(param)"));
            Assert.That(body, Is.EqualTo("operator2(param){operator3();}operator4();"));
        }

        [Test]
        public void SplitBodyTest()
        {
            var str = "operator2(param){operator3();}operator4();";

            var operators = new List<string>();

            StringOperations.SplitBody(str, operators);

            Assert.That(operators.Count, Is.EqualTo(2));
            Assert.That(operators[0], Is.EqualTo("operator2(param){operator3();}"));
            Assert.That(operators[1], Is.EqualTo("operator4();"));
        }

        [Test]
        public void ReadOperatorParametersTest()
        {
            var str = "operator(param1,param2,param3);";
            var parameters = StringOperations.ReadOperatorParameters("operator", str);
            Assert.That(parameters.Length, Is.EqualTo(3));
            Assert.That(parameters[0], Is.EqualTo("param1"));
            Assert.That(parameters[1], Is.EqualTo("param2"));
            Assert.That(parameters[2], Is.EqualTo("param3"));
        }

        [Test]
        public void WriteOperatorHeaderTest()
        {
            var str = StringOperations.WriteOperatorHeader("operator", new[] { "param1", "param2", "param3" }, "prefix");
            Assert.That(str, Is.EqualTo("prefixoperator(param1, param2, param3)"));
        }

        [Test]
        public void RemoveSymbolsTest()
        {
            var str = "121314151617";
            Assert.That(str.RemoveSymbols("1"), Is.EqualTo("234567"));
            Assert.That(str.RemoveSymbols("14"), Is.EqualTo("23567"));
        }

        [Test]
        public void TrimAllTest()
        {
            var str = "1  2\n3 \t 4 \r 5 ";
            Assert.That(str.TrimAll(), Is.EqualTo("12345"));

            str = "12~comment~3~comment1~4\n5 ";
            Assert.That(str.TrimAll(), Is.EqualTo("12345"));
        }

        [Test]
        public void SplitOperatorsTest()
        {
            var str = "part1,part2,,part3:part4:,::part5";
            var split = str.SplitOperators(",:");
            Assert.That(split.Length, Is.EqualTo(5));
            Assert.That(split[0], Is.EqualTo("part1"));
            Assert.That(split[1], Is.EqualTo("part2"));
            Assert.That(split[2], Is.EqualTo("part3"));
            Assert.That(split[3], Is.EqualTo("part4"));
            Assert.That(split[4], Is.EqualTo("part5"));
        }

        [Test]
        public void BetweenTest()
        {
            var str = "0123456789";
            Assert.That(str.Between(4, 7), Is.EqualTo("4567"));
        }

        [Test]
        public void FromTest()
        {
            var str = "0123456789";
            Assert.That(str.From(4), Is.EqualTo("456789"));
        }

        [Test]
        public void ToTest()
        {
            var str = "0123456789";
            Assert.That(str.To(4), Is.EqualTo("01234"));
        }
    }
}