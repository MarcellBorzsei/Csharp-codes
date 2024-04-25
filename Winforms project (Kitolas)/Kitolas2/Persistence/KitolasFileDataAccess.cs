using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitolas.Persistence
{
    public class KitolasFileDataAccess : IKitolasDataAccess
    {
        public async Task SaveAsync(String path, SlideTable table)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path)) 
                {
                    writer.Write(table.nSizeNum); 
                    await writer.WriteLineAsync(" " + table.circleNum + " " + table.blackCounter + " " + table.whiteCounter + " " + table.currentPlayer);
                    for (int i = 0; i < table.nSizeNum; i++)
                    {
                        for (int j = 0; j < table.nSizeNum; j++)
                        {
                            await writer.WriteAsync(table.gameTable[i, j] + " "); 
                        }
                        await writer.WriteLineAsync();
                    }
                }
            }
            catch
            {
                throw new KitolasDataException();
            }
        }

        public async Task<SlideTable> LoadAsync(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path)) 
                {
                    String line = await reader.ReadLineAsync() ?? String.Empty;
                    String[] numbers = line.Split(' '); 
                    int nSizenum = int.Parse(numbers[0]); 
                    int _circleNum = int.Parse(numbers[1]);
                    int _blackCounter = int.Parse(numbers[2]);
                    int _whiteCounter = int.Parse(numbers[3]);
                    string _currentPlayer = numbers[4];
                    SlideTable table = new SlideTable(nSizenum);

                    table.circleNum = _circleNum;
                    
                    for (int i = 0; i < nSizenum; i++)
                    {
                        line = await reader.ReadLineAsync() ?? string.Empty;
                        numbers = line.Split(' ');

                        for (int j = 0; j < nSizenum; j++)
                        {
                            table.gameTable[i, j] = int.Parse(numbers[j]);
                        }
                    }

                    table.LoadInitialize(_circleNum, _blackCounter, _whiteCounter, _currentPlayer);

                    return table;
                }
            }
            catch
            {
                throw new KitolasDataException();
            }
        }
    }
}
