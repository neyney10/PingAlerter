using PingAlerter.Common;
using PingAlerter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.ViewModels
{
    public class AboutViewModel : BaseViewModel<Object>
    {
        private AboutModel AboutModel { get; set; }

        #region Exposing Model's properties

        /// <summary>
        /// Program's Version Number (stored in the model).
        /// </summary>
        public string VersionNumber { get { return this.AboutModel.VersionNumber; } }

        /// <summary>
        /// Get this program's version type (stored in the model).
        /// </summary>
        public string VersionType { get { return this.AboutModel.VersionType; } }

        /// <summary>
        /// Get this program's version timeStamp (stored in the model).
        /// </summary>
        public string VersionTimeStamp { get { return this.AboutModel.VersionTimeStamp; } }

        /// <summary>
        /// Get this project Website URL (stored in the model).
        /// </summary>
        public string WebsiteURL { get { return this.AboutModel.WebsiteURL; } }

        /// <summary>
        /// Get this project's author (stored in the model).
        /// </summary>
        public string Author { get { return this.AboutModel.Author; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor with default values for the model.
        /// </summary>
        public AboutViewModel() : this(new AboutModel(
            "0.20", 
            "Alpha (Release)", 
            "20/11/2022", 
            @"https://github.com/neyney10/PingAlerter",
            "Neyney10"))
        { }

        /// <summary>
        /// Constructor with Dependency injection of the model.
        /// </summary>
        public AboutViewModel(AboutModel model)
        {
            // Set values for dependency injection, models and such.
            this.AboutModel = model;
        }

        #endregion
    }
}
