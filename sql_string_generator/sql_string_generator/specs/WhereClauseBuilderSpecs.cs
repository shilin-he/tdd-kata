 using System.Collections.Generic;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{   

  [Subject(typeof(WhereClauseBuilder<Person>))]
  public class WhereClauseBuilderSpecs
  {
    public abstract class concern : Observes<WhereClauseBuilder<Person>>
    {
        
    }

    public class when_building_sql_where_clause : concern
    {
      Establish c = () =>
      {
        person = new Person {id = 1};
        depends.on(person);

        id_property_name = "id";
        depends.on<IGetPropertyValueUsingPropertyName<Person>>((item, prop_name) =>
        {
          item.ShouldEqual(person);
          prop_name.ShouldEqual(id_property_name);
          return 1;
        });

        mapping = depends.on<IMapModelToTable<Person>>();
        id_column_name = "product_id";
        id_property_names = new[] {id_property_name};
        mapping.setup(x => x.get_id_property_names()).Return(id_property_names);
        mapping.setup(x => x.get_id_column_name(id_property_name)).Return(id_column_name);
        where_clause = "where product_id=1";
      };

      Because b = () =>
        result = sut.build(person);

      It should_build_a_where_clause_using_id_columns = () =>
        result.ShouldEqual(where_clause);

      static string result;
      static string where_clause;
      static Person person;
      static IMapModelToTable<Person> mapping;
      static IEnumerable<string> id_property_names;
      static IGetPropertyValueUsingPropertyName<Person> property_value_getter;
      static string id_property_name;
      static string id_column_name;
    }
  }
}
