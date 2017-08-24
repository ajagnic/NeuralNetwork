using System;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        public int[] Layers;                    // ex. Layers = [2, 3, 3, 1]

        private float[][] Nodes;                // ex. Nodes = [currentLayer][nodes] = [0][2]

        private float[][][] Weights;            // ex. Weights = [''][''][prevNodes] = [1][3][2]

        public Random random = new Random();    //RNG

        //========================================================================================
        public NeuralNetwork(int[] layerInfo)
        {
            Layers = new int[layerInfo.Length];

            for (int i = 0; i < layerInfo.Length; i++)
            {
                Layers[i] = layerInfo[i];
            }

            CreateNeuronMatrix();
            CreateWeightMatrix();
        }

        public void FeedForward()
        {

        }

        public void Query()
        {

        }

        public void Train()
        {

        }

        public void CreateNeuronMatrix()
        {

        }

        public void CreateWeightMatrix()
        {

        }
    }
}
