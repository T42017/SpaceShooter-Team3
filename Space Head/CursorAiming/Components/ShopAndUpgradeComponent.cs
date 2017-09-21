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
        private Texture2D _shopBackground;
        private List<MenuChoice> _upgradePlayer, _upgradeWeapon;
        private static MenuChoice AttackSpeed, Health, Damage, MoveSpeed, UpgradePlayerTitle, UpgradeWeaponTitle;
        private static int _gunAtkSpeedUpgradeCost, _gunDamageUpgradeCost;

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

            _gunAtkSpeedUpgradeCost = Gun.GunAtkSpeedLevel * 100;
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
            
            foreach (var choice in _upgradeWeapon)
                SpriteBatch.DrawString(_font, choice.Text, new Vector2(choice.X, choice.Y), Color.Green);

            foreach (var choice in _upgradePlayer)
                SpriteBatch.DrawString(_font, choice.Text, new Vector2(choice.X, choice.Y), Color.Green);

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        public void UpgradeGunDamage(Gun gunType)
        {
            if (SpaceHeadGame.GameState == GameState.ShopUpgradeMenu)
                if (_gunDamageUpgradeCost <= Player.Coins)
                {
                    gunType.Damage += (int)(gunType.Damage * 0.2);
                    Gun.GunAtkLevel++;
                    Player.Coins -= _gunDamageUpgradeCost;
                    _gunDamageUpgradeCost = Gun.GunAtkLevel * 100;
                }
        }

        public void UpgradeGunAtkSpeed(Gun gunType)
        {
            if (SpaceHeadGame.GameState == GameState.ShopUpgradeMenu)
                if (_gunAtkSpeedUpgradeCost <= Player.Coins)
                {
                    Player._attackSpeed -= Player._attackSpeed * 0.2f;
                    Gun.GunAtkSpeedLevel++;
                    Player.Coins -= _gunAtkSpeedUpgradeCost;
                    _gunAtkSpeedUpgradeCost = Gun.GunAtkSpeedLevel * 100;
                    
                }
        }

        public void UpgradePlayerHitPoints()
        {
            if (SpaceHeadGame.GameState == GameState.ShopUpgradeMenu)
                if (Player.PlayerSkillPoints > 0)
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
            UpgradePlayerHitPoints();
            Health.Text = "UPGRADE HIT POINTS // LEVEL: " + Player.HealthLevel;
        }

        private void UpgradeMovementSpdClicked()
        {
            UpgradePlayerMovementSpeed();
            MoveSpeed.Text = "UPGRADE MOVEMENT SPEED // LEVEL: " + Player.MoveSpeedLevel;
        }

        #endregion

        public static void Reset()
        {
            AttackSpeed.Text = "UPGRADE ATTACK SPEED // LEVEL: " + Gun.GunAtkSpeedLevel;
            Health.Text = "UPGRADE HIT POINTS // LEVEL: " + Player.HealthLevel;
            MoveSpeed.Text = "UPGRADE MOVEMENT SPEED // LEVEL: " + Player.MoveSpeedLevel;
            Damage.Text = "UPGRADE WEAPON DAMAGE // LEVEL: " + Gun.GunAtkLevel;
        }

    }
}
