// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.VortexProjGravity
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class VortexProjGravity : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) null;

    public override bool IgnoresMutantPresence => true;

    public override void PostUpdateEquips(Player player)
    {
      foreach (Projectile projectile1 in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => p != null && ((Entity) p).active && p.friendly && p.owner == ((Entity) player).whoAmI)))
      {
        Projectile toProj = projectile1;
        foreach (Projectile projectile2 in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => p != null && ((Entity) p).active && p.friendly && ((Entity) p).whoAmI != ((Entity) toProj).whoAmI && p.owner == ((Entity) player).whoAmI && !TungstenEffect.TungstenAlwaysAffectProj(p) && p.FargoSouls().CanSplit && FargoSoulsUtil.CanDeleteProjectile(p))))
        {
          Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) toProj).Center, ((Entity) projectile2).Center);
          int f = (int) ((Vector2) ref vector2_1).LengthSquared();
          if (f >= 1 && !float.IsNaN((float) f))
          {
            int num = f + 100;
            Vector2 vector2_2 = Vector2.op_Multiply(Utils.SafeNormalize(vector2_1, Vector2.UnitY), 9000f / (float) num);
            Projectile projectile3 = projectile2;
            ((Entity) projectile3).velocity = Vector2.op_Addition(((Entity) projectile3).velocity, vector2_2);
          }
        }
      }
    }
  }
}
