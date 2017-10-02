using NUnit.Framework;

namespace OpenTSDBApiWrapper.Validators
{
    [TestFixture]
    public class RegexMetricTextFilterTests
    {
        [TestCase("Occasions-Accus,Batterijen,LadersAdapters", "Occasions-AccusBatterijenLadersAdapters")]
        [TestCase(" Occasions-Accus Batterijen LadersAdapters ", "Occasions-AccusBatterijenLadersAdapters")]
        [TestCase("\u0419Occasions-Accus\u0419Batterijen\u0419LadersAdapters\u0419", "Occasions-AccusBatterijenLadersAdapters")]
        [TestCase("vAlId0/\\_- !`[]#\u0419\t\n\r", "vAlId0/\\_-")]
        [TestCase(@" ", "")]
        [TestCase("\n\r\t", "")]
        [TestCase(@" = ", "")] 
        [TestCase(@"`", "")]
        [TestCase(@"[]|+}", "")]
        [TestCase(@"#*(*&^(", "")]
        [TestCase(@"|@#$", "")] 
        [TestCase(@"|", "")]
        [TestCase(@"|@#$", "")] 
        [TestCase(@"|", "")]
        [TestCase("\u0419", "")]
        [TestCase("»«@£§€", "")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789/.\\_-", "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789/.\\_-")]
        public void Filter_FromSubjects_ReturnsExpectedResult(string subject, string expectedResult)
        {
            var sut = new RegexMetricTextFilter();

            Assert.That(() => sut.Filter(subject), Is.EqualTo(expectedResult));
        }
    }
}
