#region

using System;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using log4net;
using Newtonsoft.Json;
using ProgressBar.CustomExceptions;
using ProgressBar.Helper;

#endregion

namespace ProgressBar.Tag
{
    public class TagAdapter : ITagAdapter
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


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

            if (deserializedContainer == null)
            {
                _log.Fatal("deserializedContainer is null");
                throw new InvaidSerializedContainerException();
            }

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