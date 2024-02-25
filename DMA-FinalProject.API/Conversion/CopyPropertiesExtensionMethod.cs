using System.Reflection;

namespace DMA_FinalProject.API.Conversion
{
    public static class CopyPropertiesExtensionMethod
    {
        /// <summary>
        /// Finds all writeable properties on the destination object 
        /// and for each of them copies the values from the corresponding property on the source object
        /// with the same name and type, if it exists.
        /// </summary>
        /// <typeparam name="T">The type of object to copy the properties to</typeparam>
        /// <param name="sourceObject">The object to copy attribute values from</param>
        /// <param name="destinationObject">The object to copy attribute values to</param>
        /// <returns></returns>
        public static T CopyPropertiesTo<T>(this object sourceObject, T destinationObject)
        {
            foreach (PropertyInfo destinationProperty in typeof(T).GetProperties().Where(p => p.CanWrite))
            {
                if (!sourceObject.GetType().GetProperties().Any(sourceProp => sourceProp.Name == destinationProperty.Name && sourceProp.PropertyType == destinationProperty.PropertyType)) continue;
                var sourceProp = sourceObject.GetType().GetProperty(destinationProperty.Name);
                destinationProperty.SetValue(destinationObject, sourceProp.GetValue(sourceObject, null), null);
            }
            return destinationObject;
        }
    }
}
