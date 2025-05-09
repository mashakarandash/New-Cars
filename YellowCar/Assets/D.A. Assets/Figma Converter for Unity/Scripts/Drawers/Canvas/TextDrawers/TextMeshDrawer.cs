﻿using DA_Assets.FCU.Extensions;
using DA_Assets.FCU.Model;
using DA_Assets.Shared;
using DA_Assets.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if TextMeshPro
using TMPro;
#endif

namespace DA_Assets.FCU.Drawers.CanvasDrawers
{
    [Serializable]
    public class TextMeshDrawer : MonoBehaviourBinder<FigmaConverterUnity>
    {
#if TextMeshPro
        public void Draw(FObject fobject)
        {
            fobject.Data.GameObject.TryAddGraphic(out TextMeshProUGUI text);

            text.text = fobject.GetText();

            text.overrideColorTags = monoBeh.Settings.TextMeshSettings.OverrideTags;
            text.enableAutoSizing = monoBeh.Settings.TextMeshSettings.AutoSize;
#if UNITY_2023_1_OR_NEWER
            if (monoBeh.Settings.TextMeshSettings.Wrapping)
            {
                text.textWrappingMode = TextWrappingModes.Normal;
            }
            else
            {
                text.textWrappingMode = TextWrappingModes.NoWrap;
            }

#else
            text.enableWordWrapping = monoBeh.Settings.TextMeshSettings.Wrapping;
#endif

            text.richText = monoBeh.Settings.TextMeshSettings.RichText;
            text.raycastTarget = monoBeh.Settings.TextMeshSettings.RaycastTarget;
            text.parseCtrlCharacters = monoBeh.Settings.TextMeshSettings.ParseEscapeCharacters;
            text.useMaxVisibleDescender = monoBeh.Settings.TextMeshSettings.VisibleDescender;
#if UNITY_2023_4_OR_NEWER == false
            text.enableKerning = monoBeh.Settings.TextMeshSettings.Kerning;
#endif
            text.extraPadding = monoBeh.Settings.TextMeshSettings.ExtraPadding;
            text.horizontalMapping = monoBeh.Settings.TextMeshSettings.HorizontalMapping;
            text.verticalMapping = monoBeh.Settings.TextMeshSettings.VerticalMapping;
            text.geometrySortingOrder = monoBeh.Settings.TextMeshSettings.GeometrySorting;

            SetFont(text, fobject);

            text.alignment = fobject.GetTextAnchor().ToTextMeshAnchor();

            SetFontSize(text, fobject);
            SetFontCase(text, fobject);
            SetOverflowMode(text, fobject);
            SetColor(text, fobject);
        }

        private void SetFontSize(TMP_Text text, FObject fobject)
        {
            if (monoBeh.Settings.TextMeshSettings.AutoSize)
            {
                text.fontSizeMin = 1;
                text.fontSizeMax = fobject.Style.FontSize;
            }
            else
            {
                text.fontSize = fobject.Style.FontSize;
            }
        }

        private void SetFont(TMP_Text text, FObject fobject)
        {
            TMP_FontAsset font = monoBeh.FontLoader.GetFontFromArray(fobject, monoBeh.FontLoader.TmpFonts);
            text.font = font;
        }

        private void SetFontCase(TMP_Text text, FObject fobject)
        {
            FontStyles textDecoration = FontStyles.Normal;
            FontStyles textCase = FontStyles.Normal;
            FontStyles textItalic = FontStyles.Normal;
            FontStyles textBold = FontStyles.Normal;

            if (fobject.Data.HasFontAsset == false)
            {
                if (fobject.Style.Italic.ToBoolNullFalse())
                {
                    textItalic = FontStyles.Italic;
                }

                if (fobject.Style.FontWeight > 600)
                {
                    textBold = FontStyles.Bold;
                }
            }

            switch (fobject.Style.TextDecoration)
            {
                case "UNDERLINE":
                    textDecoration = FontStyles.Underline;
                    break;
                case "STRIKETHROUGH":
                    textDecoration = FontStyles.Strikethrough;
                    break;
            }

            switch (fobject.Style.TextCase)
            {
                case "UPPER":
                    textCase = FontStyles.UpperCase;
                    break;
                case "LOWER":
                    textCase = FontStyles.LowerCase;
                    break;
                case "TITLE":
                    textCase = FontStyles.Normal;
                    break;
                case "SMALL_CAPS":
                    textCase = FontStyles.SmallCaps;
                    break;
            }

            FontStyles final = textDecoration | textCase | textItalic | textBold;

            text.fontStyle = final;
        }

        private void SetOverflowMode(TMP_Text text, FObject fobject)
        {
            TextOverflowModes textTurncate = monoBeh.Settings.TextMeshSettings.Overflow;

            if (fobject.Style.TextAutoResize.IsEmpty() == false)
            {
                switch (fobject.Style.TextCase)
                {
                    case "ENDING":
                        textTurncate = TextOverflowModes.Ellipsis;
                        break;
                }
            }

            text.overflowMode = textTurncate;
        }

        private void SetColor(TMP_Text text, FObject fobject)
        {
            fobject.TryGetFills(monoBeh, out Paint solidFill, out Paint gradientFill);
            bool hasStroke = fobject.TryGetStrokes(monoBeh, out Paint solidStroke, out Paint gradientStroke);

            text.enableVertexGradient = false;

            if (solidFill.IsDefault() == false)
            {
                text.color = solidFill.Color.SetFigmaAlpha(solidFill.Opacity);
            }
            else if (gradientFill.IsDefault() == false)
            {
                List<GradientColorKey> gradientColorKeys = gradientFill.ToGradientColorKeys();
                text.color = gradientColorKeys.First().color.SetFigmaAlpha(gradientFill.Opacity);
            }

            if (hasStroke && fobject.StrokeAlign == "INSIDE")
            {
                float normalizedWidth = fobject.StrokeWeight / text.preferredHeight;
                text.outlineWidth = normalizedWidth;

                if (solidStroke.IsDefault() == false)
                {
                    text.outlineColor = solidStroke.Color.SetFigmaAlpha(solidStroke.Opacity);
                }
                else if (gradientStroke.IsDefault() == false)
                {
                    List<GradientColorKey> gradientColorKeys = gradientStroke.ToGradientColorKeys();
                    text.outlineColor = gradientColorKeys.First().color.SetFigmaAlpha(gradientStroke.Opacity);
                }
            }
            else
            {
                text.outlineWidth = 0;
            }
        }
#endif
    }
}
