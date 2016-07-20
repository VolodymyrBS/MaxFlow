using System;
using System.Collections.Generic;

namespace MaxStream
{
	class Edge
	{
		public int c;
		public int f;
		public int u;
		public int v;

		public Edge (int u, int v, int c, int f) {
			this.u = u;
			this.v = v;
			this.c = c;
			this.f = f;
		}

		public bool Compare(Edge other)
		{
			if (other.u == u && other.v == v)
				return true;
			else
				return false;
		}
	}

	class Network 
	{
		public List<Edge> edges = new List<Edge> ();

		public void Add(Edge e)
		{
			edges.Add (e);
		}
		public int Count
		{
			get
			{ 
				return edges.Count;
			}
		}
	}

	class Way
	{
		public List<int> p = new List<int>();
		public int minDelta = 0;
	}

	class MainClass
	{
		public static int MaxFlow (Network[] G, int s, int t)
		{
			int maxFlow = 0;
			int minDelta = Int16.MaxValue;

			int[] dist = new int[G.Length];
			int[] parent = new int[G.Length];
			bool[] used = new bool[G.Length];



			while (minDelta > 0) {
				minDelta = Int16.MaxValue;
				Network[] Gf = new Network [G.Length];
				for (int i = 0; i < G.Length; i++)
					Gf [i] = new Network ();
				for (int i = 0; i < G.Length; i++) {
					if (G [i].Count > 0) {
						foreach (Edge n in G[i].edges) {
							if (n.f < n.c)
								Gf [i].Add (new Edge (i, n.v, n.c - n.f, 0));
							if (n.f > 0) {
								Gf [n.v].Add (new Edge (n.v, i, n.f, 0));
							}

						}
					}
				}
					
				for (int i = 0; i < dist.Length; i++) {
					dist [i] = Int16.MaxValue;
					used [i] = false;
				}
				dist [s] = 0;
				used [s] = true;

				Queue<Network> queue = new Queue<Network> ();

				queue.Enqueue (Gf [s]);

				while (queue.Count > 0) {
					Network entry = queue.Dequeue ();
					foreach (Edge e in entry.edges) {
						used [e.u] = true;
						if ((dist [e.v] > dist [e.u] + 1) && !used [e.v]) {
							parent [e.v] = e.u;
							dist [e.v] = dist [e.u] + 1;
							queue.Enqueue (Gf [e.v]);
						}
					}
				}

				if (dist [t] == Int16.MaxValue)
					return maxFlow;
				
				int pre = t;
				while(pre != s) {
					int V = parent [pre];
					foreach (Edge e in Gf[V].edges) {
						if (e.v == pre && minDelta > e.c)
							minDelta = e.c;
					}
					pre = V;
				}

				pre = t;
				while(pre != s) {
					int V = parent [pre];
					foreach (Edge e in G[V].edges) {
						if (e.v == pre)
							e.f += minDelta;
					}
					pre = V;
				}

				maxFlow += minDelta;

				Console.WriteLine ("|");
			}

			return maxFlow;
		}

		public static void Main (string[] args)
		{
			Network[] n = new Network[6];
			n [0] = new Network();
			n [0].Add (new Edge (0, 1, 3, 0));
			n [0].Add (new Edge (0, 5, 2, 0));

			n [1] = new Network();
			n [1].Add (new Edge (1, 2, 3, 0));
			n [1].Add (new Edge (1, 5, 2, 0));

			n [2] = new Network();
			n [2].Add (new Edge (2, 3, 2, 0));
			n [2].Add (new Edge (2, 4, 4, 0));

			n [3] = new Network();

			n [4] = new Network();
			n [4].Add (new Edge (4, 3, 3, 0));

			n [5] = new Network();
			n [5].Add (new Edge (5, 4, 2, 0));

			int m = MaxFlow (n, 0, 3);

			Console.WriteLine ("Hello World!" + m);
		}
	}
}
