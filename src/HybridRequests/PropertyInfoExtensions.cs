using System.Reflection;

namespace HybridRequests
{
    public static class PropertyInfoExtensions
    {
        public static void ForceStateValue(this PropertyInfo propertyInfo, object currentObject, object value)
        {
            var backingFieldInfo = currentObject.GetType().GetField($"<{propertyInfo.Name}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);  
            if (backingFieldInfo != null)  
            {  
                backingFieldInfo.SetValue(currentObject, value.ToString());  
            }  
        }
    }
}