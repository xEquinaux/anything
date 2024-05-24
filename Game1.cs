using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Windows.Markup;

namespace anything
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			new Main();
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			Main.Instance.Preload(this.Content);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			Main.Instance.Update();
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			Main.Instance.Draw(_spriteBatch);
			base.Draw(gameTime);
		}
	}
	sealed class Main
	{
		public static Main Instance;
		public static Random rand;
		public static int Width = 800;
		public static int Height = 600;
		public Main()
		{
			Instance = this;
			Init();
		}
		public void Init()
		{
			new Rand();
		}							
		public void Preload(ContentManager content)
		{

		}
		public void Update()
		{

		}
		public void Draw(SpriteBatch sb)
		{

		}
	}
	sealed class World
	{
		public static World Instance;
		public Tile[] overworld = new Tile[Main.Width / Tile.Size * (Main.Height / Tile.Size)];
		public World()
		{ 
			Instance = this;
			Init();
		}
		public void Init()
		{
		}
		public void Generate()
		{
			for (int i = 0; i < overworld.Length; i++)
			{
				ModifyCoord(i, Main.rand.Next(TileID.Total));
			}
		} 
		public bool ModifyCoord(int index, int result)
		{
			if (overworld[index].value == result)
			{
				return false;
			}
			else
			{
				overworld[index].value = result;
				return true;
			}
		}
		public bool ModifyCoord(int x, int y, int width, int result)
		{
			int index = y * width + x;
			if (overworld[index].value == result)
			{
				return false;
			}
			else
			{
				overworld[index].value = result;
				return true;
			}
		}
		public int GetValue(int x, int y, int width)
		{
			return overworld[y * width + x].value;
		}
	}
	struct Rand
	{
		public static Rand Instance;
		private int seed = 0;
		public Rand()
		{
			Instance = this;
			Init();
		}
		private void Init()
		{
			seed = DateTime.Now.Millisecond;
			Main.rand = new Random(seed);
		}
	}
	struct Tile
	{
		public readonly Biome biome
		{
			get
			{
				switch (value)
				{
					case TileID.Water:
						return Biome.Ocean;
					case TileID.Grass:
						return Biome.Grasslands;
					case TileID.Tree:
						return Biome.Forest;
					case TileID.Dirt:
						return Biome.Steppe;
					case TileID.Sand:
						return Biome.Desert;
					case TileID.Snow:
						return Biome.Tundra;
					case TileID.Mycelium:
						return Biome.Steppe;
					case TileID.Lake:
						return Biome.Grasslands;
				}
				return Biome.None;
			}
		}
		private bool water	  => value == 0;
		private bool grass	  => value == 1;
		private bool tree		  => value == 2;
		private bool dirt		  => value == 3;
		private bool sand		  => value == 4;
		private bool snow		  => value == 5;
		private bool mycelium  => value == 6;
		private bool steppe	  => value == 7;
		private bool lake      => value == 8;
		public static int Size => 50;
		public int i, j;
		public int value;
	}
	static class TileID
	{
		public static int Total => 9;
		public const int 
			Water = 0, 
			Grass = 1,
			Tree = 2,
			Dirt = 3,
			Sand = 4,
			Snow = 5,
			Mycelium = 6,
			Steppe = 7,
			Lake = 8;
	}
	enum Biome
	{
		None = -1,
		Ocean = 0,
		Grasslands = 1,
		Forest = 2,
		Steppe = 3,
		Tundra = 4,
		Desert = 5
	}
}
