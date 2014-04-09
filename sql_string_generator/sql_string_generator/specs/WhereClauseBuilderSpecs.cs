using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
      class and_the_parameter_is_a_model_object
      {
        Establish c = () =>
        {
          person = new Person { id = 1 };
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
          id_property_names = new[] { id_property_name };
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

      class and_the_parameter_is_an_expression
      {
        Establish c = () =>
        {
          expr = x => x.last_name == "He";
          where_clause = "where last_name='He'";
          translator = depends.on<IExpressionToSqlTranslator<Person>>();
          translator.setup(x => x.translate(expr)).Return("last_name='He'");
        };

        Because b = () =>
          result = sut.build(expr);

        It converts_the_expression_into_a_sql_where_clause = () =>
          result.ShouldEqual(where_clause);

        static string result;
        static string where_clause;
        static Expression<Func<Person, bool>> expr;
        static IExpressionToSqlTranslator<Person> translator;
      }
    }
  }
}
