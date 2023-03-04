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

        private bool TimerLoop()
        {
            this.Invalidate();
            return true;
        }
    }

	public class GraphicsDrawable : IDrawable
	{
		private int _pointX = 0;
		private int _direct = 1;

		public void Draw(ICanvas canvas, RectF dirtyRect)
		{
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
        }
	}
}

