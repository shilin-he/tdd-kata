using System.Net.Sockets;

namespace template_engine.specs
{
  public class Person
  {
    public string first_name { get; set; }
    public string last_name { get; set; }
    public Address address { get; set; }
  }

  public class Address
  {
    public int id { get; set; }
    public string street { get; set; }
    public string city { get; set; }
  }
}