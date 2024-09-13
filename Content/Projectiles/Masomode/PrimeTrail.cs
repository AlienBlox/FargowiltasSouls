// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.PrimeTrail
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.VanillaEternity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class PrimeTrail : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 20;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 600;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.aiStyle = -1;
      this.Projectile.scale = 0.8f;
      this.Projectile.hide = true;
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      behindNPCs.Add(index);
    }

    public virtual void AI()
    {
      bool flag = false;
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], 128, 131, 129, 130);
      if (npc != null)
      {
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
        if ((double) this.Projectile.ai[1] == 0.0)
        {
          if (!npc.GetGlobalNPC<PrimeLimb>().IsSwipeLimb || (double) npc.ai[2] < 140.0)
            flag = true;
        }
        else if ((double) this.Projectile.ai[1] == 1.0)
        {
          if (npc.GetGlobalNPC<PrimeLimb>().IsSwipeLimb || (double) Main.npc[(int) npc.ai[1]].ai[1] != 1.0 && (double) Main.npc[(int) npc.ai[1]].ai[1] != 2.0)
            flag = true;
        }
        else if ((double) this.Projectile.ai[1] == 2.0 && (npc.GetGlobalNPC<PrimeLimb>().IsSwipeLimb || (double) Main.npc[(int) npc.ai[1]].ai[1] == 1.0 || (double) Main.npc[(int) npc.ai[1]].ai[1] == 2.0))
          flag = true;
      }
      else
        flag = true;
      if (flag)
      {
        this.Projectile.alpha += 8;
        if (this.Projectile.alpha <= (int) byte.MaxValue)
          return;
        this.Projectile.alpha = (int) byte.MaxValue;
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.alpha -= (double) this.Projectile.ai[1] == 0.0 ? 16 : 8;
        if (this.Projectile.alpha >= 0)
          return;
        this.Projectile.alpha = 0;
      }
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      float num1 = (double) this.Projectile.ai[1] != 1.0 ? ((double) this.Projectile.ai[1] != 2.0 ? 0.25f : 0.5f) : 0.1f;
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += num1)
      {
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          Color color;
          if ((double) this.Projectile.ai[1] == 1.0)
            color = Color.op_Multiply(Color.Red, 0.7f);
          else if ((double) this.Projectile.ai[1] == 2.0)
          {
            color = Color.op_Multiply(new Color(51, (int) byte.MaxValue, 191, 210), 0.75f);
          }
          else
          {
            // ISSUE: explicit constructor call
            ((Color) ref color).\u002Ector((int) byte.MaxValue, (int) byte.MaxValue, 75, 210);
          }
          color = Color.op_Multiply(color, 0.3f * this.Projectile.Opacity);
          color = Color.op_Multiply(color, ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          float num2 = this.Projectile.scale * (((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          Vector2 vector2 = Vector2.op_Addition(Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(), color, this.Projectile.rotation, Vector2.op_Division(Utils.Size(texture2D), 2f), num2, (SpriteEffects) 0, 0.0f);
        }
      }
      return false;
    }
  }
}
