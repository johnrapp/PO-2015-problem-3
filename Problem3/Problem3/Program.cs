using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem3 {
	class Program {
		static void Main(string[] args) {

			Console.WriteLine("Antal rader ? ");
			ulong R = ulong.Parse(Console.ReadLine());
			ulong M = R + 1;
			ulong[] k = new ulong[M];
			ulong[] m = new ulong[M];

			for (ulong i = 1; i < M; i++) {
				Console.WriteLine("Rad " + i + ", vänster ?");
				ulong left = ulong.Parse(Console.ReadLine());
				Console.WriteLine("Rad " + i + ", höger ?");
				ulong right = ulong.Parse(Console.ReadLine());

				k[M - i] = left;
				m[M - i] = left + right + 1;
			}

			Console.WriteLine("Talet N ? ");
			ulong N = ulong.Parse(Console.ReadLine());

			Func<ulong, ulong> lambda = null;
			lambda = delegate(ulong n) {
				return ((N - Math.Sum(1, n - 1, i => lambda(i) * Math.Product(1, i - 1, t => m[t]))) / Math.Product(1, n - 1, i => m[i])) % m[n];
			};

			Func<ulong, ulong> a = null;
			a = delegate(ulong n) {
				if (n == 0) return 0;
				if (n == 1) return k[n] + lambda(n);
				return k[n] + lambda(n) + a(n - 1) / m[n - 1];
			};

			Func<ulong, ulong> kHat = null;
			kHat = delegate(ulong n) {
				return a(n) % m[n];
			};

			Console.WriteLine();
			for (ulong i = 0; i < R; i++) {
				ulong j = R - i;
				ulong left = kHat(j);
				ulong right = m[j] - left - 1;
				Console.WriteLine(left + " " + right);
			}
			Console.ReadKey();
		}
	}
	class Math {
		public static ulong Sum(ulong start, ulong n, Func<ulong, ulong> fn) {
			ulong sum = 0;
			for (ulong i = 0; i < n; i++) {
				sum += fn(i + start);
			}
			return sum;
		}
		public static ulong Product(ulong start, ulong n, Func<ulong, ulong> fn) {
			ulong product = 1;
			for (ulong i = 0; i < n; i++) {
				product *= fn(i + start);
			}
			return product;
		}
	}
}
