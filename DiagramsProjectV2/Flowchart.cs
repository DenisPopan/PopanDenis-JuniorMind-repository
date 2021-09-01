﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DiagramsProjectV2
{
    public class Flowchart
    {
        public Flowchart(string[] commands)
        {
            ProjectUtils.EnsureIsNotNull(commands, nameof(commands));
            AddFlowchartElements(commands);
        }

        public List<Node> Nodes { get; } = new List<Node>();

        public List<Edge> Edges { get; } = new List<Edge>();

        public void Draw(string location)
        {
            Canva.InitialiseDrawing();
            float startX = 50;
            float startY = 50;

            foreach (var group in Nodes.GroupBy(x => x.Level))
            {
                System.Linq.IOrderedEnumerable<Node> orderedGroup = group.OrderBy(x => x.Level);

                if (group.Key > 1)
                {
                    orderedGroup = group.OrderBy(x => x.Parent.Id);
                }

                foreach (var node in orderedGroup)
                {
                    node.Rectangle = new RectangleF(startX, startY, node.Width, node.Height);
                    Program.DrawSimpleRectangle(node.Text, node.Rectangle);
                    startX = startX + node.Width + 100;
                }

                startY += 200;
                startX = 50;
            }

            foreach (var edge in Edges)
            {
                Program.DrawLink(edge.FirstNode.Rectangle, edge.SecondNode.Rectangle);
            }

            Canva.SaveDrawing(location);
        }

        void AddFlowchartElements(string[] commands)
        {
            string[] nodesText;
            Node firstNode;
            Node secondNode;

            for (int i = 1; i < commands.Length; i++)
            {
                nodesText = commands[i].Split(" --- ");
                firstNode = AddNode(nodesText[0]);
                secondNode = AddNode(nodesText[1]);
                if (firstNode.Level == 0)
                {
                    if (secondNode.Level == 1 || secondNode.Level == 0)
                    {
                        secondNode.Parent = firstNode;
                        secondNode.Level = 2;
                        firstNode.AddChild(secondNode);
                    }

                    firstNode.Level = secondNode.Level - 1;
                }
                else
                {
                    if (secondNode.Level == 0)
                    {
                        secondNode.Level = firstNode.Level + 1;
                        secondNode.Parent = firstNode;
                        firstNode.AddChild(secondNode);
                    }
                }

                if (firstNode.Id < secondNode.Parent.Id)
                {
                    secondNode.Parent.RemoveChild(secondNode);
                    secondNode.Parent = firstNode;
                    firstNode.AddChild(secondNode);
                }

                AddEdge(firstNode, secondNode);
            }
        }

        Node AddNode(string text)
        {
            var node = Nodes.Find(x => x.Text.Equals(text));
            if (node == null)
            {
                Nodes.Add(new Node(Nodes.Count + 1, text));
                node = Nodes[Nodes.Count - 1];
            }

            return node;
        }

        void AddEdge(Node firstNode, Node secondNode)
        {
            Edges.Add(new Edge(firstNode, secondNode));
        }
    }
}