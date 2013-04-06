using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChapterRelics;

namespace HokiMacroLib
{
    public class Hero : MacroBase
    {
        public Hero(IMacroToControl macroControlService, string name = "Hero")
            : base(macroControlService, name) { }

        public override void registerMacros()
        {
            //Register defined in Extensions class
            EventRegistrar.Register(key.three, slam);
            EventRegistrar.Register(key.four, spearBack);
            EventRegistrar.Register(key.five, spearSide);
            EventRegistrar.Register(key.tab, toggleOnOff);
            EventRegistrar.Register(key.Q, cancelQueue);
            EventRegistrar.Register(key.R, cancelQueue);
        }

        private void cancelQueue(KeyArgs keyArgs)
        {
            if (!keyArgs.KeyUp)
            {
                attackQueueAction = null;
            }
        }

        private Action attackAction = null;
        private Action attackQueueAction = null;

        private void heroAttack()
        {
            if (attackAction != null)
            {
                Task.Factory.StartNew(() =>
                {
                    attackAction();
                    attackAction = attackQueueAction;
                    attackQueueAction = null;
                    heroAttack();
                });
            }
        }

        #region BodyguardGuard

        private void bodyguard(KeyArgs keyArgs)
        {
            if (!keyArgs.KeyUp && ProcessMacros)
            {
                int pressDuration = 5;
                int sleepMin = 10;
                int sleepMax = 18;
                KeySim.KeyPress(key.eight, pressDuration);
                sleep(sleepMin, sleepMax);
                KeySim.KeyPress(key.zero_numpad, pressDuration);
                sleep(sleepMin, sleepMax);
                KeySim.KeyPress(key.eight_numpad, pressDuration);
                sleep(sleepMin, sleepMax);
                KeySim.KeyPress(key.nine_numpad, pressDuration);
            }
        }

        #endregion BodyguardGuard

        #region Slam

        private void slam(KeyArgs keyArgs)
        {
            if (!keyArgs.KeyUp && ProcessMacros)
            {
                if (attackAction == null)
                {
                    attackAction = slam_macro;
                    heroAttack();
                }
                else
                    attackQueueAction = slam_macro;
            }
        }

        private void slam_macro()
        {
            int pressDuration = 5;
            int sleepMin = 10;
            int sleepMax = 18;
            KeySim.KeyPress(key.eight, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.zero_numpad, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.one_numpad, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.one_numpad, pressDuration);
        }

        #endregion Slam


        #region SwordBackStyle

        private void swordBack(KeyArgs keyArgs)
        {
            if (!keyArgs.KeyUp && ProcessMacros)
            {
                if (attackAction == null)
                {
                    attackAction = swordBack_macro;
                    heroAttack();
                }
                else
                    attackQueueAction = swordBack_macro;
            }
        }

        private void swordBack_macro()
        {
            int pressDuration = 5;
            int sleepMin = 10;
            int sleepMax = 18;
            KeySim.KeyPress(key.eight, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.zero_numpad, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.two_numpad, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.two_numpad, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.three_numpad, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.three_numpad, pressDuration);
            sleep(1000);
        }

        #endregion SwordBackStyle


        #region SpearBackStyle

        private void spearBack(KeyArgs keyArgs)
        {
            if (!keyArgs.KeyUp && ProcessMacros)
            {
                if (attackAction == null)
                {
                    attackAction = spearBack_macro;
                    heroAttack();
                }
                else
                    attackQueueAction = spearBack_macro;
            }
        }

        private void spearBack_macro()
        {
            int pressDuration = 5;
            int sleepMin = 10;
            int sleepMax = 18;
            KeySim.KeyPress(key.eight, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.zero_numpad, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.four_numpad, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.four_numpad, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.six_numpad, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.six_numpad, pressDuration);
            sleep(1000);
        }

        #endregion SpearBackStyle


        #region SpearSideStyle

        private void spearSide(KeyArgs keyArgs)
        {
            if (!keyArgs.KeyUp && ProcessMacros)
            {
                if (attackAction == null)
                {
                    attackAction = spearSide_macro;
                    heroAttack();
                }
                else
                    attackQueueAction = spearSide_macro;
            }
        }

        private void spearSide_macro()
        {
            int pressDuration = 5;
            int sleepMin = 10;
            int sleepMax = 18;
            KeySim.KeyPress(key.eight, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.zero_numpad, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.five_numpad, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.five_numpad, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.six_numpad, pressDuration);
            sleep(sleepMin, sleepMax);
            KeySim.KeyPress(key.six_numpad, pressDuration);
            sleep(1000);
        }

        #endregion SpearSideStyle
    }
}
