﻿using SPT_AKI_Profile_Editor.Views;

namespace SPT_AKI_Profile_Editor.Helpers
{
    public class ViewModelsFactory
    {
        public ViewModelsFactory(IDialogManager dialogManager,
                                 IWorker worker,
                                 IApplicationManager applicationManager,
                                 IWindowsDialogs windowsDialogs,
                                 RelayCommand saveCommand,
                                 RelayCommand reloadCommand,
                                 RelayCommand faqCommand)
        {
            FastMode = new(saveCommand);
            InfoTab = new();
            MerchantsTab = new();
            QuestsTab = new(dialogManager, reloadCommand, faqCommand);
            HideoutTab = new();
            SkillsTab = new(dialogManager, reloadCommand, faqCommand);
            MasteringTab = new(dialogManager, reloadCommand, faqCommand);
            ExaminedItemsTab = new();
            StashTab = new(dialogManager, worker, applicationManager);
            ClothingTab = new();
            WeaponBuildsTab = new(dialogManager, worker, windowsDialogs);
            ChangesTab = new(windowsDialogs, worker);
            BackupsTab = new(dialogManager, worker);
            AboutTab = new(applicationManager);
        }

        public FastModeViewModel FastMode { get; }

        public InfoTabViewModel InfoTab { get; }

        public MerchantsTabViewModel MerchantsTab { get; }

        public QuestsTabViewModel QuestsTab { get; }

        public HideoutTabViewModel HideoutTab { get; }

        public CommonSkillsTabViewModel SkillsTab { get; }

        public MasteringTabViewModel MasteringTab { get; }

        public ExaminedItemsTabViewModel ExaminedItemsTab { get; }

        public StashTabViewModel StashTab { get; }

        public ClothingTabViewModel ClothingTab { get; }

        public WeaponBuildsViewModel WeaponBuildsTab { get; }

        public ChangesTabViewModel ChangesTab { get; }

        public BackupsTabViewModel BackupsTab { get; }

        public AboutTabViewModel AboutTab { get; }
    }
}