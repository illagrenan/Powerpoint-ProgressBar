#region

using System;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using ProgressBar.Helper;

#endregion

namespace ProgressBar.Tag
{
    public class TagAdapter : ITagAdapter
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
            TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
        };

        private readonly ITagWriter _tagWriter;

        public TagAdapter(ITagWriter tagWriter)
        {
            _tagWriter = tagWriter;
        }

        public bool HasPersistedBar()
        {
            return _tagWriter.GetTagByKey(TagNameHelper.MainTagKey) != String.Empty;
        }

        public ITagContainer GetPersistedBar()
        {
            var deserializedContainer = JsonConvert.DeserializeObject<TagContainer>(
                _tagWriter.GetTagByKey(TagNameHelper.ContainerKey),
                _jsonSerializerSettings
                );

            return deserializedContainer;
        }


        public void PersistContainer(TagContainer containerToPersist)
        {
            _tagWriter.SaveTag(TagNameHelper.MainTagKey, true.ToString());

            string serializedContainer = JsonConvert.SerializeObject(
                containerToPersist,
                Formatting.Indented,
                _jsonSerializerSettings
                );

            _tagWriter.SaveTag(TagNameHelper.ContainerKey, serializedContainer);
        }

        public void RemoveTagContainer()
        {
            _tagWriter.RemoveTagByKey(TagNameHelper.MainTagKey);
        }
    }
}