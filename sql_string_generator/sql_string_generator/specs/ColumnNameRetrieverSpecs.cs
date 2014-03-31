 using System;
 using System.Linq.Expressions;
 using System.Security.Cryptography.X509Certificates;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;
 using Rhino.Mocks;

namespace sql_string_generator.specs
{   

  [Subject(typeof(ColumnNameRetriever<Person>))]
  public class ColumnNameRetrieverSpecs
  {
    public abstract class concern : Observes<ColumnNameRetriever<Person>>
    {
        
    }

    public class when_getting_column_name : concern
    {
      Establish c = () =>
      {
        first_name_expr = x => x.first_name; 
        model_to_table_mapper = depends.on<IMapModelToTable<Person>>();
        model_to_table_mapper.setup(x => x.get_column_name(first_name_expr)).Return("first_name");
      };

      Because b = () => result = sut.get_column_name(first_name_expr);

      It should_get_the_column_name = () =>
        result.ShouldEqual("first_name");

      static string result;
      static IMapModelToTable<Person> model_to_table_mapper;
      static Expression<Func<Person, string>>  first_name_expr;
    }
  }
}
