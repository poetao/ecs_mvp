using System;

namespace MVP.Framework.Core.States
{
    public class TypeSelectionEnable : Attribute
    {
        public string Category = "Default";

        public TypeSelectionEnable(string category)
        {
            this.Category = category;
        }
    }
}