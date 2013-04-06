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
    public class MacroControl : IMacroToControl, IFormToControl
    {
        public MacroControl(IControlToForm formService)
        {
            _formService = formService;
            _macros = new List<IControlToMacro>();
            _macros.Add(new Savage(this));
            _macros.Add(new Hero(this));
            _macroService = _macros.FirstOrDefault();
            //globalMouseHook.delToGlobalMouseEvent = new GlobalMouseHook.DelegateToGlobalMouseEvent(invokeGlobalMouseEventDelegate);
            _globalKeyboardHook.GlobalKeyboardEvent = (keyArgs) => 
            {
                if (_macroService != null)
                    _macroService.KeyboardEventTrigger(keyArgs);
            };
            _globalKeyboardHook.Start();
        }

        private List<IControlToMacro> _macros;

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
        private IControlToForm _formService;

        /// <summary>
        /// Limited interface to MacroBase basically.
        /// With multiple layers of shit talking to each other its easy
        /// to forget which layer should do what to which layer.
        /// </summary>
        private IControlToMacro _macroService;
        private OnOff _onOffState = OnOff.off;

        #region Interface Properties

        public void ToggleOnOffFromMacro()
        {
            toggleOnOff();
        }

        public void ToggleOnOffFromForm()
        {
            toggleOnOff();
        }

        public IList<IControlToMacro> Macros
        {
            get { return _macros; }
        }

        public void ChangeMacro(IControlToMacro macro)
        {
            _onOffState = OnOff.off;
            _formService.ToggleDisplayOnOff(_onOffState);
            _macroService.ToggleMacroOnOff(_onOffState);
            _macroService = macro;
        }

        #endregion Interface Properties

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
