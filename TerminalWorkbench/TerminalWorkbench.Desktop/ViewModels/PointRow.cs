using TerminalWorkbench.Core;

namespace TerminalWorkbench.Desktop.ViewModels
{
    public sealed class PointRow : ObservableObject
    {
        private string name;
        private string value;
        private string unit;
        private string changeTime;

        public PointRow(TerminalPoint point)
        {
            Number = point.Number;
            Address = point.Address;
            Apply(point);
        }

        public int Number { get; }
        public int Address { get; }
        public string AddressText => "0x" + Address.ToString("X4");
        public string Name => name;
        public string Value => value;
        public string Unit => unit;
        public string ChangeTime => changeTime;

        public void Apply(TerminalPoint point)
        {
            SetProperty(ref name, point.Name, nameof(Name));
            SetProperty(ref value, point.Value, nameof(Value));
            SetProperty(ref unit, point.Unit, nameof(Unit));
            SetProperty(ref changeTime, point.ChangeTime, nameof(ChangeTime));
        }
    }
}
