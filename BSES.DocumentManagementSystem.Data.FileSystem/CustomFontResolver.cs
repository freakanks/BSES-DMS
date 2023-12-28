﻿using PdfSharp.Fonts;

namespace BSES.DocumentManagementSystem.Data.FileSystem
{
    internal class CustomFontResolver : IFontResolver
    {
        public byte[] GetFont(string faceName)
        {
            if (faceName.Equals("ArialMT", StringComparison.OrdinalIgnoreCase))
            {
                string fontFilePath = Path.Combine("/", "usr", "share", "fonts", "truetype", "msttcorefonts", "Arial.ttf");
                return File.ReadAllBytes(fontFilePath);
            }
            else
            {
                string fontFilePath = Path.Combine("/", "usr", "share", "fonts", "truetype", "msttcorefonts", "Arial.ttf");
                return File.ReadAllBytes(fontFilePath);
            }
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            return isBold && isItalic
                ? new FontResolverInfo("Arial", PdfSharp.Drawing.XStyleSimulations.BoldItalicSimulation)
                : isBold ? new FontResolverInfo("Arial", PdfSharp.Drawing.XStyleSimulations.BoldSimulation)
                : isItalic ? new FontResolverInfo("Arial", PdfSharp.Drawing.XStyleSimulations.ItalicSimulation)
                : new FontResolverInfo("Arial", PdfSharp.Drawing.XStyleSimulations.None);
        }
    }
}
