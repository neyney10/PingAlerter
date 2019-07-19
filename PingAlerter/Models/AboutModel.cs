using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Models
{
    /// <summary>
    /// AboutModel class is a model which contains data about the program's version.
    /// </summary>
    public class AboutModel
    {
        #region Public Properties

        /// <summary>
        /// Numerical version number of this PingAlerter Program.
        /// </summary>
        public string VersionNumber { get; set; }

        /// <summary>
        /// program's version type: Alpha, Beta (Development), release ... etc.
        /// </summary>
        public string VersionType { get; set; }

        /// <summary>
        /// program's version time stamp: the date when this version is created/released.
        /// </summary>
        public string VersionTimeStamp { get; set; }

        /// <summary>
        /// URL to project's website.
        /// </summary>
        public string WebsiteURL { get; set; }

        /// <summary>
        /// Project's author.
        /// </summary>
        public String Author { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Full-Featured constructor that creates a new AboutModel object.
        /// </summary>
        /// <param name="verNum">Version Number (e.g "1.0.0" ...)</param>
        /// <param name="verType">Version Type (e.g "Alpha" | "Beta" | "Release" ...)</param>
        /// <param name="verTimeStamp">Version TimeStamp (e.g "19/07/2019")</param>
        /// <param name="webURL">URL</param>
        /// <param name="author">Author name/identity</param>
        public AboutModel(string verNum, string verType, string verTimeStamp, string webURL, string author)
        {
            this.VersionNumber = verNum;
            this.VersionType = verType;
            this.VersionTimeStamp = verTimeStamp;
            this.WebsiteURL = webURL;
            this.Author = author;
        }

        #endregion
    }
}
