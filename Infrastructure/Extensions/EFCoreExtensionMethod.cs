using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Extensions
{
    public static class EFCoreExtensionMethod
    {
        public static void UpdatePartial<T>(this EntityEntry<T> item, T updateTarget) where T : class
        {
            var targetProperties = updateTarget.GetType().GetProperties();
            foreach (var property in targetProperties)
            {
                var targetItemValue = property.GetValue(updateTarget);
                if (targetItemValue == null)
                {
                    item.Property(property.Name).IsModified = false;
                }
                else
                {
                    var updatedProperty = item.Properties
                        .FirstOrDefault(x => x.Metadata.Name == property.Name && !x.Metadata.IsKey());
                    if (updatedProperty != null)
                    {
                        updatedProperty.IsModified = true;
                    }
                }
            }
        }
    }
}