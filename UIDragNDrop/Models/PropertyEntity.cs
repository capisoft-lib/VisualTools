using System;
namespace UIDragNDrop.Models
{
    public class PropertyEntity
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public VisibilityEnum Visibility { get; set; } = VisibilityEnum.PUBLIC;

        public PropertyEntity()
        {
        }

        public string SimpleString { get => ToString(); }

        public override string ToString()
        {
            string visibilityChar = "-";
            switch (Visibility)
            {
                case VisibilityEnum.PRIVATE:
                    visibilityChar = "-";
                    break;
                case VisibilityEnum.PROTECTED:
                    visibilityChar = "~";
                    break;
                case VisibilityEnum.PUBLIC:
                    visibilityChar = "+";
                    break;
            }
            return $"{visibilityChar}{Name}: {Type.Name}";
        }

        public string ArgString()
        {
            return $"{Name}: {Type.Name}";
        }
    }
}
