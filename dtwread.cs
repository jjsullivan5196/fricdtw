using System;
using System.IO;
using System.Collections.Generic;
using FricDTW;

namespace Application
{
	class Program
	{
		static void Main(string[] args)
		{	
			string[] training = File.ReadAllLines(args[0]);
			string[] test = File.ReadAllLines(args[1]);
			int dfield = Int32.Parse(args[2]);
			
			List<tPoint> training_data = new List<tPoint>();
			List<tPoint> test_data = new List<tPoint>();
			
			foreach(string line in training)
			{
				string[] data = line.Split(',');
				training_data.Add(new tPoint(Convert.ToDouble(data[dfield]) * 100, Convert.ToDouble(data[3])));
			}
			
			foreach(string line in test)
			{
				string[] data = line.Split(',');
				test_data.Add(new tPoint(Convert.ToDouble(data[dfield]) * 100, Convert.ToDouble(data[3])));
			}
			
			RecognizerDTW rec = new RecognizerDTW(training_data);
			
			double score = args.Length == 4 ? rec.RecognizeWindow(test_data, Int32.Parse(args[3])) : rec.Recognize(test_data);
			Console.WriteLine("{0:0.0000}", score);
		}
	}
}