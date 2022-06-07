using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNedelja3Vezbe.Api.Payment
{
    public class OrderProcessor
    {
        private IPaymentMethod _paymentMethod;
        public bool emailSent = false;

        public OrderProcessor(IPaymentMethod paymentMethod)
        {
            _paymentMethod = paymentMethod;
        }

        public void ProcessOrder(Order o)
        {
            emailSent = false;
            //Ispisati stavke
            foreach(var ol in o.Lines)
            {
                Console.WriteLine(ol.Name + ": " + ol.Price);
            }

            Console.WriteLine("Total: " + o.Lines.Sum(x => x.Price * x.Quantity));

            var result = _paymentMethod.Pay(o.Lines.Sum(x => x.Price * x.Quantity));

            if(!result)
            {
                throw new Exception("Placanje neuspesno.");
            }

            SendEmail();
        }

        public void ProcessOrder(IEnumerable<OrderLine> lines)
        {
            emailSent = false;
            //Ispisati stavke
            foreach (var ol in lines)
            {
                Console.WriteLine(ol.Name + ": " + ol.Price);
            }

            Console.WriteLine("Total: " + lines.Sum(x => x.Price * x.Quantity));

            var result = _paymentMethod.Pay(lines.Sum(x => x.Price * x.Quantity));

            if (!result)
            {
                throw new Exception("Placanje neuspesno.");
            }

            SendEmail();
        }

        private void SendEmail()
        {
            emailSent = true;
        }
    }



    public class Order
    {
        public IEnumerable<OrderLine> Lines { get; set; }
    }

    public class OrderLine
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
