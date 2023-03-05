using System;
using Microsoft.Maui.Graphics;
using GFont = Microsoft.Maui.Graphics.Font;

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
			this.StartInteraction += startInteraction;
			this.DragInteraction += dragInteraction;
			this.EndInteraction += endInteraction;
		}

		private void OnLoad(object sender, EventArgs e)
		{
			TimeSpan ts = TimeSpan.FromMilliseconds(1000 / 60);
            IDispatcherTimer timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = ts;
            timer.Tick += (s, e) => this.Invalidate();
            timer.Start();
        }

		private void startInteraction(object sender, TouchEventArgs e)
		{
			Console.WriteLine("start interaction");
		}

		private void dragInteraction(object sender, TouchEventArgs e)
		{
            Console.WriteLine("drag interaction");
        }

		private void endInteraction(object sender, TouchEventArgs e)
		{
            Console.WriteLine("end interaction");
			_drawable.ChangeColor();
        }
    }

	public class GraphicsDrawable : IDrawable
	{
		private int _pointX = 0;
		private int _direct = 1;
		private int _previusTick = 0;
		private Color[] pattern =
		{
			Colors.Red,
			Colors.Violet,
			Colors.Blue,
			Colors.Chocolate,
			Colors.DarkGreen,
			Colors.GreenYellow
		};
		private int _color = 0;

		public GraphicsDrawable()
		{
			_previusTick = Environment.TickCount;
		}

		public void ChangeColor()
		{
			_color++;
			if (_color >= pattern.Length)
			{
				_color = 0;
			}
		}

        public void Draw(ICanvas canvas, RectF dirtyRect)
		{
			int currentTick = Environment.TickCount;


            canvas.FontColor = pattern[_color];
            canvas.FontSize = 18;
			canvas.Font = GFont.DefaultBold;
            canvas.DrawString($"{1000/(currentTick - _previusTick)}FPS", 0, 0, 380, 100, HorizontalAlignment.Left, VerticalAlignment.Top);

            canvas.FillColor = pattern[_color];
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

