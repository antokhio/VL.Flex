using YogaSharp;

namespace VL.Flex
{
    /// <summary>
    /// Default (global) config that set by default to every node unless, config set for a node explicitly.
    /// </summary>
    public class FlexConfigGlobal : IDisposable
    {
        protected unsafe YGConfig* _handle = YGConfig.GetDefault();
        public unsafe YGConfig* Handle { get => _handle; }

        private float _pointScaleFactor = 0.0f;
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
            ConfigDefault();
        }

        public unsafe void ConfigDefault()
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
