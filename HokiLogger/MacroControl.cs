using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChapterRelics;

namespace HokiMacroLib
{
    /// <summary>
    /// MacroControl chooses which macro to run and routes keyboard
    /// and mouse events to the macro.
    /// Also acts as a central location from turning on and off macros.
    /// </summary>
    public class MacroControl : IMacroToControlDependencyService, IFormToControlDependencyService
    {
        public MacroControl(IControlToFormDependencyService formService)
        {
            _formService = formService;
            _macroService = new Savage(this);
            //globalMouseHook.delToGlobalMouseEvent = new GlobalMouseHook.DelegateToGlobalMouseEvent(invokeGlobalMouseEventDelegate);
            _globalKeyboardHook.delToGlobalKeyboardEvent = new GlobalKeyboardHook.DelegateToGlobalKeyboardEvent(_macroService.KeyboardEventTrigger);
            _globalKeyboardHook.Start();
        }

        /// <summary>
        /// Chapter Relic
        /// Hooking the global mouse events is slow as fuck 
        /// cause it fires 3000 times per inch.
        /// </summary>
        private GlobalMouseHook _globalMouseHook = new GlobalMouseHook();

        /// <summary>
        /// Chapter Relic
        /// </summary>
        private GlobalKeyboardHook _globalKeyboardHook = new GlobalKeyboardHook();

        /// <summary>
        /// Limited interface to HokiMacro(form) basically.
        /// With multiple layers of shit talking to each other its easy
        /// to forget which layer should do what to which layer.
        /// </summary>
        private IControlToFormDependencyService _formService;

        /// <summary>
        /// Limited interface to MacroBase basically.
        /// With multiple layers of shit talking to each other its easy
        /// to forget which layer should do what to which layer.
        /// </summary>
        private IControlToMacroDependencyService _macroService;
        private OnOff _onOffState = OnOff.off;

        public void ToggleOnOffFromMacro()
        {
            toggleOnOff();
        }

        public void ToggleOnOffFromForm()
        {
            toggleOnOff();
        }

        private void toggleOnOff()
        {
            if (_onOffState == OnOff.on)
                _onOffState = OnOff.off;
            else
                _onOffState = OnOff.on;
            
            _formService.ToggleDisplayOnOff(_onOffState);
            _macroService.ToggleMacroOnOff(_onOffState);
        }
    }
}
