using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSystem
{
    public class Tile
    {
        public event EventHandler HighlightStatusChanged;

        private bool _isHighlighted = false;
        public bool IsHighlighted
        {
            get => _isHighlighted;
            internal set
            {
                _isHighlighted = value;
                OnHighlightStatusChanged(EventArgs.Empty);
            }
        }

        public Position Position { get; }
       // public bool IsHighlighted { get; internal set; }

        public Tile(int x, int y)
        {
            Position = new Position {X = x, Y = y};
        }
        protected virtual void OnHighlightStatusChanged(EventArgs args)
        {
            EventHandler handler = HighlightStatusChanged;
            handler?.Invoke(this, args);

        }
    }
}
