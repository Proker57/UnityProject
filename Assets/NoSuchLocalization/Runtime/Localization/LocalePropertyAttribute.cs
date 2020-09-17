using NoSuchStudio.Localization;
using UnityEngine;

/// <summary>
/// Different supported drawers for <see cref="Locale"/> fields. Usable by adding a <see cref="LocalePropertyAttribute" /> to Locale or string fields.
/// </summary>
public enum LocalePropertyDrawMode {
    Auto,
    FlatDropdown,
    MenuDropdown,
    // DoubleDropdown,
    // AutoCompletePopup
}

public class LocalePropertyAttribute : PropertyAttribute {
    bool _filterToAvailable;
    LocalePropertyDrawMode _drawMode;
    public bool filterToAvailable {
        get { return _filterToAvailable; }
    }
    public LocalePropertyDrawMode drawMode {
        get { return _drawMode; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filterToAvailable">Whether the field value should be restricted to locales that are explicitly added to the <see cref="LocalizationService"/></param>
    /// <param name="drawMode">The UI element to use for drawing the locale field in inspector.</param>
    public LocalePropertyAttribute(bool filterToAvailable = false, LocalePropertyDrawMode drawMode = LocalePropertyDrawMode.FlatDropdown) {
        _filterToAvailable = filterToAvailable;
        _drawMode = drawMode;

        /*if (_filterToAvailable && _drawMode == LocalePropertyDrawMode.DoubleDropdown) {
            _drawMode = LocalePropertyDrawMode.FlatDropdown;
            UnityObjectLoggerExt.LogWarn<LocalePropertyAttribute>("Double dropdown mode isn't compatible with filtered selection. Falling back to FlatDropDown.");
        }*/
    }
}