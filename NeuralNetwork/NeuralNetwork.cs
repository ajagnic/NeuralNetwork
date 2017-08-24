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
         * So we need an array of arrays, but we dont know the length. So we use Lists for the variable size and Add to them.
         * Finally, we convert the List to a normal array. Jagged array is simply an array of arrays, symbolized with "[][]" */
        public void CreateNodeMatrix()
        {
            List<float[]> nodeList = new List<float[]>();

            for (int i = 0; i < Layers.Length; i++)         // ex. Layers[2, 3, 3, 1]
            {
                nodeList.Add(new float[Layers[i]]);         // ex. nodeList = [ [x,x], [x,x,x], [x,x,x], [x] ]
            }                                               // ex.w/input  [ [ 0.5, 1 ] , [x, x, x] , ...]

            Nodes = nodeList.ToArray();                     // conversion from List<[]> to jagged array [][]
        }

        public void CreateWeightMatrix()
        {

        }
    }
}
