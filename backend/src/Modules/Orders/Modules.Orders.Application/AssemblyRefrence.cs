using System.Reflection;

namespace Modules.Orders.Application;

public static class AssemblyRefrence
{
    public static Assembly Assembly => typeof(AssemblyRefrence).Assembly;
}
