using System.ServiceModel;

namespace Web_API_playground.Services.CalculatorSerice
{
    public class CalculatorService
    {
        
        [ServiceContract]
        public interface IcalculatorService
        {
            [OperationContract]
            int Add(int a, int b);
        }

        public class CalculatorServices : IcalculatorService
        {
            public int Add(int a, int b) => a + b;
        }
    }
}
