using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Util;

namespace Environment.Puzzle.Action
{
    public class ActionTileMap: AbstractAction
    {

        [SerializeField] private List<Tuple2> fill;
        [SerializeField] private List<Tuple2> empties;
        [SerializeField] private Tile fillTile;
        [SerializeField] private Tilemap map;
        
        public override void perform()
        {
            foreach (Tuple2 t in fill)
            {
                Vector3Int i = new Vector3Int(t.x, t.y, 0);
                map.SetTile(i, fillTile);
            }


            foreach (Tuple2 t in empties)
            {
                Vector3Int i = new Vector3Int(t.x, t.y, 0);
                map.SetTile(i,null);
            }

        }
    }
}