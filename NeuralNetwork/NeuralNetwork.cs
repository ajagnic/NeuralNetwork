using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
    public class NeuralNetwork : IComparable<NeuralNetwork>
    {
        private int[] Layers;                    // ex. Layers = [2, 3, 3, 1]    ---INPUT EXAMPLE: [ 0.5, 1.0 ]

        private float[][] Nodes;                // ex. Nodes = [currentLayer][node] = [i0][i0] = [i0][ 0.5, 1.0 ]

        private float[][][] Weights;            // ex. Weights = [currentLayer][node][connections] = [i1][i0][2]

        public float Fitness { get; set; }

        private Random random = new Random();    //RNG

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
        }
        public NeuralNetwork(NeuralNetwork otherNetwork)    //network cloning
        {
            Layers = new int[otherNetwork.Layers.Length];

            for (int i = 0; i < otherNetwork.Layers.Length; i++)
            {
                Layers[i] = otherNetwork.Layers[i];
            }

            CreateNodeMatrix();
            CreateWeightMatrix();
            WeightCopy(otherNetwork.Weights);
        }

        /* Place the input values into the input layer, hence targeting Nodes[0]
         * Loop through every node with a connection, and sum together every input*weight, adding a slight bias.
         * Then pass this value into the activation function which outputs a range between -1 and 1.
         */
        public float[] FeedForward(float[] inputs)         // ex. inputs[ 0.5, 1.0 ]
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                Nodes[0][i] = inputs[i];                   // ex. Nodes [ [ 0.5, 1.0 ], [...] ]
            }

            //------------------------------------
            for (int i = 1; i < Layers.Length; i++)
            {
                //--------------------------------------
                for (int j = 0; j < Nodes[i].Length; j++)
                {
                    float value = 0.25f;                    // const bias

                    //------------------------------------------
                    for (int k = 0; k < Nodes[i - 1].Length; k++)
                    {
                        value += Weights[i - 1][j][k] * Nodes[i - 1][k];
                    }

                    Nodes[i][j] = (float)Math.Tanh(value);  // activation
                }
            }

            return Nodes[Nodes.Length - 1];                 // output layer
        }

        /* Loop through every weight, and create a probability it will be mutated.
         */
        public void MutateWeights()
        {
            for (int i = 0; i < Weights.Length; i++)
            {
                for (int j = 0; j < Weights[i].Length; j++)
                {
                    for (int k = 0; k < Weights[i][j].Length; k++)
                    {
                        float Weight = Weights[i][j][k];                // all weights

                        float rand = (float)random.NextDouble() * 10f;  // create range 0 - 100

                        if (rand <= 2f)
                        {
                            Weight *= -1f;
                        }
                        else if(rand <= 4f)
                        {
                            Weight = (float)random.NextDouble() - 0.5f;
                        }
                        else if(rand <= 6f)
                        {
                            float maximize = (float)random.NextDouble() + 1f;

                            Weight *= maximize;
                        }
                        else if(rand <= 8f)
                        {
                            float minimize = (float)random.NextDouble();

                            Weight *= minimize;
                        }

                        Weights[i][j][k] = Weight;
                    }
                }
            }
        }

        /* Need each node to reference information from inputs. So each node must be an array.
         * Use Lists for the variable size.
         * Convert the List to a normal array.
         * Now Nodes can be indexed by Nodes[i][j]
         */
        public void CreateNodeMatrix()
        {
            List<float[]> nodeList = new List<float[]>();

            for (int i = 0; i < Layers.Length; i++)         // ex. Layers[2, 3, 3, 1]
            {
                nodeList.Add(new float[Layers[i]]);         // ex. nodeList = [ [x,x], [x,x,x], [x,x,x], [x] ]
            }                                               // ex.w/input  [ [ 0.5, 1.0 ] , [x, x, x] , ...]

            Nodes = nodeList.ToArray();                     // conversion from List<[]> to jagged array [][]
        }

        /* Similar to node matrix
         * Start Layer loop at i=1, first layer has no weighted connections.
         * Initialize the weights to random values.
         * The weights are now indexed by Weights[i][j][k]
         */
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
                        nodeWeights[k] = (float)random.NextDouble() - 0.5f; //random value -0.5 to 0.5
                    }

                    currentLayerWeights.Add(nodeWeights);                   // ex. cLW [ [x,x], [x,x]... ]
                }

                weightList.Add(currentLayerWeights.ToArray());              // ex. wL [ [ [x,x], [x,x]... ] ]
            }

            Weights = weightList.ToArray();
        }

        /* Create copy of weight values */
        public void WeightCopy(float[][][] weightCopy)
        {
            for (int i = 0; i < Weights.Length; i++)
            {
                for (int j = 0; j < Weights[i].Length; j++)
                {
                    for (int k = 0; k < Weights[i][j].Length; k++)
                    {
                        Weights[i][j][k] = weightCopy[i][j][k];
                    }
                }
            }
        }


        public void AddFitness(float newFitness)
        {
            Fitness += newFitness;
        }

        /* Sort by fitness */
        public int CompareTo(NeuralNetwork other)
        {
            if (other == null)
            {
                return 1;
            }
            if (Fitness > other.Fitness)
            {
                return 1;
            }
            else if (Fitness < other.Fitness)
            {
                return -1;
            }
            else
            {
                return 0;
            }

        }
    }
}
