using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface ITileListener
{
    void OnTileSelected(BoardTile tile);
    void OnTileUnselected(BoardTile tile);
}