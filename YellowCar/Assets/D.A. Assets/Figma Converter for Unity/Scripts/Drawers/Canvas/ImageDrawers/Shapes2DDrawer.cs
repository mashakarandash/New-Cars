﻿using DA_Assets.FCU.Model;
using DA_Assets.Shared;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DA_Assets.Shared.Extensions;
using UnityEngine.UI;
using DA_Assets.FCU.Extensions;

#if SHAPES_EXISTS
using Shapes2D;
using Shape = Shapes2D.Shape;
#endif

namespace DA_Assets.FCU.Drawers.CanvasDrawers
{
    public class Shapes2DDrawer : MonoBehaviourBinder<FigmaConverterUnity>
    {
        public void Draw(FObject fobject, Sprite sprite, GameObject target)
        {
            if (sprite != null)
            {
#if SHAPES_EXISTS
                if (target.TryGetComponent(out Shape oldShape))
                {
                    oldShape.Destroy();
                }
#endif
            }

            target.TryAddGraphic(out Image img);
            img.sprite = sprite;
            img.material = null;
            img.type = monoBeh.Settings.Shapes2DSettings.Type;
            img.raycastTarget = monoBeh.Settings.Shapes2DSettings.RaycastTarget;
            img.preserveAspect = monoBeh.Settings.Shapes2DSettings.PreserveAspect;
            img.raycastTarget = monoBeh.Settings.Shapes2DSettings.RaycastTarget;
#if UNITY_2020_1_OR_NEWER
            img.raycastPadding = monoBeh.Settings.Shapes2DSettings.RaycastPadding;
#endif

            if (sprite == null)
            {
#if SHAPES_EXISTS
                target.TryAddComponent(out Shape shape);

                SetColor(fobject, shape);
                SetCorners(fobject, shape);
                SetBlur(fobject, shape);
#endif
            }
            else
            {
                monoBeh.CanvasDrawer.ImageDrawer.UnityImageDrawer.SetColor(fobject, img);
            }
        }

#if SHAPES_EXISTS
        private void SetColor(FObject fobject, Shape shape)
        {
            bool hasFills = fobject.TryGetFills(monoBeh, out Paint solidFill, out Paint gradientFill);
            bool hasStroke = fobject.TryGetStrokes(monoBeh, out Paint solidStroke, out Paint gradientStroke);

            monoBeh.Log($"SetUnityImageColor | {fobject.Data.Hierarchy} | {fobject.Data.FcuImageType} | hasFills: {hasFills} | hasStroke: {hasStroke}");

            shape.settings.fillType = FillType.SolidColor;

            if (gradientFill.IsDefault() == false)
            {
                shape.settings.blur = 0.1f;
            }

            if (fobject.IsDrawableType())
            {
                if (hasFills)
                {
                    if (solidFill.IsDefault() == false)
                    {
                        Color c = solidFill.Color.SetFigmaAlpha(solidFill.Opacity);
                        shape.settings.fillColor = c;
                    }
                    else
                    {
                        Color c = Color.white;
                        shape.settings.fillColor = c;
                    }

                    if (gradientFill.IsDefault() == false)
                    {
                        monoBeh.CanvasDrawer.ImageDrawer.UnityImageDrawer.AddGradient(gradientFill, shape.gameObject);
                    }
                }
                else
                {
                    Color c = Color.white;
                    c.a = 0;
                    shape.settings.fillColor = c;
                }

                if (hasStroke)
                {
                    if (fobject.StrokeAlign == "INSIDE")
                    {
                        shape.settings.outlineSize = fobject.StrokeWeight;

                        if (gradientStroke.IsDefault())
                        {
                            shape.settings.outlineColor = solidStroke.Color.SetFigmaAlpha(solidStroke.Opacity);
                        }
                        else
                        {
                            List<GradientColorKey> gradientColorKeys = gradientStroke.ToGradientColorKeys();
                            shape.settings.outlineColor = gradientColorKeys.First().color;
                        }
                    }
                    else if (fobject.StrokeAlign == "OUTSIDE")
                    {
                        monoBeh.CanvasDrawer.ImageDrawer.UnityImageDrawer.AddUnityOutline(fobject, shape.gameObject, solidStroke, gradientStroke);
                    }
                    else
                    {
                        shape.settings.outlineSize = 0;
                    }
                }
                else
                {
                    shape.settings.outlineSize = 0;
                }
            }
        }

        private void SetCorners(FObject fobject, Shape shape)
        {
            if (fobject.Type == "ELLIPSE")
            {
                shape.settings.shapeType = ShapeType.Ellipse;
            }
            else
            {
                shape.settings.shapeType = ShapeType.Rectangle;

                if (fobject.CornerRadiuses != null)
                {
                    Vector4 cr = fobject.GetCornerRadius(ImageComponent.Shape);

                    shape.settings.roundnessPerCorner = true;

                    shape.settings.roundnessBottomLeft = cr.x;
                    shape.settings.roundnessBottomRight = cr.y;
                    shape.settings.roundnessTopRight = cr.z;
                    shape.settings.roundnessTopLeft = cr.w;
                }
                else if (fobject.CornerRadius.ToFloat() != 0)
                {
                    shape.settings.roundnessPerCorner = true;

                    shape.settings.roundnessBottomLeft = fobject.CornerRadius.ToFloat();
                    shape.settings.roundnessBottomRight = fobject.CornerRadius.ToFloat();
                    shape.settings.roundnessTopRight = fobject.CornerRadius.ToFloat();
                    shape.settings.roundnessTopLeft = fobject.CornerRadius.ToFloat();

                    //for new versions only
                    //shape.settings.roundnessPerCorner = false;
                    //shape.settings.roundness = source.CornerRadius;
                }
            }
        }

        private void SetBlur(FObject fobject, Shape shape)
        {
            foreach (Effect effect in fobject.Effects)
            {
                if (effect.Visible == false)
                {
                    continue;
                }

                if (effect.Type == "LAYER_BLUR")
                {
                    shape.settings.blur = effect.Radius;
                    break;
                }
            }
        }
#endif

    }
}
