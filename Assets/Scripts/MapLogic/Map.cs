using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MapLogic
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        
        public Cell[,] Cells { get; private set; }
        public int MushroomCount { get; private set; }

        public IEnumerable<Cell> CellsEnumerable
        {
            get
            {
                var width = Cells.GetLength(0);
                var height = Cells.GetLength(1);
                
                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        yield return Cells[x, y];
                    }
                }
            }
        }

        public void Create()
        {
            var map = Maps.Random();

            var width = map.GetLength(0);
            var height = map.GetLength(1);

            Cells = new Cell[width, height];

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var cell = Instantiate(cellPrefab, new Vector3(x, y), Quaternion.identity, transform);
                    var cellComponent = cell.GetComponent<Cell>();
                    cellComponent.Set(new Vector2(x, y));

                    if (map[x, y] == 1)
                        cellComponent.SpawnMushroom();

                    Cells[x, y] = cellComponent;
                }
            }
        }
        
        public Cell GetCell(Func<Cell, bool> selector)
        {
            return CellsEnumerable.First(selector);
        }

        public Cell GetRandomCell()
        {
            var index = Random.Range(0, Cells.Length);
            var allCells = CellsEnumerable.ToList();
            
            return allCells[index];
        }

        public void SpawnMushroom()
        {
            var cell = GetCell(x => !x.HasMushroom);
            cell.SpawnMushroom();

            MushroomCount++;
        }
    }
}