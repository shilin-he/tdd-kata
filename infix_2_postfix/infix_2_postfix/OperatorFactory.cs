using System;
using System.Collections.Generic;

namespace tdd_kata
{
  public class OperatorFactory
  {
    static readonly IDictionary<char, Operator> registry = new Dictionary<char, Operator>
    {
      {'^', new Operator(4, Associativity.Right, "^")},
      {'*', new Operator(3, Associativity.Left, "*")},
      {'/', new Operator(3, Associativity.Left, "/")},
      {'+', new Operator(2, Associativity.Left, "+")},
      {'-', new Operator(2, Associativity.Left, "-")},
    };

    public static Operator create(char symbol)
    {
      if (!registry.ContainsKey(symbol))
        throw new ArgumentException("Invalid Operator Symbol.");

      return registry[symbol];
    }
  }
}