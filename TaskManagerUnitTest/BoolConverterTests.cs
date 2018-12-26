using NUnit.Framework;
using TaskManagerView.Components;

namespace TaskManagerUnitTest
{
    [TestFixture]
    public class BoolConverterTests //Добавить тест Invalid
    {
        [TestCase(true, "Yes")]
        [TestCase(false, "No")]
        public void ConvertCorrectTest(bool boolValue, string expected)
        {
            var converter = new BoolConverter();
            var parameter = "No;Yes";

            Assert.AreEqual(expected, converter.Convert(boolValue, null, parameter, null));
        }

        [Test]
        public void ConvertNotBoolTest()
        {
            var converter = new BoolConverter();
            var parameter = "No;Yes";

            Assert.IsNull(converter.Convert("True", null, parameter, null));
        }

        [Test]
        public void ConvertWithNotStringParameterTest()
        {
            var converter = new BoolConverter();
            var parameter = new object();

            Assert.IsNull(converter.Convert(true, null, parameter, null));
        }

        [TestCase("")]
        [TestCase("true")]
        [TestCase("true;false;null")]
        public void ConvertWithInvalidLengthParameterTest(string parameter)
        {
            var converter = new BoolConverter();

            Assert.IsNull(converter.Convert(true, null, parameter, null));
        }

        [TestCase("No", false)]
        [TestCase("Yes", true)]
        public void ConvertBackCorrectTest(string value, bool expected)
        {
            var converter = new BoolConverter();
            var parameter = "No;Yes";

            Assert.AreEqual(expected, converter.ConvertBack(value, null, parameter, null));
        }

        [Test]
        public void ConvertBackInvalidValueTest()
        {
            var converter = new BoolConverter();
            var parameter = "No;Yes";

            Assert.AreEqual(null, converter.ConvertBack("InvalidValue", null, parameter, null));
        }

        [Test]
        public void ConvertBackNotStringTest()
        {
            var converter = new BoolConverter();
            var parameter = "No;Yes";

            Assert.IsNull(converter.ConvertBack(true, null, parameter, null));
        }

        [Test]
        public void ConvertBackWithNotStringParameterTest()
        {
            var converter = new BoolConverter();
            var parameter = new object();

            Assert.IsNull(converter.ConvertBack("Yes", null, parameter, null));
        }

        [TestCase("")]
        [TestCase("true")]
        [TestCase("true;false;null")]
        public void ConvertBackWithInvalidLengthParameterTest(string parameter)
        {
            var converter = new BoolConverter();

            Assert.IsNull(converter.ConvertBack("Yes", null, parameter, null));
        }
    }
}
