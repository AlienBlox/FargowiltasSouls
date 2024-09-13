// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.FuseBomb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Earth;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class FuseBomb : ModProjectile
  {
    public virtual string Texture => FargoSoulsUtil.EmptyTexture;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 300;
      ((Entity) this.Projectile).height = 300;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = false;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 2;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.hide = true;
      this.Projectile.extraUpdates = 1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (NPC.golemBoss != -1 && ((Entity) Main.npc[NPC.golemBoss]).active && Main.npc[NPC.golemBoss].type == 245)
      {
        target.AddBuff(24, 600, true, false);
        target.AddBuff(36, 600, true, false);
        target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
        target.AddBuff(195, 600, true, false);
        Tile tileSafely = Framing.GetTileSafely(((Entity) Main.npc[NPC.golemBoss]).Center);
        if (((Tile) ref tileSafely).WallType != (ushort) 87)
          target.AddBuff(67, 120, true, false);
      }
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.championBoss, ModContent.NPCType<EarthChampion>()))
        return;
      target.AddBuff(24, 300, true, false);
      target.AddBuff(67, 300, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 45; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 30; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      for (int index6 = 0; index6 < 3; ++index6)
      {
        float num = 0.4f;
        if (index6 == 1)
          num = 0.8f;
        int index7 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore1 = Main.gore[index7];
        gore1.velocity = Vector2.op_Multiply(gore1.velocity, num);
        ++Main.gore[index7].velocity.X;
        ++Main.gore[index7].velocity.Y;
        int index8 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore2 = Main.gore[index8];
        gore2.velocity = Vector2.op_Multiply(gore2.velocity, num);
        --Main.gore[index8].velocity.X;
        ++Main.gore[index8].velocity.Y;
        int index9 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore3 = Main.gore[index9];
        gore3.velocity = Vector2.op_Multiply(gore3.velocity, num);
        ++Main.gore[index9].velocity.X;
        --Main.gore[index9].velocity.Y;
        int index10 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore4 = Main.gore[index10];
        gore4.velocity = Vector2.op_Multiply(gore4.velocity, num);
        --Main.gore[index10].velocity.X;
        --Main.gore[index10].velocity.Y;
      }
    }
  }
}
