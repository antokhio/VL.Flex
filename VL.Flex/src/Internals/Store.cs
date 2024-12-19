namespace VL.Flex.Internals
{
    /// <summary>
    /// Singleton storage for internals
    /// </summary>
    internal class Store
    {
        private static FlexInternals _internals = new FlexInternals();
        private static NodeRegistry _registry = new NodeRegistry();
        public static FlexInternals GetInternals()
        {
            if (_internals == null)
            {
                _internals = new FlexInternals();
            }
            return _internals;
        }

        public static NodeRegistry GetRegistry()
        {
            if (_registry == null)
            {
                _registry = new NodeRegistry();
            }

            return _registry;
        }
    }
}
