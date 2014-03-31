using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{

  [Subject(typeof(SelectBuilder<Person>))]
  public class SelectGeneratorSpecs
  {
    public abstract class concern : Observes<SelectBuilder<Person>>
    {

    }

    public class when_generating_select_statements : concern
    {
      Establish c = () =>
      {
        first_name_column_expr = x => x.first_name;
        select_factory = depends.on<ICreateSelectClauses<Person>>();
        select_factory.setup(x => x.create(first_name_column_expr)).Return("select first_name");
        factory = depends.on<IBuildFromClauses<Person>>();
        factory.setup(x => x.build()).Return("from people");
        sql_string = "select first_name" + Environment.NewLine + "from people";
        depends.on(new[] {first_name_column_expr});
      };

      Because b = () =>
        result = sut.generate();

      It should_output_a_select_sql_string = () =>
        result.ShouldEqual(sql_string);

      static string result;
      static string sql_string;
      static ICreateSelectClauses<Person> select_factory;
      static IBuildFromClauses<Person> factory;
      static Expression<Func<Person, object>> first_name_column_expr;
    }


  }
}
