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
            var deserializedTagContainer = new TagContainer
            {
                ActiveColor =
                    JsonConvert.DeserializeObject<Color>(_tagWriter.GetTagByKey(TagNameHelper.MakeKey("active_color"))),
                InactiveColor =
                    JsonConvert.DeserializeObject<Color>(_tagWriter.GetTagByKey(TagNameHelper.MakeKey("inactive_color"))),
                SizeSelectedItemIndex =
                    JsonConvert.DeserializeObject<int>(
                        _tagWriter.GetTagByKey(TagNameHelper.MakeKey("size_selected_item_index"))),
                ThemeSelectedItemIndex =
                    JsonConvert.DeserializeObject<int>(
                        _tagWriter.GetTagByKey(TagNameHelper.MakeKey("theme_selected_item_index"))),
                PositionOptions =
                    JsonConvert.DeserializeObject<PositionOptions>(
                        _tagWriter.GetTagByKey(TagNameHelper.MakeKey("position_options")),
                        _jsonSerializerSettings),
                DisableFirstSlideChecked =
                    JsonConvert.DeserializeObject<bool>(
                        _tagWriter.GetTagByKey(TagNameHelper.MakeKey("disable_first_slide_checked"))),
                Bar =
                    JsonConvert.DeserializeObject<IBar>(_tagWriter.GetTagByKey(TagNameHelper.MakeKey("bar")),
                        _jsonSerializerSettings)
            };

            return deserializedTagContainer;
        }


        public void PersistContainer(TagContainer containerToPersist)
        {
            _tagWriter.SaveTag(TagNameHelper.MainTagKey, true.ToString());

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


            string ibbb = JsonConvert.SerializeObject(containerToPersist.Bar, Formatting.Indented,
                _jsonSerializerSettings);
            _tagWriter.SaveTag(TagNameHelper.MakeKey("bar"), ibbb);
        }

        public void RemoveTagContainer()
        {
            _tagWriter.RemoveTagByKey(TagNameHelper.MainTagKey);
        }
    }
}