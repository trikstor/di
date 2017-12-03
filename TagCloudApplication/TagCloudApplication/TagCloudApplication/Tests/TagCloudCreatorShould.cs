﻿using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloudApplication.Tests
{
    [TestFixture]
    public class TagCloudCreatorShould
    {
        private TagCloudCreator Creator;
        private readonly int MinFontSize = 8;
        private readonly int MaxFontSize = 45;
        private readonly string FontName = "Arial";

        [SetUp]
        public void SetUp()
        {
            Creator = new TagCloudCreator(MinFontSize, MaxFontSize, FontName);
        }
        
        [Test]
        public void GiveCorrespondingFontsForTagCollection()
        {
            var tagCollection = new Dictionary<string, int>
            {
                {"арбуз", 3},
                {"ананас", 2},
                {"яблоко", 1}
            };
            var expected = new Dictionary<string, Font>
            {
                {"арбуз", new Font(FontName, 45)},
                {"ананас", new Font(FontName, 30)},
                {"яблоко", new Font(FontName, 15)}
            };
            Creator.Create(tagCollection).Values.ShouldBeEquivalentTo(expected.Values, options => 
                options.Excluding(pr => pr.SelectedMemberInfo.Name == "NativeFont"));   
        }
    }
}