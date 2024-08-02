using CustomerApplication.Models;

namespace CustomerApplication.GlobalResponse
{
    public class GlobalResponse
    {

        public record class CustomerResponse(bool Flag, Customer? Customer, string Message);

    }
}
