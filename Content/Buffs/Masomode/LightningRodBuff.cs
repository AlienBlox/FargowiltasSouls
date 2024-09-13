// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.LightningRodBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class LightningRodBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = true;
    }

    private static void SpawnLightning(Entity obj, int type, int damage, IEntitySource source)
    {
      if (obj is Player)
      {
        Point tileCoordinates = Utils.ToTileCoordinates(obj.Top);
        tileCoordinates.X += Main.rand.Next(-25, 25);
        tileCoordinates.Y -= Main.rand.Next(4, 8);
        float y = obj.Center.Y;
        Projectile.NewProjectile(obj.GetSource_Misc(""), (float) (tileCoordinates.X * 16 + 8), (float) (tileCoordinates.Y * 16 + 17 - 900), 0.0f, 0.0f, ModContent.ProjectileType<RainLightning>(), damage, 2f, Main.myPlayer, Utils.ToRotation(Vector2.UnitY), y, 0.0f);
      }
      else
      {
        Point tileCoordinates = Utils.ToTileCoordinates(obj.Top);
        tileCoordinates.X += Main.rand.Next(-25, 25);
        tileCoordinates.Y -= 15 + Main.rand.Next(-5, 5) - (type == ModContent.ProjectileType<LightningVortexHostile>() ? 20 : 0);
        for (int index = 0; index < 10 && !WorldGen.SolidTile(tileCoordinates.X, tileCoordinates.Y, false) && tileCoordinates.Y > 10; ++index)
          --tileCoordinates.Y;
        Projectile.NewProjectile(source, (float) (tileCoordinates.X * 16 + 8), (float) (tileCoordinates.Y * 16 + 17), 0.0f, 0.0f, type, damage, 2f, Main.myPlayer, 0.0f, (float) obj.whoAmI, 0.0f);
      }
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      ++player.FargoSouls().lightningRodTimer;
      if (player.FargoSouls().lightningRodTimer < (byte) 60)
        return;
      player.FargoSouls().lightningRodTimer = (byte) 0;
      int damage = (Main.hardMode ? 120 : 60) / 4;
      LightningRodBuff.SpawnLightning((Entity) player, ModContent.ProjectileType<LightningVortexHostile>(), damage, player.GetSource_Buff(buffIndex));
    }

    public virtual void Update(NPC npc, ref int buffIndex)
    {
      if (Main.netMode == 1)
        return;
      FargoSoulsGlobalNPC fargoSoulsGlobalNpc = npc.FargoSouls();
      ++fargoSoulsGlobalNpc.lightningRodTimer;
      if (fargoSoulsGlobalNpc.lightningRodTimer < 60)
        return;
      fargoSoulsGlobalNpc.lightningRodTimer = 0;
      LightningRodBuff.SpawnLightning((Entity) npc, ModContent.ProjectileType<LightningVortex>(), 60, ((Entity) npc).GetSource_FromThis((string) null));
    }
  }
}
