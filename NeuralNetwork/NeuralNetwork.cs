using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        public int[] Layers;                    // ex. Layers = [2, 3, 3, 1]    ---INPUT EXAMPLE: [ 0.5, 1 ]

        private float[][] Nodes;                // ex. Nodes = [currentLayer][nodes] = [0][2] = [0][ 0.5, 1 ]   //REFERENCES LAYERS

        private float[][][] Weights;            // ex. Weights = [''][''][prevNodes] = [1][3][2]                //REFERENCES LAYERS, NODES

        public Random random = new Random();    //RNG

        //========================================================================================
        public NeuralNetwork(int[] layerInfo)   // ex. input [2, 3, 3, 1]
        {
            Layers = new int[layerInfo.Length];

            for (int i = 0; i < layerInfo.Length; i++)
            {
                Layers[i] = layerInfo[i];       //deep-copy of array
            }

            CreateNodeMatrix();
            CreateWeightMatrix();
        }//--------------------------------------------------------------------------------------

        public void FeedForward()
        {

        }

        public void Query()
        {

        }

        public void Train()
        {

        }

        /* We have an amount of layers and we know how many nodes we want... But we need each node to reference information from inputs. So each node must be an array of numbers.
         * So we need an array of arrays, but we dont want to specify the array length for each... So we use Lists for the variable size and Add to them in the loop.
         * Finally, we convert the List to a normal array. Jagged array is simply an array of arrays, symbolized with "[][]". */
        public void CreateNodeMatrix()
        {
            List<float[]> nodeList = new List<float[]>();

            for (int i = 0; i < Layers.Length; i++)         // ex. Layers[2, 3, 3, 1]
            {
                nodeList.Add(new float[Layers[i]]);         // ex. nodeList = [ [x,x], [x,x,x], [x,x,x], [x] ]
            }                                               // ex.w/input  [ [ 0.5, 1 ] , [x, x, x] , ...]

            Nodes = nodeList.ToArray();                     // conversion from List<[]> to jagged array [][]
        }

        /* This will be similar to node matrix, but we go a level deeper to log the connections b/w each node, hence the 3D array "[][][]".
         * Start Layer loop at i=1 since the first layer has no weighted connections.
         * We initialize the weights to random values, these will later be adjusted with training. */
        public void CreateWeightMatrix()
        {
            List<float[][]> weightList = new List<float[][]>();

            //------------------------------------
            for (int i = 1; i < Layers.Length; i++)                         // ex. Layers[2, 3, 3, 1]
            {
                List<float[]> currentLayerWeights = new List<float[]>();

                int nodesInPreviousLayer = Layers[i - 1];                   // ex. 2 for i=1

                //--------------------------------------
                for (int j = 0; j < Nodes[i].Length; j++)                   // ex. Nodes [ [x,x], [x,x,x]... ]
                {
                    float[] nodeWeights = new float[nodesInPreviousLayer];  // ex. [x,x] for i=1

                    //-------------------------------------------
                    for (int k = 0; k < nodesInPreviousLayer; k++)
                    {
                        nodeWeights[k] = (float)random.NextDouble() - 0.5f; //initialize with random value -0.5 to 0.5
                    }

                    currentLayerWeights.Add(nodeWeights);                   // ex. cLW [ [x,x], [x,x]... ]
                }

                weightList.Add(currentLayerWeights.ToArray());              // ex. wL [ [ [x,x], [x,x]... ] ]
            }

            Weights = weightList.ToArray();
        }



    }//class
}//namespace
