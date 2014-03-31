using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{

  [Subject(typeof(SelectFactory<Person>))]
  public class SelectFactorySpecs
  {
    public abstract class concern : Observes<SelectFactory<Person>>
    {

    }

    public class when_creating : concern
    {
      Establish c = () =>
      {
        first_name_column_expr = x => x.first_name;
        birth_date_column_expr = x => x.birth_date;
        column_name_retriever = depends.on<IMapModelToTable<Person>>();
        column_name_retriever.setup(x => x.get_column_name(first_name_column_expr)).Return("first_name");
        column_name_retriever.setup(x => x.get_column_name(birth_date_column_expr)).Return("birth_date");
      };

      Because b = () =>
        result = sut.create(first_name_column_expr, birth_date_column_expr);

      It should_build_a_select_clause_using_specified_columns = () =>
        result.ShouldEqual("select first_name, birth_date");

      static string result;
      static IMapModelToTable<Person> column_name_retriever;
      static Expression<Func<Person, object>>  birth_date_column_expr;
      static Expression<Func<Person, object>>  first_name_column_expr;
    }
  }
}
