using ProgressBar.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Bar
{
    public class BarInfo : IBarInfo
    {
        private System.Drawing.Image thumbnailImage;
        private string friendlyName;

        public BarInfo(System.Drawing.Image thumbnailImage, string friendlyName)
        {
            if (friendlyName == String.Empty)
            {
                throw new InvalidArgumentException("Friendly name cannot be empty - it is important for users.");
            }

            this.thumbnailImage = thumbnailImage;
            this.friendlyName = friendlyName;
        }

        public System.Drawing.Image Image
        {
            get
            {
                return this.thumbnailImage;
            }
        }

        public string FriendlyName
        {
            get
            {
                return this.friendlyName;
            }
        }
    }
}
