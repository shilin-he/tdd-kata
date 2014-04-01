using System;
using System.Threading;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace sql_string_generator
{

  [Subject(typeof(ValuesClauseBuilder<Product>))]
  public class ValuesClauseBuilderSpecs
  {
    public abstract class concern : Observes<ValuesClauseBuilder<Product>>
    {

    }

    public class when_build : concern
    {
      class and_no_mapping_exists
      {
        Establish c = () =>
        {
          the_product = new Product {name = "test_product", discontinued = false};
          mapping = depends.on<IMapModelToTable<Product>>();
          val_to_sql_literal_converter = depends.on<IConvertValueToSqlLiteral>();
          val_to_sql_literal_converter.setup(x => x.convert("test_product")).Return("'test_product'");
          val_to_sql_literal_converter.setup(x => x.convert(false)).Return("0");
        };

        Because b = () =>
          spec.catch_exception(() => sut.build(the_product));

        It throws_an_argument_exceptiono = () =>
          spec.exception_thrown.ShouldBeOfType<ArgumentException>();

        static Product the_product;
        static IMapModelToTable<Product> mapping;
        static IConvertValueToSqlLiteral val_to_sql_literal_converter;
      }

      class and_the_mapping_exists
      {
        Establish c = () =>
        {
          the_product = new Product {name = "test_product", discontinued = false};
          mapping = depends.on<IMapModelToTable<Product>>();
          mapping.setup(x => x.get_mapped_property_names()).Return(new[] { "name", "discontinued" });
          mapping.setup(x => x.get_column_name("name")).Return("product_name");
          mapping.setup(x => x.get_column_name("discontinued")).Return("discontinued");
          depends.on<IGetPropertyValueUsingPropertyName<Product>>((item, prop) =>
          {
            item.ShouldEqual(the_product);
            if (prop == "name") return "test_product";
            if (prop == "discontinued") return false;
            throw new ArgumentException();
          });
          val_to_sql_literal_converter = depends.on<IConvertValueToSqlLiteral>();
          val_to_sql_literal_converter.setup(x => x.convert("test_product")).Return("'test_product'");
          val_to_sql_literal_converter.setup(x => x.convert(false)).Return("0");
        };

        Because b = () =>
          result = sut.build(the_product);

        It builds_the_values_clause = () =>
          result.ShouldEqual("(product_name,discontinued) values ('test_product',0)");

        static string result;
        static Product the_product;
        static IMapModelToTable<Product> mapping;
        static IConvertValueToSqlLiteral val_to_sql_literal_converter;

      }
    }
  }
}
