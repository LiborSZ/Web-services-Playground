using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using static Web_API_playground.Services.CalculatorSerice.CalculatorService;

public class CalculatorServiceClient : ClientBase<IcalculatorService>, IcalculatorService
{
    public CalculatorServiceClient(Binding binding, EndpointAddress endpointAddress) :
           base(binding, endpointAddress)
    {
    }

    public int Add(int a, int b)
    {
        return Channel.Add(a, b);
    }
}