using System;
using BerlinClock.Classes;
using NUnit.Framework;

namespace NUnitTestProject
{
    public class TimeConeverterUnitTests
    {
        [Test]
        public void Throw_ArgumentNullException_When_Parameter_Is_Null()
        {
            //arrange
            string timeParameter = null;

            //assert
            Assert.Throws<ArgumentNullException>(() => new TimeConverter().convertTime(timeParameter));

        }

        [TestCase(" ")]
        [TestCase("ab:cd:ef")]
        [TestCase("1:02:03")]
        [TestCase("01:2:03")]
        [TestCase("01:02:3")]
        [TestCase("24:00:03")]
        [TestCase("01:60:03")]
        [TestCase("01:02:60")]
        [TestCase("25:00:00")]
        [TestCase("01-02-03")]
        [TestCase("01:02")]
        [TestCase("01:02:03:04")]
        public void Throw_FormatException_When_Parameter_Is_Invalid(string timeParameter)
        {
            //assert
            Assert.Throws<FormatException>(() => new TimeConverter().convertTime(timeParameter));
        }

        [Test]
        public void Midnight_00_00_00_Is_Passed()
        {
            //arrange
            var timeParameter = "00:00:00";
            var expectedResult = "Y" +
                                 Environment.NewLine +
                                 "OOOO" +
                                 Environment.NewLine +
                                 "OOOO" +
                                 Environment.NewLine +
                                 "OOOOOOOOOOO" +
                                 Environment.NewLine +
                                 "OOOO";

            //act
            var convertResult = new TimeConverter().convertTime(timeParameter);

            //assert
            Assert.AreEqual(expectedResult, convertResult);
        }

        [Test]
        public void Middle_Of_The_Afternoon_Is_Passed()
        {
            //arrange
            var timeParameter = "13:17:01";
            var expectedResult = "O" +
                                 Environment.NewLine +
                                 "RROO" +
                                 Environment.NewLine +
                                 "RRRO" +
                                 Environment.NewLine +
                                 "YYROOOOOOOO" +
                                 Environment.NewLine +
                                 "YYOO";

            //act
            var convertResult = new TimeConverter().convertTime(timeParameter);

            //assert
            Assert.AreEqual(expectedResult, convertResult);
        }

        [Test]
        public void Just_Before_Midnight_Is_Passed()
        {
            //arrange
            var timeParameter = "23:59:59";
            var expectedResult = "O" +
                                 Environment.NewLine +
                                 "RRRR" +
                                 Environment.NewLine +
                                 "RRRO" +
                                 Environment.NewLine +
                                 "YYRYYRYYRYY" +
                                 Environment.NewLine +
                                 "YYYY";

            //act
            var convertResult = new TimeConverter().convertTime(timeParameter);

            //assert
            Assert.AreEqual(expectedResult, convertResult);
        }

        [Test]
        public void Midnight_24_00_00_Is_Passed()
        {
            //arrange
            var timeParameter = "24:00:00";
            var expectedResult = "Y" +
                                 Environment.NewLine +
                                 "RRRR" +
                                 Environment.NewLine +
                                 "RRRR" +
                                 Environment.NewLine +
                                 "OOOOOOOOOOO" +
                                 Environment.NewLine +
                                 "OOOO";

            //act
            var convertResult = new TimeConverter().convertTime(timeParameter);

            //assert
            Assert.AreEqual(expectedResult, convertResult);
        }

    }
}