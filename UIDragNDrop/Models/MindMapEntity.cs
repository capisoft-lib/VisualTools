using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UIDragNDrop.Models
{
    public class MindMapEntity : BaseNotifiedEntity
    {
        private string title;
        public string Title
        {
            get => title; set { title = value; NotifyPropertyChanged(); }
        }
        private string description;
        public string Description { get => description; set { description = value; NotifyPropertyChanged(); } }
        public MindMapEntity Parent { get; set; }
        public ICollection<MindMapEntity> Children { get; set; }
        public ICollection<string> Tags { get; set; }

        public MindMapEntity()
        {
            Children = new List<MindMapEntity>();
            Tags = new List<string>();
        }

        public void AddChild(MindMapEntity child)
        {
            this.Children.Add(child);
        }


    }
}
