﻿namespace Squalr.Properties
{
    using Squalr.Engine.Scanning;
    using Squalr.Source.Docking;
    using System;
    using System.Threading;

    /// <summary>
    /// View model for the Settings.
    /// </summary>
    internal class SettingsViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        private static Lazy<SettingsViewModel> settingsViewModelInstance = new Lazy<SettingsViewModel>(
                () => { return new SettingsViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="SettingsViewModel"/> class from being created.
        /// </summary>
        private SettingsViewModel() : base("Settings")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not 'write' flags are required in retrieved virtual memory pages.
        /// </summary>
        public Boolean RequiredProtectionWrite
        {
            get
            {
                return Settings.Default.RequiredWrite;
            }

            set
            {
                Settings.Default.RequiredWrite = value;
                this.RaisePropertyChanged(nameof(this.RequiredProtectionWrite));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not 'execute' flags are required in retrieved virtual memory pages.
        /// </summary>
        public Boolean RequiredProtectionExecute
        {
            get
            {
                return Settings.Default.RequiredExecute;
            }

            set
            {
                Settings.Default.RequiredExecute = value;
                this.RaisePropertyChanged(nameof(this.RequiredProtectionExecute));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not 'copy on write' flags are required in retrieved virtual memory pages.
        /// </summary>
        public Boolean RequiredProtectionCopyOnWrite
        {
            get
            {
                return Settings.Default.RequiredCopyOnWrite;
            }

            set
            {
                Settings.Default.RequiredCopyOnWrite = value;
                this.RaisePropertyChanged(nameof(this.RequiredProtectionCopyOnWrite));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages exclude those with 'write' flags.
        /// </summary>
        public Boolean ExcludedProtectionWrite
        {
            get
            {
                return Settings.Default.ExcludedWrite;
            }

            set
            {
                Settings.Default.ExcludedWrite = value;
                this.RaisePropertyChanged(nameof(this.ExcludedProtectionWrite));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages exclude those with 'execute' flags.
        /// </summary>
        public Boolean ExcludedProtectionExecute
        {
            get
            {
                return Settings.Default.ExcludedExecute;
            }

            set
            {
                Settings.Default.ExcludedExecute = value;
                this.RaisePropertyChanged(nameof(this.ExcludedProtectionExecute));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages exclude those with 'copy on write' flags.
        /// </summary>
        public Boolean ExcludedProtectionCopyOnWrite
        {
            get
            {
                return Settings.Default.ExcludedCopyOnWrite;
            }

            set
            {
                Settings.Default.ExcludedCopyOnWrite = value;
                this.RaisePropertyChanged(nameof(this.ExcludedProtectionCopyOnWrite));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages allow 'none' memory type.
        /// </summary>
        public Boolean MemoryTypeNone
        {
            get
            {
                return Settings.Default.MemoryTypeNone;
            }

            set
            {
                Settings.Default.MemoryTypeNone = value;
                this.RaisePropertyChanged(nameof(this.MemoryTypeNone));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages allow 'private' memory type.
        /// </summary>
        public Boolean MemoryTypePrivate
        {
            get
            {
                return Settings.Default.MemoryTypePrivate;
            }

            set
            {
                Settings.Default.MemoryTypePrivate = value;
                this.RaisePropertyChanged(nameof(this.MemoryTypePrivate));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages allow 'mapped' memory type.
        /// </summary>
        public Boolean MemoryTypeMapped
        {
            get
            {
                return Settings.Default.MemoryTypeMapped;
            }

            set
            {
                Settings.Default.MemoryTypeMapped = value;
                this.RaisePropertyChanged(nameof(this.MemoryTypeMapped));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages allow 'image' memory type.
        /// </summary>
        public Boolean MemoryTypeImage
        {
            get
            {
                return Settings.Default.MemoryTypeImage;
            }

            set
            {
                Settings.Default.MemoryTypeImage = value;
                this.RaisePropertyChanged(nameof(this.MemoryTypeImage));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages must be in usermode range.
        /// </summary>
        public Boolean IsUserMode
        {
            get
            {
                return Settings.Default.IsUserMode;
            }

            set
            {
                Settings.Default.IsUserMode = value;
                this.RaisePropertyChanged(nameof(this.IsUserMode));
                this.RaisePropertyChanged(nameof(this.IsNotUserMode));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages can be in any address range.
        /// </summary>
        public Boolean IsNotUserMode
        {
            get
            {
                return !Settings.Default.IsUserMode;
            }

            set
            {
                Settings.Default.IsUserMode = !value;
                this.RaisePropertyChanged(nameof(this.IsUserMode));
                this.RaisePropertyChanged(nameof(this.IsNotUserMode));
            }
        }

        /// <summary>
        /// Gets or sets a the interval of reupdating frozen values.
        /// </summary>
        public Int32 FreezeInterval
        {
            get
            {
                return Settings.Default.FreezeInterval;
            }

            set
            {
                Settings.Default.FreezeInterval = value;
                this.RaisePropertyChanged(nameof(this.FreezeInterval));
            }
        }

        /// <summary>
        /// Gets or sets a the interval between repeated scans.
        /// </summary>
        public Int32 RescanInterval
        {
            get
            {
                return Settings.Default.RescanInterval;
            }

            set
            {
                Settings.Default.RescanInterval = value;
                this.RaisePropertyChanged(nameof(this.RescanInterval));
            }
        }

        /// <summary>
        /// Gets or sets a the interval between reading scan results.
        /// </summary>
        public Int32 ResultReadInterval
        {
            get
            {
                return Settings.Default.ResultReadInterval;
            }

            set
            {
                Settings.Default.ResultReadInterval = value;
                this.RaisePropertyChanged(nameof(this.ResultReadInterval));
            }
        }

        /// <summary>
        /// Gets or sets a the interval between reading values in the table.
        /// </summary>
        public Int32 TableReadInterval
        {
            get
            {
                return Settings.Default.TableReadInterval;
            }

            set
            {
                Settings.Default.TableReadInterval = value;
                this.RaisePropertyChanged(nameof(this.TableReadInterval));
            }
        }

        /// <summary>
        /// Gets or sets a the allowed period of time for a given input to register as correlated with memory changes.
        /// </summary>
        public Int32 InputCorrelatorTimeOutInterval
        {
            get
            {
                return Settings.Default.InputCorrelatorTimeOutInterval;
            }

            set
            {
                Settings.Default.InputCorrelatorTimeOutInterval = value;
                this.RaisePropertyChanged(nameof(this.InputCorrelatorTimeOutInterval));
            }
        }

        /// <summary>
        /// Gets or sets the virtual memory alignment required in scans.
        /// </summary>
        public Int32 Alignment
        {
            get
            {
                return Settings.Default.Alignment;
            }

            set
            {
                Settings.Default.Alignment = value;
                this.RaisePropertyChanged(nameof(this.Alignment));
            }
        }

        /// <summary>
        /// Gets or sets the start address of virtual memory scans.
        /// </summary>
        public UInt64 StartAddress
        {
            get
            {
                return Settings.Default.StartAddress;
            }

            set
            {
                Settings.Default.StartAddress = value;
                this.RaisePropertyChanged(nameof(this.StartAddress));
            }
        }

        /// <summary>
        /// Gets or sets the end address of virtual memory scans.
        /// </summary>
        public UInt64 EndAddress
        {
            get
            {
                return Settings.Default.EndAddress;
            }

            set
            {
                Settings.Default.EndAddress = value;
                this.RaisePropertyChanged(nameof(this.EndAddress));
            }
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static SettingsViewModel GetInstance()
        {
            return SettingsViewModel.settingsViewModelInstance.Value;
        }

        /// <summary>
        /// Saves the current settings.
        /// </summary>
        public void Save()
        {
            Settings.Default.Save();
        }
    }
    //// End class
}
//// End namespace