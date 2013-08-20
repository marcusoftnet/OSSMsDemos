using Nancy;

namespace DemoNancy
{
    public class SampleModule : NancyModule
    {
        public SampleModule()
        {
            Get["/"] = _ => "Hello World!";
            Get["/Customer"] = _ =>
                {
                    var customer = new Customer {Name = "Marcus", Age = 32};
                    return View["customer.cshtml", customer];
                };
        }
    }

    public class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}