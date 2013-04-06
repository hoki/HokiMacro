using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
//using HokiMacro.Debug;
using System.Media;
using HokiMacroLib;

namespace HokiMacro
{
    public partial class HokiMacro : Form, IControlToForm
    {
        public HokiMacro()
        {
            InitializeComponent();
        }

        SoundPlayer _soundOn;
        SoundPlayer _soundOff;

        /// <summary>
        /// Limited interface to MacroControl basically.
        /// With multiple layers of shit talking to each other its easy
        /// to forget which layer should do what to which layer.
        /// </summary>
        IFormToControl _controlService;

        public Action<OnOff> ToggleDisplayOnOff
        {
            get
            {
                return (onOff) =>
                {
                    if (onOff == OnOff.on)
                    {
                        btnStart.Text = "On";
                        btnStart.ForeColor = Color.Green;
                        _soundOn.Play();
                    }
                    else
                    {
                        btnStart.Text = "Off";
                        btnStart.ForeColor = Color.Red;
                        _soundOff.Play();
                    }
                };
            }
        }

        private void HokiMacro_Load(object sender, EventArgs e) 
        {
            _controlService = new MacroControl(this);
            _soundOn = new SoundPlayer(@"on.wav");
            _soundOff = new SoundPlayer(@"off.wav");
            comboBox1.DataSource = _controlService.Macros;
            comboBox1.DisplayMember = "Name";
            this.comboBox1.SelectedIndexChanged += (comboSender, comboArgs) => 
            {
                _controlService.ChangeMacro((IControlToMacro)comboBox1.SelectedItem);
            };
        }

        public void btnStart_Click(object sender, EventArgs e)
        {
            _controlService.ToggleOnOffFromForm();
        }
    }
}
