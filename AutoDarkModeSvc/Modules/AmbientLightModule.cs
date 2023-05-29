using AutoDarkModeLib;
using AutoDarkModeSvc.Core;
using AutoDarkModeSvc.Events;
using AutoDarkModeSvc.Timers;
using NLog;
using System;

namespace AutoDarkModeSvc.Modules
{
    internal class AmbientLightModule : AutoDarkModeModule
    {
        private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly AdmConfigBuilder builder;
        private readonly GlobalState state;

        public AmbientLightModule(string name, bool fireOnRegistration) : base(name, fireOnRegistration)
        {
            builder = AdmConfigBuilder.Instance();
            state = GlobalState.Instance();
        }

        public override string TimerAffinity => TimerName.SensorPoll;

        public override void Fire()
        {
            // module logic here
            // call RequestSwitch() after sensor readout is stable and the state.AmbientLight.Requested was changed
        }

        private void RequestSwitch()
        {
            // set state.AmbientLight.Requested to the light or dark depending on what theme should be applied
            SwitchEventArgs eventArgs = new(SwitchSource.AmbientLight, state.AmbientLight.Requested, DateTime.Now);
            ThemeManager.RequestSwitch(eventArgs);
        }

        public override void EnableHook()
        {
            Logger.Info("module not implemented");
            // code here gets called when the module is enabled
        }

        public override void DisableHook()
        {
            // code is exectued here when the module gets disabled
        }
    }
}
