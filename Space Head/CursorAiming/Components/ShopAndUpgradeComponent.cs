using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    internal class ShopAndUpgradeComponent : SpaceHeadBaseComponent
    {
        private SpriteFont _font;
        private MouseState _previousMouseState;
        private Texture2D _shopBackground, _lifeTexture;
        private List<MenuChoice> _upgradePlayer, _upgradeWeapon;
        private static MenuChoice AttackSpeed, Health, Damage, MoveSpeed, UpgradePlayerTitle, UpgradeWeaponTitle;
        private static int _gunAtkSpeedUpgradeCost, _gunDamageUpgradeCost;

        public ShopAndUpgradeComponent(Game game) : base(game)
        {
            DrawOrder = 2;
            DrawableStates = GameState.ShopUpgradeMenu;
            UpdatableStates = GameState.ShopUpgradeMenu;
        }

        public override void Initialize()
        {
            _upgradeWeapon = new List<MenuChoice>();
            _upgradePlayer = new List<MenuChoice>();

            _gunAtkSpeedUpgradeCost = 250;
            _gunDamageUpgradeCost = Gun.GunAtkLevel * 100;

            UpgradePlayerTitle = new MenuChoice
            {
                Text = "UPGRADE YOUR CHARACTER",
                ClickAction = DoNothing
            };
            _upgradePlayer.Add(UpgradePlayerTitle);

            UpgradeWeaponTitle = new MenuChoice
            {
                Text = "UPGRADE YOUR WEAPON",
                ClickAction = DoNothing
            };
            _upgradeWeapon.Add(UpgradeWeaponTitle);

            AttackSpeed = new MenuChoice
            {
                Text = "UPGRADE WEAPON ATTACK SPEED // COST: " + _gunAtkSpeedUpgradeCost + " // LEVEL:" + Gun.GunAtkSpeedLevel,
                ClickAction = UpgradeAtkSpeedClicked
            };
            _upgradeWeapon.Add(AttackSpeed);
            
            
            Health = new MenuChoice
            {
                Text = "UPGRADE HIT POINTS // LEVEL: " + Player.HealthLevel,
                ClickAction = UpgradeHitPointsClicked
            };

            _upgradePlayer.Add(Health);

            Damage = new MenuChoice
            {
                Text = "UPGRADE WEAPON DAMAGE // COST: " + _gunDamageUpgradeCost + " // LEVEL:" + Gun.GunAtkLevel,
                ClickAction = UpgradeWeaponDamageClicked
            };
            _upgradeWeapon.Add(Damage);

            MoveSpeed = new MenuChoice
            {
                Text = "UPGRADE MOVEMENT SPEED // LEVEL: " + Player.MoveSpeedLevel,
                ClickAction = UpgradeMovementSpdClicked
            };
            _upgradePlayer.Add(MoveSpeed);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _font = Game.Content.Load<SpriteFont>("Font");
            _shopBackground = Game.Content.Load<Texture2D>("gameOverBackground");
            _lifeTexture = Game.Content.Load<Texture2D>("spaceRocketParts_012");

            var startY1 = Globals.ScreenHeight * 0.20f;
            var startY2 = Globals.ScreenHeight * 0.50f;

            foreach (var choice in _upgradeWeapon)
            {
                var size = _font.MeasureString(choice.Text);
                choice.Y = startY1;
                choice.X = Globals.ScreenWidth * 0.5f - size.X / 2;
                choice.HitBox = new Rectangle((int) choice.X, (int) choice.Y, (int) size.X, (int) size.Y);
                startY1 += 70;
            }

            foreach (var choice in _upgradePlayer)
            {
                var size = _font.MeasureString(choice.Text);
                choice.Y = startY2;
                choice.X = Globals.ScreenWidth * 0.5f - size.X / 2;
                choice.HitBox = new Rectangle((int) choice.X, (int) choice.Y, (int) size.X, (int) size.Y);
                startY2 += 70;
            }

            _previousMouseState = Mouse.GetState();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            

            foreach (var choice in _upgradeWeapon)
                if (choice.HitBox.Contains(mouseState.X, mouseState.Y))
                {
                    _upgradeWeapon.ForEach(c => c.Selected = false);
                    choice.Selected = true;

                    if (_previousMouseState.LeftButton == ButtonState.Released
                        && mouseState.LeftButton == ButtonState.Pressed)
                        choice.ClickAction.Invoke();
                }

            foreach (var choice in _upgradePlayer)
                if (choice.HitBox.Contains(mouseState.X, mouseState.Y))
                {
                    _upgradePlayer.ForEach(c => c.Selected = false);
                    choice.Selected = true;

                    if (_previousMouseState.LeftButton == ButtonState.Released
                        && mouseState.LeftButton == ButtonState.Pressed)
                        choice.ClickAction.Invoke();
                }

            _previousMouseState = mouseState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(_shopBackground, GraphicsDevice.Viewport.Bounds, Color.White);

            SpriteBatch.DrawString(_font, "MS: " + Player.MoveSpeed, new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.65f), Color.Green);

            SpriteBatch.DrawString(_font, "DMG: " + Player.Gun.Damage, new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.7f), Color.Green);

            SpriteBatch.DrawString(_font, "AS: " + Math.Round(1 / Player._attackSpeed, 2), new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.75f), Color.Green);

            SpriteBatch.DrawString(_font, "Gold: " + Player.Coins, new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.85f), Color.Green);

            SpriteBatch.DrawString(_font, "Skill Points: " + Player.PlayerSkillPoints, new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.9f), Color.Green);

            SpriteBatch.DrawString(_font, "Score: " + Player.Points, new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.95f), Color.Green);

            for (var i = 0; i < Player.HealthLevel; i++)
                if (i < Player.Health)
                    SpriteBatch.Draw(_lifeTexture,
                        new Vector2(Globals.ScreenHeight * 0.01f + i * 50, 0 + Globals.ScreenHeight * 0.01f), Color.Green);
                else SpriteBatch.Draw(_lifeTexture,
                        new Vector2(Globals.ScreenHeight * 0.01f + i * 50, 0 + Globals.ScreenHeight * 0.01f), Color.IndianRed);

            foreach (var choice in _upgradeWeapon)
                SpriteBatch.DrawString(_font, choice.Text, new Vector2(choice.X, choice.Y), Color.Green);

            foreach (var choice in _upgradePlayer)
                SpriteBatch.DrawString(_font, choice.Text, new Vector2(choice.X, choice.Y), Color.Green);

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        public void UpgradeGunDamage(Gun gunType)
        {
            if (_gunDamageUpgradeCost <= Player.Coins)
            {
                gunType.Damage += (int) (gunType.Damage * 0.2);
                Gun.GunAtkLevel++;
                Player.Coins -= _gunDamageUpgradeCost;
                _gunDamageUpgradeCost = Gun.GunAtkLevel * 100;
            }
        }

        public void UpgradeGunAtkSpeed(Gun gunType)
        {
            if (_gunAtkSpeedUpgradeCost <= Player.Coins)
            {
                Player._attackSpeed -= Player._attackSpeed * 0.1f;
                Gun.GunAtkSpeedLevel++;
                Player.Coins -= _gunAtkSpeedUpgradeCost;
                _gunAtkSpeedUpgradeCost = Gun.GunAtkSpeedLevel * 250;

            }
        }

        public void UpgradePlayerHitPoints()
        {
            if (Player.PlayerSkillPoints > 0 && Player.HealthLevel < 20)
            {
                Player.Health++;
                Player.HealthLevel++;
                Player.PlayerSkillPoints--;
            }
        }

        public void UpgradePlayerMovementSpeed()
        {
            if (SpaceHeadGame.GameState == GameState.ShopUpgradeMenu)
                if (Player.PlayerSkillPoints > 0)
                {
                    Player.MoveSpeed += 20;
                    Player.MoveSpeedLevel++;
                    Player.PlayerSkillPoints--;
                }
        }

        public void DoNothing()
        {

        } //Den gör mer än vad man kanske tror..

        #region Menu Clicks

        private void UpgradeAtkSpeedClicked()
        {
            UpgradeGunAtkSpeed(Player.Gun);
            AttackSpeed.Text = "UPGRADE WEAPON ATTACK SPEED // COST: " + _gunAtkSpeedUpgradeCost + " // LEVEL:" + Gun.GunAtkSpeedLevel;
        }

        private void UpgradeWeaponDamageClicked()
        {
            UpgradeGunDamage(Player.Gun);
            Damage.Text = "UPGRADE WEAPON DAMAGE // COST: " + _gunDamageUpgradeCost + " // LEVEL:" + Gun.GunAtkLevel;
        }

        private void UpgradeHitPointsClicked()
        {
            if (Player.HealthLevel > 9)
            {
                DoNothing();
            }
            else if (Player.HealthLevel == 9)
            {
                Health.Text = "Unable to further increase HP ";
                UpgradePlayerHitPoints();
            }
            else
            {
                UpgradePlayerHitPoints();
                Health.Text = "UPGRADE HIT POINTS // LEVEL: " + Player.HealthLevel;
            }

        }

        private void UpgradeMovementSpdClicked()
        {
            UpgradePlayerMovementSpeed();
            MoveSpeed.Text = "UPGRADE MOVEMENT SPEED // LEVEL: " + Player.MoveSpeedLevel;
        }

        #endregion

        public static void Reset()
        {
            AttackSpeed.Text = "UPGRADE WEAPON ATTACK SPEED // COST: " + _gunAtkSpeedUpgradeCost + " // LEVEL:" + Gun.GunAtkSpeedLevel;
            Damage.Text = "UPGRADE WEAPON DAMAGE // COST: " + _gunDamageUpgradeCost + " // LEVEL:" + Gun.GunAtkLevel;

            Health.Text = "UPGRADE HIT POINTS // LEVEL: " + Player.HealthLevel;
            MoveSpeed.Text = "UPGRADE MOVEMENT SPEED // LEVEL: " + Player.MoveSpeedLevel;
        }

    }
}
