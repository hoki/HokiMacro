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
            int sleepMin = 10;
            int sleepMax = 18;
            TimeSpan diff;
            diff = DateTime.Now - lastCastSavageBuffsDT;
            if (!keyArgs.KeyUp && ProcessMacros)
            {

                KeySim.KeyPress((byte)key.three, pressDuration);
                sleep(sleepMin, sleepMax);
                KeySim.KeyPress((byte)key.four, pressDuration);
                sleep(sleepMin, sleepMax);
                KeySim.KeyPress((byte)key.four, pressDuration);
                sleep(sleepMin, sleepMax);
                KeySim.KeyPress((byte)key.four, pressDuration);
                sleep(sleepMin, sleepMax);

                //stick
                KeySim.KeyPress((byte)key.six, pressDuration);

                if(diff.TotalMilliseconds > 20000)
                {
                    //cast savage buffs
                    KeySim.KeyPress((byte)key.eight, pressDuration);
                    sleep(sleepMin, sleepMax);
                    KeySim.KeyPress((byte)key.seven, pressDuration);
                    lastCastSavageBuffsDT = DateTime.Now;
                }

                Thread.Sleep(1500);
            }
        }
        #endregion MainSavageAttackSequence
    }
}
