﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.CSS.Editor.Completion;

namespace Glyphfriend.GlyphCompletionProviders
{
	[Export(typeof(ICssCompletionEntryGlyphProvider))]
	class IonicGlyphCompletionProvider : ICssCompletionEntryGlyphProvider
	{
		// Store the default glyph for this particular library
		private static BitmapFrame _defaultGlyph = BitmapFrame.Create(new Uri("pack://application:,,,/Glyphfriend;component/Glyphs/Ionic/_default.png", UriKind.RelativeOrAbsolute));

		// Define a Regular Expression check for matches from this library
		private static Regex _regex = new Regex(@"^ionicons(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public ImageSource GetCompletionGlyph(string entryName, Uri sourceUri, CssNameType nameType)
        {
            // If the source Uri for the image is null, ignore it
            if (sourceUri == null) { return null; }
            // Get the file path of the source being used
            string filename = Path.GetFileName(sourceUri.ToString()).Trim();
            // Determine if this matches our filename
            if (_regex.IsMatch(filename))
            {
                try
                {
                    // Attempt to grab an icon for the current entry
                    return BitmapFrame.Create(new Uri(String.Format("pack://application:,,,/Glyphfriend;component/Glyphs/Ionic/{0}.png", entryName.Substring(5)), UriKind.RelativeOrAbsolute));
                }
                catch
                {
                    // If one was not available, serve the default icon
                    return _defaultGlyph;
                }
            }

            return null;
        }
	}
}