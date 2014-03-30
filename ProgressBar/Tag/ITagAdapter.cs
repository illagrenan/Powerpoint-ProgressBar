using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgressBar.Bar;

namespace ProgressBar.Tag
{
    public interface ITagAdapter
    {
        

        bool HasBarInTags();

        IBarTag GetBarFromTag();

        Bar.IBar Bar { get; set; }

        void SavePresentationToTag(BarTag bt);

        void RemovePresentation();
    }
}
