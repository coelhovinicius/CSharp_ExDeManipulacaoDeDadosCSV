/* Exercicio de Fixacao 
 * 
 Fazer um programa para ler o caminho de um arquivo .csv contendo os dados de itens vendidos. Cada item possui um nome, 
preco unitario e quantidade, separados por virgula. Você deve gerar um novo arquivo chamado "summary.csv", localizado
em uma subpasta chamada "out", a partir da pasta original do arquivo de origem, contendo apenas o nome e o valor total para
aquele item (preço unitario multiplicado pela quantidade), conforme exemplo:
    
    Source File:
        TV LED,1290.99,1
        Video Game Chair,350.50,3
        Iphone X,900.00,2
        Samsung Galaxy 9,850.00,2

    Output File (out/summary.csv):
        TV LED,1290.99
        Video Game Chair,1051.50
        Iphone X,1800.00
        Samsung Galaxy 9,1700.00

    Obs.: Arquivos ".cvs" sao arquivos do Excel com valores separados por virgula.
 */

/* >>> PROGRAMA PRINCIPAL <<< */
using System;
using System.Globalization;
using System.IO;
using Aula192_ExFixacao.Entities;

/* >>> PROGARMA PRINCIPAL <<< */
namespace Aula192_ExFixacao
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter file full path: ");
            string sourceFilePath = Console.ReadLine(); // Entrar com o caminho completo do arquivo de origem

            try // Bloco de teste de erros
            {
                string[] lines = File.ReadAllLines(sourceFilePath);

                string sourceFolderPath = Path.GetDirectoryName(sourceFilePath); // Obtem o caminho completo do arquivo
                string targetFolderPath = sourceFolderPath + @"\out"; // Indica o nome da pasta a ser adicionada
                string targetFilePath = targetFolderPath + @"\summary.csv"; // Indica o nome do arquivo de saida

                Directory.CreateDirectory(targetFolderPath); // Cria o arquivo a partir das instrucoes acima

                using (StreamWriter sw = File.AppendText(targetFilePath)) // Insere dados ao final da stream do arquivo de destino
                {
                    foreach (string line in lines) // Para cada linha da matriz das streams do arquivo de origem (lines)
                    {

                        string[] fields = line.Split(','); // Separa as linhas em posicoes separadas por virgulas
                        string name = fields[0]; // Atribui o valor da posicao 0 da linha a "name"
                        double price = double.Parse(fields[1], CultureInfo.InvariantCulture); // Atribui o valor da posicao 1 a "price"
                        int quantity = int.Parse(fields[2]); // Atribui o valor da posicao 2 a "quantity"

                        Product prod = new Product(name, price, quantity); // Instancia o construtor com argumentos

                        /* Escreve (sw) o nome do produto (posicao 0) e o total, dependendo da quantidade (metodo "Total" */
                        sw.WriteLine(prod.Name + "," + prod.Total().ToString("F2", CultureInfo.InvariantCulture));
                    }
                }
            }
            catch (IOException e) // Tratamento dos erros
            {
                Console.WriteLine("ERROR!");
                Console.WriteLine(e.Message);
            }
        }
    }
}
