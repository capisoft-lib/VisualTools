using System;
using System.Linq;

namespace UIDragNDrop.Models
{
    public class MethodEntity
    {
        public string Name { get; set; }
        public Type ReturnType { get; set; }
        public PropertyEntity[] Params { get; set; }
        public VisibilityEnum Visibility { get; set; } = VisibilityEnum.PUBLIC;

        public MethodEntity()
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
            string returnTypeString = "";
            if (ReturnType != null)
            {
                returnTypeString = $": {ReturnType.Name}";
            }
            return $"{visibilityChar}{Name}({String.Join(", ", Params.Select(p => p.ArgString()))}){returnTypeString}";
        }
    }
}
