using System;
using System.Data;
using System.Data.Common;

namespace sql_string_generator.query_play_ground
{
  public class Customers
  {
    public string City;
    public string ContactName;

    public string Country;
    public string CustomerID;
    public string Phone;
  }

  public class Orders
  {
    public string CustomerID;

    public DateTime OrderDate;
    public int OrderID;
  }

  public class Northwind
  {
    public Query<Customers> Customers;

    public Query<Orders> Orders;

    public Northwind(IDbConnection connection)
    {
      QueryProvider provider = new DbQueryProvider(connection);

      Customers = new Query<Customers>(provider);

      Orders = new Query<Orders>(provider);
    }
  }
}