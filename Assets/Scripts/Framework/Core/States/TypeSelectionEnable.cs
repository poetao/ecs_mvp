using System;

namespace Framework.Core.States
{
    public class TypeSelectionEnable : Attribute
    {
        public readonly string Category;

        public TypeSelectionEnable(string category)
        {
            Category = category;
        }
    }
}