using NUnit.Framework;

namespace OpenTSDBApiWrapper.Test.Validators
{
    [TestFixture]
    public class ValidatorHelperTests
    {
        [Test]
        public void TagAndMetricNameValidator_WhenAllLegalCharacters_ReturnsExpected()
        {
            string allLegalCharacters = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789/.\\_-";

            var result = ValidatorHelper.TagAndMetricNameValidator(allLegalCharacters);

            Assert.That(result, Is.EqualTo(allLegalCharacters));
        }

        [TestCase(@" "), TestCase("\n\r\t"),
         TestCase(@" = "), TestCase(@"`"), TestCase(@"[]|+}"), TestCase(@"#*(*&^("),
         TestCase(@"|@#$"), TestCase(@"|"), TestCase(@"|@#$"), TestCase(@"|"),
         TestCase("\u0419"), TestCase("»«@£§€")]
        public void TagAndMetricNameValidator_WhenAllIllegalCharacters_ReturnsEmpty(string value)
        {
            var result = ValidatorHelper.TagAndMetricNameValidator(value);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [TestCase("Occasions-Accus,Batterijen,LadersAdapters", "Occasions-AccusBatterijenLadersAdapters"),
         TestCase(" Occasions-Accus Batterijen LadersAdapters ", "Occasions-AccusBatterijenLadersAdapters"),
         TestCase("\u0419Occasions-Accus\u0419Batterijen\u0419LadersAdapters\u0419", "Occasions-AccusBatterijenLadersAdapters"),
         TestCase("vAlId0/\\_- !`[]#\u0419\t\n\r", "vAlId0/\\_-")]

        public void TagAndMetricNameValidator_WhenTestCase_returnsExpected(
            string value,
            string expectedResult)
        {
            var result = ValidatorHelper.TagAndMetricNameValidator(value);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
