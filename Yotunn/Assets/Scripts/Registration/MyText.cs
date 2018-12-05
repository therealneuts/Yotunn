using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.IO;
namespace Assets.Scripts.Registration
{
    public class MyText : MaskableGraphic
    {
        protected static Material s_DefaultText;
        [SerializeField]
        [TextArea(3, 10)]
        protected string m_Text;
        protected bool m_DisableFontTextureRebuiltCallback;

        public MyText()
        {
        }

        public MyText(string pString)
        {
            m_Text = pString;
        }

        //
        // Résumé :
        //     The minimum size the text is allowed to be.
        public int resizeTextMinSize { get; set; }
        //
        // Résumé :
        //     The maximum size the text is allowed to be. 1 = infinitly large.
        public int resizeTextMaxSize { get; set; }
        //
        // Résumé :
        //     The positioning of the text reliative to its RectTransform.
        public TextAnchor alignment { get; set; }
        //
        // Résumé :
        //     Use the extents of glyph geometry to perform horizontal alignment rather than
        //     glyph metrics.
        public bool alignByGeometry { get; set; }
        //
        // Résumé :
        //     The size that the Font should render at.
        public int fontSize { get; set; }
        //
        // Résumé :
        //     Horizontal overflow mode.
        public HorizontalWrapMode horizontalOverflow { get; set; }
        //
        // Résumé :
        //     Vertical overflow mode.
        public VerticalWrapMode verticalOverflow { get; set; }
        //
        // Résumé :
        //     FontStyle used by the text.
        public FontStyle fontStyle { get; set; }
        //
        // Résumé :
        //     Should the text be allowed to auto resized.
        public bool resizeTextForBestFit { get; set; }
        //
        // Résumé :
        //     (Read Only) Provides information about how fonts are scale to the screen.
        //
        // Résumé :
        //     Line spacing, specified as a factor of font line height. A value of 1 will produce
        //     normal line spacing.
        public float lineSpacing { get; set; }
        //
        // Résumé :
        //     Whether this Text will support rich text.
        public bool supportRichText { get; set; }
        //
        // Résumé :
        //     The Font used by the text.
        public Font font { get; set; }
        //
        // Résumé :
        //     Called by the layout system.


    }
}
