using System;
using SourceGeneration.Attributes;

namespace SourceGeneration.Models {
    [Mappable]
    public class Product {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [MappableIgnore]
        public string Description { get; set; }
    }
}