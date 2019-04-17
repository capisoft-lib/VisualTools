using System;
namespace UIDragNDrop.Models
{
    public class Entity
    {
        public string Name { get; set; }
        public Entity ParentClass { get; set; }
        public PropertyEntity[] Properties { get; set; }
        public MethodEntity[] Methods { get; set; }

        public Entity()
        {
        }
    }
}
