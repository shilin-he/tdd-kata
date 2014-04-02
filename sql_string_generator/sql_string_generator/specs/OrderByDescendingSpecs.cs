 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{   

  [Subject(typeof(OrderByDescending))]
  public class OrderByDescendingSpecs
  {
    public abstract class concern : Observes<OrderByDescending>
    {
        
    }

    public class when_apply_to : concern
    {
      Establish c = () =>
      {
        column_name = "product_name";
        depends.on(column_name);
      };

      Because b = () =>
        result = sut.apply_to(column_name);

      It returns_column_name_with_desc = () =>
        result.ShouldEqual("product_name desc");

      static string result;
      static string column_name;
    }
  }
}
