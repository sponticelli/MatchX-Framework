using System;
using System.Collections;
using System.Collections.Generic;
using ZigZaggle.MatchX;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace ZigZaggle.MatchX.PuzzleMatch
{

    public class GridController : MonoBehaviour
    {
        [Header("Config")]
        public ResolutionConfigScriptableObject resolutionConfig;
        
        [Header("Background")] 
        public Sprite backgroundSprite;
        public Color backgroundColor;

        [Header("Grid")] 
        public Transform gridCenter;
        public float horizontalSpacing = 0.02f;
        public float verticalSpacing = 0.02f;

        [Header("Tiles")] 
        public Sprite[] blockTileSprites;
        
        private GameOrthoGrid orthoGrid;
        private Camera mainCamera;
        private float tileWidth;
        private float tileHeight;
        private float totalWidth;
        private float totalHeight;

        public void Start()
        {
            mainCamera = Camera.main;
            
            LoadGrid();
            CreateBackgroundTiles();
            CreateFrontTiles();
        }

        
        private void LoadGrid()
        {
            orthoGrid = new GameOrthoGrid(6, 6);
            for (var j = 0; j < orthoGrid.Height; j++)
            {
                for (var i = 0; i < orthoGrid.Width; i++)
                {
                    orthoGrid.SetData(i, j, new BlockTile() { type = BlockType.RandomBlock});
                }
            }
        }

        private void CreateBackgroundTiles()
        {
            var tiles = new List<GameObject>();
            var tileContainer = new GameObject("BackgroundTiles");
            for (var j = 0; j < orthoGrid.Height; j++)
            {
                for (var i = 0; i < orthoGrid.Width; i++)
                {
                    var data = orthoGrid.GetData(i, j);
                    var tile = data as BlockTile;
                    if (tile != null && tile.type == BlockType.Empty)
                    {
                        continue;
                    }
                    
                    var go = new GameObject("Background_" + i +"_" +j );
                    go.transform.parent = tileContainer.transform;
                    var sprite = go.AddComponent<SpriteRenderer>();
                    sprite.sprite = backgroundSprite;
                    sprite.color = backgroundColor;
                    sprite.sortingLayerName = "Game";
                    sprite.sortingOrder = -2;
                    var bounds = sprite.bounds;
                    tileWidth = bounds.size.x;
                    tileHeight = bounds.size.y;
                    
                     go.transform.position = new Vector2(i * (tileWidth + horizontalSpacing),
                        -j * (tileHeight + verticalSpacing));
                     
                     tiles.Add(go);
                }
            }
            
            CalcGridSize();
            RepositionTiles(tiles, totalWidth, totalHeight);
            SetCameraSize();
        }
        
        private void CreateFrontTiles()
        {
            var tiles = new List<GameObject>();
            var tileContainer = new GameObject("FrontTiles");
            for (var j = 0; j < orthoGrid.Height; j++)
            {
                for (var i = 0; i < orthoGrid.Width; i++)
                {
                    var data = orthoGrid.GetData(i, j);
                    var tile = data as BlockTile;
                    if (tile != null && tile.type == BlockType.Empty)
                    {
                        continue;
                    }

                    var go = CreateBlock(tile.type, "Game", orthoGrid.Height - j);
                    go.name = "Block_" + i +"_" +j ;
                    go.transform.parent = tileContainer.transform;
                    go.transform.position = new Vector2(i * (tileWidth + horizontalSpacing),
                        -j * (tileHeight + verticalSpacing));
                    tiles.Add(go);
                }
            }

            RepositionTiles(tiles, totalWidth, totalHeight);
        }
        
        private void RepositionTiles(List<GameObject> tiles, float totalWidth, float totalHeight)
        {
            foreach (var block in tiles)
            {
                var newPos = block.transform.position;
                newPos.x -= totalWidth / 2;
                newPos.y += totalHeight / 2;
                newPos.y += gridCenter.position.y;
                block.transform.position = newPos;
            }
        }
        
        private void SetCameraSize()
        {
            var zoomLevel = resolutionConfig.GetZoomLevel();
            mainCamera.orthographicSize = (totalWidth * zoomLevel) * (Screen.height / (float) Screen.width) * 0.5f;
        }
        
        private void CalcGridSize()
        {
            totalWidth = (orthoGrid.Width - 1) * (tileWidth + horizontalSpacing);
            totalHeight = (orthoGrid.Height - 1) * (tileHeight + verticalSpacing);
        }

        private GameObject CreateBlock(BlockType type, string sortingLayer, int sortingOrder)
        {
            var go = new GameObject();
            var sprite = go.AddComponent<SpriteRenderer>();
            sprite.sprite = GetBlockSprite(type);
            sprite.color = Color.white;
            sprite.sortingLayerName = sortingLayer;
            sprite.sortingOrder = sortingOrder;
            return go;
        }

        private Sprite GetBlockSprite(BlockType type)
        {
            switch (type)
            {
                case BlockType.Block1:
                    return blockTileSprites[0];
                case BlockType.Block2:
                    return blockTileSprites[1 % blockTileSprites.Length];
                case BlockType.Block3:
                    return blockTileSprites[2 % blockTileSprites.Length];
                case BlockType.Block4:
                    return blockTileSprites[3 % blockTileSprites.Length];
                case BlockType.Block5:
                    return blockTileSprites[4 % blockTileSprites.Length];
                case BlockType.RandomBlock:
                    var randomIdx = Random.Range(0, blockTileSprites.Length);
                    return blockTileSprites[randomIdx];
            }

            return null;
        }
    }
}