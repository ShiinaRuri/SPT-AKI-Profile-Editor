﻿using MahApps.Metro.Controls.Dialogs;
using SPT_AKI_Profile_Editor.Classes;
using SPT_AKI_Profile_Editor.Core;
using SPT_AKI_Profile_Editor.Core.Enums;
using SPT_AKI_Profile_Editor.Core.ProfileClasses;
using SPT_AKI_Profile_Editor.Core.ServerClasses;
using SPT_AKI_Profile_Editor.Helpers;
using System.Collections.ObjectModel;
using System.Linq;

namespace SPT_AKI_Profile_Editor
{
    public class ContainerWindowViewModel : BindableViewModel
    {
        private readonly InventoryItem _item;
        private readonly StashEditMode _editMode;
        private readonly IDialogManager _dialogManager;
        private readonly IWorker _worker;
        private readonly IApplicationManager _applicationManager;
        private ObservableCollection<AddableCategory> categoriesForItemsAdding;

        public ContainerWindowViewModel(InventoryItem item,
                                        StashEditMode editMode,
                                        IDialogCoordinator dialogCoordinator,
                                        IApplicationManager applicationManager,
                                        IDialogManager dialogManager = null,
                                        IWorker worker = null)
        {
            _dialogManager = dialogManager ?? new MetroDialogManager(this, dialogCoordinator);
            _worker = worker ?? new Worker(_dialogManager);
            WindowTitle = item.LocalizedName;
            _item = item;
            _editMode = editMode;
            _applicationManager = applicationManager;
        }

        public RelayCommand OpenContainer => new(obj => _applicationManager.OpenContainerWindow(obj, _editMode));

        public RelayCommand InspectWeapon => new(obj => _applicationManager.OpenWeaponBuildWindow(obj, _editMode));

        public string WindowTitle { get; }

        public ObservableCollection<InventoryItem> Items => new(GetInventory().Items?.Where(x => x.ParentId == _item.Id));

        public bool HasItems => Items.Count > 0;

        public bool ItemsAddingAllowed => _item.CanAddItems && CategoriesForItemsAdding.Count > 0;

        public bool ItemsAddingBlocked => !ItemsAddingAllowed || Items.Where(x => !x.IsInItemsDB).Any();

        public ObservableCollection<AddableCategory> CategoriesForItemsAdding
        {
            get
            {
                categoriesForItemsAdding ??= ServerDatabase.HandbookHelper.CategoriesForItemsAddingWithFilter(_item.Tpl);
                return categoriesForItemsAdding;
            }
        }

        public RelayCommand RemoveItem => new(async obj =>
        {
            if (obj is string id && await _dialogManager.YesNoDialog("remove_stash_item_title", "remove_stash_item_caption"))
                RemoveItemFromContainer(id);
        });

        public RelayCommand RemoveAllItems => new(async obj =>
        {
            if (await _dialogManager.YesNoDialog("remove_stash_item_title", "remove_stash_items_caption"))
                _worker.AddTask(new WorkerTask
                {
                    Action = () => RemoveAllItemsFromContainer(),
                    Title = AppLocalization.GetLocalizedString("progress_dialog_title"),
                    Description = AppLocalization.GetLocalizedString("remove_stash_item_title")
                });
        });

        public RelayCommand AddItem => new(obj =>
        {
            if (obj is AddableItem item)
                _worker.AddTask(new WorkerTask { Action = () => AddItemToContainer(item) });
        });

        public RelayCommand ShowAllItems => new(async obj => await _dialogManager.ShowAllItemsDialog(AddItem, false));

        private void RemoveItemFromContainer(string id)
        {
            GetInventory().RemoveItems(new() { id });
            OnPropertyChanged("");
        }

        private void RemoveAllItemsFromContainer()
        {
            GetInventory().RemoveItems(Items.Select(x => x.Id).ToList());
            OnPropertyChanged("");
        }

        private void AddItemToContainer(AddableItem item)
        {
            GetInventory().AddNewItemsToContainer(_item, item, "main");
            OnPropertyChanged("");
        }

        private CharacterInventory GetInventory() => _editMode switch
        {
            StashEditMode.Scav => Profile.Characters.Scav.Inventory,
            _ => Profile.Characters.Pmc.Inventory,
        };
    }
}