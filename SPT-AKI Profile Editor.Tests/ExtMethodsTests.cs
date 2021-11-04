﻿using NUnit.Framework;
using SPT_AKI_Profile_Editor.Core;

namespace SPT_AKI_Profile_Editor.Tests
{
    class ExtMethodsTests
    {
        AppSettings settings;

        [OneTimeSetUp]
        public void Setup()
        {
            settings = new AppSettings();
            settings.Load();
        }

        [Test]
        public void AppSettingsServerPathIsServerBase() => Assert.IsTrue(ExtMethods.PathIsServerFolder(settings));

        [Test]
        public void PathIsServerBaseTrue() => Assert.IsTrue(ExtMethods.PathIsServerFolder(settings, @"C:\SPT"));

        [Test]
        public void PathIsServerBaseFalse() => Assert.IsFalse(ExtMethods.PathIsServerFolder(settings, @"D:\WinSetupFromUSB"));
    }
}