using System;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace template_engine.specs
{

  [Subject(typeof(InstanceValueResolver))]
  public class InstanceValueFactorySpecs
  {
    public abstract class concern : Observes<InstanceValueResolver>
    {

    }

    public class when_resolve : concern
    {
      static Person person;

      Establish c = () =>
      {
        person = new Person { first_name = "foo", last_name = "bar", 
          address = new Address { street = "123 street", city = "meanwhile" } };
      };

      class and_the_instance_has_the_specified_property
      {
        class and_the_property_is_a_direct_property_of_the_instance
        {

          Establish c = () =>
          {
            instance_value = "foo";
          };

          Because b = () =>
            result = sut.resolve(person, "first_name");

          It returns_the_string_value_of_a_property_of_the_model_instance = () =>
            result.ShouldEqual(instance_value);

          static string result;
          static string instance_value;
        }

        class and_the_property_is_a_nested_property
        {
          Because b = () =>
            result = sut.resolve(person, "address.city");

          It returns_the_string_value_of_the_innermost_property = () =>
            result.ShouldEqual("meanwhile");

          static string result;
        }
      }

      class and_the_instance_does_not_have_the_specified_proerty
      {
        Because b = () =>
          spec.catch_exception(() => sut.resolve(person, "non_exist_property_name"));

        It throws_an_argument_out_of_range_exception = () =>
          spec.exception_thrown.ShouldBeOfType<ArgumentOutOfRangeException>();
      }
    }
  }
}
