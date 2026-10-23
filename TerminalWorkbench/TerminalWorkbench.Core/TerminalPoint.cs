namespace TerminalWorkbench.Core
{
    public sealed class TerminalPoint
    {
        public TerminalPoint(int number, int address, string name, string value, string unit, string changeTime)
        {
            Number = number;
            Address = address;
            Name = name ?? string.Empty;
            Value = value ?? string.Empty;
            Unit = unit ?? string.Empty;
            ChangeTime = changeTime ?? string.Empty;
        }

        public int Number { get; }
        public int Address { get; }
        public string Name { get; }
        public string Value { get; }
        public string Unit { get; }
        public string ChangeTime { get; }
    }
}
