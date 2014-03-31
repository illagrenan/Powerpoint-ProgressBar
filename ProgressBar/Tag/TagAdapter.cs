#region

using System;
using System.Drawing;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using ProgressBar.Bar;
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

            var r = JsonConvert.DeserializeObject<TagContainer>(
                _tagWriter.GetTagByKey(TagNameHelper.MakeKey("bar")),
                _jsonSerializerSettings
                );

            return r;
        }


        public void PersistContainer(TagContainer containerToPersist)
        {
            _tagWriter.SaveTag(TagNameHelper.MainTagKey, true.ToString());

            /*
            string activeColor = JsonConvert.SerializeObject(containerToPersist.ActiveColor);
            _tagWriter.SaveTag(TagNameHelper.MakeKey("active_color"), activeColor);

            string inactiveColor = JsonConvert.SerializeObject(containerToPersist.InactiveColor);
            _tagWriter.SaveTag(TagNameHelper.MakeKey("inactive_color"), inactiveColor);

            string positionOptions = JsonConvert.SerializeObject(containerToPersist.PositionOptions, Formatting.Indented,
                _jsonSerializerSettings);
            _tagWriter.SaveTag(TagNameHelper.MakeKey("position_options"), positionOptions);

            string sizeSelected = JsonConvert.SerializeObject(containerToPersist.SizeSelectedItemIndex);
            _tagWriter.SaveTag(TagNameHelper.MakeKey("size_selected_item_index"), sizeSelected);

            string themeSelected = JsonConvert.SerializeObject(containerToPersist.ThemeSelectedItemIndex);
            _tagWriter.SaveTag(TagNameHelper.MakeKey("theme_selected_item_index"), themeSelected);

            string dddisableFirstSlideChecked = JsonConvert.SerializeObject(containerToPersist.DisableFirstSlideChecked);
            _tagWriter.SaveTag(TagNameHelper.MakeKey("disable_first_slide_checked"), dddisableFirstSlideChecked);
            */

            string ibbb = JsonConvert.SerializeObject(
                containerToPersist,
                Formatting.Indented,
                _jsonSerializerSettings
                );

            _tagWriter.SaveTag(TagNameHelper.MakeKey("bar"), ibbb);
        }

        public void RemoveTagContainer()
        {
            _tagWriter.RemoveTagByKey(TagNameHelper.MainTagKey);
        }
    }
}