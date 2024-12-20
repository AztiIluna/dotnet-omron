using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1;

public class Option<T> : OneOfBase<None, T>
{
    public Option(OneOf<None, T> _) : base(_) { }
    public Option() : base(new None()) { }

    public static Option<T> FromNullableValue(T? value)
    {
        if (value == null) return new None();
        return new Option<T>(value);
    }
    public bool IsNone => IsT0;
    public void IfSomething(Action<T> action)
    {
        Switch(
            none => { },
            value => action(value));
    }
    public bool CheckValue(T value)
    {
        return Match(
            none => false,
            some => some.Equals(value));
    }

    public bool CompareValue(Func<T, bool> comparer)
    {
        return Match(
            none => false,
            some => comparer(some));
    }

    public override string ToString()
    {
        return Match(
            none => "None",
            some => some.ToString());
    }

    public static implicit operator Option<T>(T _) => new Option<T>(_);
    public static explicit operator T(Option<T> _) => _.AsT1;

    public static implicit operator Option<T>(None _) => new Option<T>(_);
    public static explicit operator None(Option<T> _) => _.AsT0;
}
