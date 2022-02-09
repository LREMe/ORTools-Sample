using Google.OrTools.ConstraintSolver;
using Maps.ORTools.BAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maps.ORTools.BAL.VRP
{
    public class VRPSolution
    {
        DataModel data  = new DataModel();
        /// <summary>
        /// Get the solution of a number of geo points  /// restriction: only one vehicle
        /// </summary>
        /// <param name="CoordinateMatrix"></param>
        /// <returns></returns>
        public double[,] GetSolution(double [,] CoordinateMatrix) {

            //long totalDistance = 0;
            double[,] CoordinateMatrixSolution = new double[CoordinateMatrix.GetLength(0), 2];
            // [END data]

            data.DistanceMatrix = DistanceAlgorithm.GetDistanceMatrix(CoordinateMatrix);

            // Create Routing Index Manager
            // [START index_manager]
            RoutingIndexManager manager =
                new RoutingIndexManager(data.DistanceMatrix.GetLength(0), data.VehicleNumber, data.Depot);
            // [END index_manager]

            // Create Routing Model.
            // [START routing_model]
            RoutingModel routing = new RoutingModel(manager);
            // [END routing_model]

            // Create and register a transit callback.
            // [START transit_callback]
            int transitCallbackIndex = routing.RegisterTransitCallback((long fromIndex, long toIndex) =>
            {
                // Convert from routing variable Index to distance matrix NodeIndex.
                var fromNode = manager.IndexToNode(fromIndex);
                var toNode = manager.IndexToNode(toIndex);
                return data.DistanceMatrix[fromNode, toNode];
            });
            // [END transit_callback]

            // Define cost of each arc.
            // [START arc_cost]
            routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);
            // [END arc_cost]

            // Setting first solution heuristic.
            // [START parameters]

            // Setting first solution heuristic.
            RoutingSearchParameters searchParameters =
                operations_research_constraint_solver.DefaultRoutingSearchParameters();
            searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;

            // Solve the problem.
            Assignment solution = routing.SolveWithParameters(searchParameters);


            for (int i = 0; i < data.VehicleNumber; ++i)
            {

                var index = routing.Start(i);
                int j = 0; 
                while (routing.IsEnd(index) == false)
                {

                    CoordinateMatrixSolution[j, 0] = CoordinateMatrix[manager.IndexToNode((int)index), 0];
                    CoordinateMatrixSolution[j, 1] = CoordinateMatrix[manager.IndexToNode((int)index), 1];


                    index = solution.Value(routing.NextVar(index));

                    j++;
                }


            }
            return CoordinateMatrixSolution;
        }



    }
}
