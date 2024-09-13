// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.PuffJump
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class PuffJump : ExtraJump
  {
    public virtual ExtraJump.Position GetDefaultPosition()
    {
      return (ExtraJump.Position) new ExtraJump.After(ExtraJump.BlizzardInABottle);
    }

    public virtual IEnumerable<ExtraJump.Position> GetModdedConstraints()
    {
      return (IEnumerable<ExtraJump.Position>) null;
    }

    public virtual float GetDurationMultiplier(Player player) => 0.375f;

    public virtual void UpdateHorizontalSpeeds(Player player)
    {
    }

    public virtual void OnStarted(Player player, ref bool playSound)
    {
      int num1 = ((Entity) player).height;
      if ((double) player.gravDir == -1.0)
        num1 = 0;
      int num2 = num1 - 16;
      for (int index = 0; index < 2; ++index)
      {
        Dust dust = Dust.NewDustDirect(Vector2.op_Addition(((Entity) player).position, new Vector2(-34f, (float) num2)), 102, 32, 16, (float) (-(double) ((Entity) player).velocity.X * 0.5), ((Entity) player).velocity.Y * 0.5f, 100, Color.Gray, 1.5f);
        dust.velocity = Vector2.op_Subtraction(Vector2.op_Multiply(dust.velocity, 0.5f), Vector2.op_Multiply(((Entity) player).velocity, new Vector2(0.1f, 0.3f)));
      }
      PuffJump.SpawnCloudPoof(player, Vector2.op_Addition(((Entity) player).Top, new Vector2(-16f, (float) num2)));
    }

    private static void SpawnCloudPoof(Player player, Vector2 position)
    {
      Gore gore = Gore.NewGoreDirect(((Entity) player).GetSource_FromThis((string) null), position, Vector2.op_UnaryNegation(((Entity) player).velocity), Main.rand.Next(11, 14), 1.25f);
      gore.velocity.X = (float) ((double) gore.velocity.X * 0.10000000149011612 - (double) ((Entity) player).velocity.X * 0.10000000149011612);
      gore.velocity.Y = (float) ((double) gore.velocity.Y * 0.10000000149011612 - (double) ((Entity) player).velocity.Y * 0.05000000074505806);
    }

    public virtual void ShowVisuals(Player player)
    {
    }
  }
}
