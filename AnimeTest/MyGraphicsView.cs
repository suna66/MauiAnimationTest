using System;
using Microsoft.Maui.Graphics;

namespace AnimeTest
{
	public class MyGraphicsView : GraphicsView
	{
		GraphicsDrawable _drawable = null;

		public MyGraphicsView()
		{
			_drawable = new GraphicsDrawable();
			this.Drawable = _drawable;
			this.BackgroundColor = Colors.White;
            this.Loaded += OnLoad;

		}

		private void OnLoad(object sender, EventArgs e)
		{
			TimeSpan ts = TimeSpan.FromMilliseconds(1000 / 60);
            IDispatcherTimer timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = ts;
            timer.Tick += (s, e) => this.Invalidate();
            timer.Start();
        }
    }

	public class GraphicsDrawable : IDrawable
	{
		private int _pointX = 0;
		private int _direct = 1;
		private int _previusTick = 0;

		public GraphicsDrawable()
		{
			_previusTick = Environment.TickCount;
		}

        public void Draw(ICanvas canvas, RectF dirtyRect)
		{
			int currentTick = Environment.TickCount;


            canvas.FontColor = Colors.Blue;
            canvas.FontSize = 18;
            canvas.DrawString($"{1000/(currentTick - _previusTick)}FPS", 0, 0, 380, 100, HorizontalAlignment.Left, VerticalAlignment.Top);

            canvas.FillColor = Colors.YellowGreen;
			canvas.FillCircle(new Point(_pointX, 100), 10.0d);
            _pointX += _direct * 10;
			if (_pointX >= 600)
			{
				_direct = -1;
			}
			if (_pointX <= 0)
			{
				_direct = 1;
			}
			_previusTick = currentTick;
        }
	}
}

