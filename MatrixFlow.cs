using System;
using System.Collections.Generic;

namespace MaxStream
{
	public class MatrixFlow
	{
		public static bool bfs(long[,] G, int N, int s, int t, out int[] dist)
		{
			Queue<int> queue = new Queue<int> ();
			dist = new int[N];

			for (int i = 0; i < N; i++)
				dist [i] = -1;

			queue.Clear ();
			queue.Enqueue (s);

			dist [s] = 0;

			while (queue.Count > 0) {
				bool e = false;
				int r = queue.Dequeue ();
				for (int i = 0; i < N; i++) {
					if (G[r, i] > 0 && dist[i] == -1) {
						dist [i] = dist [r] + 1;
						queue.Enqueue (i);
					} 
				}
			}


			return (dist [t] != -1);
		}

		public static long dfs (long[,] G, int u, int t, int N, long minC, int[] p, int[] d)
		{
			long delta = 0;
			if (u == t || minC == 0)
				return minC;

			for (int v = p [u]; v < N; v++) {
				if (d [v] == d [u] + 1) {
					delta = dfs (G, v, 0, t, Math.Min (minC, G [u, v]), p, d);
					if (delta != 0) {
						G [u, v] -= delta;
						G [v, u] += delta;
						return delta;
					}

				}
				p [u]++;
			}
			return 0;

		}

		public static long MaxFlow(long[,] G, int N, int s, int t)
		{
			
			int[] dist;
			int[] p = new int[N];

			long maxFlow = 0, flow = 0;

			for (int i = 0; i < N; i++)
				p [i] = 0;

			while (bfs(G, N, s, t, out dist)) {
				flow = dfs (G, s, t, N, Int64.MaxValue-1, p, dist);
				while (flow != 0) {
					maxFlow += flow;
					flow = dfs (G, s, t, N, Int64.MaxValue-1, p, dist);
				}
			}

			return maxFlow;
		}

		public static void Main (string[] args)
		{
			int N, M, a, b;
			long c;

			string[] s = Console.ReadLine ().Split (new char[] {' '}, StringSplitOptions.RemoveEmptyEntries );

			Int32.TryParse (s [0], out N);
			Int32.TryParse (s [1], out M);

			long[,] G = new long[N,N];
			for (int i = 0; i < N; i++)
				for (int j = 0; j < N; j++)
					G [i, j] = 0;
			while (M-- > 0) {
				s = Console.ReadLine ().Split (new char[] {' '}, StringSplitOptions.RemoveEmptyEntries );

				Int32.TryParse (s [0], out a);
				Int32.TryParse (s [1], out b);
				Int64.TryParse (s [2], out c);

 				G [a-1, b-1] = +c;
			}

			long m = MaxFlow (G, N, 0, N-1)
				;

			Console.WriteLine (m);
		}
	}
}

