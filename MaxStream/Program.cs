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
				edges.Count;
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
		public static int MaxStream (Network[] network, int s, int t)
		{
			Network[] G = new Network [network.Length];
			for (int i = 0; i < network.Length; i++)
				G [i] = new Network ();
			for (int i = 0; i < network.Length; i++) {
				if (network [i].Count > 0) {
					foreach (Edge n in network[i].edges) {
						if (n.f < n.c)
							G [i].Add (new Edge (i, n.v, n.c, n.c - n.f));
						if (n.f > 0) {
							G[n.v].Add(new Edge (n.v, i, 0, n.f));
						}

					}
				}
			}
				
			List<Way> = new List<Way> ();
			bool[] used = new bool[network.Length];


			Queue<Network> queue = new Queue<Network>();

			queue.Enqueue(network[s]);

			while (queue.Count > 0) {
				Network Entry = queue.Dequeue();


			}

			return 0;
		}

		public static void Main (string[] args)
		{
			Network[] n = new Network[6];
			n [0] = new Network();
			n [0].Add (new Edge (0, 1, 3, 0));
			n [0].Add (new Edge (0, 5, 2, 0));

			n [1] = new Network();
			n [1].Add (new Edge (1, 2, 3, 0));
			n [1].Add (new Edge (1, 5, 1, 0));

			n [2] = new Network();
			n [2].Add (new Edge (2, 3, 1, 0));
			n [2].Add (new Edge (2, 4, 1, 0));

			n [3] = new Network();

			n [4] = new Network();
			n [4].Add (new Edge (4, 3, 5, 0));

			n[5] = new Network();
			n [5].Add (new Edge (5, 4, 4, 0));
			n [5].Add (new Edge (5, 1, 1, 0));

			MaxStream (n);

			Console.WriteLine ("Hello World!");
		}
	}
}
