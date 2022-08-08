﻿using MahApps.Metro.Controls.Dialogs;
using NUnit.Framework;
using SPT_AKI_Profile_Editor.Core.Enums;
using SPT_AKI_Profile_Editor.Core.ProfileClasses;
using SPT_AKI_Profile_Editor.Tests.Hepers;

namespace SPT_AKI_Profile_Editor.Tests.ViewModelsTests
{
    internal class ContainerWindowViewModelTests
    {
        private static readonly TestsDialogManager dialogManager = new();

        [OneTimeSetUp]
        public void Setup() => TestHelpers.LoadDatabase();

        [Test]
        public void InitializeCorrectlyForPmc()
        {
            ContainerWindowViewModel pmcContainer = TestViewModel(StashEditMode.PMC);
            Assert.Multiple(() =>
            {
                Assert.That(pmcContainer, Is.Not.Null, "ContainerWindowViewModel is null");
                Assert.That(pmcContainer.Worker, Is.Not.Null, "Worker is null");
                Assert.That(pmcContainer.WindowTitle, Is.EqualTo(TestHelpers.GetTestName("ContainerWindowViewModel", StashEditMode.PMC)), "Wrong WindowTitle");
                Assert.That(pmcContainer.HasItems, Is.True, "HasItems is false");
                Assert.That(pmcContainer.Items.Count, Is.EqualTo(3), "Items.Count is not 3");
                Assert.That(pmcContainer.ItemsAddingAllowed, Is.False, "ItemsAddingAllowed is true");
                Assert.That(pmcContainer.CategoriesForItemsAdding.Count, Is.GreaterThan(0), "CategoriesForItemsAdding is empty");
            });
        }

        [Test]
        public void InitializeCorrectlyForScav()
        {
            ContainerWindowViewModel pmcContainer = TestViewModel(StashEditMode.Scav);
            Assert.Multiple(() =>
            {
                Assert.That(pmcContainer, Is.Not.Null, "ContainerWindowViewModel is null");
                Assert.That(pmcContainer.Worker, Is.Not.Null, "Worker is null");
                Assert.That(pmcContainer.WindowTitle, Is.EqualTo(TestHelpers.GetTestName("ContainerWindowViewModel", StashEditMode.Scav)), "Wrong WindowTitle");
                Assert.That(pmcContainer.HasItems, Is.True, "HasItems is false");
                Assert.That(pmcContainer.Items.Count, Is.EqualTo(5), "Items.Count is not 5");
                Assert.That(pmcContainer.ItemsAddingAllowed, Is.False, "ItemsAddingAllowed is true");
                Assert.That(pmcContainer.CategoriesForItemsAdding.Count, Is.GreaterThan(0), "CategoriesForItemsAdding is empty");
            });
        }

        [Test]
        public void CanRemoveItem()
        {
            ContainerWindowViewModel pmcContainer = TestViewModel(StashEditMode.PMC);
            var item = pmcContainer.Items[0];
            pmcContainer.RemoveItem.Execute(item.Id);
            Assert.That(pmcContainer.Items.IndexOf(item), Is.EqualTo(-1), "Item not removed");
        }

        private static ContainerWindowViewModel TestViewModel(StashEditMode editMode)
        {
            TestHelpers.SetupTestCharacters("ContainerWindowViewModel", editMode);
            InventoryItem item = new()
            {
                Id = TestHelpers.GetTestName("ContainerWindowViewModel", editMode),
                Tpl = TestHelpers.GetTestName("ContainerWindowViewModel", editMode)
            };
            return new(item, editMode, DialogCoordinator.Instance, dialogManager);
        }
    }
}