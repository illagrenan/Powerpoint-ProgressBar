#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Newtonsoft.Json;
using ProgressBar.Bar;
using ProgressBar.CustomExceptions;
using ProgressBar.Tag;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

#endregion

namespace ProgressBar.Adapter
{
    internal class PowerPointAdapter : IPowerPointAdapter
    {
        private const string TagKey = "5XQ8HZCIiAVwnvP7QDECuRd1ygcAHb";

        private const string ActiveColor = "ac";
        private const string Getpositionoptions = "PositionOptions";
        private const string Iac = "iac";
        private const string Sizeselecteditemindex = "SizeSelectedItemIndex";
        private const string Themeselecteditemindex = "ThemeSelectedItemIndex";
        private const string DisableFirstSlideChecked = "DisableFirstSlideChecked";
        private const string IBAr = "IBAr";
        private readonly ShapeNameHelper _nameHelper;
        private readonly Application _powerPointApp;

        public PowerPointAdapter(Application powerPointApp, ShapeNameHelper nameHelper)
        {
            _powerPointApp = powerPointApp;
            _nameHelper = nameHelper;
        }

        public float PresentationWidth()
        {
            if (VisibleSlides().Count <= 0)
            {
                throw new InvalidStateException("Height of slides is unknown. Presentation has no slides.");
            }

            return VisibleSlides()[0].Master.Width;
        }

        public void InsertShape(Shape s)
        {
            throw new NotImplementedException();
        }

        public List<Slide> VisibleSlides()
        {
            return
                _powerPointApp.ActivePresentation.Slides.Cast<Slide>()
                    .Where(slide => slide.SlideShowTransition.Hidden != MsoTriState.msoTrue)
                    .ToList();
        }

        public int HiddenSlidesCount()
        {
            throw new NotImplementedException();
        }


        public float PresentationHeight()
        {
            if (VisibleSlides().Count <= 0)
            {
                throw new InvalidStateException("Height of slides is unknown. Presentation has no slides.");
            }

            return VisibleSlides()[0].Master.Height;
        }


        public List<Shape> AddInShapes()
        {
            List<Shape> addInShapes = new List<Shape>();

            foreach (Slide slide in VisibleSlides())
            {
                addInShapes.AddRange(
                    slide.Shapes.Cast<Shape>().Where(
                        shape => _nameHelper.IsShapeAddInShape(shape.Name)
                        )
                    );
            }

            return addInShapes;
        }


        public bool HasSlides
        {
            get { return (_powerPointApp.ActivePresentation.Slides.Count > 0); }
            set { throw new NotImplementedException(); }
        }


        public bool HasBarInTags()
        {
            return Tags()[TagKey] != String.Empty;
        }

        public IBarTag GetBarFromTag()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            BarTag bt = new BarTag
            {
                ActiveColor = JsonConvert.DeserializeObject<Color>(GetTagByKey(ActiveColor)),
                InactiveColor = JsonConvert.DeserializeObject<Color>(GetTagByKey(Iac)),
                SizeSelectedItemIndex = JsonConvert.DeserializeObject<int>(GetTagByKey(Sizeselecteditemindex)),
                ThemeSelectedItemIndex = JsonConvert.DeserializeObject<int>(GetTagByKey(Themeselecteditemindex)),
                PositionOptions = JsonConvert.DeserializeObject<PositionOptions>(GetTagByKey(Getpositionoptions), jsonSerializerSettings),
                DisableFirstSlideChecked = JsonConvert.DeserializeObject<bool>(GetTagByKey(DisableFirstSlideChecked)),
                IBar = JsonConvert.DeserializeObject<IBar>(GetTagByKey(IBAr), jsonSerializerSettings)
            };

            return bt;
        }


        public void SaveTag(string key, string value)
        {
            if (key == string.Empty || value == string.Empty)
            {
                throw new InvalidArgumentException("Key nor the value cannot be empty.");
            }

            _powerPointApp.ActivePresentation.Tags.Add(key, value);
        }

        public void SavePresentationToTag(IBarTag bt)
        {
            SaveTag(TagKey, true.ToString());

            // private const string ActiveColor = "ac";
            // private const string Getpositionoptions = "PositionOptions";
            // private const string Iac = "iac";
            // private const string Sizeselecteditemindex = "SizeSelectedItemIndex";
            // private const string Themeselecteditemindex = "ThemeSelectedItemIndex";

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            };


            string activeColor = JsonConvert.SerializeObject(bt.ActiveColor);
            SaveTag(ActiveColor, activeColor);

            string inactiveColor = JsonConvert.SerializeObject(bt.InactiveColor);
            SaveTag(Iac, inactiveColor);

            string positionOptions = JsonConvert.SerializeObject(bt.PositionOptions, Formatting.Indented, settings);
            SaveTag(Getpositionoptions, positionOptions);

            string sizeSelected = JsonConvert.SerializeObject(bt.SizeSelectedItemIndex);
            SaveTag(Sizeselecteditemindex, sizeSelected);

            string themeSelected = JsonConvert.SerializeObject(bt.ThemeSelectedItemIndex);
            SaveTag(Themeselecteditemindex, themeSelected);

            string dddisableFirstSlideChecked = JsonConvert.SerializeObject(bt.DisableFirstSlideChecked);
            SaveTag(DisableFirstSlideChecked, dddisableFirstSlideChecked);

            
            string ibbb = JsonConvert.SerializeObject(bt.IBar, Formatting.Indented, settings);
            SaveTag(IBAr, ibbb);
        }

        private Tags Tags()
        {
            return _powerPointApp.ActivePresentation.Tags;
        }

        private string GetTagByKey(string key)
        {
            return _powerPointApp.ActivePresentation.Tags[key];
        }
    }
}