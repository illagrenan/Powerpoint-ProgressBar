#region

using System;
using Microsoft.Office.Interop.PowerPoint;
using ProgressBar.CustomExceptions;

#endregion

namespace ProgressBar.Tag
{
    internal class TagWriter : ITagWriter
    {
        private readonly Tags _tags;

        public TagWriter(Tags tags)
        {
            _tags = tags;
        }

        public string GetTagByKey(string key)
        {
            return _tags[key];
        }

        public void RemoveTagByKey(string key)
        {
            _tags.Add(key, String.Empty);
        }

        public void SaveTag(string key, string value)
        {
            if (key == string.Empty || value == string.Empty)
            {
                throw new InvalidArgumentException("Key nor the value cannot be empty.");
            }

            _tags.Add(key, value);
        }
    }
}