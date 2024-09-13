// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Pets.MutantSpawn
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Pets
{
  public class MutantSpawn : ModProjectile
  {
    public bool yFlip;
    public float notlocalai1;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 12;
      Main.projPet[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 36;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = 26;
      this.AIType = 319;
      this.Projectile.netImportant = true;
      this.Projectile.friendly = true;
      this.Projectile.extraUpdates = 1;
    }

    public virtual void SendExtraAI(BinaryWriter writer) => writer.Write(this.notlocalai1);

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.notlocalai1 = reader.ReadSingle();
    }

    public virtual bool PreAI()
    {
      Main.player[this.Projectile.owner].blackCat = false;
      return true;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.dead)
        fargoSoulsPlayer.MutantSpawn = false;
      if (fargoSoulsPlayer.MutantSpawn)
        this.Projectile.timeLeft = 2;
      if (this.Projectile.tileCollide && (double) ((Entity) this.Projectile).velocity.Y > 0.0)
      {
        this.yFlip = !this.yFlip;
        if (this.yFlip)
          ((Entity) this.Projectile).position.Y -= ((Entity) this.Projectile).velocity.Y;
      }
      if (!Vector2.op_Equality(((Entity) player).velocity, Vector2.Zero))
        return;
      this.BeCompanionCube();
    }

    public void BeCompanionCube()
    {
      Player player = Main.player[this.Projectile.owner];
      Color color1 = Lighting.GetColor((int) ((Entity) this.Projectile).Center.X / 16, (int) ((Entity) this.Projectile).Center.Y / 16);
      Vector3 vector3_1 = ((Color) ref color1).ToVector3();
      Color color2 = Lighting.GetColor((int) ((Entity) player).Center.X / 16, (int) ((Entity) player).Center.Y / 16);
      Vector3 vector3_2 = ((Color) ref color2).ToVector3();
      if ((double) ((Vector3) ref vector3_1).Length() < 0.15000000596046448 && (double) ((Vector3) ref vector3_2).Length() < 0.15)
        ++this.notlocalai1;
      else if ((double) this.notlocalai1 > 0.0)
        --this.notlocalai1;
      this.notlocalai1 = MathHelper.Clamp(this.notlocalai1, -3600f, 600f);
      if ((double) this.notlocalai1 <= (double) Main.rand.Next(300, 600) || player.immune)
        return;
      this.notlocalai1 = (float) (Main.rand.Next(30) * -10 - 300);
      switch (Main.rand.Next(3))
      {
        case 0:
          if (this.Projectile.owner != Main.myPlayer)
            break;
          SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          player.Hurt(PlayerDeathReason.ByOther(6, -1), 777, 0, false, false, -1, false, 0.0f, 0.0f, 4.5f);
          player.immune = false;
          player.immuneTime = 0;
          break;
        case 1:
          if (!FargoSoulsUtil.HostCheck)
            break;
          FargoSoulsUtil.NewNPCEasy(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>(), velocity: new Vector2());
          FargoSoulsUtil.PrintLocalization("Announcement.HasAwoken", new Color(175, 75, (int) byte.MaxValue), (object) Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".NPCs.MutantBoss.DisplayName"));
          break;
        default:
          if (this.Projectile.owner != Main.myPlayer)
            break;
          CombatText.NewText(((Entity) this.Projectile).Hitbox, Color.LimeGreen, Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Items.SpawnSack.NotSafe"), false, false);
          break;
      }
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = (double) ((Entity) Main.player[this.Projectile.owner]).position.Y > (double) ((Entity) this.Projectile).Center.Y;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity) => false;

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/Pets/" + ((ModType) this).Name + "_Glow", (AssetRequestMode) 1).Value;
      for (int index = 1; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), 0.6f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
