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
        public Savage(IMacroToControl macroControlService, string name = "Savage")
            : base(macroControlService, name) { }

        public override void registerMacros()
        {
            //Register defined in Extensions class
            EventRegistrar.Register(key.E, asyncMainSavageAttackSequence);
            EventRegistrar.Register(key.tab, toggleOnOff);
            EventRegistrar.Register(key.Q, cancelQueue);
            EventRegistrar.Register(key.R, cancelQueue);
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
                if (diff.TotalMilliseconds > 28000)
                {
                    //cast savage buffs
                    KeySim.KeyPress(key.nine, pressDuration);
                    sleep(sleepMin, sleepMax);
                    KeySim.KeyPress(key.zero, pressDuration);
                    lastCastSavageBuffsDT = DateTime.Now;
                }

                //stick
                KeySim.KeyPress(key.six, pressDuration);
                sleep(sleepMin, sleepMax);
                KeySim.KeyPress(key.six, pressDuration);
                sleep(sleepMin, sleepMax);
                KeySim.KeyPress(key.three, pressDuration);
                sleep(sleepMin, sleepMax);
                KeySim.KeyPress(key.four, pressDuration);
                sleep(sleepMin, sleepMax);
                KeySim.KeyPress(key.four, pressDuration);
                sleep(sleepMin, sleepMax);
                KeySim.KeyPress(key.four, pressDuration);
                sleep(sleepMin, sleepMax);

                

                Thread.Sleep(200);
            }
        }
        #endregion MainSavageAttackSequence
    }
}
