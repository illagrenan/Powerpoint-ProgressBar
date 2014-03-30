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
        private readonly ITagWriter ttt;

        public TagAdapter(ITagWriter ttt)
        {
            this.ttt = ttt;
        }

        public bool HasBarInTags()
        {
            return ttt.GetTagByKey(TagNameHelper.TagKey) != String.Empty;
            throw new NotImplementedException();
        }

        public IBarTag GetBarFromTag()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            var bt = new BarTag
            {
                ActiveColor = JsonConvert.DeserializeObject<Color>(ttt.GetTagByKey(TagNameHelper.ActiveColor)),
                InactiveColor = JsonConvert.DeserializeObject<Color>(ttt.GetTagByKey(TagNameHelper.Iac)),
                SizeSelectedItemIndex =
                    JsonConvert.DeserializeObject<int>(ttt.GetTagByKey(TagNameHelper.Sizeselecteditemindex)),
                ThemeSelectedItemIndex =
                    JsonConvert.DeserializeObject<int>(ttt.GetTagByKey(TagNameHelper.Themeselecteditemindex)),
                PositionOptions =
                    JsonConvert.DeserializeObject<PositionOptions>(ttt.GetTagByKey(TagNameHelper.Getpositionoptions),
                        jsonSerializerSettings),
                DisableFirstSlideChecked =
                    JsonConvert.DeserializeObject<bool>(ttt.GetTagByKey(TagNameHelper.DisableFirstSlideChecked)),
                Bar = JsonConvert.DeserializeObject<IBar>(ttt.GetTagByKey(TagNameHelper.IbAr), jsonSerializerSettings)
            };

            return bt;
        }

        public IBar Bar
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void SavePresentationToTag(BarTag bt)
        {
            ttt.SaveTag(TagNameHelper.TagKey, true.ToString());

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            };


            string activeColor = JsonConvert.SerializeObject(bt.ActiveColor);
            ttt.SaveTag(TagNameHelper.ActiveColor, activeColor);

            string inactiveColor = JsonConvert.SerializeObject(bt.InactiveColor);
            ttt.SaveTag(TagNameHelper.Iac, inactiveColor);

            string positionOptions = JsonConvert.SerializeObject(bt.PositionOptions, Formatting.Indented, settings);
            ttt.SaveTag(TagNameHelper.Getpositionoptions, positionOptions);

            string sizeSelected = JsonConvert.SerializeObject(bt.SizeSelectedItemIndex);
            ttt.SaveTag(TagNameHelper.Sizeselecteditemindex, sizeSelected);

            string themeSelected = JsonConvert.SerializeObject(bt.ThemeSelectedItemIndex);
            ttt.SaveTag(TagNameHelper.Themeselecteditemindex, themeSelected);

            string dddisableFirstSlideChecked = JsonConvert.SerializeObject(bt.DisableFirstSlideChecked);
            ttt.SaveTag(TagNameHelper.DisableFirstSlideChecked, dddisableFirstSlideChecked);


            string ibbb = JsonConvert.SerializeObject(bt.Bar, Formatting.Indented, settings);
            ttt.SaveTag(TagNameHelper.IbAr, ibbb);
        }


        public void RemovePresentation()
        {
            ttt.RemoveTagByKey(TagNameHelper.TagKey);
        }
    }
}