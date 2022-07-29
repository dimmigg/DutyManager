using System;
using System.ComponentModel;

namespace DutyManager.Models
{
    internal class TableProperty
    {
        internal PropertyDescriptor Descriptor;
        internal string Name;
        internal TableProperty() { }
        internal TableProperty(PropertyDescriptor descriptor, string name) =>
            (Descriptor, Name) = (descriptor, name);
        internal Type PropertyType => Nullable.GetUnderlyingType(Descriptor.PropertyType) ?? Descriptor.PropertyType;
        internal object GetValue(object item) => Descriptor.GetValue(item);
    }
}
