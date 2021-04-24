using System;

namespace SourceGeneration.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class MappableAttribute : Attribute {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MappableIgnoreAttribute : Attribute {

    }
}