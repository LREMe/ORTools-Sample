

// [START program]
// [START import]
using System;
using System.Collections.Generic;
using Google.OrTools.ConstraintSolver;
// [END import]

/// <summary>
///   Minimal TSP using distance matrix.
/// </summary>
public class Vrp
{


    public class DistanceAlgorithm
    {
        const double PIx = 3.141592653589793;
        const double RADIO = 6378137; //Earth radius in meters
        
        /// <summary>
        /// This class cannot be instantiated.
        /// </summary>
        private DistanceAlgorithm() { }

        /// <summary>
        /// Convert degrees to Radians
        /// </summary>
        /// <param name="x">Degrees</param>
        /// <returns>The equivalent in radians</returns>
        public static double Radians(double x)
        {
            return x * PIx / 180;
        }

        /// <summary>
        /// Calculate the distance between two places in metters using Haversine formula.
        /// </summary>
        /// <param name="lon1"></param>
        /// <param name="lat1"></param>
        /// <param name="lon2"></param>
        /// <param name="lat2"></param>
        /// <returns></returns>
        public static double DistanceBetweenPlaces(
            double lon1,
            double lat1,
            double lon2,
            double lat2)
        {
            double dlon = Radians(lon2 - lon1);
            double dlat = Radians(lat2 - lat1);

            double a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + Math.Cos(Radians(lat1)) * Math.Cos(Radians(lat2)) * (Math.Sin(dlon / 2) * Math.Sin(dlon / 2));
            double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return (angle * RADIO) ;//distance in kilometers
        }

    }



    // [START data_model]
    class DataModel
    {

        public double[,] CoordinateMatrix = {
{    -77.06170400000000                      ,    -11.96723000000000                      },
{    -77.04723400000000                      ,    -11.95922400000000                      },
{    -77.06027700000000                      ,    -11.96066500000000                      },
{    -77.06530400000000                      ,    -11.97900900000000                      },
{    -77.06059400000000                      ,    -11.98665400000000                      },
{    -77.05517399999990                      ,    -12.00247300000000                      },
{    -77.05023799999990                      ,    -12.01030700000000                      },
{    -77.05162799999990                      ,    -12.00459800000000                      },
{    -77.05292900000000                      ,    -12.00639800000000                      },
{    -77.05213800000000                      ,    -12.00386000000000                      },
{    -77.05310700000000                      ,    -12.00247100000000                      },
{    -77.05285200000000                      ,    -12.00480300000000                      },
{    -77.05260700000000                      ,    -12.00587600000000                      }
        };


        public long[,] GetDistanceMatrix( ) {


            //int initialPosition = 0;
            DistanceMatrix = new long[CoordinateMatrix.GetLength(0), CoordinateMatrix.GetLength(0)];


            for (int i = 0; i < CoordinateMatrix.GetLength(0); i++)
            {
                var pointX = CoordinateMatrix[i, 0];
                var pointY = CoordinateMatrix[i, 1];

                for (int j = 0; j < CoordinateMatrix.GetLength(0); j++) {
                   double distance = DistanceAlgorithm.DistanceBetweenPlaces(pointX, pointY, CoordinateMatrix[j, 0], CoordinateMatrix[j, 1]);
                   DistanceMatrix[i, j] = Convert.ToInt64( distance);
                }
            }

            return DistanceMatrix;

        }





