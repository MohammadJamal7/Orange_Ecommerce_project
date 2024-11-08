using latayef.Models;

namespace Ecommerce_Project.ViewModels
{
    public class AllTypesTestioViewModel
    {
      public  List<Testimonial> pendingTestios {  get; set; }
      public   List<Testimonial> AcceptedTestios { get; set; }
      public   List<Testimonial> RejectedTetios { get; set; }
    }
}
