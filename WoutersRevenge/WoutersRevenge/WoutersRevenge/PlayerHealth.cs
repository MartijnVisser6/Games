using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoutersRevenge
{
    class PlayerHealth : GameObject
    {
        Player player;
        Rectangle healthBar;

        public PlayerHealth(Vector2 position, Player player, Texture2D texture) : base(texture, position)
        {
            healthBar = new Rectangle(100, 100, player.playerHealth, 20);
            this.player = player;
        }

        public void Update()
        {
            RefreshHealth();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, new Rectangle((int)this.Position.X, (int)this.Position.Y, player.playerHealth, 20), Color.Red);
        }

        private void RefreshHealth()
        {
            healthBar = new Rectangle(100, 100, player.playerHealth, 20);
        }

    }
}
