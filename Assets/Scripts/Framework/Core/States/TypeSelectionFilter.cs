using UnityEngine;

namespace Framework.Core.States
{
    public class TypeSelectionFilter : PropertyAttribute
    {
        public readonly string Category;

        public TypeSelectionFilter(string category)
        {
            Category = category;
        }
    }
}