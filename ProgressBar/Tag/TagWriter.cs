using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgressBar.CustomExceptions;

namespace ProgressBar.Tag
{
    class TagWriter : ITagWriter
    {
        private Microsoft.Office.Interop.PowerPoint.Tags tags;

        public TagWriter(Microsoft.Office.Interop.PowerPoint.Tags tags)
        {
            this.tags = tags;
        }

        public string GetTagByKey(string key)
        {
            return tags[key];
        }

        public void SaveTag(string key, string value)
        {
            if (key == string.Empty || value == string.Empty)
            {
                throw new InvalidArgumentException("Key nor the value cannot be empty.");
            }

            tags.Add(key, value);
        }


        public void RemoveTagByKey(string p)
        {
            tags.Add(p, String.Empty);
        }
    }
}
