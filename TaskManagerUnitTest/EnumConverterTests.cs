using System;
using NUnit.Framework;
using TaskManagerView.Components;

namespace TaskManagerUnitTest
{
    [TestFixture]
    public class EnumConverterTests
    {
        public enum TestEnum
        {
            [System.ComponentModel.Description("DescriptionA")]
            A = 0,
            [System.ComponentModel.Description("DescriptionB")]
            B = 1,
            [System.ComponentModel.Description("DescriptionC")]
            C = 2
        }

        [TestCase(TestEnum.A, "DescriptionA")]
        [TestCase(TestEnum.B, "DescriptionB")]
        [TestCase(TestEnum.C, "DescriptionC")]
        public void ConvertCorrectTest(TestEnum value, string expected)
        {
            var converter = new EnumConverter();
            var parameter = typeof (TestEnum);

            Assert.AreEqual(expected, converter.Convert(value, null, parameter, null));
        }

        [Test]
        public void ConvertInvalidTest()
        {
            var converter = new EnumConverter();
            var parameter = typeof(TestEnum);

            Assert.AreEqual(string.Empty, converter.Convert("InvalidValue", null, parameter, null));
        }

        [Test]
        public void ConvertInvalidParameterTest()
        {
            var converter = new EnumConverter();
            var parameter = new object();

            Assert.Throws<ArgumentException>(() => converter.Convert(TestEnum.A, null, parameter, null));
        }

        [Test]
        public void ConvertNullValueTest()
        {
            var converter = new EnumConverter();
            var parameter = typeof(TestEnum);

            Assert.AreEqual(string.Empty, converter.Convert(null, null, parameter, null));
        }

        [TestCase("DescriptionA", TestEnum.A)]
        [TestCase("DescriptionB", TestEnum.B)]
        [TestCase("DescriptionC", TestEnum.C)]
        public void ConvertBackCorrectTest(string value, TestEnum expected)
        {
            var converter = new EnumConverter();
            var parameter = typeof(TestEnum);

            Assert.AreEqual(expected, converter.ConvertBack(value, null, parameter, null));
        }

        [Test]
        public void ConvertBackInvalidTest()
        {
            var converter = new EnumConverter();
            var parameter = typeof(TestEnum);

            Assert.AreEqual(null, converter.ConvertBack("InvalidDescription", null, parameter, null));
        }

        [Test]
        public void ConvertBackInvalidParameterTest()
        {
            var converter = new EnumConverter();
            var parameter = new object();

            Assert.AreEqual(null, converter.ConvertBack("DescriptionA", null, parameter, null));
        }

        [Test]
        public void ConvertBackNullValueTest()
        {
            var converter = new EnumConverter();
            var parameter = typeof(TestEnum);

            Assert.AreEqual(null, converter.ConvertBack(null, null, parameter, null));
        }
    }
}
