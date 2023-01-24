using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using UdemyUnitTest.APP;
using Xunit;

namespace UdemyUnitTest.Test
{
    public class CalculatorTest
    {
        public Calculator calculator { get; set; }
        public Mock<ICalculatorService> myMock { get; set; }

        public CalculatorTest()
        {
            myMock = new Mock<ICalculatorService>();
            this.calculator = new Calculator(myMock.Object); // Taklit
            //  calculator = new Calculator(new CalculatorService());

        }



        //[Fact]
        public void AddTest1()
        {
            //ARRANGE

            int a = 5;
            int b = 20;


            //ACT

            var total = calculator.Add(a, b);

            //ASSERT

            Assert.Equal<int>(25, total);


        }


        //[Fact] //Contains &&  DoesNotContain
        public void AddTest2()
        {
            Assert.Contains("Fatih", "Fatih Çakıroğlu");

            Assert.DoesNotContain("Fatih", "Fatih Çakıroğlu");


            var names = new List<string>() { "Fatih", "Emre" };
            Assert.Contains(names, x => x == "Fatih");


        }

        //[Fact] //True && False
        public void AddTest3()
        {
            //  Assert.True(5 > 2);

            //  Assert.False(5 < 2);

            //  Assert.True("".GetType()==typeof(string));

            //  Assert.True("".GetType()==typeof(int));

        }


        //[Fact] //Matches && DoesNotMatch  // dog ile başlayan ifadeleri arar.
        public void AddTest4()
        {
            var regEx = "^dog";

            Assert.Matches(regEx, "dog fatih");

            Assert.DoesNotMatch(regEx, "adog fatih");

        }


        //[Fact] // StartsWith && EndsWith // ifadeyle başlayanları arar && son durumu arar.
        public void AddTest5()
        {
            Assert.StartsWith("Bir", "Bir masal");

            Assert.EndsWith("masal", "Bir masal");
        }

        //[Fact] // Empty && NotEmpty // aldığı dizinin boş olup olmadığını kontrol eder.
        public void AddTest6()
        {
            Assert.Empty(new List<string>());

            Assert.NotEmpty(new List<string>() { "uğur" });

        }

        //[Fact] // InRange && NotInRange // gelen değerin belli bir aralıkta olup olmadığını kontrol eder.
        public void AddTest7()
        {
            Assert.InRange(10, 2, 20);

            Assert.NotInRange(10, 2, 20);
        }

        //[Fact] //Single  // Bir elemanı varsa true fazla varsa false döner.
        public void AddTest8()
        {
            Assert.Single(new List<string>() { "uğur" }); //True

            Assert.Single(new List<string>() { "uğur", "kadir" }); //False

            Assert.Single<int>(new List<int>() { 1 });

        }

        //[Fact] //IsType && IsNotType // içerideki ifadenin tipinin doğru olup olmadığını kontrol eder.
        public void AddTest9()
        {
            Assert.IsType<string>("uğur");

            Assert.IsNotType<string>(1);

        }

        //[Fact] //IsAssignableFrom // bir tipin bir tipe referans verip veremeyeceğini kontrol eder.
        public void AddTest10()
        {
            Assert.IsAssignableFrom<IEnumerable<string>>(new List<string>());

            Assert.IsAssignableFrom<Object>("fatih");

        }

        //[Fact] // Null && NotNull // içerisine verdiğimiz ifade null ise true döner.
        public void AddTest11()
        {
            string value = null;
            Assert.Null(value);
            Assert.NotNull(value);
        }

        //[Fact] // Equal && NotEqual // iki değeri karşılaştırmak için kullanılır
        public void AddTest12()
        {
            Assert.Equal<int>(2, 2);
        }

        //[Theory]
        //[InlineData(2,5,7)]
        public void AddTest13(int a, int b, int expectedTotal)
        {


            var actualData = calculator.Add(a, b);

            Assert.Equal(expectedTotal, actualData);

        }


        [Theory]
        [InlineData(2, 5, 7)] //İsimlendirme kuralı best practice
        public void Add_SimpleValues_ReturnTotalValue(int a, int b, int expectedTotal)
        {

            myMock.Setup(x => x.Add(a, b)).Returns(expectedTotal);

            var actualData = calculator.Add(a, b);

            Assert.Equal(expectedTotal, actualData);

            myMock.Verify(x => x.Add(a, b), Times.Once);

        }

        //[Theory]
        //[InlineData(0, 5, 0)]
        //[InlineData(10, 0, 0)]
        public void Add_ZeroValues_ReturnZeroValue(int a, int b, int expectedTotal)
        {
            var actualData = calculator.Add(a, b);

            Assert.Equal(expectedTotal, actualData);

        }


        [Theory]
        [InlineData(3, 5, 15)]
        public void Multip_SimpleValues_ReturnMultipValue(int a, int b, int expectedValue)
        {
            int actualMultip =0;

            myMock.Setup(x => x.Multip(It.IsAny<int>(), It.IsAny<int>()))
                .Callback<int, int>((x, y) => actualMultip = x * y);

            calculator.Multip(a, b);

            Assert.Equal(expectedValue,actualMultip );
            
            calculator.Multip(5, 20);

            Assert.Equal(100,actualMultip);

        }


        [Theory]
        [InlineData(0, 5, 10)]
        public void Multip_ZeroValue_ReturnsException(int a, int b, int expectedTotal)
        {
            myMock.Setup(x => x.Multip(a, b)).Throws(new Exception("a=0 olamaz"));

            Exception exception = Assert.Throws<Exception>(() => calculator.Multip(a, b));
            Assert.Equal("a=0 olamaz", exception.Message);

        }


    }
}
