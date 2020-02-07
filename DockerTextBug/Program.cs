using System;
using System.IO;
using SkiaSharp;

namespace DockerTextBug
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Rendering DockerTextBug...");

			using var surface = SKSurface.Create(new SKImageInfo(256, 256));
			using var canvas = surface.Canvas;
			canvas.Clear(SKColors.SkyBlue);

			using var typeface = SKTypeface.FromFile("Karla-Regular.ttf");
			using var paint = new SKPaint
			{
				Color = SKColors.Yellow,
				TextAlign = SKTextAlign.Center,
				TextSize = 64,
				// Doesn't work
				// Typeface = SKTypeface.Default
				// Works
				Typeface = typeface
			};

			canvas.DrawText("foobar", 128, 128, paint);
			using var image = surface.Snapshot();
			using var data = image.Encode();

			using var stream = File.Create($"test_{Environment.OSVersion.Platform}.png");
			Console.WriteLine($"Saving to {stream.Name}...");
			data.SaveTo(stream);
		}
	}
}
