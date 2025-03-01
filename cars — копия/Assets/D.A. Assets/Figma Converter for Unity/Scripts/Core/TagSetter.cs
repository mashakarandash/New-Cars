﻿using DA_Assets.FCU.Extensions;
using DA_Assets.FCU.Model;
using DA_Assets.Shared;
using DA_Assets.Shared.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DA_Assets.FCU
{
    [Serializable]
    public class TagSetter : MonoBehaviourBinder<FigmaConverterUnity>
    {
        public Dictionary<FcuTag, int> TagsCounter { get; set; }

        public void SetTags(FObject page)
        {
            DALogger.Log(FcuLocKey.log_tagging.Localize());

            SetTags1(page);
            SetTags2(page);
        }

        private void SetTags1(FObject parent)
        {
            if (parent.ContainsTag(FcuTag.Frame))
            {
                parent.Data.Hierarchy = parent.Data.FormattedName;
            }

            for (int i = 0; i < parent.Children.Count; i++)
            {
                FObject child = parent.Children[i];

                child.Data = new SyncData
                {
                    ChildIndexes = new List<int>()
                };

                child.SetFixedName();
                child.Data.Parent = parent;

                if (GetManualTag(child, out FcuTag manualTag))
                {
                    child.AddTag(manualTag);
                    monoBeh.Log($"GetManualTag | {child.Name} | {manualTag}", FcuLogType.SetTag);

                    if (manualTag == FcuTag.Image)
                    {
                        child.Data.ForceImage = true;
                    }
                    else if (manualTag == FcuTag.Container)
                    {
                        child.Data.ForceContainer = true;
                    }
                }

                if (parent.ContainsTag(FcuTag.Page))
                {
                    child.AddTag(FcuTag.Frame);
                }

                if (child.IsInstanceType())
                {
                    /*string instanceKey = monoBeh.CurrentProject.GetInstanceKey(child.Id);
                    string compId = monoBeh.ComponentsParser.GetComponentIdByKey(instanceKey);
                    FObject comp = monoBeh.ComponentsParser.GetById(compId);

                    child.Data.FComponent = new FComponent
                    {
                        Component = comp,
                        ProjectUrl = ""
                    };*/
                }

                if (child.LayoutWrap == "WRAP" ||
                    child.LayoutMode == "HORIZONTAL" ||
                    child.LayoutMode == "VERTICAL")
                {
                    child.AddTag(FcuTag.AutoLayoutGroup);
                }

                if (child.IsAnyMask())
                {
                    child.AddTag(FcuTag.Mask);
                }

                if (child.IsTextType())
                {
                    child.AddTag(FcuTag.Text);

                    if (child.Style.IsDefault() == false)
                    {
                        if (child.Style.TextAutoResize == "WIDTH_AND_HEIGHT")
                        {
                            child.AddTag(FcuTag.ContentSizeFitter);
                        }
                    }
                }
                else if (child.IsVectorType())
                {
                    child.AddTag(FcuTag.Vector);
                    child.AddTag(FcuTag.Image);
                }
                else if (child.Fills.IsEmpty() == false)
                {
                    child.AddTag(FcuTag.Image);
                }
                else if (child.Strokes.IsEmpty() == false)
                {
                    child.AddTag(FcuTag.Image);
                }

                if (child.Effects.IsEmpty() == false)
                {
                    foreach (Effect effect in child.Effects)
                    {
                        if (effect.IsShadowType())
                        {
                            child.AddTag(FcuTag.Shadow);
                            break;
                        }
                    }
                }

                if (child.Opacity != null && child.Opacity != 1)
                {
                    child.AddTag(FcuTag.CanvasGroup);
                }

                child.Data.Hierarchy = $"{parent.Data.Hierarchy}/{child.Data.FormattedName}";

                parent.Children[i] = child;

                if (child.Children.IsEmpty())
                {
                    continue;
                }

                SetTags1(child);
            }
        }

        private void SetFcuButtonTags(FObject parent, FObject child)
        {
            if (parent.ContainsTag(FcuTag.Button))
            {
                if (GetFcuButtonTag(child, out FcuTag customButtonTag))
                {
                    child.AddTag(customButtonTag);
                }
            }
        }

        private void SetTags2(FObject parent)
        {
            foreach (FObject child in parent.Children)
            {
                SetFcuButtonTags(parent, child);

                child.Data.IsEmpty = IsEmpty(child);

                if (child.Data.ForceImage)
                {
                    ///If a component is tagged with the 'img' tag, it will downloaded as a single image,
                    ///which means there is no need to look for child components for it.
                    child.Data.TagReason = nameof(child.Data.ForceImage);
                    monoBeh.Log($"SetTags2 | {child.Data.TagReason} | {child.Data.Hierarchy}", FcuLogType.SetTag);
                    continue;
                }

                if (IsRootVector(child, parent))
                {
                    ///If the component is a vector that is at the root of your frame, 
                    ///then we recognize it as a single image and do not look for child components for it, 
                    ///because vectors do not have it.
                    child.Data.TagReason = nameof(IsRootVector);
                    monoBeh.Log($"SetTags2 | {child.Data.TagReason} | {child.Data.Hierarchy}", FcuLogType.SetTag);
                    child.AddTag(FcuTag.Image);
                    child.Data.ForceImage = true;
                    continue;
                }

                if (monoBeh.Settings.MainSettings.RawImport == false)
                {
                    if (ContainsIcon(child))
                    {
                        child.Data.ForceContainer = true;
                        child.Data.TagReason = "ContainsIcon";
                    }
                    else if (CanBeSingleImage(child))
                    {
                        ///If the component tree contains only vectors and/or components whose tags
                        ///have flag 'CanBeInsideSingleImage == false', recognize that component as a single image.
                        child.Data.ForceImage = true;
                        child.AddTag(FcuTag.Image);
                        child.RemoveNotDownloadableTags();
                        child.Data.TagReason = "SingleImage";
                        monoBeh.Log($"SetTags2 | {child.Data.TagReason} | {child.Data.Hierarchy}", FcuLogType.SetTag);
                        continue;
                    }
                }

                if (child.Children.IsEmpty() == false)
                {
                    child.Data.TagReason = "children not empty";
                    monoBeh.Log($"SetTags2 | (child.Children.IsEmpty() == false) | {child.Data.Hierarchy}", FcuLogType.SetTag);
                    child.AddTag(FcuTag.Container);
                }

                if (child.Children.IsEmpty())
                    continue;

                SetTags2(child);
            }
        }

        private bool IsRootVector(FObject child, FObject parent)
        {
            bool isRootVector = false;

            if (child.Data.Tags.Contains(FcuTag.Vector) && parent.ContainsTag(FcuTag.Frame))
            {
                isRootVector = true;
            }

            return isRootVector;
        }

        private bool GetManualTag(FObject fobject, out FcuTag manualTag)
        {
            if (fobject.Data.FormattedName.Contains(FcuConfig.Instance.RealTagSeparator) == false)
            {
                manualTag = FcuTag.None;
                return false;
            }

            IEnumerable<FcuTag> fcuTags = Enum.GetValues(typeof(FcuTag))
               .Cast<FcuTag>()
               .Where(x => x != FcuTag.None);

            foreach (FcuTag fcuTag in fcuTags)
            {
                bool tagFind = FindManualTag(fobject.Data.FormattedName, fcuTag, false);

                if (tagFind)
                {
                    manualTag = fcuTag;
                    return true;
                }
            }

            manualTag = FcuTag.None;
            return false;
        }

        private bool GetFcuButtonTag(FObject fobject, out FcuTag customButtonTag)
        {
            IEnumerable<FcuTag> fcuTags = Enum.GetValues(typeof(FcuTag))
               .Cast<FcuTag>();

            foreach (FcuTag fcuTag in fcuTags)
            {
                TagConfig tc = fcuTag.GetTagConfig();

                if (tc.CustomButtonTag)
                {
                    bool tagFind = FindManualTag(fobject.Data.FormattedName, fcuTag, true);

                    if (tagFind)
                    {
                        customButtonTag = fcuTag;
                        return true;
                    }
                }
            }

            customButtonTag = FcuTag.None;
            return false;
        }

        private bool FindManualTag(string fixedName, FcuTag fcuTag, bool onlyContains)
        {
            string figmaTag = fcuTag.GetTagConfig().FigmaTag.ToLower();

            if (figmaTag.IsEmpty())
                return false;

            if (onlyContains)
            {
                if (fixedName.ToLower().Contains(figmaTag))
                {
                    return true;
                }
            }

            string[] nameParts = fixedName.ToLower().Replace(" ", "").Split(FcuConfig.Instance.RealTagSeparator);

            if (nameParts.Length >= 1)
            {
                string tagPart = nameParts[0];

                if (tagPart == figmaTag)
                {
                    monoBeh.Log($"CheckForTag | GetFigmaType | {fixedName} | tag: {figmaTag}", FcuLogType.SetTag);
                    return true;
                }
            }

            return false;
        }

        private bool CanBeSingleImage(FObject fobject)
        {
            int count = 0;
            CanBeSingleImageRecursive(fobject, ref count);
            return count == 0;
        }

        private bool ContainsIcon(FObject fobject)
        {
            if (fobject.Children.IsEmpty())
                return false;

            foreach (var item in fobject.Children)
            {
                if (item.Name.ToLower().Contains("icon"))
                {
                    return true;
                }
            }

            return false;
        }

        private void CanBeSingleImageRecursive(FObject fobject, ref int count)
        {
            if (CanBeInsideSingleImage(fobject) == false)
            {
                count++;
                return;
            }

            if (fobject.Children.IsEmpty())
                return;

            foreach (FObject child in fobject.Children)
                CanBeSingleImageRecursive(child, ref count);
        }

        private bool CanBeInsideSingleImage(FObject fobject)
        {
            if (fobject.Data.ForceContainer)
                return false;

            if (fobject.Data.ForceImage)
                return false;

            foreach (FcuTag fcuTag in fobject.Data.Tags)
            {
                TagConfig tc = fcuTag.GetTagConfig();

                if (tc.CanBeInsideSingleImage == false)
                    return false;

                if (monoBeh.UsingTrueShadow() && fcuTag == FcuTag.Shadow)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsEmpty(FObject fobject)
        {
            int count = 0;
            IsEmptyRecursive(fobject, ref count);
            return count == 0;
        }

        public void IsEmptyRecursive(FObject fobject, ref int count)
        {
            if (count > 0)
                return;

            if (fobject.Opacity == 0)
                return;

            if (fobject.IsZeroSize() && fobject.Type != "LINE")
                return;

            else if (fobject.IsVisible() == false)
                return;
            else if (fobject.Fills.IsEmpty() &&
                fobject.Strokes.IsEmpty() &&
                fobject.Effects.IsEmpty())
            {

            }
            else
            {
                count++;
                return;
            }

            if (fobject.Children.IsEmpty())
                return;

            foreach (var item in fobject.Children)
                IsEmptyRecursive(item, ref count);
        }

        public void CountTags(List<FObject> fobjects)
        {
            ConcurrentDictionary<FcuTag, ConcurrentBag<bool>> tagsCounter = new ConcurrentDictionary<FcuTag, ConcurrentBag<bool>>();

            Array fcuTags = Enum.GetValues(typeof(FcuTag));

            foreach (FcuTag tag in fcuTags)
            {
                tagsCounter.TryAdd(tag, new ConcurrentBag<bool>());
            }

            Parallel.ForEach(fobjects, fobject =>
            {
                if (fobject.Data.GameObject == null)
                {
                    return;
                }

                foreach (FcuTag tag in fobject.Data.Tags)
                {
                    tagsCounter[tag].Add(true);
                }
            });

            Dictionary<FcuTag, int> dictionary = tagsCounter.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Count
            );

            this.TagsCounter = dictionary;
        }
    }
}