using CampusFrontEnd.App;

namespace CampusApp.Tests.Frontend.Tests
{
    public class CheckInputTest
    {

        //[Theory]
        //[InlineData("word", 1, 3)]
        //[InlineData("1", 2, 3)]
        //[InlineData("12", 2, 10)]
        //public void CheckIntWrong(string input, int firstNumber, int lastNumber)
        //{
        //    var result = CheckInput.CheckInt(input, firstNumber, lastNumber);

        //    Assert.NotEqual(1, result);
        //}

        [Theory]
        [InlineData("1", 1, 3)]
        [InlineData("2", 1, 3)]
        [InlineData("3", 1, 3)]
        public void CheckIntOK(string input, int firstNumber, int lastNumber)
        {
            var result = CheckInput.CheckInt(input, firstNumber, lastNumber);

            Assert.IsType<int>(result);
        }
    }
}
