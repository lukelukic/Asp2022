using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNedelja3Vezbe.Api.Payment
{
    public interface IPaymentMethod
    {
        bool Pay(decimal amount);
        
    }

    public interface ILogger
    {
        void Log(string msg);
    }

    public class CreditCardPaymentMethod : IPaymentMethod, ILogger
    {
        private string _cardNumber;
        private int _ccv;
        private string exp; //"01/22"

        public CreditCardPaymentMethod(string cardNumber, int ccv, string exp)
        {
            _cardNumber = cardNumber;
            _ccv = ccv;
            this.exp = exp;
        }

        public void Log(string msg)
        {
            throw new NotImplementedException();
        }

        public bool Pay(decimal amount)
        {
            Console.WriteLine("Paying with card....");
            return true;
        }
    }

    public class PaypalPaymentMethod : IPaymentMethod
    {
        public bool Pay(decimal amount)
        {
            Console.WriteLine("Paying with card....");
            return true;
        }
    }

    public class BankPaymentMethod : IPaymentMethod
    {
        public bool Pay(decimal amount)
        {
            Console.WriteLine("Paying with bank transfer....");
            return true;
        }
    }


}
