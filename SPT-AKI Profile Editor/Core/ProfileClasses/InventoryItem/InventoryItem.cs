﻿using Newtonsoft.Json;
using SPT_AKI_Profile_Editor.Core.HelperClasses;
using SPT_AKI_Profile_Editor.Helpers;
using System.Windows.Media.Imaging;

namespace SPT_AKI_Profile_Editor.Core.ProfileClasses
{
    public class InventoryItem : BindableEntity
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_tpl")]
        public string Tpl { get; set; }

        [JsonProperty("slotId")]
        public string SlotId { get; set; }

        [JsonConverter(typeof(LocationJsonConverter))]
        [JsonProperty("location")]
        public ItemLocation Location { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("upd")]
        public ItemUpd Upd { get; set; }

        [JsonIgnore]
        public string LocalizedName => (!string.IsNullOrEmpty(Tag) ? $"[{Tag}] " : string.Empty) + GlobalName + CountString;

        [JsonIgnore]
        public bool IsInItemsDB =>
            AppData.ServerDatabase.ItemsDB?.ContainsKey(Tpl) ?? false;

        [JsonIgnore]
        public BitmapSource CategoryIcon => AppData.ServerDatabase?.HandbookHelper?.GetItemCategory(Tpl)?.BitmapIcon;

        [JsonIgnore]
        public bool IsPockets => SlotId == AppData.AppSettings.PocketsSlotId;

        [JsonIgnore]
        public string Tag => Upd?.Tag?.Name;

        [JsonIgnore]
        public string CountString => (Upd?.StackObjectsCount ?? 1) > 1 ? $" [{Upd.StackObjectsCount}]" : string.Empty;

        [JsonIgnore]
        public bool IsContainer => IsInItemsDB && AppData.ServerDatabase.ItemsDB[Tpl].Properties?.Grids?.Length > 0;

        [JsonIgnore]
        public bool CanAddItems => IsInItemsDB && AppData.ServerDatabase.ItemsDB[Tpl].Properties?.Grids?.Length == 1;

        [JsonIgnore]
        public bool IsWeapon => IsInItemsDB && AppData.ServerDatabase.ItemsDB[Tpl].IsWeapon;

        [JsonIgnore]
        private string GlobalName =>
            AppData.ServerDatabase.LocalesGlobal?.ContainsKey(Tpl.Name()) ?? false ? AppData.ServerDatabase.LocalesGlobal[Tpl.Name()] : Tpl;

        public static InventoryItem CopyFrom(InventoryItem item) => new()
        {
            Id = item.Id,
            Tpl = item.Tpl,
            SlotId = item.SlotId,
            Location = item.Location,
            ParentId = item.ParentId,
            Upd = item.Upd
        };
    }
}