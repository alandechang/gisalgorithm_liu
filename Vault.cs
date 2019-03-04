using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 跳马练习
{
    public class Vault
    {
        private static int[,] Move = new int[,] { { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 }, { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 } };
        public class Node
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Operater { get; set; }
        }
        public Vault(int rows, int cols)
        {
            this.Rows = rows;
            this.Columns = cols;
        }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        private Stack<Node> Path;
        private int StartX;
        private int StartY;
        private List<Node[]> Result;
        public IEnumerable<Node[]> Run(int x,int y)
        {
            this.Path = new Stack<Node>();
            this.Result = new List<Node[]>();
            this.StartX = x;
            this.StartY = y;
            if (this.IsVaildLocation(x, y))
            {
                Node n = new Node();
                n.X = StartX;
                n.Y = StartY;
                n.Operater = -1;
                Path.Push(n);
                this.Iteration();
            }
            return Result.ToArray();
        }
        private void Iteration()
        {
            Node current = this.Path.Peek();
            for(int i = 0; i < 8; i++)
            {
                Node n = new Node();
                n.X = current.X + Move[i, 0];
                n.Y = current.Y + Move[i, 1];
                n.Operater = i + 1;
                if(this.IsVaildLocation(n.X,n.Y)
                    && (!this.IsSelfIntersection(n.X, n.Y, this.Path)))
                {
                    this.Path.Push(n);
                    if ((n.X == this.StartX) && (n.Y == this.StartY))
                    {
                        if (this.IsFindWay(this.Path))
                        {
                            Node[] p = this.Path.ToArray();
                            this.Result.Add(p.Reverse().ToArray());
                        }
                        this.Path.Pop();
                    }
                    else
                    {
                        this.Iteration();
                    }
                }
            }
            this.Path.Pop();
        }
        private bool IsVaildLocation(int x,int y)
        {
            if((1<=x)&&(x<=this.Columns)
                && (1 <= y) && (y <= this.Rows))
            {
                return true;
            }
            return false;
        }
        private bool IsSelfIntersection(int x,int y, Stack<Node> path)
        {
            if (path == null) return false;
            Node[] nodes = path.ToArray();
            for(int i = 0; i < nodes.Length - 1; i++)
            {
                Node n = nodes[i];
                if ((n.X == x) && (n.Y == y)) return true;
            }
            return false;
        }
        private bool IsFindWay(Stack<Node> path)
        {
            foreach(Node n in path)
            {
                if((n.X==1)||(n.Y==1)
                    ||(n.X==this.Columns)||(n.Y==this.Rows))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
