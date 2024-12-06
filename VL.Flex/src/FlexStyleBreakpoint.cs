namespace VL.Flex
{
    public class FlexStyleBreakpoint : IFlexStyle, IDisposable
    {
        public Func<FlexBase?, IFlexStyle>? Breakpoint { internal get; set; }

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

            var root = node.GetRoot();

            var style = Breakpoint?.Invoke(node);
            style?.ApplyStyle(node);
        }

        private void OnLayoutChanged(FlexBase node)
        {
            var root = node.GetRoot();

            var style = Breakpoint?.Invoke(root);
            style?.ApplyStyle(node);


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
