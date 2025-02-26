using YogaSharp;

namespace VL.Flex
{
    /// <summary>
    /// Default (global) config that used by default on every node unless, config set for a node explicitly.
    /// </summary>
    public class FlexConfigGlobal : IDisposable
    {
        protected unsafe YGConfig* _handle = YGConfig.GetDefault();
        public unsafe YGConfig* Handle { get => _handle; }

        /// <summary>
        /// Default rounding for DiP vvvv should be 100 e.g. 1/100 = 0.01 DiP one pixel
        /// </summary>
        private float _pointScaleFactor = 100.0f;

        /// <summary>
        /// Yoga will by default round final layout positions and dimensions to the nearest point.<br/>
        /// `pointScaleFactor` controls the density of the grid used for layout rounding (e.g. to round to the closest display pixel).<br/>
        /// May be set to 0.0f to avoid rounding the layout results.
        /// </summary>
        public float PointScaleFactor
        {
            internal get => _pointScaleFactor;
            set
            {
                if (value != _pointScaleFactor)
                {
                    unsafe
                    {
                        _handle->SetPointScaleFactor(value);
                    }
                    _pointScaleFactor = value;
                }
            }
        }

        private YGErrata _errata = YGErrata.None;

        /// <summary>
        /// Configures how Yoga balances W3C conformance vs compatibility with layouts created against earlier versions of Yoga.<br/>
        /// <br/>
        /// By default Yoga will prioritize W3C conformance. `Errata` may be set to ask Yoga to produce specific incorrect behaviors, e.g. `YGConfigSetErrata(config, <see cref="YGErrata.StretchFlexBasis"/>)`.<br/>
        /// <br/>
        /// YGErrata is a bitmask, and multiple errata may be set at once. Predefined constants exist for convenience:<br/>
        /// <list type="table">
        ///   <item>
        ///     <term><see cref="YGErrata.None"/></term>
        ///     <description>No errata</description>
        ///   </item>
        ///   <item>
        ///     <term><see cref="YGErrata.Classic"/></term>
        ///     <description>Match layout behaviors of Yoga 1.x</description>
        ///   </item>
        ///   <item>
        ///     <term><see cref="YGErrata.All"/></term>
        ///     <description>Match layout behaviors of Yoga 1.x, including `UseLegacyStretchBehaviour`</description>
        ///   </item>
        /// </list>
        /// </summary>
        public YGErrata Errata
        {
            internal get => _errata;
            set
            {
                if (value != _errata)
                {
                    unsafe
                    {
                        _handle->SetErrata(value);
                    }
                    _errata = value;
                }
            }
        }

        private bool _useWebDefaults = false;

        /// <summary>
        /// Yoga by default creates new nodes with style defaults different from flexbox on web (e.g. <see cref="YGFlexDirection.Column"/> and <see cref="YGPositionType.Relative"/>).<br/>
        /// `UseWebDefaults` instructs Yoga to instead use a default style consistent with the web.
        /// </summary>
        public bool UseWebDefaults
        {
            internal get => _useWebDefaults;
            set
            {
                if (_useWebDefaults != value)
                {
                    unsafe
                    {
                        _handle->SetUseWebDefaults(value);
                    }
                    _useWebDefaults = value;
                }
            }
        }

        public FlexConfigGlobal()
        {
            ConfigSetDefaults();
        }

        public unsafe void ConfigSetDefaults()
        {
            _handle->SetPointScaleFactor(_pointScaleFactor);
            _handle->SetErrata(_errata);
            _handle->SetUseWebDefaults(_useWebDefaults);
        }

        public void Dispose()
        {
            unsafe
            {
                if (_handle != null)
                {
                    _handle->Dispose();
                }
            }

            GC.SuppressFinalize(this);
        }

    }
}