        public long[,]? DistanceMatrix;/* = {
            { 0, 548, 776, 696, 582, 274, 502, 194, 308, 194, 536, 502, 388, 354, 468, 776, 662 },
            { 548, 0, 684, 308, 194, 502, 730, 354, 696, 742, 1084, 594, 480, 674, 1016, 868, 1210 },
            { 776, 684, 0, 992, 878, 502, 274, 810, 468, 742, 400, 1278, 1164, 1130, 788, 1552, 754 },
            { 696, 308, 992, 0, 114, 650, 878, 502, 844, 890, 1232, 514, 628, 822, 1164, 560, 1358 },
            { 582, 194, 878, 114, 0, 536, 764, 388, 730, 776, 1118, 400, 514, 708, 1050, 674, 1244 },
            { 274, 502, 502, 650, 536, 0, 228, 308, 194, 240, 582, 776, 662, 628, 514, 1050, 708 },
            { 502, 730, 274, 878, 764, 228, 0, 536, 194, 468, 354, 1004, 890, 856, 514, 1278, 480 },
            { 194, 354, 810, 502, 388, 308, 536, 0, 342, 388, 730, 468, 354, 320, 662, 742, 856 },
            { 308, 696, 468, 844, 730, 194, 194, 342, 0, 274, 388, 810, 696, 662, 320, 1084, 514 },
            { 194, 742, 742, 890, 776, 240, 468, 388, 274, 0, 342, 536, 422, 388, 274, 810, 468 },
            { 536, 1084, 400, 1232, 1118, 582, 354, 730, 388, 342, 0, 878, 764, 730, 388, 1152, 354 },
            { 502, 594, 1278, 514, 400, 776, 1004, 468, 810, 536, 878, 0, 114, 308, 650, 274, 844 },
            { 388, 480, 1164, 628, 514, 662, 890, 354, 696, 422, 764, 114, 0, 194, 536, 388, 730 },
            { 354, 674, 1130, 822, 708, 628, 856, 320, 662, 388, 730, 308, 194, 0, 342, 422, 536 },
            { 468, 1016, 788, 1164, 1050, 514, 514, 662, 320, 274, 388, 650, 536, 342, 0, 764, 194 },
            { 776, 868, 1552, 560, 674, 1050, 1278, 742, 1084, 810, 1152, 274, 388, 422, 764, 0, 798 },
            { 662, 1210, 754, 1358, 1244, 708, 480, 856, 514, 468, 354, 844, 730, 536, 194, 798, 0 }
        };*/
        public int VehicleNumber = 1;
        public int Depot = 0;
    };
    // [END data_model]

    // [START solution_printer]
    /// <summary>
    ///   Print the solution.
    /// </summary>
    static void PrintSolution(in DataModel data, in RoutingModel routing, in RoutingIndexManager manager,
                              in Assignment solution)
    {
        Console.WriteLine("Objective: {0}", solution.ObjectiveValue());
        // Inspect solution.
        long totalDistance = 0;
        for (int i = 0; i < data.VehicleNumber; ++i)
        {
            Console.WriteLine("Route for Vehicle {0}:", i);
            long routeDistance = 0;
            var index = routing.Start(i);
            while (routing.IsEnd(index) == false)
            {
                Console.Write("{0} -> ", manager.IndexToNode((int)index));
                var previousIndex = index;
                index = solution.Value(routing.NextVar(index));
                routeDistance += routing.GetArcCostForVehicle(previousIndex, index, 0);
            }
            Console.WriteLine("{0}", manager.IndexToNode((int)index));
            Console.WriteLine("Distance of the route: {0}m", routeDistance);
            totalDistance += routeDistance;
        }
        Console.WriteLine("Total Distance of all routes: {0}m", totalDistance);
    }
    // [END solution_printer]

    public static void Main(String[] args)
    {
        // Instantiate the data problem.
        // [START data]
        DataModel data = new DataModel();
        // [END data]

        data.DistanceMatrix = data.GetDistanceMatrix();

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
        // Add Distance constraint.
        /*routing.AddDimension(transitCallbackIndex, 0, 9000,
                             true, // start cumul to zero
                             "Distance");*/
       // RoutingDimension distanceDimension = routing.GetMutableDimension("Distance");
       // distanceDimension.SetGlobalSpanCostCoefficient(100);

        // Setting first solution heuristic.
        RoutingSearchParameters searchParameters =
            operations_research_constraint_solver.DefaultRoutingSearchParameters();
        searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;

        // FirstSolutionStrategy.Types.Value.

        // Solve the problem.
        Assignment solution = routing.SolveWithParameters(searchParameters);

        if (solution != null)
        {
            // Print solution on console.
            PrintSolution(data, routing, manager, solution);
            // [END print_solution]
        }
        else {

            Console.WriteLine("There is not solution");
        }
    }
}
// [END program]
