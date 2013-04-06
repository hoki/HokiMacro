using System;
using System.Collections.Generic;
using ChapterRelics;

namespace HokiMacroLib
{
    public interface IFormToControl
    {
        /// <summary>
        /// Form tells Control -> Hey guy, he clicked teh On/Off button
        /// </summary>
        void ToggleOnOffFromForm();

        IList<IControlToMacro> Macros { get; }

        void ChangeMacro(IControlToMacro macro);
    }

    public interface IControlToForm
    {
        /// <summary>
        /// Control tells Form Display -> Oh hi, Macros are On/Off
        /// </summary>
        Action<OnOff> ToggleDisplayOnOff { get; }
    }

    public interface IMacroToControl
    {
        /// <summary>
        /// Macro tells Control -> Greetings, he pressed an On/Off key combination
        /// </summary>
        void ToggleOnOffFromMacro();
    }

    public interface IControlToMacro
    {
        /// <summary>
        /// Name/description of the macro
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Control tells Macro -> Yo dog, turn off macros
        /// </summary>
        Action<OnOff> ToggleMacroOnOff { get; }

        /// <summary>
        /// This is the entry point delegate.
        /// The Chapter Relics will pass keyboard events and args to this.
        /// </summary>
        /// <param name="keyArgs"></param>
        void KeyboardEventTrigger(KeyArgs keyArgs);
    }
}
