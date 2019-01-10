using BerlinClock.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BerlinClock.BDD
{
    [Binding]
    public class TheBerlinClockSteps
    {
        private ITimeConverter berlinClock = new TimeConverter();
        private string _theTime;

        
       [When(@"the time is ""((?:[01]\d|2[0-3]):(?:[0-5]\d):(?:[0-5]\d)|(?:24:00:00))""")]
        public void WhenTheTimeIs(string time)
        {
            _theTime = time;
        }
        
        [Then(@"the clock should look like")]
        public void ThenTheClockShouldLookLike(string theExpectedBerlinClockOutput)
        {
            Assert.AreEqual(berlinClock.convertTime(_theTime), theExpectedBerlinClockOutput);
        }

    }
}
