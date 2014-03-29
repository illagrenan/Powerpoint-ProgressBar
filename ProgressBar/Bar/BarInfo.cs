#region

using System;
using System.Drawing;
using ProgressBar.CustomExceptions;

#endregion

namespace ProgressBar.Bar
{
    public class BarInfo : IBarInfo
    {
        private readonly string _friendlyName;
        private readonly Image _thumbnailImage;

        public BarInfo(Image thumbnailImage, string friendlyName)
        {
            if (friendlyName == String.Empty)
            {
                throw new InvalidArgumentException("Friendly name cannot be empty - it is important for users.");
            }

            _thumbnailImage = thumbnailImage;
            _friendlyName = friendlyName;
        }

        public Image Image
        {
            get { return _thumbnailImage; }
        }

        public string FriendlyName
        {
            get { return _friendlyName; }
        }
    }
}