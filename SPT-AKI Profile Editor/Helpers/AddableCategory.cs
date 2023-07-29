﻿using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SPT_AKI_Profile_Editor.Helpers
{
    public abstract class AddableCategory : BindableViewModel
    {
        private string localizedName;
        private bool isExpanded;

        [JsonIgnore]
        public virtual ObservableCollection<AddableCategory> Categories { get; set; }

        [JsonIgnore]
        public virtual ObservableCollection<AddableItem> Items { get; set; }

        public string ParentId { get; set; }

        public string Icon { get; set; }

        [JsonIgnore]
        public BitmapImage BitmapIcon { get; set; }

        [JsonIgnore]
        public string LocalizedName
        {
            get => localizedName;
            set
            {
                localizedName = value;
                OnPropertyChanged(nameof(LocalizedName));
            }
        }

        [JsonIgnore]
        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        [JsonIgnore]
        public bool IsNotHidden => Items.Count > 0 || Categories.Count > 0;

        public bool ApplyFilter(string text, bool includeDesriptions)
        {
            bool categories = false;
            foreach (var category in Categories)
            {
                if (category.ContainsItemsWithTextInName(text ?? "", includeDesriptions))
                    categories = true;
            }
            ICollectionView cv = CollectionViewSource.GetDefaultView(Categories);
            if (cv == null)
                return categories;
            if (string.IsNullOrEmpty(text))
                cv.Filter = null;
            cv.Filter = o =>
            {
                AddableCategory p = o as AddableCategory;
                return p.ContainsItemsWithTextInName(text ?? "", includeDesriptions);
            };
            return categories;
        }

        public bool ContainsItemsWithTextInName(string text, bool includeDesriptions)
        {
            FilterItems();
            return Items.Any(x => x.ContainsText(text, includeDesriptions))
                || ApplyFilter(text, includeDesriptions);

            void FilterItems()
            {
                ICollectionView cv = CollectionViewSource.GetDefaultView(Items);
                if (cv == null)
                    return;
                if (string.IsNullOrEmpty(text))
                    cv.Filter = null;
                else
                    cv.Filter = o => (o as AddableItem).ContainsText(text, includeDesriptions);
            }
        }
    }
}