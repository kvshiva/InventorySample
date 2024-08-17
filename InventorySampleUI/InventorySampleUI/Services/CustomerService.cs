using InventorySampleUI.Model;

namespace InventorySampleUI.Services
{
    public class CustomerService : ICustomerService
    {
        public ResponseDto GetList()
        {
            var customers = new List<Customer>(){
                new Customer
                {
                    Id = 1,
                    Name = "مشتری 1"
                },
                new Customer
                {
                    Id = 2,
                    Name = "مشتری 2"
                }
            };
            var result = new ResponseDto()
            {
                Count = customers.Count,
                Data = customers
            };
            return result;
        }
    }
}
