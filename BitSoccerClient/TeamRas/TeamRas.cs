using Common;

namespace TeamRas
{
    public class TeamRas : ITeam
    {
        public void Action(Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
        {
            var LDPos = new Vector(Field.Borders.Left.X + 400, Field.Borders.Top.Y + 300);
            var RDPos = new Vector(Field.Borders.Left.X + 400, Field.Borders.Bottom.Y - 300);

            var LFPos = new Vector(Field.Borders.Left.X + 700, Field.Borders.Top.Y + 200);
            var RFPos = new Vector(Field.Borders.Left.X + 700, Field.Borders.Bottom.Y - 200);

            var CFPos = new Vector(Field.Borders.Left.X + 800, Field.Borders.Bottom.Y - 540);


            foreach (var player in myTeam.Players)
                if (ball.Owner == null)
                {
                    if (player.CanPickUpBall(ball))
                    {
                        player.ActionPickUpBall();
                    }
                    else if (player.CanTackle(player.GetClosest(enemyTeam)))
                    {
                        player.ActionTackle(player.GetClosest(enemyTeam));
                    }

                    else if (ball.GetClosest(myTeam) == player && player.PlayerType != PlayerType.Keeper)
                    {
                        if (ball.Velocity.X < 0)
                            player.ActionGo(ball.Position + ball.Velocity * 10);

                        else
                            player.ActionGo(ball.Position + ball.Velocity);
                    }

                    else if (player.PlayerType == PlayerType.Keeper)
                    {
                        var ballDirectionToGoal = new Vector(ball.Position.X - Field.MyGoal.Center.X,
                            ball.Position.Y - Field.MyGoal.Center.Y);
                        ballDirectionToGoal.Normalize();
                        if (ball.Position.X - Field.MyGoal.X < 400)
                            if (ball.Position.Y - Field.MyGoal.Top.Y < -200 &&
                                ball.Position.Y - Field.MyGoal.Bottom.Y < 200)
                                player.ActionGo(ball.Position);
                            else
                                player.ActionGo(Field.MyGoal.Center + ballDirectionToGoal * 100);

                        else
                            player.ActionGo(Field.MyGoal.Center + ballDirectionToGoal * 300);
                    }

                    else if (player.PlayerType == PlayerType.LeftDefender)
                    {
                        var ballDirectionToLD = new Vector(ball.Position.X - LDPos.X, ball.Position.Y - LDPos.Y);
                        ballDirectionToLD.Normalize();
                        player.ActionGo(LDPos + ballDirectionToLD * 200 + ball.Velocity * 2);
                    }
                    else if (player.PlayerType == PlayerType.RightDefender)
                    {
                        var ballDirectionToRD = new Vector(ball.Position.X - RDPos.X, ball.Position.Y - RDPos.Y);
                        ballDirectionToRD.Normalize();
                        player.ActionGo(RDPos + ballDirectionToRD * 200 + ball.Velocity * 2);
                    }
                    else if (player.PlayerType == PlayerType.LeftForward)
                    {
                        var ballDirectionToLF = new Vector(ball.Position.X - LFPos.X, ball.Position.Y - LFPos.Y);
                        ballDirectionToLF.Normalize();
                        player.ActionGo(LFPos + ballDirectionToLF * 200 + ball.Velocity * 2);
                    }
                    else if (player.PlayerType == PlayerType.RightForward)
                    {
                        var ballDirectionToRF = new Vector(ball.Position.X - RFPos.X, ball.Position.Y - RFPos.Y);
                        ballDirectionToRF.Normalize();
                        player.ActionGo(RFPos + ballDirectionToRF * 200 + ball.Velocity * 2);
                    }
                    else if (player.PlayerType == PlayerType.CenterForward)
                    {
                        var ballDirectionToCF = new Vector(ball.Position.X - CFPos.X, ball.Position.Y - CFPos.Y);
                        ballDirectionToCF.Normalize();
                        player.ActionGo(CFPos + ballDirectionToCF * 200 + ball.Velocity * 2);
                    }
                }


                else if (ball.Owner.Team == myTeam)
                {
                    if (player.CanTackle(player.GetClosest(enemyTeam)))
                        player.ActionTackle(player.GetClosest(enemyTeam));

                    if (ball.Owner == player)
                        if (player.GetDistanceTo(Field.EnemyGoal) < 100)
                        {
                        }
                        else
                        {
                            player.ActionShootGoal();
                        }
                }

                else if (ball.Owner.Team == enemyTeam)
                {
                    if (player.CanPickUpBall(ball))
                        player.ActionPickUpBall();
                    if (player.CanTackle(player.GetClosest(enemyTeam)))
                        player.ActionTackle(player.GetClosest(enemyTeam));
                }
        }
    }
}