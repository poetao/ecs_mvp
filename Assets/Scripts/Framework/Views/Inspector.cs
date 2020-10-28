using System;
using MVP.Framework.Core;
using MVP.Framework.Core.Reflections;

namespace MVP.Framework.Views
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InspectorAttribute : Attribute
    {
    }

    public static class Inspector
    {
        public static void Bind(String name, ProxyParameter parameter, Component component)
        {
            Reflection.SetField(component, name, parameter.GetObjectValue());
        }
    }
}