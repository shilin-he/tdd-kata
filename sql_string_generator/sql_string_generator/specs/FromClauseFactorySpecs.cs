 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{   

  [Subject(typeof(FromClauseBuilder<Person>))]
  public class FromClauseFactorySpecs
  {
    public abstract class concern : Observes<FromClauseBuilder<Person>>
    {
        
    }

    public class when_creating : concern
    {
      Establish c = () =>
      {
        table_mapping = depends.on<IMapModelToTable<Person>>();
        table_mapping.setup(x => x.get_table_name()).Return("people");
      };

      Because b = () =>
        result = sut.build();

      It should_create_a_from_clause_using_the_table_name = () =>
        result.ShouldEqual("from people");

      static string result;
      static IMapModelToTable<Person> table_mapping;
    }
  }
}
