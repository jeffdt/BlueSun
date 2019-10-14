using BlueSun.src.parameters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using pigeon.collision;
using pigeon.core;
using pigeon.gameobject;
using pigeon.input;
using pigeon.rand;
using pigeon.utilities;
using pigeon.utilities.extensions;
using pigeon.gfx.drawable.shape;
using pigeon;
using pigeon.gfx.drawable.text;
using BlueSun.resources;
using pigeon.sound.music;

namespace BlueSun.worlds.collision {
    class CollisionWorld : World {
        private string[] songs = new string[] {
            @"music\metallicwing\thuglas.nsf",
            @"music\metallicwing\distress_signals.nsf",
            @"music\metallicwing\predation.nsf",
            @"music\metallicwing\shadowvapors.nsf",
            @"music\metallicwing\predation.nsf",

        };

        private const int tileSize = 16;

        private const int numObstacles = 20;
        private const int numEnemies = 5;

        public GameObject[,] GameGrid = new GameObject[Display.ScreenWidth / tileSize, Display.ScreenHeight / tileSize];

        protected override void Load() {
            ColliderStrategy = new DumbSatColliderStrategy();

            BackgroundColor = Color.Black;

            addPlayer();
            addEnemies();
            addObstacles();
            addOuterWalls();

            // Music.Load(songs.RandomElement());
            // Music.Play();
        }

        private void addEnemies() {
            for (int i = 0; i < numEnemies; i++) {
                Point coords = getUnoccupiedCell();
                var enemyObj = EnemyController.BuildObject(coords, tileSize);
                AddObj(enemyObj);
                GameGrid[coords.X, coords.Y] = enemyObj;
            }
        }

        private void addPlayer() {
            Point coords = new Point(1, 1);
            var playerObj = PlayerController.BuildObject(coords, tileSize);
            AddObj(playerObj);
            GameGrid[coords.X, coords.Y] = playerObj;
        }

        private void addOuterWalls() {
            for (int y = 0; y < Display.ScreenHeight / tileSize; y++) {
                for (int x = 0; x < Display.ScreenWidth / tileSize; x++) {
                    if (y == 0 || x == 0 || y == (Display.ScreenHeight / tileSize) - 1 || x == (Display.ScreenWidth / tileSize) - 1) {
                        var wallObj = buildWall(x, y);
                        var wallCollider = wallObj.GetComponent<SimpleBoxCollider>();
                        if (y == 0 || y == (Display.ScreenHeight / tileSize) - 1) { // top or bottom row
                            wallCollider.IgnoredSides[1] = true; // right
                            wallCollider.IgnoredSides[3] = true; // left
                        }

                        if (x == 0 || x == (Display.ScreenWidth / tileSize) - 1) { // left or right col
                            wallCollider.IgnoredSides[0] = true; // top
                            wallCollider.IgnoredSides[2] = true; // bot
                        }

                        //wallObj.AddChild(new GameObject("label1") { LocalPosition = new Point(tileSize / 2, tileSize / 4 + 3), Layer = 1f }.AddComponent(new TextRenderer() { Text = col.ToString(), Color = Color.Gray, Justification = Justifications.Center, Font = Fonts.Console }))
                        //    .AddChild(new GameObject("label2") { LocalPosition = new Point(tileSize / 2, 3 * tileSize / 4 + 3), Layer = 1f }.AddComponent(new TextRenderer() { Text = row.ToString(), Color = Color.Gray, Justification = Justifications.Center, Font = Fonts.Console }));
                        AddObj(wallObj);
                        GameGrid[x, y] = wallObj;
                    }
                }
            }
        }

        private void addObstacles() {
            for (int i = 0; i < numObstacles; i++) {
                Point coords = getUnoccupiedCell();
                Pigeon.Logger.Information("Placing obstacle at coords {obstacleCoords}", coords);
                var wallObj = buildWall(coords.X, coords.Y);
                AddObj(wallObj);
                GameGrid[coords.X, coords.Y] = wallObj;
            }
        }

        private Point getUnoccupiedCell() {
            var coords = Rand.RandPositionWithinArea(new Rectangle(0, 0, GameGrid.GetLength(0), GameGrid.GetLength(1)), new Point(1, 1));
            while (GameGrid[coords.X, coords.Y] != null
                && (GameGrid[coords.X, coords.Y - 1] != null
                || GameGrid[coords.X, coords.Y + 1] != null
                || GameGrid[coords.X - 1, coords.Y] != null
                || GameGrid[coords.X + 1, coords.Y] != null)) {
                coords = Rand.RandPositionWithinArea(new Rectangle(0, 0, GameGrid.GetLength(0), GameGrid.GetLength(1)), new Point(1, 1));
            }

            return coords;
        }

        private static GameObject buildWall(int x, int y) {
            Color fillColor = Color.DimGray;
            RectRenderer wallRenderer = new RectRenderer() {
                Rect = new Rectangle(0, 0, tileSize, tileSize),
                DrawStyle = ShapeDrawStyles.Filled,
                FillColor = fillColor
            };
            SimpleBoxCollider wallCollider = new SimpleBoxCollider() {
                Passive = true,
                Hitbox = new Rectangle(0, 0, tileSize, tileSize),
                Tags = { "reflective" }
            };
            
            GameObject wallObj = new GameObject("Wall [" + x + "," + y + "]") { LocalPosition = new Point(x * tileSize, y * tileSize), Layer = .1f };
            wallObj.AddComponent(wallRenderer);
            wallObj.AddComponent(wallCollider);
            return wallObj;
        }

        protected override void Unload() { }
    }
}
