using UnityEngine;
using UnityEngine.UI;

namespace NoSuchStudio.Localization.Localizers {
    /// <summary>
    /// Localizes <see cref="Image"/> by mirroring it (using its <see cref="Transform.localScale"/> property)
    /// based on the RTL-ness of <see cref="LocalizationService.CurrentLocale"/>.
    /// </summary>
    [RequireComponent(typeof(Image))]
    [AddComponentMenu(LocalizationService.ComponentMenuPath + "/Image Transform Localizer (Mirror)")]
    public class ImageTransformLocalizer : ComponentLocalizer<ImageTransformLocalizer, Image> {
        public static readonly Vector3 mirrorVector = new Vector3(-1, 1, 1);

        public override void UpdateComponent() {
            Locale locale = LocalizationService.CurrentLocale;
            Vector3 curScale = _component.transform.localScale;
            _component.transform.localScale = new Vector3(Mathf.Abs(curScale.x) * (locale.IsRTL ? -1 : 1), curScale.y, curScale.z);
        }
    }

}