using UnityEngine;

namespace MVP.Framework.Core.States
{
    public class TypeSelectionFilter : PropertyAttribute
    {
        public string Category = "Default";

        public TypeSelectionFilter(string category)
        {
            this.Category = category;
        }
    }
}