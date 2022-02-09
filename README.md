# ORTools-Sample
Using Google OR Tools to solve routing problems in logistics


This sample is part of an application to route optimization in logistics. The objetive is to reproduce it on a Google maps project with a friendly user interface.

The best description of the problem is here: https://developers.google.com/optimization/routing/vrp#example . The library has great solutions to tackle optimization problems.

<b>Update 08/feb/21</b>
Provided sample application using google maps. The example consider a class that uses VRP algorithm to get a solution with the optimal route. Some constraints: 

1. It's considered just one vehicle (for simplicity, the same as TSP). If you use more vehicles in a route, you will need more computing power and time to solve. In the main application, I use multiple vehicles, but I threat them indepently (different set of clients for each one). 
2. You need to provide the origin (on this case, is the index 0 of the locations). 
3. As is described on the Google samples, you need to consider that the vehicle retourned to the origin.


