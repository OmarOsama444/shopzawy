using System.Reflection;

namespace Modules.Catalog.Presentation;

public static class AssemblyRefrence
{
    public static Assembly Assembly => typeof(AssemblyRefrence).Assembly;
}
