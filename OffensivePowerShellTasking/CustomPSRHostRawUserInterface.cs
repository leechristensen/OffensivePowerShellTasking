using System;
using System.Collections.Generic;
using System.Management.Automation.Host;
using System.Text;

namespace OffensivePowerShellTasking
{
    public class CustomPSRHostRawUserInterface : PSHostRawUserInterface
    {
        // Warning: Setting _outputWindowSize too high will cause OutOfMemory execeptions.  I assume this will happen with other properties as well
        private Size _windowSize = new Size { Width = 120, Height = 100 };

        private Coordinates _cursorPosition = new Coordinates { X = 0, Y = 0 };

        private int _cursorSize = 1;
        private ConsoleColor _foregroundColor = ConsoleColor.White;
        private ConsoleColor _backgroundColor = ConsoleColor.Black;

        private Size _maxPhysicalWindowSize = new Size
        {
            Width = int.MaxValue,
            Height = int.MaxValue
        };

        private Size _maxWindowSize = new Size { Width = 100, Height = 100 };
        private Size _bufferSize = new Size { Width = 100, Height = 1000 };
        private Coordinates _windowPosition = new Coordinates { X = 0, Y = 0 };
        private String _windowTitle = "";

        public override ConsoleColor BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        public override Size BufferSize
        {
            get { return _bufferSize; }
            set { _bufferSize = value; }
        }

        public override Coordinates CursorPosition
        {
            get { return _cursorPosition; }
            set { _cursorPosition = value; }
        }

        public override int CursorSize
        {
            get { return _cursorSize; }
            set { _cursorSize = value; }
        }

        public override void FlushInputBuffer()
        {
            throw new NotImplementedException("FlushInputBuffer is not implemented.");
        }

        public override ConsoleColor ForegroundColor
        {
            get { return _foregroundColor; }
            set { _foregroundColor = value; }
        }

        public override BufferCell[,] GetBufferContents(Rectangle rectangle)
        {
            throw new NotImplementedException("GetBufferContents is not implemented.");
        }

        public override bool KeyAvailable
        {
            get { throw new NotImplementedException("KeyAvailable is not implemented."); }
        }

        public override Size MaxPhysicalWindowSize
        {
            get { return _maxPhysicalWindowSize; }
        }

        public override Size MaxWindowSize
        {
            get { return _maxWindowSize; }
        }

        public override KeyInfo ReadKey(ReadKeyOptions options)
        {
            throw new NotImplementedException("ReadKey is not implemented.  The script is asking for input, which is a problem since there's no console.  Make sure the script can execute without prompting the user for input.");
        }

        public override void ScrollBufferContents(Rectangle source, Coordinates destination, Rectangle clip, BufferCell fill)
        {
            throw new NotImplementedException("ScrollBufferContents is not implemented");
        }

        public override void SetBufferContents(Rectangle rectangle, BufferCell fill)
        {
            throw new NotImplementedException("SetBufferContents is not implemented.");
        }

        public override void SetBufferContents(Coordinates origin, BufferCell[,] contents)
        {
            throw new NotImplementedException("SetBufferContents is not implemented");
        }

        public override Coordinates WindowPosition
        {
            get { return _windowPosition; }
            set { _windowPosition = value; }
        }

        public override Size WindowSize
        {
            get { return _windowSize; }
            set { _windowSize = value; }
        }

        public override string WindowTitle
        {
            get { return _windowTitle; }
            set { _windowTitle = value; }
        }
    }
}
