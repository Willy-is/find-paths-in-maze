using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace FindTheCat
{
    public class FindTheCat
    {
        public static int Solve(int[,] matrix)
        {
            int result = CountWall(matrix);
            return result;
        }
        
        private static bool PathWay(int[,] matrix, int x, int y, bool[,] visited)
        {
            // N x C grid -> Pathway: (0 <= x < N, 0 <= y < C) && not visited yet
            return x >= 0 && x < matrix.GetLength(0) && y >= 0 && y < matrix.GetLength(1) && ! visited[x,y];
        }

        private static void EntranceDFS(int[,] matrix, int x, int y, bool[,] visited)
        {
            // Check North, East, South and West neighbours
            int[] xNeighbors = new int[] {-1, 0, 1, 0};
            int[] yNeighbors = new int[] {0, 1, 0, -1};

            // Starting point: visited and tracks other cells by recursion
            visited[x,y] = true;
            for (int i = 0; i < 4; i++)
            {
                // If there is a path, moves to that cell
                if (PathWay(matrix, x + xNeighbors[i], y + yNeighbors[i], visited)
                        && matrix[x + xNeighbors[i],y + yNeighbors[i]] == 0)
                    EntranceDFS(matrix, x + xNeighbors[i], y + yNeighbors[i], visited);
                // If there is a wall, checks there is one wall broken by going back to the previous cell
                if (PathWay(matrix, x + xNeighbors[i], y + yNeighbors[i], visited)
                        && matrix[x + xNeighbors[i],y + yNeighbors[i]] == 1)
                {
                    visited[x + xNeighbors[i],y + yNeighbors[i]] = true;
                    EntranceDFS(matrix, x, y, visited);
                }
            }
        }

        private static void ExitDFS(int[,] matrix, int x, int y, bool[,] visited)
        {
            // Check West, North, East and South
            int[] xNeighbors = new int[] {0, -1, 0, 1};
            int[] yNeighbors = new int[] {-1, 0, 1, 0};

            // Starting point: visited and tracks other cells by recursion
            visited[x,y] = true;
            for (int i = 0; i < 4; i++)
            {
                // If there is a path and no wall, moves to that cell
                if (PathWay(matrix, x + xNeighbors[i], y + yNeighbors[i], visited)
                        && matrix[x + xNeighbors[i],y + yNeighbors[i]] == 0)
                    ExitDFS(matrix, x + xNeighbors[i], y + yNeighbors[i], visited);
                // If there is a wall, checks there is one wall broken by going back to the previous cell
                if (PathWay(matrix, x + xNeighbors[i], y + yNeighbors[i], visited)
                        && matrix[x + xNeighbors[i],y + yNeighbors[i]] == 1)
                {
                    visited[x + xNeighbors[i],y + yNeighbors[i]] = true;
                    ExitDFS(matrix, x, y, visited);
                }
            }
        }

        private static int CountWall(int[,] matrix)
        {
            // Track two different people from (0,0) and (N-1,C-1)
            // fromEntrance: for the first person && fromExit: for the other person
            bool[,] fromEntrance = new bool[matrix.GetLength(0), matrix.GetLength(1)];
            bool[,] fromExit = new bool[matrix.GetLength(0), matrix.GetLength(1)];

            int Count = 0;
            EntranceDFS(matrix, 0, 0, fromEntrance);
            ExitDFS(matrix, matrix.GetLength(0) - 1, matrix.GetLength(1) - 1, fromExit);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    // If there is a wall and both can get access to it, can break the wall to meet up
                    if (matrix[i,j] == 1 && fromEntrance[i,j] && fromExit[i,j]) Count++;
                }
            }
            return Count;
        }

    }
}