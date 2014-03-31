using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;
using Rhino.Mocks.Constraints;

namespace sql_string_generator.specs
{

  [Subject(typeof(TableMapping<Person>))]
  public class TableMappingSpecs
  {
    public abstract class concern : Observes<TableMapping<Person>>
    {

    }

    public class when_getting_column_name : concern
    {
      class and_the_column_has_been_mapped
      {
        Establish c = () =>
        {
          prop_expr = x => x.last_name;
        };

        Because b = () =>
        {
          sut.map_column(prop_expr, "lname");
          result = sut.get_column_name(prop_expr);
        };

        It should_return_the_mapped_column_name = () =>
          result.ShouldEqual("lname");

        static string result;
        static Expression<Func<Person, object>> prop_expr;
      }

      class and_the_column_has_not_been_mapped
      {
        Establish c = () =>
        {
          prop_expr = x => x.first_name;
        };

        Because b = () =>
        {
          result = sut.get_column_name(prop_expr);
        };

        It should_return_the_prop_name_as_the_default = () =>
          result.ShouldEqual("first_name");

        static string result;
        static Expression<Func<Person, object>> prop_expr;

      }

      class and_passing_in_a_property_name_as_parameter
      {
        Establish c = () =>
        {
          prop_expr = x => x.last_name;
          prop_name = "last_name";
          column_name = "lname";
        };

        Because b = () =>
        {
          sut.map_column(prop_expr, column_name);
          result = sut.get_column_name(prop_name);
        };

        It should_get_the_registered_column_name = () =>
          result.ShouldEqual(column_name);

        static string result;
        static string column_name;
        static string prop_name;
        static Expression<Func<Person, object>> prop_expr;
      }
    }

    class when_getting_mapped_property_names : concern
    {
      Establish c = () =>
      {
        prop_expr1 = x => x.last_name;
        prop_expr2 = x => x.first_name;
      };

      Because b = () =>
      {
        sut.map_column(prop_expr1, "lname");
        sut.map_column(prop_expr2, "fname");
        result = sut.get_mapped_property_names();
      };

      It should_return_all_mapped_property_names = () =>
        result.ShouldEqual(new[]{"last_name", "first_name"});

      static IEnumerable<string> result;
      static Expression<Func<Person, object>> prop_expr1;
      static Expression<Func<Person, object>> prop_expr2;
    }

    class when_getting_id_property_names : concern
    {
      
      Establish c = () =>
      {
        id_expr = x => x.id;
      };

      Because b = () =>
      {
        id_property_names = new[] {"id"};
        sut.map_id(id_expr, "person_id");
        result = sut.get_id_property_names();
      };

      It should_get_id_property_names = () =>
        result.ShouldEqual(id_property_names);

      static IEnumerable<string> result;
      static Expression<Func<Person, object>> id_expr;
      static IEnumerable<string> id_property_names;
    }



    class when_geting_table_name : concern
    {
      class and_the_table_has_been_registered
      {
        Because b = () =>
        {
          sut.map_table_name("people_tbl");
          result = sut.get_table_name();
        };

        It should_return_the_registered_table_name = () =>
          result.ShouldEqual("people_tbl");

        static string result;
      }

      class and_the_table_has_not_been_registered
      {
        Because b = () =>
        {
          result = sut.get_table_name();
        };

        It should_return_the_model_name_as_the_default = () =>
          result.ShouldEqual("Person");

        static string result;
      }
    }

  }
}
