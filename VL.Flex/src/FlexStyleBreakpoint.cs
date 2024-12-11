namespace VL.Flex
{
    public class FlexStyleBreakpoint : IFlexStyle, IDisposable
    {
        public Func<FlexLayoutArgs, IFlexStyle>? Breakpoint { internal get; set; }

        private FlexBase? _node = null;

        public void ApplyStyle(FlexBase node)
        {
            if (_node != node)
            {
                if (_node == null)
                {
                    _node = node;
                    _node.OnLayoutChanged += OnLayoutChanged;
                }
                else
                {
                    _node.OnLayoutChanged -= OnLayoutChanged;
                    _node = node;
                    _node.OnLayoutChanged += OnLayoutChanged;
                }
            }
        }

        private void OnLayoutChanged(FlexLayoutArgs args)
        {
            var style = Breakpoint?.Invoke(args);

            if (_node != null)
            {
                style?.ApplyStyle(_node);
            }
        }

        public void Dispose()
        {
            if (_node != null)
            {
                _node.OnLayoutChanged -= OnLayoutChanged;
            }
        }
    }
}
