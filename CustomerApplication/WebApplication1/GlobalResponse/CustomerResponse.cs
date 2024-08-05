using CustomerApplication.Models;

namespace CustomerApplication.GlobalResponse
{
    public class GlobalResponse
    {

        public record class CustomerResponse(bool result, Customer? Customer, string Message)
        {
           
        }
    }
}
