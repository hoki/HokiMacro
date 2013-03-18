using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChapterRelics;

namespace HokiMacroLib
{
    public abstract class MacroBase : IControlToMacroDependencyService
    {
        protected MacroBase(IMacroToControlDependencyService macroControlService)
        {
            _macroControlService = macroControlService;
            EventRegistrar.Clear();
            registerMacros();
        }

        protected IMacroToControlDependencyService _macroControlService;
        protected Dictionary<key, Action<KeyArgs>> EventRegistrar = new Dictionary<key, Action<KeyArgs>>();
        protected bool ProcessMacros = false;
        protected Random rand = new Random();

        public abstract void registerMacros();

        protected virtual void sleep(int min, int max)
        {
            Thread.Sleep(rand.Next(min, max));
        }

        protected virtual void toggleOnOff(KeyArgs keyArgs)
        {
           if (!keyArgs.KeyUp && keyArgs.Shift)
               _macroControlService.ToggleOnOffFromMacro();
        }

        #region IControlToMacroDependencyService

        /// <summary>
        /// This is the entry point delegate.
        /// The Chapter Relics will pass keyboard events and args to this.
        /// </summary>
        /// <param name="keyArgs"></param>
        public virtual void KeyboardEventTrigger(KeyArgs keyArgs)
        {
            Action<KeyArgs> action;
            if (EventRegistrar.TryGetValue((key)keyArgs.KeyCode, out action))
            {
                action(keyArgs);
            }
        }

        public virtual Action<OnOff> ToggleMacroOnOff
        {
            get { return (onOff) => ProcessMacros = (onOff == OnOff.on); }
        }

        #endregion IControlToMacroDependencyService
    }
}
