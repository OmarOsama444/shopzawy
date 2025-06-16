using System.Reflection;

namespace Modules.Orders.Presentation;

public static class AssemblyRefrence
{
    public static Assembly Assembly => typeof(AssemblyRefrence).Assembly;
}
