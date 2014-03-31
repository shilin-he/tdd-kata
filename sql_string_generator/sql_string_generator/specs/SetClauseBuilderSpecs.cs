 using System;
 using System.Collections;
 using System.Collections.Generic;
 using System.Linq.Expressions;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;
 using Rhino.Mocks;

namespace sql_string_generator.specs
{   

  [Subject(typeof(SetClauseBuilder<Product>))]
  public class SetClauseBuilderSpecs
  {
    public abstract class concern : Observes<SetClauseBuilder<Product>>
    {
        
    }

    public class when_building : concern
    {
      Establish c = () =>
      {
        mapping = depends.on<IMapModelToTable<Product>>();
        mapping.setup(x => x.get_column_name("name")).Return("product_name");
        mapping.setup(x => x.get_mapped_property_names()).Return(new[] {"name"});
        value = "abc";
        the_product = new Product {name = value};
        depends.on(the_product);
        val_to_sql_literal_converter = depends.on<IConvertValueToSqlLiteral>();
        val_to_sql_literal_converter.setup(x => x.convert(value)).Return("'abc'");
        set_clause = "set product_name='abc'";
        depends.on<IGetPropertyValueUsingPropertyName<Product>>((item, prop_name) =>
        {
          item.ShouldEqual(the_product);
          prop_name.ShouldEqual("name");
          return value;
        });
      };

      Because b = () =>
        result = sut.build(the_product);

      It should_build_a_set_clause = () =>
        result.ShouldEqual(set_clause);

      static string result;
      static string set_clause;
      static IMapModelToTable<Product> mapping;
      static Product the_product;
      static IConvertValueToSqlLiteral val_to_sql_literal_converter;
      static string value;
    }
  }
}
