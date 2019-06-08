using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Models
{

    public class AlertConfigModel
    {
        #region private Data Members
        private System.Media.SoundPlayer Player;
        #endregion

        #region Properties
        public bool IsSoundOn { get; set; }
        public string SoundFilepath
        {
            get { return Player.SoundLocation; }
            set { this.Player.SoundLocation = value; }
        }
        #endregion

        #region Constructor
        public AlertConfigModel(string soundFilepath)
        {
            this.IsSoundOn = true;
            this.Player = new System.Media.SoundPlayer();
            this.Player.SoundLocation = soundFilepath;
        }
        #endregion

        #region Methods

        public void PlaySound()
        {
            if (Player != null)
                this.Player.Play();
        }

        #endregion

    }

}
