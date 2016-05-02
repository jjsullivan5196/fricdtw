//dtwread
//
//Usage:
//dtwread {training_data} {test_data} data_column {opt: match_window}
//
//Arguments:
//
//training_data/test_data: two 4 field CSVs without titles, assumed format of values x,y,z,time
//data_column: which column of the data to test
//match_window (Optional): set a match window distance for which to map points, must be larger
//than the absolute difference of your input/training sets' size or score will be -1 (Null)

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
				training_data.Add(new tPoint(Double.Parse(data[dfield]) * 100, Double.Parse(data[3])));
			}
			
			foreach(string line in test)
			{
				string[] data = line.Split(',');
				test_data.Add(new tPoint(Double.Parse(data[dfield]) * 100, Double.Parse(data[3])));
			}
			
			RecognizerDTW rec = new RecognizerDTW(training_data);
			
			double score = rec.DTWDistance(test_data);
			Console.WriteLine("{0:0.0000}", score);
		}
	}
}