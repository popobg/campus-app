using CampusFrontEnd.App;

namespace CampusApp.Tests.Frontend.Tests
{
    public class CheckInputTest
    {

        [Theory]
        [InlineData("word", 1, 3)]
        [InlineData("1", 2, 3)]
        [InlineData("12", 2, 10)]
        public void CheckInt_WrongInput_ReturnsFalse(string input, int firstNumber, int lastNumber)
        {
            // Act
            bool actual = CheckInput.CheckInt(input, firstNumber, lastNumber);

            // Assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData("1", 1, 3)]
        [InlineData("2", 1, 3)]
        [InlineData("3", 1, 3)]
        public void CheckInt_CorrectInput_ReturnsTrue(string input, int firstNumber, int lastNumber)
        {
            bool actual = CheckInput.CheckInt(input, firstNumber, lastNumber);

            Assert.True(actual);
        }

        [Theory]
        [InlineData("*$ù:", MenuChoices.CHECK_PERSON_NAME)]
        [InlineData("*$ù:fef", MenuChoices.CHECK_COURSE_NAME)]
        [InlineData("", MenuChoices.CHECK_PERSON_NAME)]
        [InlineData("2", MenuChoices.CHECK_PERSON_NAME)]
        [InlineData("Paul3", MenuChoices.CHECK_PERSON_NAME)]
        [InlineData("3Paul3", MenuChoices.CHECK_PERSON_NAME)]
        [InlineData("Paul3ciosc", MenuChoices.CHECK_PERSON_NAME)]
        public void CheckName_IncorrectPersonName_ReturnsFalse(string name, int value)
        {
            bool actual = CheckInput.CheckName(name, value);

            Assert.False(actual);
        }

        [Theory]
        [InlineData("paul", MenuChoices.CHECK_PERSON_NAME)]
        [InlineData("Paul", MenuChoices.CHECK_PERSON_NAME)]
        [InlineData("jean-paul", MenuChoices.CHECK_PERSON_NAME)]
        [InlineData("jean paul", MenuChoices.CHECK_PERSON_NAME)]
        [InlineData("Jean-Paul", MenuChoices.CHECK_PERSON_NAME)]
        [InlineData("Jean Paul Sartre", MenuChoices.CHECK_PERSON_NAME)]
        [InlineData("José", MenuChoices.CHECK_PERSON_NAME)]
        [InlineData("François Verdier", MenuChoices.CHECK_PERSON_NAME)]
        public void CheckName_CorrectPersonName_ReturnsTrue(string name, int value)
        {
            bool actual = CheckInput.CheckName(name, value);

            Assert.True(actual);
        }

        [Theory]
        [InlineData("*$ù:", MenuChoices.CHECK_COURSE_NAME)]
        [InlineData("*$ù:fef", MenuChoices.CHECK_COURSE_NAME)]
        [InlineData("", MenuChoices.CHECK_COURSE_NAME)]
        public void CheckName_IncorrectCourseName_ReturnsFalse(string name, int value)
        {
            bool actual = CheckInput.CheckName(name, value);

            Assert.False(actual);
        }

        [Theory]
        [InlineData("2", MenuChoices.CHECK_COURSE_NAME)]
        [InlineData("v", MenuChoices.CHECK_COURSE_NAME)]
        [InlineData("v2", MenuChoices.CHECK_COURSE_NAME)]
        [InlineData("v2 ", MenuChoices.CHECK_COURSE_NAME)]
        [InlineData("histoire-géo", MenuChoices.CHECK_COURSE_NAME)]
        [InlineData("histoire géo", MenuChoices.CHECK_COURSE_NAME)]
        [InlineData("Histoire2 Géo", MenuChoices.CHECK_COURSE_NAME)]
        [InlineData("Français", MenuChoices.CHECK_COURSE_NAME)]
        public void CheckName_CorrectCourseName_ReturnsTrue(string name, int value)
        {
            bool actual = CheckInput.CheckName(name, value);

            Assert.True(actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("2")]
        [InlineData("21-02-2003")]
        [InlineData("21-02-03")]
        [InlineData("ioeevzv")]
        [InlineData("50/02/2003")]
        [InlineData("5/2/2003")]
        [InlineData("12/13/2003")]
        public void CheckDateTime_IncorrectInput_ReturnsFalse(string input)
        {
            bool actual = CheckInput.CheckDateTime(input);

            Assert.False(actual);
        }

        [Theory]
        [InlineData("21/06/2014")]
        [InlineData("01/06/1920")]
        [InlineData("03/10/1850")]
        [InlineData("10/11/2050")]
        public void CheckDateTime_CorrectInput_ReturnsTrue(string input)
        {
            bool actual = CheckInput.CheckDateTime(input);

            Assert.True(actual);
        }

        [Theory]
        [InlineData("-20")]
        [InlineData("")]
        [InlineData("ccishofe")]
        [InlineData("30")]
        [InlineData("30zicho")]
        public void CheckInputGrade_IncorrectGrade_ReturnsFalse(string input)
        {
            bool actual = CheckInput.CheckDateTime(input);

            Assert.False(actual);
        }

        [Theory]
        [InlineData("20")]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("15")]
        public void CheckInputGrade_CorrectGrade_ReturnsTrue(string input)
        {
            bool actual = CheckInput.CheckInputGrade(input);

            Assert.True(actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("ojpeve")]
        [InlineData("yes")]
        [InlineData("no")]
        [InlineData("NO")]
        [InlineData("0")]
        [InlineData("1")]
        public void ConfirmationCheck_WrongInput_ReturnsFalse(string input)
        {
            bool actual = CheckInput.ConfirmationCheck(input);

            Assert.False(actual);
        }

        [Theory]
        [InlineData("y")]
        [InlineData("n")]
        public void ConfirmationCheck_CorrectInput_ReturnsTrue(string input)
        {
            bool actual = CheckInput.ConfirmationCheck(input);

            Assert.True(actual);
        }
    }
}
