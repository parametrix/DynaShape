﻿using System;
using System.Collections.Generic;
using Autodesk.DesignScript.Runtime;


namespace DynaShape.Goals
{
    [IsVisibleInDynamoLibrary(false)]
    public class CoSphericalGoal : Goal
    {
        public CoSphericalGoal(List<Triple> nodeStartingPositions, float weight = 1f)
        {
            if (nodeStartingPositions.Count <= 4) throw new Exception("CoSpherical Goal: Node count must be at least 5");
            Weight = weight;
            StartingPositions = nodeStartingPositions.ToArray();
            Moves = new Triple[StartingPositions.Length];
            Weights = new float[StartingPositions.Length];
        }

        public override void Compute(List<Node> allNodes)
        {
            List<Triple> points = new List<Triple>(NodeCount);
            for (int i = 0; i < NodeCount; i++)
                points.Add(allNodes[NodeIndices[i]].Position);

            // Here we use our own sphere fitting function (implemented in the Util class)
            // .. which runs much faster than calling the Dynamo method Sphere.ByBestFit

            Triple sphereCenter;
            float sphereRadius;
            Util.ComputeBestFitSphere(points, out sphereCenter, out sphereRadius);

            for (int i = 0; i < NodeCount; i++)
            {
                Triple move = sphereCenter - allNodes[NodeIndices[i]].Position;
                float l = move.Length;
                Moves[i] = move * (l - sphereRadius) / l;
                Weights[i] = Weight;
            }
        }
    }
}
