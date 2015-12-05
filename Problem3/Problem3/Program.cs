using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem3 {
	class Program {
		static void Main(string[] args) {

			Console.WriteLine("Antal rader ? ");
			int R = int.Parse(Console.ReadLine());
			int M = R + 1;
			int[] k = new int[M];
			int[] m = new int[M];

			for (int i = 1; i < M; i++) {
				Console.WriteLine("Rad " + i + ", vänster ?");
				int left = int.Parse(Console.ReadLine());
				Console.WriteLine("Rad " + i + ", höger ?");
				int right = int.Parse(Console.ReadLine());

				k[M - i] = left;
				m[M - i] = left + right + 1;
			}

			Console.WriteLine("Talet N ? ");
			int N = int.Parse(Console.ReadLine());

			Func<int, int> lambda = null;
			lambda = delegate(int n) {
				return ((N - Math.Sum(1, n - 1, i => lambda(i) * Math.Product(1, i - 1, t => m[t]))) / Math.Product(1, n - 1, i => m[i])) % m[n];
			};

			Func<int, int> a = null;
			a = delegate(int n) {
				if (n == 0) return 0;
				if (n == 1) return k[n] + lambda(n);
				return k[n] + lambda(n) + a(n - 1) / m[n - 1];
			};

			Func<int, int> kHat = null;
			kHat = delegate(int n) {
				return a(n) % m[n];
			};

			int[] result = Util.count(R).Select(n => n + 1).Select(kHat).ToArray();
			
			Console.WriteLine();
			for(int i = 0; i < R; i++) {
				int j = R - i;
				int left = kHat(j);
				int right = m[j] - left - 1;
				Console.WriteLine(left + " " + right);
			}
			Console.ReadKey();
		}
	}
	class Math {
		public static int Sum(int start, int n, Func<int, int> fn) {
			var sum = 0;
			for(var i = 0; i < n; i++) {
				sum += fn(i + start);
			}
			return sum;
		}
		public static int Product(int start, int n, Func<int, int> fn) {
			var product = 1;
			for(var i = 0; i < n; i++) {
				product *= fn(i + start);
			}
			return product;
		}
	}
	class Util {
		public static int[] count(int n) {
			var array = new int[n];
			for (var i = 0; i < n; i++) {
				array[i] = i;
			}
			return array;
		}
	}
}
