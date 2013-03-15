using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChapterRelics;

namespace HokiMacroLib
{
    public class Savage : MacroBase
    {
        public Savage(IMacroToControlDependencyService macroControlService)
            : base(macroControlService) { }

        public override void registerMacros()
        {
            EventRegistrar.Add(key.E, asyncMainSavageAttackSequence);
            EventRegistrar.Add(key.tab, toggleOnOff);
            EventRegistrar.Add(key.Q, cancelQueue);
            EventRegistrar.Add(key.R, cancelQueue);
        }

        private void cancelQueue(KeyArgs keyArgs)
        {
            if (!keyArgs.KeyUp)
            {
                isMainSavageAttackSequenceQueued = false;
                mainSavageAttackSequenceQueuedArgs = null;
            }
        }

        #region MainSavageAttackSequence
        private bool isMainSavageAttackSequenceRunning = false;
        private bool isMainSavageAttackSequenceQueued = false;
        private KeyArgs mainSavageAttackSequenceQueuedArgs;
        private DateTime lastCastSavageBuffsDT = new DateTime(2000, 1, 1);

        private void asyncMainSavageAttackSequence(KeyArgs keyArgs)
        {
            if (!keyArgs.KeyUp && ProcessMacros)
            {
                if (!isMainSavageAttackSequenceRunning)
                {
                    isMainSavageAttackSequenceRunning = true;
                    Task.Factory.StartNew(() =>
                    {
                        mainSavageAttackSequence(keyArgs);
                        mainSavageAttackSequenceCallback();
                    });
                }
                else
                {
                    isMainSavageAttackSequenceQueued = true;
                    mainSavageAttackSequenceQueuedArgs = keyArgs;
                }
            }
        }

        private void mainSavageAttackSequenceCallback()
        {
            isMainSavageAttackSequenceRunning = false;
            if (isMainSavageAttackSequenceQueued)
            {
                isMainSavageAttackSequenceQueued = false;
                asyncMainSavageAttackSequence(mainSavageAttackSequenceQueuedArgs);
            }
        }

        private void mainSavageAttackSequence(KeyArgs keyArgs)
        {
            int pressDuration = 5;
            int sleeptime = 15;
            TimeSpan diff;
            diff = DateTime.Now - lastCastSavageBuffsDT;
            if (!keyArgs.KeyUp && ProcessMacros)
            {
                KeySim.KeyPress((byte)key.one, pressDuration);
                Thread.Sleep(sleeptime);
                KeySim.KeyPress((byte)key.two, pressDuration);
                Thread.Sleep(sleeptime);
                KeySim.KeyPress((byte)key.three, pressDuration);
                Thread.Sleep(sleeptime);
                KeySim.KeyPress((byte)key.four, pressDuration);
                Thread.Sleep(sleeptime);
                KeySim.KeyPress((byte)key.five, pressDuration);
                Thread.Sleep(sleeptime);
                KeySim.KeyPress((byte)key.six, pressDuration);
                Thread.Sleep(sleeptime);
                KeySim.KeyPress((byte)key.seven, pressDuration);
                Thread.Sleep(sleeptime);
                KeySim.KeyPress((byte)key.eight, pressDuration);
                Thread.Sleep(sleeptime);
                KeySim.KeyPress((byte)key.nine, pressDuration);
                Thread.Sleep(sleeptime);
                KeySim.KeyPress((byte)key.zero, pressDuration);
                Thread.Sleep(1500);
            }
        }
        #endregion MainSavageAttackSequence
    }
}
