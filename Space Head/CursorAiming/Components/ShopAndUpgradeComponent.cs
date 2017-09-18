﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    class ShopAndUpgradeComponent : SpaceHeadBaseComponent
    {
        private Texture2D _shopBackground;
        SpriteFont _font;
        List<MenuChoice> _upgradePlayer, _upgradeWeapon;
        MouseState _previousMouseState;
        private MenuChoice AS, Health, Damage, MoveSpeed;

        public ShopAndUpgradeComponent(Game game) : base(game)
        {
            DrawOrder = 3;
            DrawableStates = GameState.ShopUpgradeMenu;
            UpdatableStates = GameState.ShopUpgradeMenu;
        }

        public override void Initialize()
        {
            _upgradeWeapon = new List<MenuChoice>();
            _upgradePlayer = new List<MenuChoice>();
            AS = new MenuChoice() { Text = "UPGRADE ATTACK SPEED || LEVEL: " + Player.Gun.GunAtkSpeedLevel, Selected = true, ClickAction = UpgradeAtkSpeedClicked };
            _upgradeWeapon.Add(AS);

            Health = new MenuChoice() { Text = "UPGRADE HIT POINTS || LEVEL: " + Player.HealthLevel, Selected = true, ClickAction = UpgradeHitPointsClicked };
            _upgradePlayer.Add(Health);

            Damage = new MenuChoice() {Text = "UPGRADE WEAPON DAMAGE || LEVEL: " + Player.Gun.GunAtkLevel, ClickAction = UpgradeWeaponDamageClicked};
            _upgradeWeapon.Add(Damage);

            MoveSpeed = new MenuChoice() {Text = "UPGRADE MOVEMENT SPEED || LEVEL: " + Player.MoveSpeedLevel, ClickAction = UpgradeMovementSpdClicked};
            _upgradePlayer.Add(MoveSpeed);

            base.Initialize();
        }

        #region Menu Clicks
        private void UpgradeAtkSpeedClicked()
        {
            UpgradeGunAtkSpeed(Player.Gun);
            AS.Text = "UPGRADE ATTACK SPEED || LEVEL: " + Player.Gun.GunAtkSpeedLevel;
        }

        private void UpgradeWeaponDamageClicked()
        {
            UpgradeGunDamage(Player.Gun);
            Damage.Text = "UPGRADE WEAPON DAMAGE || LEVEL: " + Player.Gun.GunAtkLevel;
        }

        private void UpgradeHitPointsClicked()
        {
            UpgradePlayerHitPoints();
            Health.Text = "UPGRADE HIT POINTS || LEVEL: " + Player.HealthLevel;
        }

        private void UpgradeMovementSpdClicked()
        {
            UpgradePlayerMovementSpeed();
            MoveSpeed.Text = "UPGRADE MOVEMENT SPEED || LEVEL: " + Player.MoveSpeedLevel;
        }
        #endregion

        protected override void LoadContent()
        {
            _font = Game.Content.Load<SpriteFont>("Font");
            _shopBackground = Game.Content.Load<Texture2D>("gameOverBackground");

            float startY1 = 0.2f * Globals.ScreenHeight;
            float startY2 = 0.2f * Globals.ScreenHeight;

            foreach (var choice in _upgradeWeapon)
            {
                Vector2 size = _font.MeasureString(choice.Text);
                choice.Y = startY1;
                choice.X = Globals.ScreenWidth * 0.25f - size.X / 2;
                choice.HitBox = new Rectangle((int)choice.X, (int)choice.Y, (int)size.X, (int)size.Y);
                startY1 += 70;
            }

            foreach (var choice in _upgradePlayer)
            {
                Vector2 size = _font.MeasureString(choice.Text);
                choice.Y = startY2;
                choice.X = Globals.ScreenWidth * 0.75f - size.X / 2;
                choice.HitBox = new Rectangle((int)choice.X, (int)choice.Y, (int)size.X, (int)size.Y);
                startY2 += 70;
            }

            _previousMouseState = Mouse.GetState();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            
            foreach (var choice in _upgradeWeapon)
            {
                if (choice.HitBox.Contains(mouseState.X, mouseState.Y))
                {
                    _upgradeWeapon.ForEach(c => c.Selected = false);
                    choice.Selected = true;

                    if (_previousMouseState.LeftButton == ButtonState.Released
                        && mouseState.LeftButton == ButtonState.Pressed)
                        choice.ClickAction.Invoke();
                }
            }

            foreach (var choice in _upgradePlayer)
            {
                if (choice.HitBox.Contains(mouseState.X, mouseState.Y))
                {
                    _upgradePlayer.ForEach(c => c.Selected = false);
                    choice.Selected = true;

                    if (_previousMouseState.LeftButton == ButtonState.Released
                        && mouseState.LeftButton == ButtonState.Pressed)
                        choice.ClickAction.Invoke();
                }
            }
            
            _previousMouseState = mouseState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            foreach (var choice in _upgradeWeapon)
            {
                SpriteBatch.DrawString(_font, choice.Text, new Vector2(choice.X, choice.Y), Color.Green);
            }

            foreach (var choice in _upgradePlayer)
            {
                SpriteBatch.DrawString(_font, choice.Text, new Vector2(choice.X, choice.Y), Color.Green);
            }

            SpriteBatch.End();

            base.Draw(gameTime);
        }
        
        public void UpgradeGunDamage(Gun gunType)
        {
            int cost = gunType.GunAtkLevel * 100;

            if (SpaceHeadGame.GameState == GameState.ShopUpgradeMenu)
            {
                if (cost <= Player.PlayerGoldAmount)
                {
                    gunType.Damage++;
                    gunType.GunAtkLevel++;
                    Player.PlayerGoldAmount -= cost;
                }
            }
        }

        public void UpgradeGunAtkSpeed(Gun gunType)
        {
            int cost = gunType.GunAtkSpeedLevel * 100;

            if (SpaceHeadGame.GameState == GameState.ShopUpgradeMenu)
            {
                if (cost <= Player.PlayerGoldAmount)
                {
                    gunType.ShotSpeed += 100;
                    gunType.GunAtkSpeedLevel++;
                    Player.PlayerGoldAmount -= cost;
                }
            }
        }

        public void UpgradePlayerHitPoints()
        {
            if (SpaceHeadGame.GameState == GameState.ShopUpgradeMenu)
            {
                if (Player.PlayerSkillPoints >= 1)
                {
                    Player.Health++;
                    Player.HealthLevel++;
                    Player.PlayerSkillPoints--;
                }
            }
        }

        public void UpgradePlayerMovementSpeed()
        {
            if (SpaceHeadGame.GameState == GameState.ShopUpgradeMenu)
            {
                if (Player.PlayerSkillPoints >= 1)
                {
                    Player.MoveSpeed += 10;
                    Player.MoveSpeedLevel++;
                    Player.PlayerSkillPoints--;
                }
            }
        }

        public void UpgradePlayerHealth()
        {
            if (SpaceHeadGame.GameState == GameState.ShopUpgradeMenu)
            {
                if (Player.PlayerSkillPoints > 0)
                {
                    Player.Health++;
                    Player.PlayerSkillPoints -= 1;
                }
            }
        }

        public void UpgradePlayerMovementSpeed()
        {
            if (SpaceHeadGame.GameState == GameState.ShopUpgradeMenu)
            {
                if (Player.PlayerSkillPoints > 0)
                {
                    Player.MoveSpeed += 10;
                    Player.PlayerSkillPoints -= 1;
                }
            }
        }


    }
}
