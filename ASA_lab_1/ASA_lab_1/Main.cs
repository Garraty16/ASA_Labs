using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace ASA_lab_1
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// Read data 
			string text = File.ReadAllText("Laba1_1.csv", Encoding.UTF8);
			//Console.WriteLine(text);
			// Split to lines
			string[] lines = text.Split("\n".ToCharArray());
			
			// Convert lines to List<Elem>
			List<Elem> elems = new List<Elem>();
			int i = 1;
			foreach (string line in lines){
				elems.Add(new Elem(line));
				i++;
				if (i == lines.Length)
					break;
			}
			
			Console.WriteLine("Press any key to see ascendingly sorted data...");
			Console.Read();
			Quicksort(elems, 0, 999, 1);
			foreach(Elem el in elems){
				el.Print();	
			}
			
			Console.WriteLine("\nPress any key to see descendingly sorted data...");
			Console.Read();
			Quicksort(elems, 0, 999, 0);
			foreach(Elem el in elems){
				el.Print();	
			}
			
			DateTime dtFrom = new DateTime(1980, 1, 1, 0, 0, 0, 0);
			DateTime dtTo = new DateTime(1984, 1, 31, 0, 0, 0, 0);
			Console.WriteLine("\nPress any key to see min from {0} to {1}...", dtFrom.ToString(), dtTo.ToString());
			Console.Read();
			FindMinDate(elems, dtFrom, dtTo);
		}
		
		// finds and prints min in range
		public static void FindMinDate(List<Elem> elems, DateTime dtFrom, DateTime dtTo){
			double min = -1;
			double dFrom = TimeConvertor.ToTimeStamp(dtFrom);
			double dTo = TimeConvertor.ToTimeStamp(dtTo);
			
			foreach (Elem el in elems){
				if (el.timestamp >= dFrom && el.timestamp <= dTo){
					if (min == -1){
						min = el.timestamp;	
					}
					else {	
						if (min < el.timestamp)
							min = el.timestamp;
					}
				}
			}
			
			if (min == -1)
				Console.WriteLine("There are no matches!");
			else {
				Console.WriteLine("The minimum date is {0}", TimeConvertor.ToDateTime(min).ToString());
			}
		}
		
		// order >= 1 -> ascending sort order
		// order < 1 -> descdending sort order
		public static void Quicksort(List<Elem> elements, int left, int right, int order = 1)
        {
            int i = left, j = right;
            Elem pivot = elements[(left + right) / 2];
 
            while (i <= j)
            {
				if (order >= 1)
	                while (elements[i].CompareTo(pivot) > 0)
	                {
	                    i++;
	                }
				else 
 					while (elements[i].CompareTo(pivot) < 0)
	                {
	                    i++;
	                }
				if (order >=1)
	                while (elements[j].CompareTo(pivot) < 0)
	                {
	                    j--;
	                }
				else
					while (elements[j].CompareTo(pivot) > 0)
	                {
	                    j--;
	                }
 
                if (i <= j)
                {
                    // Swap
                    Elem tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;
 
                    i++;
                    j--;
                }
            }
 
            // Recursive calls
            if (left < j)
            {
                Quicksort(elements, left, j, order);
            }
 
            if (i < right)
            {
                Quicksort(elements, i, right, order);
            }
        }
	}
	
	// object, contains value and timestamp
	class Elem {
		public double val;
		private DateTime dt;
		public double timestamp;
		
		public Elem(string line){
			val = double.Parse(line.Substring(0, line.IndexOf(",")));
			int commaPos = line.IndexOf(",");
			line = line.Substring(commaPos+1, line.Length - commaPos - 1);
			timestamp = double.Parse(line);
			dt = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
			dt = dt.AddSeconds(timestamp);
			//Console.WriteLine("{2} - {0} : {1}", val, timestamp, ++i);
		}
		
	    public int CompareTo(Elem that){
	        if (this.timestamp >  that.timestamp) return -1;
	        if (this.timestamp == that.timestamp) return 0;
	        return 1;
	    }
		
		public void Print(){
			Console.WriteLine("{0} - {1}", dt.ToString(), val);
		}
	}
	
	static class TimeConvertor
	{
		// converts to DateTime
		public static DateTime ToDateTime( double unixTimeStamp ) {
		    DateTime dt = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
			dt = dt.AddSeconds(unixTimeStamp);
			return dt;
		}
		
		// converts to TimeStamp
		public static double ToTimeStamp ( DateTime dtDateTime ) {
			double unixTimestamp = (dtDateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
			return unixTimestamp;
		}
	}
	
}
