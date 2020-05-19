using System;
using System.ServiceProcess;

namespace matrizInversa
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Digite a ordem de sua matriz: \nLinhas: ");
            int MATRIX_LINHAS = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nColunas: ");
            int MATRIX_COLUNAS = Convert.ToInt32(Console.ReadLine());

            float[,] matrix = new float[MATRIX_LINHAS, MATRIX_COLUNAS];
            float[,] matrixInversa = new float[MATRIX_LINHAS, MATRIX_COLUNAS];
            if (MATRIX_LINHAS == MATRIX_COLUNAS)
            {
                for (int i = 0; i < MATRIX_LINHAS; i++)
                {
                    for (int j = 0; j < MATRIX_COLUNAS; j++)
                    {
                        Console.Write("Insira o valor({0},{1}): ", i, j);
                        float input = Convert.ToSingle(Console.ReadLine());
                        matrix[i, j] = input;
                    }
                }
                for (int i = 0; i < MATRIX_LINHAS; i++)
                {
                    for (int j = 0; j < MATRIX_COLUNAS; j++)
                    {
                        matrixInversa[i, j] = 0;
                        if (i == j)
                        {
                            matrixInversa[i, j] = 1;
                        }
                    }
                }
                printMatriz(matrix, MATRIX_LINHAS, MATRIX_COLUNAS);
                Console.Write("\n");
                printMatriz(matrixInversa, MATRIX_LINHAS, MATRIX_COLUNAS);
                Calculo(matrix, matrixInversa, MATRIX_LINHAS, MATRIX_COLUNAS);
                simplificacaoMatriz(matrix, matrixInversa, MATRIX_LINHAS, MATRIX_COLUNAS);
                printMatriz(matrix, MATRIX_LINHAS, MATRIX_COLUNAS);
                printMatriz(matrixInversa, MATRIX_LINHAS, MATRIX_COLUNAS);
                Console.Write("Aperte Enter, aqui só serve para visualizar o resultado");
                String soPraVisualizar = Console.ReadLine();
            }
            else
            {
                Console.Write("Matriz não quadrada");
            }
        }
        public static void printMatriz(float[,] matrix, int MATRIX_LINHAS, int MATRIX_COLUNAS)
        {
            for (int i = 0; i < MATRIX_LINHAS; i++)
            {
                Console.Write("\n");
                for (int j = 0; j < MATRIX_COLUNAS; j++)
                {
                    if(matrix[i,j] == -0)
                    {
                        matrix[i, j] = 0;
                    }
                    Console.Write("|" + matrix[i, j] + "|");
                }
            }
            Console.Write("\n");
        }
        public static void simplificacaoMatriz(float[,] matrix, float[,] matrixInversa, int MATRIX_LINHAS, int MATRIX_COLUNAS)
        {
            float valor;
            for (int i = 0; i < MATRIX_LINHAS; i++)
            {
                for (int j = 0; j < MATRIX_COLUNAS; j++)
                {
                    if (i == j)
                    {
                        valor = matrix[i, j];
                        matrix[i, j] = matrix[i, j] / valor;
                        for (int x = 0; x < MATRIX_LINHAS; x++)
                        {
                            matrixInversa[i, x] = matrixInversa[i, x] / valor;
                            
                        }
                    }
                }
            }
        }
        public static void Calculo(float[,] matrix, float[,] matrixInversa, int MATRIX_LINHAS, int MATRIX_COLUNAS)
        {

            float[] pivo = new float[MATRIX_COLUNAS];
            float[] pivoInversa = new float[MATRIX_COLUNAS];
            float m, teste;
            int valCol, testeCol = 0, valLin, testeLin = 0;

            for (int i = 0; i < MATRIX_LINHAS; i++)
            {
                valCol = 0;
                for (int j = 0; j < MATRIX_COLUNAS; j++)
                {
                    valLin = 0;
                    for (int Linha = 0; Linha < MATRIX_LINHAS; Linha++)
                    {
                        if (testeLin == matrix[Linha, j])
                        {
                           
                            valLin = valLin + 1;
                            if (valLin == MATRIX_LINHAS)
                            {
                                Console.Write("\n Determinante igual a 0");
                                System.Environment.Exit(0);
                            }
                        }
                    }
                    if (matrix[i, i] != 0)
                    {
                        for (int x = 0; x < MATRIX_COLUNAS; x++)
                        {
                            pivo[x] = matrix[i, x];
                            pivoInversa[x] = matrixInversa[i, x];
                        }
                    }
                    else
                    {
                        for (int aux = 0; aux < MATRIX_LINHAS; aux++) { 
                            if (matrix[aux, i] != 0)
                            {
                                for (int x = 0; x < MATRIX_COLUNAS; x++)
                                {
                                    if (testeCol == matrix[i, x])
                                    {
                                        valCol = valCol + 1;
                                        if (valCol == MATRIX_COLUNAS)
                                        {
                                            Console.Write("\n Determinante igual a 0");
                                            System.Environment.Exit(0);
                                        }
                                    }

                                    if (aux > 0 && aux <= MATRIX_LINHAS - 1)
                                    {
                                        int aux2 = aux;
                                        teste = matrix[aux2, x];
                                        matrix[aux2, x] = matrix[aux2 - 1, x];
                                        matrix[aux2 - 1, x] = teste;
                                        teste = matrixInversa[aux2, x];
                                        matrixInversa[aux2, x] = matrixInversa[aux2 - 1, x];
                                        matrixInversa[aux2 - 1, x] = teste;
                                        pivo[x] = matrix[aux2, x];
                                        pivoInversa[x] = matrixInversa[aux2, x];
                                    }
                                    else
                                    {
                                        i = aux;
                                        teste = matrix[i, x];
                                        matrix[i, x] = matrix[i + 1, x];
                                        matrix[i + 1, x] = teste;
                                        teste = matrixInversa[i, x];
                                        matrixInversa[i, x] = matrixInversa[i + 1, x];
                                        matrixInversa[i + 1, x] = teste;
                                        pivo[x] = matrix[i, x];
                                        pivoInversa[x] = matrixInversa[i, x];
                                    }
                                }
                                aux = MATRIX_LINHAS;
                            }
                        }
                    }
                    if ((matrix[j, i] != 0) && (i != j))
                    {
                        m = matrix[j, i] / pivo[i];
                        for (int x = 0; x < MATRIX_COLUNAS; x++)
                        {
                            
                            matrix[j, x] = matrix[j, x] - ((m) * pivo[x]);
                            matrixInversa[j, x] = matrixInversa[j, x] - ((m) * pivoInversa[x]);
                            printMatriz(matrix, MATRIX_LINHAS, MATRIX_COLUNAS);
                        }
                    }
                }
            }
        }
    }
}
    