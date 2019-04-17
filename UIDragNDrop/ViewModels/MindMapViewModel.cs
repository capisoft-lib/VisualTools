using System;
using System.Collections.Generic;
using UIDragNDrop.Models;

namespace UIDragNDrop.ViewModels
{
    public class MindMapViewModel : BaseViewModel
    {
        private List<MindMapEntity> baseMindMaps;
        public List<MindMapEntity> BaseMindMaps { get => baseMindMaps; set { baseMindMaps = value; NotifyPropertyChanged(); } }

        private MindMapEntity currentMindMapEntity;
        public MindMapEntity CurrentMindMapEntity { get => currentMindMapEntity; set { currentMindMapEntity = value; NotifyPropertyChanged(); } }

        public MindMapViewModel() : base()
        {
        }

        public void ClearMindMaps()
        {
            BaseMindMaps = new List<MindMapEntity>();
        }

        public void AddBaseMindMap(MindMapEntity mapEntity)
        {
            BaseMindMaps.Add(mapEntity);
            NotifyPropertyChanged(nameof(BaseMindMaps));
        }
    }
}
