using AspNedelja3Vezbe.Api.Payment;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNedelja3Vezbe.Tests.SOLID
{
    /*
    SOLID Principles
    https://stackify.com/solid-design-principles/

    S - Single responsibility principle (Princip jedne odgovornosti)
    O - Open/Closed Principle
    L - Liskov Substitution Principle
    I - Interface seggregation principle
    D - Dependency Inversion Principle
    
     */
    public class SolidTests
    {
        [Fact]
        public void PaymentProcessorThrowsException_WhenPaymentFails()
        {
            var processor = new OrderProcessor(new TestPaymentMethod());
            var order = Order;

            Action a = () => processor.ProcessOrder(order);

            a.Should().ThrowExactly<Exception>().WithMessage("Placanje neuspesno.");
            processor.emailSent.Should().BeFalse();
            //
        }

        [Fact]
        public void EmailSent_WhenPaymentDoesntFail()
        {
            var mock = new Mock<IPaymentMethod>();

            mock.Setup(x => x.Pay(360)).Returns(true);

            var paymentMethod = mock.Object;

            var processor = new OrderProcessor(paymentMethod);

            var order = Order;

            Action a = () => processor.ProcessOrder(order);

            a.Should().NotThrow();
            processor.emailSent.Should().BeTrue();

            mock.Verify(x => x.Pay(360), Times.Once);
        }

        public class TestPaymentMethod : IPaymentMethod
        {
            public bool Pay(decimal amount)
            {
                return false;
            }
        }

        public Order Order => new Order
        {
            Lines = new List<OrderLine> 
            { 
                new OrderLine
                {
                    Name = "OL 1",
                    Price = 100,
                    Quantity = 3
                },
                new OrderLine
                {
                    Name = "OL 2",
                    Price = 30,
                    Quantity = 2
                }
            }
        };
    }
}
